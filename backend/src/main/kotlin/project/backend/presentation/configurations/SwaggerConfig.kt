package project.backend.presentation.configurations

import io.swagger.v3.oas.models.Components
import io.swagger.v3.oas.models.OpenAPI
import io.swagger.v3.oas.models.info.Info
import io.swagger.v3.oas.models.security.SecurityRequirement
import org.springframework.context.annotation.Bean
import org.springframework.context.annotation.Configuration

@Configuration
class SwaggerConfig {
    @Bean
    fun api(): OpenAPI {
        return OpenAPI()
            .addSecurityItem(
                SecurityRequirement()
                    .addList("Bearer Authentication"),
            )
            .components(
                Components().addSecuritySchemes(
                    "Bearer Authentication",
                    io.swagger.v3.oas.models.security.SecurityScheme()
                        .type(io.swagger.v3.oas.models.security.SecurityScheme.Type.HTTP)
                        .scheme("bearer")
                        .bearerFormat("JWT"),
                ),
            )
            .info(
                Info().title("SDIA API")
                    .version("1.0"),
            )
    }
}
