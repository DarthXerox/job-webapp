namespace Business.DTOs
{
    public class JobOfferQuestionDto : BaseDTO
    {
        public string? Text { get; set; }

        public int? JobOfferId { get; set; }

        public virtual JobOfferDto? JobOffer { get; set; }
    }
}
