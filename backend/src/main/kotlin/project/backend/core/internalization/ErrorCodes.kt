package project.backend.core.internalization

class ErrorCodes {
    companion object Factory {
        const val EmailDoesNotExists: Int = 1
        const val EmailOrPasswordAreWrong: Int = 2
        const val NotAuthorized: Int = 3
        const val InvalidPassword: Int = 4
        const val DocumentDoesNotExists: Int = 5
    }
}
