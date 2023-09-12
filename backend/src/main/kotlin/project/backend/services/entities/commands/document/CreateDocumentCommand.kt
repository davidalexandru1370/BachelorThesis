package project.backend.services.entities.commands.document

import java.util.*

data class CreateDocumentCommand(
    var storageUrl: String,
    var createdAt: Date,
)
