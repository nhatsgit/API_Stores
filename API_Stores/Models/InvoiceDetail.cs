using System;
using System.Collections.Generic;

namespace API_Stores.Models;

public partial class InvoiceDetail
{
    public int Id { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public int? InvoiceId { get; set; }

    public int? ProductId { get; set; }

    public virtual Invoice? Invoice { get; set; }

    public virtual Product? Product { get; set; }
}
