using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class JobApplicationAnswer
    {
        [MaxLength(1024)]
        public string Text { get; set; }

        public int QuestionId { get; set; }

        [ForeignKey(nameof(JobOfferQuestion))]
        public virtual JobOfferQuestion Question { get; set; }
    }
}
