namespace Proje.WebUi.Models
{
    public class ProductCreateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
    }
}
