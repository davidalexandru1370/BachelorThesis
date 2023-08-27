package project.backend.configurations

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
