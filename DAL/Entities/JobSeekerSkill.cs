using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class JobSeekerSkill : BaseEntity
    {
        public int JobSeekerId { get; set; }

        [ForeignKey(nameof(JobSeekerId))]
        public virtual JobSeeker JobSeeker { get; set; }

        public int SkillId { get; set; }

        [ForeignKey(nameof(SkillId))]
        public virtual Skill Skill { get; set; }
    }
}
