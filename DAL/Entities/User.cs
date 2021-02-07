using System.ComponentModel.DataAnnotations;
using DAL.Enums;

namespace DAL.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public Roles Role { get; set; } = Roles.None;
    }
}
