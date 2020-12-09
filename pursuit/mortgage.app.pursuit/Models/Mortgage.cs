using System;

namespace mortgage.app.pursuit.Models
{
    public record Mortgage(Guid Id, string Address, Guid UserId, Guid AccountId, decimal Balance);
}