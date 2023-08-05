package project.backend.domain.dao

import jakarta.persistence.*
import jakarta.validation.constraints.Email
import org.hibernate.validator.constraints.Length
import org.jetbrains.annotations.NotNull
import org.springframework.data.redis.connection.RedisConfiguration.WithPassword
import java.util.*

@Entity
@Table(name = "Users")
class User() {

    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    lateinit var id : UUID

    @Column(unique = true)
    @NotNull
    @Email
    var email: String = ""

    @Column
    @Length(min = 5, max = 512)
    @NotNull
    var password: String = ""

    constructor(email: String, password: String) : this() {
        this.email = email
        this.password = password
    }
}