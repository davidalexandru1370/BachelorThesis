package project.backend.presentation.models.requests.document

import jakarta.validation.constraints.NotEmpty


data class CreateDocumentRequest(
    @NotEmpty
    private var storageUrl: String = ""
)
