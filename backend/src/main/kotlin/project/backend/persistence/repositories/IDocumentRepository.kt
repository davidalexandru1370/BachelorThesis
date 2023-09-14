package project.backend.persistence.repositories

import org.hibernate.annotations.Filter
import org.hibernate.annotations.FilterDef
import org.hibernate.annotations.ParamDef
import org.hibernate.annotations.SQLDelete
import org.springframework.data.jpa.repository.JpaRepository
import project.backend.core.domain.Document
import java.util.*

@SQLDelete(sql = "UPDATE document SET isDeleted = true and deletedAt = :date WHERE id = ?")
@FilterDef(
    name = "deletedDocumentFilter",
    parameters = [ParamDef(name = "isDeleted", type = Boolean::class)],
)
@Filter(name = "deletedDocumentFilter", condition = "isDeleted = :isDeleted")
interface IDocumentRepository : JpaRepository<Document, UUID>
