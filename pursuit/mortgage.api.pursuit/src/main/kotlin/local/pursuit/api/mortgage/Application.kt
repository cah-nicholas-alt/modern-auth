package local.pursuit.api.mortgage

import org.springframework.boot.autoconfigure.SpringBootApplication
import org.springframework.boot.runApplication
import org.springframework.stereotype.Controller
import org.springframework.web.bind.annotation.GetMapping
import org.springframework.web.bind.annotation.RequestMapping
import org.springframework.web.bind.annotation.RestController
import java.security.Principal

@SpringBootApplication
class Application

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