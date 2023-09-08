package project.backend.services.interfaces

import project.backend.core.enums.DocumentType
import project.backend.services.entities.dtos.DocumentDto
import java.util.*

interface IDocumentService{
    fun addDocument(documentDto: DocumentDto): DocumentDto
    fun getDocument(id: UUID): DocumentDto
    fun deleteDocument(id: UUID): DocumentDto
    fun computeTypeOfDocument(documentUrl : String): DocumentType
}
