namespace NewWebshopAPI.Database.Entities
{
    public class OrderDetails
    {
        [Key] public int Id { get; set; }

        [ForeignKey("Order")]
        public int order_id { get; set; }

        [ForeignKey("Product")]
        public int product_id { get; set; }
    }
}
