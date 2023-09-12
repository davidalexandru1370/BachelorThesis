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
class Folder(
    @Column
    private var createdAt: Date,
    @ManyToOne(fetch = FetchType.LAZY, cascade = [CascadeType.DETACH])
    private var user: User,
    @OneToMany(cascade = [CascadeType.ALL], fetch = FetchType.LAZY, orphanRemoval = true, mappedBy = "folder")
    private var documents: Set<Document>,
) : ISoftDelete() {
    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    private lateinit var id: UUID
}
