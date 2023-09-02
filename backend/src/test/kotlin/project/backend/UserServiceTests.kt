package project.backend

import org.junit.jupiter.api.Assertions
import org.junit.jupiter.api.Test
import org.mockito.Mockito
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc
import org.springframework.boot.test.context.SpringBootTest
import org.springframework.boot.test.mock.mockito.MockBean
import org.springframework.dao.EmptyResultDataAccessException
import org.springframework.test.context.TestPropertySource
import org.springframework.test.web.servlet.MockMvc
import project.backend.businessLogic.interfaces.IUserService
import project.backend.core.interfaces.IUserRepository
import project.backend.core.internalization.ErrorCodes
import project.backend.core.utils.AuthResult
import project.backend.core.utils.UserCredentials

@TestPropertySource(
    properties = [
        "spring.datasource.url=jdbc:h2:mem:testdb",
        "spring.datasource.driverClassName=org.h2.Driver",
        "spring.datasource.username=sa",
        "spring.datasource.password=password",
        "spring.jpa.database-platform=org.hibernate.dialect.H2Dialect",
    ],
)
@AutoConfigureMockMvc
@SpringBootTest
class UserServiceTests {

    @MockBean
    private lateinit var userRepository: IUserRepository

    @Autowired
    private lateinit var mockMvc: MockMvc

    @Autowired
    private lateinit var userService: IUserService

    @Test
    fun login_userDoesNotExist_ShouldReturnAuthResultWithError() {
        val email: String = "david@gmail.com"
        val password: String = "david"
        val userCredentials: UserCredentials = UserCredentials(email = email, password = password)
        Mockito.`when`(userRepository.findByEmail(email)).thenThrow(EmptyResultDataAccessException(0))

        val result: AuthResult = userService.login(userCredentials)

        Assertions.assertEquals("", result.token)
        Assertions.assertEquals(false, result.result)
        Assertions.assertEquals(ErrorCodes.EmailDoesNotExists.toString(), result.error)
    }
}
