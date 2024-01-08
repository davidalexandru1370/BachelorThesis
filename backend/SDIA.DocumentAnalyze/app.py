import cv2
import numpy as np
from flask import Flask, request, jsonify, Response

from Domain.document_type import DocumentType
from analyze_document import ImageClassifier

app = Flask(__name__)

host: str = "0.0.0.0"
port: int = 5001


@app.route("/api/document/analyze", methods=['POST'])
def analyze_document():
    # file = request.files.get("image", '')
    # image = Image.open(file.stream)
    # image.show()
    # response = jsonify(str(dt.DocumentType.NOT_FOUND))
    try:
        image_classifier: ImageClassifier = ImageClassifier()
        # np_arr = np.fromstring(request.files["image"].read(), np.uint8)
        image = cv2.imdecode(np.frombuffer(request.files["file"].read()), cv2.IMREAD_COLOR)
        type = {'DocumentType': image_classifier.classify_image(image)}
        return Response(response=jsonify(str(type)), status=200, mimetype="application/json")
    except Exception as e:
        print(e)
        type = {'DocumentType': DocumentType.NotFound}
        return Response(response=jsonify(str(type)), status=400, mimetype="application/json")

    return response


app.run(host, port)
