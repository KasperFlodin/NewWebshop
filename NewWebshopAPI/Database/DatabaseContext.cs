namespace NewWebshopAPI.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<Product> Product { get; set; }
        //public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Peter",
                    LastName = "Lund",
                    Phone = "12345678",
                    Address = "Meterskoven 1",
                    City = "Byen",
                    Zip = "4321",
                    Email = "Peter.lund@gmail.com",
                    Password = "123456",
                    //Role = Role.Admin,
                },
                new User
                {
                    Id = 2,
                    FirstName = "Simon",
                    LastName = "Green",
                    Phone = "11223344",
                    Address = "Skoven 2",
                    City = "Tarn",
                    Zip = "1144",
                    Email = "Simon.green@gmail.com",
                    Password = "123456",
                    /*Role = Role.User,*/
                });

            // Car products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "S200",
                    Price = 3000,
                    Type = "Bed",
                    Photolink = "https://aca8cd9d105dbd447097-f6f51e4cef559c9308eef9d726fd38a7.ssl.cf1.rackcdn.com/600262-2.jpg"
                },
                 new Product
                 {
                     Id = 2,
                     Name = "WoodTable",
                     Price = 1200,
                     Type = "Table",
                     Photolink = "https://livin.dk/wp-content/uploads/2020/07/spisebord-lakeret-eg-1.jpg"
                 },
                  new Product
                  {
                      Id = 3,
                      Name = "WoodChair",
                      Price = 299,
                      Type = "Chair",
                      Photolink = "https://cdn.shopify.com/s/files/1/0810/1821/products/Emma-bla-velour-stol-11706.jpg"
                  },
                   new Product
                   {
                       Id = 4,
                       Name = "PlasticChair",
                       Price = 99,
                       Type = "Chair",
                       Photolink = "https://cdn.shopify.com/s/files/1/0810/1821/products/Emma-bla-velour-stol-11706.jpg"
                   },
                    new Product
                    {
                        Id = 5,
                        Name = "Sleepr",
                        Price = 5000,
                        Type = "Bed",
                        Photolink = "https://aca8cd9d105dbd447097-f6f51e4cef559c9308eef9d726fd38a7.ssl.cf1.rackcdn.com/600262-2.jpg"
                    },
                     new Product
                     {
                         Id = 6,
                         Name = "z00",
                         Price = 600,
                         Type = "Fence",
                         Photolink = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_1lHnZy9bZidanyjBJr5JiIuSxNX1Y2LM0_HpV1TQrdwMLbUHY1kcfC12pxme6jzb9qw&usqp=CAU"
                     });
        }
    }
}
