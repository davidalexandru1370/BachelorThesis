package project.backend.persistence.repositories

import org.springframework.data.jpa.repository.JpaRepository
import project.backend.core.domain.Folder
import java.util.*

interface IFolderRepository : JpaRepository<Folder, UUID>
