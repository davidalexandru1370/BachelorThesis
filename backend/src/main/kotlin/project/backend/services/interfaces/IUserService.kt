package project.backend.services.interfaces

import project.backend.domain.AuthResult
import project.backend.domain.UserCredentials
import java.util.UUID

interface IUserService {
    fun login(userCredentials: UserCredentials): AuthResult
    fun register(userCredentials: UserCredentials): AuthResult
    fun changePassword(formId: UUID, newPassword: String)
}