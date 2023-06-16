namespace NewWebshopAPI.DTOs.UserDTOs
{
    public class UserRequest
    {
        [Required]
        [StringLength(32, ErrorMessage = "First name cannot be longer than 32 characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "First name cannot be longer than 32 characters")]
        public string LastName { get; set; }

        [Required]
        [StringLength(12, ErrorMessage = "The number is not a valid phone number")]
        public string Phone { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Address cannot be longer than 32 characters")]
        public string Address { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "City name cannot be longer than 32 characters")]
        public string City { get; set; }

        [Required]
        [StringLength(4, ErrorMessage = "That is not a valid zip code")]
        public string Zip { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Invalid Email address")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "Error")]
        public string Password { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}
