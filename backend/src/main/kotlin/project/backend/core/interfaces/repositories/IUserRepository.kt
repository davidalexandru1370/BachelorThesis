package project.backend.core.interfaces.repositories

import org.springframework.data.jpa.repository.JpaRepository
import org.springframework.data.repository.NoRepositoryBean
import project.backend.core.domain.dao.User
import java.util.UUID

@NoRepositoryBean
interface IUserRepository : JpaRepository<User, UUID> {
    fun findByEmail(email: String): User
}
