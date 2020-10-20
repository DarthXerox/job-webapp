using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class JobSeeker : BaseEntity
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        [MaxLength(64)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(64)]
        public string Email { get; set; }

        public virtual ICollection<JobSeekerSkill> Abilities { get; set; }
    }
}
