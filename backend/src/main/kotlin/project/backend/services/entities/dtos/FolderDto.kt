package project.backend.services.entities.dtos

import project.backend.core.domain.Document
import project.backend.core.domain.User
import java.util.*

data class FolderDto(
    var id: UUID,
    var createdAt: Date,
    var user: User,
    var documents: Set<Document>,
)
