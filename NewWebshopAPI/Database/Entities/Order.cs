namespace NewWebshopAPI.Database.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int user_id { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
    }
}
