package project.backend.domain.dao

import jakarta.persistence.*
import jakarta.validation.constraints.Email
import org.hibernate.validator.constraints.Length
import java.util.*

@Entity
@Table(name = "Users")
class User {

    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    lateinit var id : UUID

    @Column(unique = true)
    @Email
    var email: String = ""

    @Column
    @Length(min = 5)
    var password: String = ""
}