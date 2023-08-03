package project.backend

import org.junit.jupiter.api.Test
import org.springframework.boot.test.context.SpringBootTest
import org.springframework.test.context.ContextConfiguration
import org.springframework.test.context.TestPropertySource
import org.springframework.test.context.support.AnnotationConfigContextLoader

@TestPropertySource(properties = [
	"spring.jpa.hibernate.ddl-auto=none",
	"spring.datasource.url="
])
class BackendApplicationTests {

	@Test
	fun contextLoads() {

	}
	@Test
	fun test(){
		assert(true)
	}

}
