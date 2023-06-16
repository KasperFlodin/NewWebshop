namespace NewWebshopAPI.DTOs.UserDTOs
{
    public class RegisterUser
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
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {6} characters long", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
