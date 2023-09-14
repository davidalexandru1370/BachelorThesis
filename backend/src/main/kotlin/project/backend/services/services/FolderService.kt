package project.backend.services.services

import org.modelmapper.ModelMapper
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.stereotype.Service
import project.backend.core.domain.Folder
import project.backend.persistence.repositories.IFolderRepository
import project.backend.services.entities.commands.folder.CreateFolderCommand
import project.backend.services.entities.dtos.FolderDto
import project.backend.services.interfaces.IDocumentService
import project.backend.services.interfaces.IFolderService

@Service
class FolderService : IFolderService {

    @Autowired
    private lateinit var folderRepository: IFolderRepository

    @Autowired
    private lateinit var documentService: IDocumentService

    @Autowired
    private lateinit var modelMapper: ModelMapper

    override fun createFolder(createFolderCommand: CreateFolderCommand): FolderDto {
        val folder = modelMapper.map(createFolderCommand, Folder::class.java)
        folder.documents.forEach { d -> d.type = documentService.computeTypeOfDocument(d.storageUrl) }

        val savedEntity = folderRepository.save(folder)
        val folderDto = modelMapper.map(savedEntity, FolderDto::class.java)

        return folderDto
    }
}
