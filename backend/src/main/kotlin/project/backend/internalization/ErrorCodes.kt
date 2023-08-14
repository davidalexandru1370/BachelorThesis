package project.backend.internalization

class ErrorCodes {
    companion object Factory{
        const val EmailDoesNotExists: Int = 1
        const val EmailOrPasswordAreWrong : Int = 2
        const val NotAuthorized: Int = 3
    }
}