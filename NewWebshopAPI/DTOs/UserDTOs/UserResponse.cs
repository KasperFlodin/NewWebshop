﻿namespace NewWebshopAPI.DTOs.UserDTOs
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
