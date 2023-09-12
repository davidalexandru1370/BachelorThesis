package project.backend.presentation.controllers

import org.springframework.beans.factory.annotation.Autowired
import org.springframework.web.bind.annotation.PostMapping
import org.springframework.web.bind.annotation.RequestMapping
import org.springframework.web.bind.annotation.RestController
import project.backend.services.interfaces.IFolderService

@RestController
@RequestMapping(path = ["/api/folder"])
class FolderController(
    @Autowired private val folderService: IFolderService
) {

    @PostMapping()
    fun createFolder() {

    }
}
