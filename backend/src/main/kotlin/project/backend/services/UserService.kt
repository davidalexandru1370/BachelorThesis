package project.backend.services

import org.springframework.stereotype.Service
import project.backend.domain.AuthResult
import project.backend.domain.UserCredentials
import project.backend.services.interfaces.IUserService

@Service
class UserService : IUserService {
    override fun login(userCredentials: UserCredentials): AuthResult {
        TODO("Not yet implemented")
    }

    override fun register(userCredentials: UserCredentials): AuthResult {
        TODO("Not yet implemented")
    }


}