package project.backend

import org.junit.jupiter.api.Test
import org.springframework.test.context.TestPropertySource

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
