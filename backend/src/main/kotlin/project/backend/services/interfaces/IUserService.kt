package project.backend.services.interfaces

import project.backend.domain.AuthResult
import project.backend.domain.UserCredentials

interface IUserService {
    fun login(userCredentials: UserCredentials) : AuthResult
    fun register(userCredentials: UserCredentials) : AuthResult
}