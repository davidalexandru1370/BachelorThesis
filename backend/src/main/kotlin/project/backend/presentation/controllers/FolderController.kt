package project.backend.presentation.controllers

import jakarta.validation.Valid
import org.modelmapper.ModelMapper
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.web.bind.annotation.PostMapping
import org.springframework.web.bind.annotation.RequestBody
import org.springframework.web.bind.annotation.RequestMapping
import org.springframework.web.bind.annotation.RestController
import project.backend.presentation.models.requests.folder.CreateFolderRequest
import project.backend.services.entities.commands.folder.CreateFolderCommand
import project.backend.services.interfaces.IFolderService

@RestController
@RequestMapping(path = ["/api/folder"])
class FolderController(
    @Autowired private val folderService: IFolderService,
    @Autowired private val modelMapper: ModelMapper,
) {

    @PostMapping(path = [""])
    fun createFolder(
        @Valid @RequestBody createFolderRequest: CreateFolderRequest,
    ) {
        val createFolderCommand = modelMapper.map(createFolderRequest, CreateFolderCommand::class.java)
        folderService.createFolder(createFolderCommand)
    }
}
