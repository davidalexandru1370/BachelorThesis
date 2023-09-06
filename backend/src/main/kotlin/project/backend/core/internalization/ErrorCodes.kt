<<<<<<<< HEAD:backend/src/main/kotlin/project/backend/domain/internalization/ErrorCodes.kt
package project.backend.domain.internalization
========
package project.backend.core.internalization
>>>>>>>> da94f09cefb24785e68474a3021cf8c4c73ab5f6:backend/src/main/kotlin/project/backend/core/internalization/ErrorCodes.kt

class ErrorCodes {
    companion object Factory {
        const val EmailDoesNotExists: Int = 1
        const val EmailOrPasswordAreWrong: Int = 2
        const val NotAuthorized: Int = 3
        const val InvalidPassword: Int = 4
    }
}
