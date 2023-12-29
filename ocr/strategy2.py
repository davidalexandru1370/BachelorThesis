# strategy using text ocr and warp perspective

import time
from paddleocr import PaddleOCR
from cv2 import Mat
from numpy import ndarray
from numpy import dtype
from numpy import generic
from typing import List, Sequence, Union, Any
from pytesseract import pytesseract

import cv2
import os
import numpy as np
import sewar

if os.name == "nt":
    pytesseract.tesseract_cmd = r"C:\Program Files\Tesseract-OCR\tesseract.exe"

image1 = cv2.imread('data/buletin2.jpg', cv2.IMREAD_COLOR)
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

face_cascade = cv2.CascadeClassifier("haarcascade/haarcascade-frontalface-default.xml")
faces = face_cascade.detectMultiScale(image1_gray, 1.3, 5)

for face in faces:
    cv2.rectangle(image1_gray, (face[0], face[1]), (face[0] + face[2], face[1] + face[3]), (255, 255, 255), -1)


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
threshold_step: int = 5
max_rase_score: float = float("inf")
best_match_image = None
start = time.time()
epochs: int = 120
cache = dict()
max_dev: float = 0
same_color_threshold: int = 30
ocr = PaddleOCR(use_angle_cls=True, lang="ro")
for threshold in range(0, epochs, threshold_step):
    print(f"epoch: {threshold}/{epochs}")
    # cv2.imwrite(f"temp/gray.jpg", img_gray)
    for k_row in range(1, 16, 2):
        for k_col in range(1, 16, 2):
            img_blur = cv2.GaussianBlur(image2_gray, (k_row, k_col), 0)
            # cv2.imwrite(f"temp/blur.jpg", img_blur)

            img_threshold = cv2.Canny(img_blur, threshold, 200)

            kernel = np.ones((k_row, k_col))
            img_dilate = cv2.dilate(img_threshold, kernel, iterations=2)
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

                pts1 = np.float32(biggest)
                pts2 = np.float32([[x, y], [w, y], [x, h], [w, h]])
                # pts2_arrangements = [np.float32([[0, 0], [width, 0], [0, height], [width, height]])]
                matrix = cv2.getPerspectiveTransform(pts1, pts2)

                img_warped = cv2.warpPerspective(roi, matrix, (w, y + h))
                # cv2.imshow("perspective", img_warped)
                # cv2.waitKey(0)
                img_warped = img_warped[pixels_remove:img_warped.shape[0] - pixels_remove,
                             pixels_remove:img_warped.shape[1] - pixels_remove]
                img_warped = cv2.cvtColor(img_warped, cv2.COLOR_BGR2GRAY)
                # img_warped = cv2.GaussianBlur(image2_gray, (k_row, k_col), 0)
                # img_warped = cv2.threshold(img_warped, 0, 256, cv2.THRESH_BINARY + cv2.THRESH_OTSU)[1]
                # config: str = '-l eng —oem 3 -psm 3'
                # text: str = pytesseract.image_to_string(img_warped, config=config)

                result = ocr.ocr(img_warped, cls=True)
                for idx in range(len(result)):
                    res = result[idx]
                    if res is not None:
                        for line in res:
                            print(line[-1])

stop = time.time() - start
print(f"Time: {stop}")
