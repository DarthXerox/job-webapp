using System.ComponentModel.DataAnnotations;
using DAL.Enums;

namespace DAL.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(64)]
        public string Name;

        [Required]
        public string PasswordHash;

        public Roles Role = Roles.None;
    }
}
