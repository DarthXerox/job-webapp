using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class JobSeeker : BaseEntity
    {
        [Required]
        [MaxLength(64)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(64)]
        public string? Surname { get; set; }

        [Required]
        [MaxLength(64)]
        public string? Email { get; set; }

        public virtual ICollection<JobSeekerSkill>? Skills { get; set; }
    }
}
