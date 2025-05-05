using System;
using System.Collections.Generic;

namespace API_Stores.Models;

public partial class Invoice
{
    public int Id { get; set; }

    public decimal TotalPrice { get; set; }

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public int? StoreId { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual Store? Store { get; set; }
}
