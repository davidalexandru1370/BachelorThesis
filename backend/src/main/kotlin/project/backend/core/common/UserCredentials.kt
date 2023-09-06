<<<<<<<< HEAD:backend/src/main/kotlin/project/backend/domain/common/UserCredentials.kt
package project.backend.domain.common
========
package project.backend.core.utils
>>>>>>>> da94f09cefb24785e68474a3021cf8c4c73ab5f6:backend/src/main/kotlin/project/backend/core/utils/UserCredentials.kt

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
