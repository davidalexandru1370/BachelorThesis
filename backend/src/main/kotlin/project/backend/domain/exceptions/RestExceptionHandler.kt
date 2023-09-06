package project.backend.domain.exceptions

import jakarta.persistence.EntityNotFoundException
import org.springframework.core.Ordered
import org.springframework.core.annotation.Order
import org.springframework.http.HttpHeaders
import org.springframework.http.HttpStatus
import org.springframework.http.HttpStatusCode
import org.springframework.http.ResponseEntity
import org.springframework.http.converter.HttpMessageNotReadableException
import org.springframework.web.bind.annotation.ControllerAdvice
import org.springframework.web.bind.annotation.ExceptionHandler
import org.springframework.web.context.request.WebRequest
import org.springframework.web.servlet.mvc.method.annotation.ResponseEntityExceptionHandler
import project.backend.domain.common.ApiError

@Order(Ordered.HIGHEST_PRECEDENCE)
@ControllerAdvice
class RestExceptionHandler : ResponseEntityExceptionHandler() {

    override fun handleHttpMessageNotReadable(
        ex: HttpMessageNotReadableException,
        headers: HttpHeaders,
        status: HttpStatusCode,
        request: WebRequest,
    ): ResponseEntity<Any>? {
        val error: String = "Malformed JSON request"
        return buildResponseEntity(ApiError(HttpStatus.BAD_REQUEST, error))
    }

    @ExceptionHandler(EntityNotFoundException::class)
    protected fun handleEntityNotFound(entityNotFoundException: EntityNotFoundException): ResponseEntity<in ApiError> {
        val apiError: ApiError = ApiError(HttpStatus.NOT_FOUND, entityNotFoundException.message ?: "")
        return buildResponseEntity(apiError)
    }

    private fun buildResponseEntity(apiError: ApiError): ResponseEntity<Any> {
        return ResponseEntity(apiError, apiError.status)
    }
}
