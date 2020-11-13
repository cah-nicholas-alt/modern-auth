package local.pursuit.api.mortgage


import org.springframework.security.access.prepost.PreAuthorize
import org.springframework.web.bind.annotation.GetMapping
import org.springframework.web.bind.annotation.RequestMapping
import org.springframework.web.bind.annotation.RestController
import java.util.*


@PreAuthorize("isAuthenticated()")
@RestController()
@RequestMapping(path = ["api"] )
class MortgageController  {
    @GetMapping("/mortgages")
    fun getMortgages() : List<Mortgage> {
        return listOf(Mortgage(UUID.randomUUID(), "123 Main Street"))
    }
}

data class Mortgage(val id: UUID, val address: String);
