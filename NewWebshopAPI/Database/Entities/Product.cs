using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NewWebshopAPI.Database.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "int")]
        public int Price { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string Type { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Photolink { get; set; }
    }
}
