package project.backend.authorization

import jakarta.servlet.FilterChain
import jakarta.servlet.http.HttpServletRequest
import jakarta.servlet.http.HttpServletResponse
import org.springframework.security.authentication.AuthenticationManager
import org.springframework.security.web.authentication.www.BasicAuthenticationFilter
import project.backend.services.interfaces.IUserService
import project.backend.utilities.JwtUtilities

class JwtAuthorizationFilter(
    private val authManager: AuthenticationManager,
    private val jwtUtilities: JwtUtilities,
    private val userService: IUserService
) : BasicAuthenticationFilter(authManager) {

    override fun doFilterInternal(request: HttpServletRequest, response: HttpServletResponse, chain: FilterChain) {
        val headerName: String = "Bearer "
        val header: String = request.getHeader(headerName);
        if (header == null || !header.startsWith(headerName)) {
            chain.doFilter(request, response)
        }

        if(jwtUtilities.)

    }
}