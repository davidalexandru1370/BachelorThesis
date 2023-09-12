package project.backend.presentation.models.requests.folder

import lombok.Getter
import lombok.Setter
import project.backend.presentation.models.requests.document.CreateDocumentRequest

@Getter
@Setter
class CreateFolderRequest {
    private lateinit var documents: Set<CreateDocumentRequest>
}
