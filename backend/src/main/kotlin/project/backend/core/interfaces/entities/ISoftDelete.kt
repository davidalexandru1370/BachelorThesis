package project.backend.core.interfaces.entities

import jakarta.persistence.Column
import java.sql.Date

abstract class ISoftDelete {
    @Column
    var isDeleted: Boolean = false

    @Column
    var deletedAt: Date? = null
}
