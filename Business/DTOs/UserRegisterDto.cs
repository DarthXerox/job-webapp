using System.ComponentModel.DataAnnotations;
using DAL.Entities;
using DAL.Enums;

namespace Business.DTOs
{
    public class UserRegisterDto : BaseDto
    {
        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        [StringLength(64)]
        public string Password { get; set; }

        [Required]
        public Roles Role { get; set; }

        public int? JobSeekerId { get; set; }

        public int? CompanyId { get; set; }
    }
}
