using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mortgage.app.pursuit.Models
{
    public record HomeViewModel(List<Mortgage> Mortgages, MortgageApplication Application = null);
}
