package project.backend.services.entities.commands.folder

import project.backend.services.entities.dtos.DocumentDto

data class CreateFolderCommand(
    var storageUrl: String,
    var documents: Set<DocumentDto>,
)
