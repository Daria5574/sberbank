using System;
using System.Collections.Generic;

namespace sberbank.Models;

public partial class Client
{
    public int IdClient { get; set; }

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string? Sname { get; set; }

    public int? IsActive { get; set; }

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public string? House { get; set; }

    public string? Room { get; set; }

    public string? Passport { get; set; }

    public DateOnly? DateOfBirthday { get; set; }

    public string? Login { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Operation> Operations { get; set; } = new List<Operation>();
}
