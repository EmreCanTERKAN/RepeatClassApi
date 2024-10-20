using System.ComponentModel.DataAnnotations;

namespace RepeatClassApi.Models
{
    public class RegisterRequest
    {
        [EmailAddress]
        [Required]
        [MinLength(10)]
        public string Email { get; set; }

        [Required]
        [Length(8,12)]
        public string Password { get; set; }
    }
}
