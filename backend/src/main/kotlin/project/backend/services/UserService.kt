package project.backend.services

import jakarta.persistence.EntityNotFoundException
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.stereotype.Service
import project.backend.domain.AuthResult
import project.backend.domain.UserCredentials
import project.backend.domain.dao.User
import project.backend.repositories.UserRepository
import project.backend.services.interfaces.IUserService
import org.springframework.dao.EmptyResultDataAccessException
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder
import project.backend.internalization.ErrorCodes
import project.backend.utilities.JwtUtilities

@Service
class UserService : IUserService {
    @set: Autowired
    lateinit var userRepository: UserRepository

    @Autowired
    var jwtUtilities = JwtUtilities();

    override fun login(userCredentials: UserCredentials): AuthResult {
        val foundUser: User = try {
            userRepository.findByEmail(userCredentials.email)
        } catch (_: EmptyResultDataAccessException) {
            null
        } ?: return AuthResult(token = "", result = false, error = ErrorCodes.EmailDoesNotExists.toString())

        val bCryptPasswordEncoder: BCryptPasswordEncoder = BCryptPasswordEncoder()

        if (!bCryptPasswordEncoder.matches(userCredentials.password, foundUser.password)) {
            return AuthResult(ErrorCodes.EmailOrPasswordAreWrong.toString())
        }

        val token: String = jwtUtilities.createJwt(foundUser)
        return AuthResult(token = token, result = true, error = "")
    }

    override fun register(userCredentials: UserCredentials): AuthResult {
        val bCryptPasswordEncoder: BCryptPasswordEncoder = BCryptPasswordEncoder()
        val foundUser: Boolean = try {
            userRepository.findByEmail(userCredentials.email)
            true
        } catch (_: EmptyResultDataAccessException) {
            false
        }

        if (foundUser) {
            return AuthResult(token = "", result = false, error = ErrorCodes.EmailOrPasswordAreWrong.toString())
        }

        userCredentials.password = bCryptPasswordEncoder.encode(userCredentials.password)

        var user: User = User(userCredentials.email, userCredentials.password)

        user = userRepository.save(user)

        val token = jwtUtilities.createJwt(user)
        return AuthResult(token = token, result = true, error = "")
    }

}