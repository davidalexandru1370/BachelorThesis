package project.backend.infrastructure.repositories

import org.springframework.core.Ordered
import org.springframework.core.annotation.Order
import org.springframework.stereotype.Repository
import project.backend.core.interfaces.repositories.IUserRepository

@Repository
@Order(Ordered.HIGHEST_PRECEDENCE)
interface UserRepository : IUserRepository
