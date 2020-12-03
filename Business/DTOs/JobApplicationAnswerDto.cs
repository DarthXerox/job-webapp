namespace Business.DTOs
{
    public class JobApplicationAnswerDto
    {
        public string? Text { get; set; }

        public int? QuestionId { get; set; }

        public JobOfferQuestionDto? Question { get; set; }
    }
}
