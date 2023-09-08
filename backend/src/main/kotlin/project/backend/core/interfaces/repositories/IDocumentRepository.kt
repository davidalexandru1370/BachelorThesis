package project.backend.core.interfaces.repositories
import org.hibernate.annotations.SQLDelete
import org.springframework.data.jpa.repository.JpaRepository
import project.backend.core.domain.dao.Document
import java.util.*

@SQLDelete(sql = "UPDATE document SET isDeleted = true and deletedAt = :date WHERE id = ?")
interface IDocumentRepository : JpaRepository<Document, UUID>
