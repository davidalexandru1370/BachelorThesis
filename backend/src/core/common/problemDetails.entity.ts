export class ProblemDetails {
  statusCode: number;
  path: string;
  message: string;
  constructor(statusCode: number, path: string, message: string) {
    this.statusCode = statusCode;
    this.path = path;
    this.message = message;
  }
}
