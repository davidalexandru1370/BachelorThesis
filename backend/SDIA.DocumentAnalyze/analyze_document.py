import cv2
import time
import os
import math
import numpy as np
import pytesseract


class AnalyzeDocument:
    def __init__(self):
        self.__execute_document_scanning()

    def __execute_document_scanning(self):
        # Load the image
        img = cv2.imread(r"resources\documents\buletin.PNG", 0)

        img_copy = img.copy()

        img_canny = cv2.Canny(img_copy, 50, 100, apertureSize=3)

        img_hough = cv2.HoughLinesP(img_canny, 1, math.pi / 180, 100, minLineLength=100, maxLineGap=10)

        (x, y, w, h) = (np.amin(img_hough, axis=0)[0, 0], np.amin(img_hough, axis=0)[0, 1],
                        np.amax(img_hough, axis=0)[0, 0] - np.amin(img_hough, axis=0)[0, 0],
                        np.amax(img_hough, axis=0)[0, 1] - np.amin(img_hough, axis=0)[0, 1])

        img_roi = img_copy[y:y + h, x:x + w]

        (height, width) = img_roi.shape
        img_roi_copy = img_roi.copy()
        dim_mrz = (x, y, w, h) = (1, round(height * 0.9), width - 3, round(height - (height * 0.9)) - 2)
        img_roi_copy = cv2.rectangle(img_roi_copy, (x, y), (x + w, y + h), (0, 0, 0), 2)

        img_mrz = img_roi[y:y + h, x:x + w]
        img_mrz = cv2.GaussianBlur(img_mrz, (3, 3), 0)
        ret, img_mrz = cv2.threshold(img_mrz, 127, 255, cv2.THRESH_TOZERO)

        if os.name == "nt":
            pytesseract.pytesseract.tesseract_cmd = r"C:\Program Files\Tesseract-OCR\tesseract.exe"

        mrz = pytesseract.image_to_string(img_mrz, config='--psm 12')
        
        cv2.imshow("da", img_roi)


if __name__ == "__main__":
    analyzeDocument: AnalyzeDocument = AnalyzeDocument()
