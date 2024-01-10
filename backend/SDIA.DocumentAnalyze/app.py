import io
import json

import cv2
import numpy as np
from flask import Flask, request, jsonify, Response

from Domain.document_type import DocumentType
from analyze_document import ImageClassifier

app = Flask(__name__)

host: str = "127.0.0.1"
port: int = 5001


def custom_serializer(obj):
    if isinstance(obj, DocumentType):
        return obj.serialize()


@app.route("/api/document/analyze", methods=['POST'])
def analyze_document():
    # file = request.files.get("image", '')
    # image = Image.open(file.stream)
    # image.show()
    # response = jsonify(str(dt.DocumentType.NOT_FOUND))
    try:
        in_memory_file = io.BytesIO()
        file = request.files["file"]
        file.save(in_memory_file)
        data = np.fromstring(in_memory_file.getvalue(), dtype=np.uint8)
        image = cv2.imdecode(data, 1)
        image_classifier: ImageClassifier = ImageClassifier()
        type = {"DocumentType": image_classifier.classify_image(image).name}
        return Response(response=json.dumps(type), status=200, mimetype="application/json")
    except Exception as e:
        print(e)
        type = {"DocumentType": DocumentType.NotFound.name}
        return Response(response=json.dumps(type), status=400, mimetype="application/json")

    return response


app.run(host, port, debug=True)
