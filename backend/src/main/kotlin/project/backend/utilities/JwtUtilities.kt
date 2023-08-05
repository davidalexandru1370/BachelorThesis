package project.backend.utilities

import io.jsonwebtoken.Jwts
import io.jsonwebtoken.io.Decoders
import io.jsonwebtoken.security.Keys
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.context.annotation.Configuration
import project.backend.configurations.JwtConfiguration
import project.backend.domain.dao.User
import java.security.Key
import java.util.*

@Configuration
class JwtUtilities {
    @Autowired
    var jwtConfiguration: JwtConfiguration = JwtConfiguration()
    fun createJwt(user: User): String {
        return Jwts
            .builder()
            .setClaims(
                mapOf(
                    "id" to user.id,
                    "email" to user.email
                )
            )
            .signWith(createSignInKey(jwtConfiguration.secret))
            .setAudience(jwtConfiguration.audience)
            .setIssuer(jwtConfiguration.issuer)
            .setIssuedAt(Date())
            .setExpiration(DateUtilities.addMinutes(Date(), jwtConfiguration.expireTimeInMinutes))
            .compact()
    }

    private fun createSignInKey(signKey: String): Key {
        var bytes: ByteArray = Decoders.BASE64.decode(signKey)
        return Keys.hmacShaKeyFor(bytes)
    }
}