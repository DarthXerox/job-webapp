namespace Business.DTOs
{
    public class JobApplicationAnswerDto : BaseDTO
    {
        public string? Text { get; set; }

        public int? QuestionId { get; set; }

        public JobOfferQuestionDto? Question { get; set; }

        public int? JobApplicationId { get; set; }

        public virtual JobApplicationDto? JobApplication { get; set; }
    }
}
