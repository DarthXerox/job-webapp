using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Skill : BaseEntity
    {
        [MaxLength(64)]
        public string? Tag { get; set; }

        public virtual ICollection<JobSeekerSkill>? JobSeekerSkills { get; set; }

        public virtual ICollection<JobOfferSkill>? JobOfferSkills { get; set; }
    }
}
