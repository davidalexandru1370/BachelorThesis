package project.backend.core.common

import jakarta.validation.constraints.Email
import jakarta.validation.constraints.NotBlank
import org.hibernate.validator.constraints.Length

data class UserCredentials(
    @field:NotBlank(message = "Email must not be blank")
    @field:Email(message = "Invalid email address")
    var email: String,
    @field:NotBlank(message = "Password can not be empty")
    @field:Length(min = 5, message = "Password must be at least 5 characters")
    var password: String,
)