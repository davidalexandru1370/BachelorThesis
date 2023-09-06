package project.backend.core.domain.dao

import jakarta.persistence.Entity
import jakarta.persistence.GeneratedValue
import jakarta.persistence.GenerationType
import jakarta.persistence.Id
import java.util.UUID

@Entity
class Document {

    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    lateinit var id: UUID
}
