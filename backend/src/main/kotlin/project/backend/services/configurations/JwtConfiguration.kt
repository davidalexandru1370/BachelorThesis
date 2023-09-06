<<<<<<<< HEAD:backend/src/main/kotlin/project/backend/services/configurations/JwtConfiguration.kt
package project.backend.services.configurations
========
package project.backend.businessLogic.configurations
>>>>>>>> da94f09cefb24785e68474a3021cf8c4c73ab5f6:backend/src/main/kotlin/project/backend/businessLogic/configurations/JwtConfiguration.kt

import org.springframework.beans.factory.annotation.Value
import org.springframework.stereotype.Component

@Component("application.yml")
class JwtConfiguration {
    @Value("\${jwt.audience}")
    lateinit var audience: String

    @Value("\${jwt.issuer}")
    lateinit var issuer: String

    @Value("\${jwt.expireTimeInMinutes}")
    var expireTimeInMinutes: Int = 30

    @Value("\${jwt.secret}")
    lateinit var secret: String
}
