package local.pursuit.api.mortgage.models

import com.fasterxml.jackson.annotation.JsonValue
import java.util.*

enum class AccountType { Savings, Checking, Mortgage;
    @JsonValue
    open fun toValue(): Int {
        return ordinal
    }
}

data class Account(val accountType: AccountType, val balance: Double, val userId: UUID);
