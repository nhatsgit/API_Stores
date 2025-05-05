namespace API_Stores.Models.Respone
{
    public class MRes_StoreProduct
    {
        public int Id { get; set; }
        public int? StoreId { get; set; }
        public string? StoreName { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
