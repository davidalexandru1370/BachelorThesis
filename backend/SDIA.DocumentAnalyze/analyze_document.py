import cv2
import os
import math
import time
import numpy as np
import pytesseract

if os.name == "nt":
    pytesseract.pytesseract.tesseract_cmd = r"C:\Program Files\Tesseract-OCR\tesseract.exe"


class AnalyzeDocument:
    def __init__(self):
        self.execute_document_scanning()

    def execute_document_scanning(self):
        # Load the image
        img = cv2.imread(r"resources\documents\drivingLicense.jpg")

        # Convert the image to gray scale
        gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
        # cv2.imshow("da", gray)
        cv2.waitKey(0)

        # Performing OTSU threshold
        ret, thresh1 = cv2.threshold(gray, 0, 255, cv2.THRESH_OTSU | cv2.THRESH_BINARY_INV)

        rect_kernel = cv2.getStructuringElement(cv2.MORPH_RECT, (100, 25))

        # Applying dilation on the threshold image
        dilation = cv2.dilate(thresh1, rect_kernel, iterations=1)

        # Finding contours
        contours, hierarchy = cv2.findContours(dilation, cv2.RETR_EXTERNAL,
                                               cv2.CHAIN_APPROX_NONE)

        im2 = img.copy()

        for contour in contours:
            x, y, w, h = cv2.boundingRect(contour)

            # Drawing a rectangle on copied image
            rect = cv2.rectangle(im2, (x, y), (x + w, y + h), (0, 255, 0), 2)

            # Cropping the text block for giving input to OCR
            cropped = im2[y:y + h, x:x + w]

            # Apply OCR on the cropped image
            text = pytesseract.image_to_string(cropped)
            if len(text.strip()) > 0:
                print(text)


if __name__ == "__main__":
    analyzeDocument: AnalyzeDocument = AnalyzeDocument()
