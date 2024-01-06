import bisect
import heapq
from typing import List

from PIL import Image
from flask import Flask, request, jsonify


app = Flask(__name__)


@app.route("/api/document/analyze", methods=['POST'])
def analyze_document():
    file = request.files.get("image", '')
    image = Image.open(file.stream)
    image.show()
    #response = jsonify(str(dt.DocumentType.NOT_FOUND))

    return response
