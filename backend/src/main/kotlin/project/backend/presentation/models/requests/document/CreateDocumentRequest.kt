package project.backend.presentation.models.requests.document

import lombok.Getter
import lombok.Setter

@Getter
@Setter
class CreateDocumentRequest {
    private var storageUrl: String = ""
}
