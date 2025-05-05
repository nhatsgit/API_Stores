namespace API_Stores.Models.Request
{
    public class MReq_Product
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? ImgUrl { get; set; }

        public string? Description { get; set; }

        public int CategoryId { get; set; }

    }
}
