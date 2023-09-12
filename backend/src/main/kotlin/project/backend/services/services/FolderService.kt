package project.backend.services.services

import org.springframework.stereotype.Service
import project.backend.services.entities.commands.folder.CreateFolderCommand
import project.backend.services.interfaces.IFolderService

@Service
class FolderService : IFolderService {
    override fun createFolder(createFolderCommand: CreateFolderCommand) {
        TODO("Not yet implemented")
    }
}
