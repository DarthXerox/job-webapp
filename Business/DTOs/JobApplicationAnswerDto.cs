using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class JobApplicationAnswerDto : BaseDto
    {
        [Required]
        [StringLength(1024)]
        public string? Text { get; set; }

        public int? QuestionId { get; set; }

        public JobOfferQuestionDto? Question { get; set; }

        public int? JobApplicationId { get; set; }

        public virtual JobApplicationDto? JobApplication { get; set; }
    }
}
