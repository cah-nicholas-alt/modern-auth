package local.pursuit.api.mortgage


import local.pursuit.api.mortgage.models.Account
import local.pursuit.api.mortgage.models.AccountType
import local.pursuit.api.mortgage.models.Mortgage
import local.pursuit.api.mortgage.models.MortgageApplication
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.beans.factory.annotation.Value
import org.springframework.http.HttpEntity
import org.springframework.http.HttpHeaders
import org.springframework.http.MediaType
import org.springframework.http.client.ClientHttpRequestInterceptor
import org.springframework.http.converter.StringHttpMessageConverter
import org.springframework.security.access.prepost.PreAuthorize
import org.springframework.util.LinkedMultiValueMap
import org.springframework.web.bind.annotation.*
import org.springframework.web.client.RestTemplate
import java.security.Principal
import java.util.UUID


@RestController()
@RequestMapping("api")
@PreAuthorize("isAuthenticated()")
class MortgageController(@Autowired var mortgageRepo: MortgageRepo) {

    @Value("\${clientId}")
    val clientId: String = "";

    @Value("\${clientSecret}")
    val clientSecret: String = "";

    @Value("\${authorizeUrl}")
    var authorizeUrl: String = "";

    @GetMapping("/mortgages")
    fun getMortgages(principal: Principal) : List<Mortgage> {
        return mortgageRepo.mortgages.filter { m -> m.userId == getUUID(principal.name) };
    }

    @PostMapping("/mortgages/apply")
    fun applyMortgage(@RequestBody application: MortgageApplication, principal: Principal) {
        val token = getAccessToken();

        val newAccount = Account(AccountType.Mortgage, application.principal, getUUID(principal.name));
        val template = RestTemplate();
        template.messageConverters = template.messageConverters.filter { c -> c::class != StringHttpMessageConverter::class }
        template.interceptors.add(ClientHttpRequestInterceptor { httpRequest, bytes, clientHttpRequestExecution ->
            httpRequest.headers.add("Authorization", "Bearer $token"); clientHttpRequestExecution.execute(httpRequest, bytes);});

        val accountId = template.postForObject("http://accounts.api.pursuit.local:5001/api/accounts", newAccount, String::class.java);

        mortgageRepo.mortgages.add(Mortgage(UUID.randomUUID(), application.address, getUUID(principal.name), UUID.fromString(accountId)));
    }

    private fun getAccessToken(): String {
        val headers = HttpHeaders();
        headers.contentType = MediaType.APPLICATION_FORM_URLENCODED;
        val formValues = LinkedMultiValueMap(mapOf(
                "client_id" to listOf(clientId),
                "client_secret" to listOf(clientSecret),
                "grant_type" to listOf("client_credentials"),
                "scope" to listOf("PursuitAccountsApi.ReadAccounts PursuitAccountsApi.Admin")));

        val tokenRequest = HttpEntity(formValues, headers);
        val tokenResponse = RestTemplate().postForObject(authorizeUrl, tokenRequest, Map::class.java);
        return tokenResponse?.get("access_token").toString();
    }

    private fun getUUID(subject: String): UUID =
        UUID.fromString(subject.replaceFirst(
            "(\\w{8})(\\w{4})(\\w{4})(\\w{4})(\\w{12})".toRegex(),
            "$1-$2-$3-$4-$5"));

}
