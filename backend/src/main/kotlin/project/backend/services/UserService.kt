package project.backend.services

import org.springframework.beans.factory.annotation.Autowired
import org.springframework.stereotype.Service
import project.backend.domain.AuthResult
import project.backend.domain.UserCredentials
import project.backend.domain.dao.User
import project.backend.repositories.UserRepository
import project.backend.services.interfaces.IUserService
import de.nycode.bcrypt.hash
import de.nycode.bcrypt.verify
import org.springframework.dao.EmptyResultDataAccessException
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder
import project.backend.utilities.JwtUtilities
import java.util.*

@Service
class UserService : IUserService {
    @set: Autowired
    lateinit var userRepository: UserRepository

    @Autowired
    var jwtUtilities = JwtUtilities();

    override fun login(userCredentials: UserCredentials): AuthResult {
        val foundUser: User? = try {
            userRepository.findByEmail(userCredentials.email)
        } catch (_: EmptyResultDataAccessException) {
            null
        }

        if (foundUser == null) {
            return AuthResult(token = "", result = false, error = "Account does not exists!")
        }

        val bCryptPasswordEncoder: BCryptPasswordEncoder = BCryptPasswordEncoder()

        if (!bCryptPasswordEncoder.matches(userCredentials.password, foundUser.password)) {
            return AuthResult("Invalid email or password!")
        }

        var token: String = jwtUtilities.createJwt(foundUser)
        return AuthResult(token = token, result = true, error = "")
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
        userCredentials.password = hash(userCredentials.password).decodeToString()

        var user: User = User(userCredentials.email, userCredentials.password)

        user = userRepository.save(user)

        var token = jwtUtilities.createJwt(user)
        return AuthResult(token = token, result = true, error = "")
    }


}