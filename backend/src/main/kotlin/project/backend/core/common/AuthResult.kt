package project.backend.core.common

data class AuthResult(val token: String?, val result: Boolean, val error: String?) {
    constructor(error: String?) : this(token = "", result = false, error = error)
}
