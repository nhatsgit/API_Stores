using System;
using System.Collections.Generic;

namespace API_Stores.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Role { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public int? StoreId { get; set; }

    public virtual Store? Store { get; set; }
}
