package project.backend.presentation.security

import org.springframework.beans.factory.annotation.Autowired
import org.springframework.context.annotation.Bean
import org.springframework.context.annotation.Configuration
import org.springframework.security.config.annotation.web.builders.HttpSecurity
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity
import org.springframework.security.config.http.SessionCreationPolicy
import org.springframework.security.web.SecurityFilterChain
import org.springframework.security.web.util.matcher.AntPathRequestMatcher
import project.backend.core.interfaces.repositories.IUserRepository
import project.backend.services.utilities.JwtUtilities

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
            if (System.getenv("ENVIRONMENT") == "dev") {
                it.requestMatchers(AntPathRequestMatcher("/v3/**")).permitAll()
                it.requestMatchers(AntPathRequestMatcher("/swagger-ui/**")).permitAll()
            }
        }
            .cors {
                it.disable()
            }

        http.sessionManagement {
            it.sessionCreationPolicy(SessionCreationPolicy.STATELESS)
        }

        return http.build()
    }
}
