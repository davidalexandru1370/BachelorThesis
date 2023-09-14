package project.backend.presentation.models.requests.document

import jakarta.validation.constraints.NotEmpty

data class CreateDocumentRequest(
    @NotEmpty
    var storageUrl: String = "",
)
