package project.backend.controllers

import jakarta.validation.Valid
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.http.HttpStatus
import org.springframework.http.ResponseEntity
import org.springframework.web.bind.annotation.PostMapping
import org.springframework.web.bind.annotation.RequestBody
import org.springframework.web.bind.annotation.RequestMapping
import org.springframework.web.bind.annotation.RestController
import project.backend.domain.AuthResult
import project.backend.domain.UserCredentials
import project.backend.services.UserService
import project.backend.services.interfaces.IUserService

@RestController
@RequestMapping(path = ["/api/user"])
class UserController(

    @Autowired private val userService: IUserService
) {

    @PostMapping(path = ["/login"])
    fun login(@Valid @RequestBody userCredentials: UserCredentials): ResponseEntity<AuthResult> {
        val loginResult: AuthResult = userService.login(userCredentials)
        return if (loginResult.result) {
            ResponseEntity.ok(loginResult)
        } else {
            ResponseEntity(loginResult, HttpStatus.BAD_REQUEST)
        }
    }


    @PostMapping(path = ["/register"])
    fun register(@Valid @RequestBody userCredentials: UserCredentials): ResponseEntity<AuthResult> {
        val isRegistered = userService.register(userCredentials)
        return if (isRegistered.result) {
            ResponseEntity.ok(isRegistered)
        } else {
            ResponseEntity(isRegistered, HttpStatus.BAD_REQUEST)
        }
    }

}