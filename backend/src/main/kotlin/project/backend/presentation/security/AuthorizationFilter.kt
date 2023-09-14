package project.backend.presentation.security

import jakarta.servlet.FilterChain
import jakarta.servlet.http.HttpServletRequest
import jakarta.servlet.http.HttpServletResponse
import org.springframework.core.Ordered
import org.springframework.core.annotation.Order
import org.springframework.http.HttpHeaders
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken
import org.springframework.security.core.context.SecurityContextHolder
import org.springframework.stereotype.Component
import org.springframework.web.filter.OncePerRequestFilter
import project.backend.persistence.repositories.IUserRepository
import project.backend.services.utilities.JwtUtilities

@Component
@Order(Ordered.LOWEST_PRECEDENCE)
class AuthorizationFilter(
    private val jwtUtilities: JwtUtilities,
    private val userRepository: IUserRepository,
) : OncePerRequestFilter() {
    override fun doFilterInternal(
        request: HttpServletRequest,
        response: HttpServletResponse,
        filterChain: FilterChain,
    ) {
        val header: String? = request.getHeader(HttpHeaders.AUTHORIZATION)
        if (header == null || !header.startsWith("Bearer ")) {
            filterChain.doFilter(request, response)
            return
        }
        val jwt = extractToken(header)

        if (jwtUtilities.isTokenValid(jwt)) {
            val claims = jwtUtilities.getClaims(jwt)
            val user = userRepository.findByEmail(claims["email"].toString())
            SecurityContextHolder.getContext().authentication = UsernamePasswordAuthenticationToken(user, user.id)
        }

        filterChain.doFilter(request, response)
    }

    private fun extractToken(header: String): String {
        return header.let {
            it.startsWith("Bearer ")
            it.split(" ").last()
        }
    }
}
