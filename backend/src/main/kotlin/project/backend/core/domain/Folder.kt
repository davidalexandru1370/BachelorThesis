package project.backend.core.domain

import jakarta.persistence.*
import project.backend.core.interfaces.entities.ISoftDelete
import java.util.*

@Entity
class Folder : ISoftDelete() {
    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    lateinit var id: UUID

    @Column
    lateinit var createdAt: Date

    @ManyToOne(fetch = FetchType.LAZY, cascade = [CascadeType.DETACH])
    lateinit var user: User

    @OneToMany(cascade = [CascadeType.ALL], fetch = FetchType.LAZY, orphanRemoval = true, mappedBy = "folder")
    lateinit var documents: Set<Document>
}
