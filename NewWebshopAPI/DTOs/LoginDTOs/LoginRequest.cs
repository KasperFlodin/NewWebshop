namespace NewWebshopAPI.DTOs.LoginDTOs
{
    public class LoginRequest
    {
        [Required]
        [StringLength(100, ErrorMessage = "Invalid Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Password must be less than 100 characters")]
        public string Password { get; set; }
    }
}
