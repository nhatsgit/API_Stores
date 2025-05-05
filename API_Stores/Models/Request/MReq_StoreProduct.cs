namespace API_Stores.Models.Request
{
    public class MReq_StoreProduct
    {
        public int? StoreId { get; set; }
        public int? ProductId { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
    }
}
