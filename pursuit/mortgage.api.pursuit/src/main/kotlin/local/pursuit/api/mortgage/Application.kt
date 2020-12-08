package local.pursuit.api.mortgage

import org.springframework.boot.autoconfigure.SpringBootApplication
import org.springframework.boot.runApplication
import org.springframework.context.annotation.Bean
import org.springframework.web.bind.annotation.GetMapping
import org.springframework.web.bind.annotation.RestController

@SpringBootApplication
class Application {
	@Bean
	fun mortgageRepo(): MortgageRepo {
		return MortgageRepo();
	}
}

fun main(args: Array<String>) {
	runApplication<Application>(*args)
}


@RestController
class PingController {
	@GetMapping("/api/ping")
	fun ping(): String {
		return "pong";
	}
}

