package project.backend.services.interfaces

import project.backend.services.entities.commands.folder.CreateFolderCommand

interface IFolderService {
    fun createFolder(createFolderCommand: CreateFolderCommand)
}
