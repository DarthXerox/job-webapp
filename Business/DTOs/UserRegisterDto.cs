using System.ComponentModel.DataAnnotations;
using DAL.Entities;
using DAL.Enums;

namespace Business.DTOs
{
    public class UserRegisterDto : BaseDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Roles Role { get; set; }

        public int? JobSeekerId { get; set; }

        public int? CompanyId { get; set; }
    }
}
