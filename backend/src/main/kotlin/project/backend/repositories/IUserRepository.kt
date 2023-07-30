package project.backend.repositories

import org.springframework.data.jpa.repository.JpaRepository
import org.springframework.data.repository.NoRepositoryBean
import project.backend.domain.dao.User
import project.backend.exceptions.RepositoryException
import java.util.UUID
import kotlin.jvm.Throws

@NoRepositoryBean
interface IUserRepository : JpaRepository<User,UUID>{
    @Throws(RepositoryException::class)
    fun findByEmail(email: String) : User
}