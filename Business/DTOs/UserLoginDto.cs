using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class UserLoginDto
    {
        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        [StringLength(64)]
        public string Password { get; set; }
    }
}
