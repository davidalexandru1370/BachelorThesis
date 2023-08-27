package project.backend.domain

import com.fasterxml.jackson.annotation.JsonFormat
import org.springframework.http.HttpStatus
import java.time.LocalDateTime

class ApiError(var status: HttpStatus, var message: String) {
    @JsonFormat(shape = JsonFormat.Shape.STRING, pattern = "dd-MM-yyyy hh:mm:ss")
    var timeStamp: LocalDateTime = LocalDateTime.now()

    constructor(status: HttpStatus) : this(status, "") {
    }
}
