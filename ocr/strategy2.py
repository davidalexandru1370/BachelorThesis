# strategy using text ocr and warp perspective

import time
from paddleocr import PaddleOCR
from cv2 import Mat
from numpy import ndarray
from numpy import dtype
from numpy import generic
from typing import List, Sequence, Union, Any, Type
from pytesseract import pytesseract

import cv2
import os
import numpy as np
import sewar

from DocumentPatterns.DocumentType import DocumentType
from DocumentPatterns.DocumentPatternAbstract import DocumentPatternAbstract
from DocumentPatterns.IdentityCardPattern import IdentityCardPattern
from DocumentPatterns.OwnershipContractPattern import OwnershipContractPattern
from DocumentPatterns.UnregisterVehiclePattern import UnregisterVehiclePattern


# if os.name == "nt":
#     pytesseract.tesseract_cmd = r"C:\Program Files\Tesseract-OCR\tesseract.exe"


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


def resize(image1, image2):
    height1, width1 = image1.shape[:2]
    height2, width2 = image2.shape[:2]

    if height1 != height2 or width1 != width2:
        image2 = cv2.resize(image2, (width1, height1))

    return image2


image2 = cv2.imread("data/input/radiere - completat.png", cv2.IMREAD_COLOR)
image2 = cv2.cvtColor(image2, cv2.COLOR_BGR2RGB)
max_rase_score: float = float("inf")
best_match_image = dict()
best_match_template: str = ""
ocr = PaddleOCR(use_angle_cls=True, lang="ro")

for template in os.listdir("data/templates"):
    image1 = cv2.imread(f'data/templates/{template}', cv2.IMREAD_COLOR)
    image1 = cv2.cvtColor(image1, cv2.COLOR_BGR2RGB)
    print(f"Working with template {template}")
    image2 = cv2.resize(image2, (image1.shape[1], image1.shape[0]), interpolation=cv2.INTER_AREA)
    # image2 = resize(image1, image2)
    max_rase_score: float = float("inf")

    image1_gray = cv2.cvtColor(image1, cv2.COLOR_BGR2GRAY)
    image2_gray = cv2.cvtColor(image2, cv2.COLOR_BGR2GRAY)

    # cv2.imshow("image1", image1_gray)
    # cv2.waitKey(0)
    # cv2.imshow("image2", image2_gray)
    # cv2.waitKey(0)
    print(image1.shape, image2.shape)
    print(image1_gray.shape, image2_gray.shape)

    # ssim_score = metrics.structural_similarity(image1_gray, image2_gray, full=True)
    # print(f"SSIM: {round(ssim_score[0], 2)}")
    # rase = sewar.rase(image1_gray, image2_gray)
    # ssim_score = sewar.ssim(image1_gray, image2_gray)
    #
    # print(f"SSIM: {ssim_score}")
    # print(f"RASE: {-rase}")

    face_cascade = cv2.CascadeClassifier("haarcascade/haarcascade-frontalface-default.xml")
    faces = face_cascade.detectMultiScale(image1_gray, 1.3, 5)

    for face in faces:
        cv2.rectangle(image1_gray, (face[0], face[1]), (face[0] + face[2], face[1] + face[3]), (255, 255, 255), -1)

    height, width = image1.shape[:2]
    threshold_step: int = 5
    start = time.time()
    epochs: int = 120
    cache = dict()
    max_dev: float = 0
    same_color_threshold: int = 10

    rase = sewar.rase(image1_gray, image2_gray, ws=8)
    std_dev = cv2.meanStdDev(image2_gray)[1]
    if rase < max_rase_score and std_dev > same_color_threshold:
        max_rase_score = rase
        best_match_image[template] = image2
        max_dev = std_dev
        # cv2.imshow("image2", image2_gray)
        # cv2.imshow("image2", image2)
        # cv2.waitKey(0)

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

                    pts1 = np.float32(biggest)
                    pts2 = np.float32([[x, y], [w, y], [x, h], [w, h]])
                    # pts2 = np.float32([[0, 0], [width, 0], [0, height], [width, height]])
                    matrix = cv2.getPerspectiveTransform(pts1, pts2)

                    img_warped = cv2.warpPerspective(roi, matrix, (w, y + h))
                    # if template == "Contract_de_vanzare_cumparare_-_Auto-Vehicule.png":
                    #     cv2.imshow("roi", img_warped)
                    #     cv2.waitKey(0)
                    # img_warped = cv2.warpPerspective(roi, matrix, (width, height))
                    # cv2.imshow("perspective", img_warped)
                    # cv2.waitKey(0)
                    img_warped = img_warped[pixels_remove:img_warped.shape[0] - pixels_remove,
                                 pixels_remove:img_warped.shape[1] - pixels_remove]
                    img_warped = cv2.resize(img_warped, (width, height), interpolation=cv2.INTER_AREA)
                    img_warped = cv2.cvtColor(img_warped, cv2.COLOR_BGR2GRAY)
                    # config: str = '-l eng —oem 3 -psm 3'
                    # text: str = pytesseract.image_to_string(img_warped, config=config)

                    std_dev = cv2.meanStdDev(img_warped)[1]
                    rase = sewar.rase(image1_gray, img_warped, ws=8)
                    if rase < max_rase_score and std_dev > same_color_threshold:
                        max_rase_score = rase
                        best_match_image[template] = img_warped
                        max_dev = std_dev

patterns: List[Type[DocumentPatternAbstract]] = [IdentityCardPattern(), OwnershipContractPattern(),
                                                 UnregisterVehiclePattern()]
confidence_level: float = 0.0
document_type: DocumentType = None
output_key = "radiere.png"
for key, value in best_match_image.items():
    lines: List[str] = []
    result = ocr.ocr(value, cls=True)
    if key == output_key:
        cv2.imwrite(f"data/output/{key}", value)
    for idx in range(len(result)):
        res = result[idx]
        if res is not None:
            for line in res:
                if key == output_key:
                    print(line[-1])
                lines.append(line[-1])

    for pattern in patterns:
        conf_level = pattern.compute_confidence_level(words=lines)

        if conf_level > confidence_level:
            confidence_level = conf_level
            document_type = pattern.get_document_type()

print(document_type)
print(confidence_level)
