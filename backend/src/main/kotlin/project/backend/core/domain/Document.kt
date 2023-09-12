package project.backend.core.domain

import jakarta.persistence.*
import project.backend.core.enums.DocumentType
import project.backend.core.interfaces.entities.ISoftDelete
import java.sql.Date
import java.util.*

@Entity
class Document() : ISoftDelete() {

    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    lateinit var id: UUID

    @Column(nullable = true)
    @Enumerated(EnumType.STRING)
    var type: DocumentType? = null

    @Column
    lateinit var createdAt: Date

    @Column
    lateinit var storageUrl: String

    @ManyToOne(fetch = FetchType.LAZY)
    lateinit var folder: Folder

    constructor(createdAt: Date, storageUrl: String) : this() {
        this.createdAt = createdAt
        this.storageUrl = storageUrl
    }
}
