package project.backend.services.utilities

import io.jsonwebtoken.Claims
import io.jsonwebtoken.JwtException
import io.jsonwebtoken.JwtParser
import io.jsonwebtoken.Jwts
import io.jsonwebtoken.io.Decoders
import io.jsonwebtoken.security.Keys
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.context.annotation.Configuration
import project.backend.domain.dao.User
import project.backend.domain.exceptions.NotAuthorizedException
import project.backend.domain.internalization.ErrorCodes
import project.backend.services.configurations.JwtConfiguration
import java.security.Key
import java.util.*
import kotlin.jvm.Throws

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
                    "email" to user.email,
                ),
            )
            .signWith(createSignInKey(jwtConfiguration.secret))
            .setAudience(jwtConfiguration.audience)
            .setIssuer(jwtConfiguration.issuer)
            .setIssuedAt(Date())
            .setExpiration(DateUtilities.addMinutes(Date(), jwtConfiguration.expireTimeInMinutes))
            .compact()
    }

    @Throws(NotAuthorizedException::class)
    fun isTokenValid(token: String): Boolean {
        val claims = getClaims(token)
        val expiration = claims.expiration
        val now = Date(System.currentTimeMillis())
        return now.before(expiration)
    }

    @Throws(NotAuthorizedException::class)
    fun getClaims(token: String): Claims {
        val parser: JwtParser = Jwts
            .parserBuilder()
            .setSigningKey(jwtConfiguration.secret)
            .build()
        try {
            return parser.parseClaimsJws(token).body
        } catch (jwtException: JwtException) {
            throw NotAuthorizedException(ErrorCodes.NotAuthorized.toString())
        }
    }

    private fun createSignInKey(signKey: String): Key {
        var bytes: ByteArray = Decoders.BASE64.decode(signKey)
        return Keys.hmacShaKeyFor(bytes)
    }
}
