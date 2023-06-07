using System.ComponentModel.DataAnnotations;

namespace NewWebshopAPI.DTOs.ProductDTOs
{
    public class ProductRequest
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 chars")]
        public string Name { get; set; }

        [Required]
        [Range(0, 50000, ErrorMessage = "Price cannot exceed then 50 thousand")]
        public int Price { get; set; } = 0;

        [Required]
        [StringLength(30, ErrorMessage = "Type cannot exceed 30 chars")]
        public string Type { get; set; }

        [Required]
        [StringLength(5000, ErrorMessage = "Link cannot exceed 5000 chars")]
        public string Photolink { get; set; }
    }
}
