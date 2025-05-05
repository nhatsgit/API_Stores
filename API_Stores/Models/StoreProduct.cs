using System;
using System.Collections.Generic;

namespace API_Stores.Models;

public partial class StoreProduct
{
    public int Id { get; set; }

    public int? StoreId { get; set; }

    public int? ProductId { get; set; }

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Store? Store { get; set; }
}
