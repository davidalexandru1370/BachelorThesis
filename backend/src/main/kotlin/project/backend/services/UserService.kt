package project.backend.services

import org.springframework.beans.factory.annotation.Autowired
import org.springframework.stereotype.Service
import project.backend.domain.AuthResult
import project.backend.domain.UserCredentials
import project.backend.repositories.UserRepository
import project.backend.services.interfaces.IUserService

@Service
class UserService : IUserService {
    @set: Autowired
    lateinit var userRepository: UserRepository

    override fun login(userCredentials: UserCredentials): AuthResult {
        TODO("Not yet implemented")
    }

    override fun register(userCredentials: UserCredentials): AuthResult {
        TODO("Not yet implemented")
    }


}