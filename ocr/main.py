import time

from cv2 import Mat
from numpy import ndarray
from numpy import dtype
from numpy import generic
from typing import List, Sequence, Union, Any

import cv2
import numpy as np
import sewar

image1 = cv2.imread('data/buletinTemplate.jpg', cv2.IMREAD_COLOR)
image1 = cv2.cvtColor(image1, cv2.COLOR_BGR2RGB)

image2 = cv2.imread("data/buletin.jpg", cv2.IMREAD_COLOR)
image2 = cv2.cvtColor(image2, cv2.COLOR_BGR2RGB)

image2 = cv2.resize(image2, (image1.shape[1], image1.shape[0]), interpolation=cv2.INTER_AREA)

image1_gray = cv2.cvtColor(image1, cv2.COLOR_BGR2GRAY)
image2_gray = cv2.cvtColor(image2, cv2.COLOR_BGR2GRAY)

# cv2.imshow("image1", image1_gray)
# cv2.waitKey(0)
# cv2.imshow("image2", image2_gray)
# cv2.waitKey(0)

print(image1_gray.shape, image2_gray.shape)

# ssim_score = metrics.structural_similarity(image1_gray, image2_gray, full=True)
# print(f"SSIM: {round(ssim_score[0], 2)}")
rase = sewar.rase(image1_gray, image2_gray)
ssim_score = sewar.ssim(image1_gray, image2_gray)

print(f"SSIM: {ssim_score}")
print(f"RASE: {-rase}")


def reorder(points: Union[Mat, ndarray[Any, dtype[generic]], ndarray]):
    # print(points.shape)
    points = points.reshape((4, 2))
    points_new = np.zeros((4, 1, 2), dtype=np.int32)
    add = points.sum(1)

    points_new[0] = points[np.argmin(add)]
    points_new[3] = points[np.argmax(add)]

    diff = np.diff(points, axis=1)

    points_new[1] = points[np.argmin(diff)]
    points_new[2] = points[np.argmax(diff)]
    return points_new


def biggest_contour(contours: Sequence[Union[Mat, ndarray[Any, dtype[generic]], ndarray]]):
    biggest = None
    max_area = 0
    for contour in contours:
        area = cv2.contourArea(contour)
        if area > 1000:
            epsilon = 0.015
            peri = cv2.arcLength(contour, True)
            approx = cv2.approxPolyDP(contour, epsilon * peri, True)
            if area > max_area and len(approx) == 4:
                biggest = approx
                max_area = area
    return biggest, max_area


height, width = image1.shape[:2]
image2 = cv2.resize(image2, (image1.shape[1], image1.shape[0]), interpolation=cv2.INTER_AREA)
threshold_step: int = 1
max_rase_score: float = 20000
best_match_image = None
start = time.time()
epochs: int = 120
cache = dict()
same_color_threshold: int = 20
for threshold in range(0, epochs, threshold_step):
    img_gray = cv2.cvtColor(image2, cv2.COLOR_BGR2GRAY)
    print(f"epoch: {threshold}/{epochs}")
    # cv2.imwrite(f"temp/gray.jpg", img_gray)
    for k_row in range(1, 16, 2):
        for k_col in range(1, 16, 2):
            img_blur = cv2.GaussianBlur(img_gray, (k_row, k_col), 0)
            # cv2.imwrite(f"temp/blur.jpg", img_blur)

            img_threshold = cv2.Canny(img_blur, threshold, 200)

            kernel = np.ones((k_row, k_col))
            img_dilate = cv2.dilate(img_threshold, kernel, iterations=1)
            img_erode = cv2.erode(img_dilate, kernel, iterations=2)

            contours, hierarchy = cv2.findContours(img_erode, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

            biggest, max_area = biggest_contour(contours)
            if biggest is not None:
                biggest = reorder(biggest)
                tuple_biggest = tuple(tuple(item) for item in biggest.reshape(4, 2))
                if tuple_biggest in cache:
                    # print("cache hit")
                    continue
                cache[tuple_biggest] = True
                x, y, w, h = cv2.boundingRect(biggest)
                w = min(w, width)
                h = min(h, height)
                roi = image2[y:y + h, x:x + w]

                # cv2.rectangle(roi, (x, y), (x + w, y + h), (0, 255, 0), 2)
                # cv2.imshow("roi", roi)
                # cv2.waitKey(0)
                pixels_remove: int = 0
                roi = cv2.resize(roi, (width, height), interpolation=cv2.INTER_AREA)

                # cv2.imshow("roi", roi)
                # cv2.waitKey(0)

                roi_grayscale = cv2.cvtColor(roi, cv2.COLOR_BGR2GRAY)
                roi_grayscale = cv2.resize(roi_grayscale, (width, height), interpolation=cv2.INTER_AREA)
                std_dev = cv2.meanStdDev(roi_grayscale)[1]

                rase = sewar.rase(image1_gray, roi_grayscale, ws=8)
                if rase < max_rase_score and std_dev > same_color_threshold:
                    max_rase_score = rase
                    best_match_image = roi_grayscale

                pts1 = np.float32(biggest)
                pts2_arrangements = [np.float32([[0, 0], [width, 0], [0, height], [width, height]]),
                                     np.float32([[x, y], [w, y], [x, h], [w, h]])]

                warp_perspective_choices = [(width, height), (w, h), (w, height), (width, h), (w, y + h),
                                            (x + w, h), (x + w, y + h)]

                for pts2 in pts2_arrangements:
                    matrix = cv2.getPerspectiveTransform(pts1, pts2)

                    for dsize in warp_perspective_choices:
                        img_warped = cv2.warpPerspective(roi, matrix, dsize)
                        # cv2.imshow("perspective", img_warped)
                        # cv2.waitKey(0)
                        img_warped = img_warped[pixels_remove:img_warped.shape[0] - pixels_remove,
                                     pixels_remove:img_warped.shape[1] - pixels_remove]

                        # cv2.imshow("img_warped", img_warped)
                        # cv2.waitKey(0)
                        img_warped = cv2.resize(img_warped, (width, height), interpolation=cv2.INTER_AREA)
                        img_warped = cv2.cvtColor(img_warped, cv2.COLOR_BGR2GRAY)

                        std_dev = cv2.meanStdDev(img_warped)[1]
                        rase = sewar.rase(image1_gray, img_warped, ws=8)
                        if rase < max_rase_score and std_dev > same_color_threshold:
                            max_rase_score = rase
                            best_match_image = img_warped

stop = time.time() - start
print(f"Time: {stop}")
print(f"Rase: {max_rase_score}")
print(f"SSIM: {sewar.ssim(image1_gray, best_match_image)}")
cv2.imwrite(f"temp/image2.jpg", best_match_image)
