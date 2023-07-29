package project.backend.domain

data class AuthResult(val token: String?, val result: Boolean, val error: String?)