package project.backend.core.domain

import jakarta.persistence.Column
import jakarta.persistence.Entity
import jakarta.persistence.Id
import lombok.Getter
import lombok.Setter
import project.backend.core.interfaces.entities.ISoftDelete
import java.util.*

@Entity
@Getter
@Setter
class Folder : ISoftDelete() {
    @Id
    private lateinit var id: UUID

    @Column
    private lateinit var createdAt: Date

    @Column
    lateinit var createdBy: String

}