from concurrent import futures

import grpc

import document_recognition_pb2_grpc


class AnalyzeDocumentService(document_recognition_pb2_grpc.DocumentRecognitionService):
    def DocumentClassification(self, request, context):
        return super().DocumentClassification(request, context)


def serve():
    server = grpc.server(thread_pool=futures.ThreadPoolExecutor(max_workers=10))
    document_recognition_pb2_grpc.add_DocumentRecognitionServiceServicer_to_server(AnalyzeDocumentService(), server)
    server.add_insecure_port("localhost:5001")
    server.start()
    server.wait_for_termination()


if __name__ == "__main__":
    serve()
