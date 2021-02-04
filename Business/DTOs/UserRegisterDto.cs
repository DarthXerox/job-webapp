using System.ComponentModel.DataAnnotations;
using DAL.Enums;

namespace Bussiness.Dto
{
    public class UserRegisterDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Roles Role { get; set; }
    }
}
