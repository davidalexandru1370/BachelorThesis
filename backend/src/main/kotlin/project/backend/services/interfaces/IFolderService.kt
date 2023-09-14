package project.backend.services.interfaces

import project.backend.services.entities.commands.folder.CreateFolderCommand
import project.backend.services.entities.dtos.FolderDto

fun interface IFolderService {
    fun createFolder(createFolderCommand: CreateFolderCommand): FolderDto
}
