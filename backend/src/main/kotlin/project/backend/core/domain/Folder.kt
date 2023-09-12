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
    private var userId: User,
    @OneToMany(cascade = [CascadeType.ALL], fetch = FetchType.LAZY)
    private var documents: List<Document>,
) : ISoftDelete() {
    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    private lateinit var id: UUID
}
