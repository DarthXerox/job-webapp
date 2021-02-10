using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public int? JobSeekerId { get; set; }

        [ForeignKey(nameof(JobSeekerId))]
        public virtual JobSeeker? JobSeeker { get; set; }

        public int? CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public virtual Company? Company { get; set; }
    }
}
