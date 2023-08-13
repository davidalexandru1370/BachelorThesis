package project.backend.services.interfaces

import org.springframework.security.core.userdetails.UserDetailsService
import project.backend.domain.AuthResult
import project.backend.domain.UserCredentials

interface IUserService {
    fun login(userCredentials: UserCredentials) : AuthResult
    fun register(userCredentials: UserCredentials) : AuthResult
}