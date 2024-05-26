using System;
using System.Collections.Generic;

namespace sberbank.Models;

public partial class Deposit
{
    public int IdDeposit { get; set; }

    public string Name { get; set; } = null!;

    public string Currency { get; set; } = null!;

    public decimal MinimumDeposit { get; set; }

    public int DepositTerm { get; set; }

    public decimal InterestRate { get; set; }

    public int InterestPeriod { get; set; }

    public int PossibilityOfRemoval { get; set; }

    public int IsActivity { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
