package project.backend.services.services

import org.springframework.beans.factory.annotation.Autowired
import org.springframework.dao.EmptyResultDataAccessException
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder
import org.springframework.stereotype.Service
import project.backend.core.common.AuthResult
import project.backend.core.common.UserCredentials
import project.backend.core.domain.dao.User
import project.backend.core.exceptions.ValidationException
import project.backend.core.interfaces.repositories.IUserRepository
import project.backend.core.internalization.ErrorCodes
import project.backend.services.interfaces.IUserService
import project.backend.services.utilities.JwtUtilities
import java.util.*

@Service
class UserService : IUserService {
    @set: Autowired
    lateinit var userRepository: IUserRepository

    @Autowired
    var jwtUtilities = JwtUtilities()

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

    override fun changePassword(formId: UUID, newPassword: String) {
        if (!checkPasswordConstraints(newPassword)) {
            throw ValidationException(ErrorCodes.InvalidPassword.toString())
        }
    }

    private fun checkPasswordConstraints(password: String): Boolean {
        var passwordMinLength: Int = 5
        return password.length >= passwordMinLength
    }
}
