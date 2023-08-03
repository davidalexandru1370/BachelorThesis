package project.backend.services

import org.springframework.beans.factory.annotation.Autowired
import org.springframework.stereotype.Service
import project.backend.domain.AuthResult
import project.backend.domain.UserCredentials
import project.backend.domain.dao.User
import project.backend.repositories.UserRepository
import project.backend.services.interfaces.IUserService
import de.nycode.bcrypt.hash
import org.springframework.dao.EmptyResultDataAccessException
import project.backend.exceptions.RepositoryException

@Service
class UserService : IUserService {
    @set: Autowired
    lateinit var userRepository: UserRepository

    override fun login(userCredentials: UserCredentials): AuthResult {
        TODO("Not yet implemented")
    }

    override fun register(userCredentials: UserCredentials): AuthResult {
        val foundUser: Boolean = try {
            userRepository.findByEmail(userCredentials.email)
            true
        } catch (_: EmptyResultDataAccessException) {
            false
        }

        if (foundUser) {
            return AuthResult(token = "", result = false, error = "Email address already taken!")
        }

        userCredentials.password = hash(userCredentials.password).contentToString()

        val user: User = User(userCredentials.email, userCredentials.password)

        userRepository.save(user)

        return AuthResult(token = "", result = true, error = "")
    }


}