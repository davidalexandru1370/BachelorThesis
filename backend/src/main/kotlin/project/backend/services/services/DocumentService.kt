package project.backend.services.services

import org.modelmapper.ModelMapper
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.stereotype.Service
import project.backend.core.domain.Document
import project.backend.core.enums.DocumentType
import project.backend.core.exceptions.NotFoundException
import project.backend.core.interfaces.repositories.IDocumentRepository
import project.backend.core.internalization.ErrorCodes
import project.backend.services.entities.dtos.DocumentDto
import project.backend.services.interfaces.IDocumentService
import java.util.*

@Service
class DocumentService : IDocumentService {
    @Autowired
    private lateinit var modelMapper: ModelMapper

    @Autowired
    private lateinit var documentRepository: IDocumentRepository

    override fun addDocument(documentDto: DocumentDto): DocumentDto {
        val document = modelMapper.map(documentDto, Document::class.java)
        document.type = computeTypeOfDocument(document.storageUrl)
        val savedDocument = documentRepository.save(document)

        return modelMapper.map(savedDocument, DocumentDto::class.java)
    }

    override fun getDocument(id: UUID): DocumentDto {
        val document = documentRepository.findById(id)
            .orElseThrow { NotFoundException(ErrorCodes.DocumentDoesNotExists.toString()) }

        return modelMapper.map(document, DocumentDto::class.java)
    }

    override fun deleteDocument(id: UUID) {
        val document = documentRepository.findById(id)
            .orElseThrow { NotFoundException(ErrorCodes.DocumentDoesNotExists.toString()) }

        documentRepository.delete(document)
    }

    override fun computeTypeOfDocument(documentUrl: String): DocumentType =
        DocumentType.ANAFDocument
}
