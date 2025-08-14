using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; } = "";

        [Required]
        [EmailAddress] // a validator for the email to make sure only an email is provided
        public string Email { get; set; } = "";

        [Required]
        [MinLength(4)]
        public string Password { get; set; } = "";
    }
}