using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Business.DTOs
{
    public class JobOfferQuestionDto : BaseDto
    {
        [StringLength(1024)]
        public string? Text { get; set; }

        [JsonIgnore]
        public int? JobOfferId { get; set; }

        [JsonIgnore]
        public virtual JobOfferDto? JobOffer { get; set; }
    }
}
