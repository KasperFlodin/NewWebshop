namespace NewWebshopAPI.Database.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string LastName { get; set; }

        [Column(TypeName = "nvarchar(8)")]
        public string Phone { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string Address { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string City { get; set; }

        [Column(TypeName = "nvarchar(4)")]
        public string Zip { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string Password { get; set; }

        //public Role Role { get; set; }
    }
}
