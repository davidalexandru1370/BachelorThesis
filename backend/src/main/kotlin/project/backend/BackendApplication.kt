package project.backend

import org.springframework.boot.autoconfigure.SpringBootApplication
import org.springframework.boot.context.properties.ConfigurationPropertiesScan
import org.springframework.boot.runApplication
import org.springframework.data.jpa.repository.config.EnableJpaRepositories

@EnableJpaRepositories(basePackages = ["project.backend.infrastructure.repositories", "project.backend.core.interfaces.repositories"])
@SpringBootApplication
@ConfigurationPropertiesScan
class BackendApplication

fun main(args: Array<String>) {
    runApplication<BackendApplication>(*args)
}
