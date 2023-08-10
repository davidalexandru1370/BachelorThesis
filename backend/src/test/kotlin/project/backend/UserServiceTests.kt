package project.backend

import org.junit.jupiter.api.Test
import org.junit.jupiter.api.Assertions
import org.mockito.InjectMocks
import org.mockito.Mockito
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.boot.test.context.SpringBootTest
import org.springframework.boot.test.mock.mockito.MockBean
import org.springframework.dao.EmptyResultDataAccessException
import org.springframework.test.context.TestPropertySource
import project.backend.domain.AuthResult
import project.backend.domain.UserCredentials
import project.backend.repositories.IUserRepository
import project.backend.services.UserService
import project.backend.services.interfaces.IUserService

@TestPropertySource(
    properties = [
        "spring.jpa.hibernate.ddl-auto=none",
        "spring.datasource.url="
    ]
)
@SpringBootTest
class UserServiceTests {

    @MockBean
    private lateinit var userRepository: IUserRepository

    @Autowired
    private lateinit var userService: IUserService
    @Test
    fun login_userDoesNotExist_ShouldReturnAuthResultWithError() {
        val email: String = "david@gmail.com"
        val password: String = "david";
        val userCredentials: UserCredentials = UserCredentials(email = email, password = password)
        Mockito.`when`(userRepository.findByEmail(email)).thenThrow(EmptyResultDataAccessException(0))

        val result: AuthResult = userService.login(userCredentials)

        Assertions.assertEquals("", result.token);
        Assertions.assertEquals(false, result.result)
        Assertions.assertEquals("", result.error)
    }
}