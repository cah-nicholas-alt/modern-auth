using System;

namespace accounts.api.pursuit
{
    public record SendFundsRequest(Guid SourceAccount, Guid TargetAccount, decimal Amount);
}
