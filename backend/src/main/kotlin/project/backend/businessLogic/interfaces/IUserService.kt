package project.backend.businessLogic.interfaces

import project.backend.core.utils.AuthResult
import project.backend.core.utils.UserCredentials
import java.util.UUID

interface IUserService {
    fun login(userCredentials: UserCredentials): AuthResult
    fun register(userCredentials: UserCredentials): AuthResult
    fun changePassword(formId: UUID, newPassword: String)
}
