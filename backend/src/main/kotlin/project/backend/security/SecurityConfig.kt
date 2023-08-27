package project.backend.security

import org.springframework.beans.factory.annotation.Autowired
import org.springframework.context.annotation.Bean
import org.springframework.context.annotation.Configuration
import org.springframework.security.config.annotation.web.builders.HttpSecurity
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity
import org.springframework.security.config.http.SessionCreationPolicy
import org.springframework.security.web.SecurityFilterChain
import org.springframework.security.web.authentication.UsernamePasswordAuthenticationFilter
import project.backend.repositories.IUserRepository
import project.backend.utilities.JwtUtilities

@Configuration
@EnableWebSecurity
class SecurityConfig(
    @Autowired var jwtUtilities: JwtUtilities,
    @Autowired var userRepository: IUserRepository,
) {
    @Bean
    fun filterChain(http: HttpSecurity): SecurityFilterChain {
        http.csrf { csrf -> csrf.disable() }.authorizeHttpRequests {
            it.requestMatchers("/api/user/**").permitAll()
        }.addFilterBefore(AuthorizationFilter(jwtUtilities, userRepository), UsernamePasswordAuthenticationFilter().javaClass)
            .cors {
                it.disable()
            }

        http.sessionManagement {
            it.sessionCreationPolicy(SessionCreationPolicy.STATELESS)
        }

        return http.build()
    }
}
