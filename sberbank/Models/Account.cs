using System;
using System.Collections.Generic;

namespace sberbank.Models;

public partial class Account
{
    public int IdAccount { get; set; }

    public int IdClient { get; set; }

    public int IdDeposit { get; set; }

    public decimal DepositAmount { get; set; }

    public DateOnly OpeningDate { get; set; }

    public DateOnly ClosingDate { get; set; }

    public decimal Balance { get; set; }

    public int? Status { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual Deposit IdDepositNavigation { get; set; } = null!;

    public virtual ICollection<Operation> Operations { get; set; } = new List<Operation>();
}
