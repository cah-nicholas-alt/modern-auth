package local.pursuit.api.mortgage.models

import java.util.*

data class Mortgage(val id: UUID, val address: String, val userId: UUID, val accountId: UUID);