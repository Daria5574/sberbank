using System;
using System.Collections.Generic;

namespace sberbank.Models;

public partial class Operation
{
    public int IdOperation { get; set; }

    public int IdAccount { get; set; }

    public int IdClient { get; set; }

    public string Type { get; set; } = null!;

    public DateTime? Date { get; set; }

    public decimal Sum { get; set; }

    public string? Description { get; set; }

    public virtual Account IdAccountNavigation { get; set; } = null!;

    public virtual Client IdClientNavigation { get; set; } = null!;
}
