using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class JobOfferSkill : BaseEntity
    {
        public int JobOfferId { get; set; }

        [ForeignKey(nameof(JobOfferId))]
        public virtual JobOffer? JobOffer { get; set; }

        public int SkillId { get; set; }

        [ForeignKey(nameof(SkillId))]
        public virtual Skill? Skill { get; set; }
    }
}
