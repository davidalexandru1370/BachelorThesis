<<<<<<<< HEAD:backend/src/main/kotlin/project/backend/domain/common/AuthResult.kt
package project.backend.domain.common
========
package project.backend.core.utils
>>>>>>>> da94f09cefb24785e68474a3021cf8c4c73ab5f6:backend/src/main/kotlin/project/backend/core/utils/AuthResult.kt

data class AuthResult(val token: String?, val result: Boolean, val error: String?) {
    constructor(error: String?) : this(token = "", result = false, error = error)
}
