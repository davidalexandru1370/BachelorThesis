import {
  ArgumentsHost,
  Catch,
  ExceptionFilter,
  HttpException,
} from "@nestjs/common";
import { Request, Response } from "express";
import { ProblemDetails } from "src/core/common/problemDetails.entity";

@Catch(HttpException)
export class HttpExceptionFilter implements ExceptionFilter {
  catch(exception: HttpException, host: ArgumentsHost) {
    const context = host.switchToHttp();
    const response = context.getResponse<Response>();
    const request = context.getRequest<Request>();
    const status = exception.getStatus();
    const problemDetails: ProblemDetails = new ProblemDetails(
      status,
      request.url,
      exception.message
    );

    response.status(status).json(problemDetails);
  }
}
