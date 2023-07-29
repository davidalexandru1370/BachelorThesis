package project.backend.controllers

import jakarta.validation.Valid
import org.springframework.http.ResponseEntity
import org.springframework.web.bind.annotation.PostMapping
import org.springframework.web.bind.annotation.RequestBody
import org.springframework.web.bind.annotation.RequestMapping
import org.springframework.web.bind.annotation.RestController
import project.backend.domain.AuthResult
import project.backend.domain.UserCredentials

@RestController
@RequestMapping(path = ["/api/user"])
class UserController {

   @PostMapping(path = ["/register"])
   fun register(@Valid @RequestBody userCredentials: UserCredentials): ResponseEntity<AuthResult> {
      val authResult : AuthResult = AuthResult("asd",false,"eroare")
      return ResponseEntity.ok(authResult);
   }

}