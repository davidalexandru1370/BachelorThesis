package project.backend.core.domain

import jakarta.persistence.*
import lombok.EqualsAndHashCode
import lombok.Getter
import lombok.NoArgsConstructor
import lombok.Setter
import project.backend.core.interfaces.entities.ISoftDelete
import java.util.*

@Entity
@Getter
@Setter
@NoArgsConstructor
@EqualsAndHashCode
data class Folder(
    @Column
    private var createdAt: Date,
    @Column
    private var createdBy: String,
    @OneToMany(cascade = [CascadeType.ALL], fetch = FetchType.LAZY)
    private var documents: List<Document>
) : ISoftDelete() {
    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    private lateinit var id: UUID
}
