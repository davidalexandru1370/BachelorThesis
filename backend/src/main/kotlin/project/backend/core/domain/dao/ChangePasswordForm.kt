package project.backend.core.domain.dao

import jakarta.persistence.*
import java.sql.Date
import java.util.UUID

@Entity
@Table
class ChangePasswordForm {
    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    lateinit var id: UUID

    @ManyToOne
    private lateinit var user: User

    @Column
    lateinit var createdAt : Date

    @Column
    lateinit var expireAt: Date

}