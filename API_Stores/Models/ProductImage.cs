using System;
using System.Collections.Generic;

namespace API_Stores.Models;

public partial class ProductImage
{
    public int Id { get; set; }

    public string? ImgUrl { get; set; }

    public int? ProductId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual Product? Product { get; set; }
}
