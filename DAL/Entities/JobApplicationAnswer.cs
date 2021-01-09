using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class JobApplicationAnswer : BaseEntity
    {
        [MaxLength(1024)]
        public string? Text { get; set; }

        public int? QuestionId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public virtual JobOfferQuestion? Question { get; set; }

        public int? JobApplicationId { get; set; }

        [ForeignKey(nameof(JobApplicationId))]
        public virtual JobApplication? JobApplication { get; set; }
    }
}
