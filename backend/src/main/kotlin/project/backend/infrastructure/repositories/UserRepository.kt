package project.backend.infrastructure.repositories

import org.springframework.stereotype.Component
import org.springframework.stereotype.Repository
import project.backend.core.interfaces.repositories.IUserRepository

@Repository
@Component
interface UserRepository : IUserRepository
