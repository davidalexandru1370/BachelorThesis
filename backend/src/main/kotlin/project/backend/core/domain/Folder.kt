package project.backend.core.domain

import jakarta.persistence.*
import lombok.Getter
import lombok.NoArgsConstructor
import lombok.Setter
import project.backend.core.interfaces.entities.ISoftDelete
import java.util.*

@Entity
@Getter
@Setter
@NoArgsConstructor
class Folder : ISoftDelete() {
    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    private lateinit var id: UUID

    @Column
    private lateinit var createdAt: Date

    @ManyToOne(fetch = FetchType.LAZY, cascade = [CascadeType.DETACH])
    private lateinit var user: User

    @OneToMany(cascade = [CascadeType.ALL], fetch = FetchType.LAZY, orphanRemoval = true, mappedBy = "folder")
    private lateinit var documents: Set<Document>
}
