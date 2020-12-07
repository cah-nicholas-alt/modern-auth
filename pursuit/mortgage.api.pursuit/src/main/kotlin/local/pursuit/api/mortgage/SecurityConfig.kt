package local.pursuit.api.mortgage

import com.nimbusds.jose.proc.DefaultJOSEObjectTypeVerifier
import org.springframework.beans.factory.annotation.Value
import org.springframework.context.annotation.Bean
import org.springframework.security.config.annotation.method.configuration.EnableGlobalMethodSecurity
import org.springframework.security.config.annotation.web.builders.HttpSecurity
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter
import org.springframework.security.config.http.SessionCreationPolicy
import org.springframework.security.oauth2.core.DelegatingOAuth2TokenValidator
import org.springframework.security.oauth2.jwt.JwtDecoder
import org.springframework.security.oauth2.jwt.JwtDecoders
import org.springframework.security.oauth2.jwt.JwtValidators
import org.springframework.security.oauth2.jwt.NimbusJwtDecoder

@EnableGlobalMethodSecurity(prePostEnabled = true)
class SecurityConfig : WebSecurityConfigurerAdapter() {

    @Value("\${jwt.audience}")
    val audience: String = ""

    @Value("\${spring.security.oauth2.resourceserver.jwt.issuer-uri}")
    val issuer: String = ""

    override fun configure(http: HttpSecurity) {
        http.sessionManagement().sessionCreationPolicy(SessionCreationPolicy.STATELESS)
                .and().csrf { cust -> cust.disable() }
                .authorizeRequests()
                .antMatchers("/api/**")
                .permitAll()
                .and().oauth2ResourceServer().jwt()
    }

    @Bean
    fun jwtDecoder(): JwtDecoder {

        DefaultJOSEObjectTypeVerifier.JWT
        val jwtDecoder: NimbusJwtDecoder = JwtDecoders.fromOidcIssuerLocation(issuer) as NimbusJwtDecoder;


        val audienceValidator = AudienceValidator(audience)
        val withIssuer = JwtValidators.createDefaultWithIssuer(issuer);
        val withAudience = DelegatingOAuth2TokenValidator(audienceValidator, withIssuer);

        jwtDecoder.setJwtValidator(withAudience);

        return jwtDecoder;

    }
}