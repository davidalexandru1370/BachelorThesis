package project.backend.presentation.models.requests.folder

import jakarta.validation.constraints.NotEmpty
import project.backend.presentation.models.requests.document.CreateDocumentRequest

class CreateFolderRequest(
    @NotEmpty
    var documents: Set<CreateDocumentRequest> = HashSet<CreateDocumentRequest>(),
)
