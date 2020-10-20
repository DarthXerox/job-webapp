using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class JobOffer : BaseEntity
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [MaxLength(64)]
        public string City { get; set; }

        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public virtual Company Company { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        public virtual ICollection<Skill> RelevantSkills { get; set; }

        public virtual ICollection<JobOfferQuestion> Questions { get; set; }
    }
}
