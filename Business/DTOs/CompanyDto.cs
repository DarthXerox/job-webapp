using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Business.DTOs
{
    public class CompanyDto : BaseDto
    {
        [Required]
        [StringLength(64)]
        public string? Name { get; set; }

        [JsonIgnore]
        public ICollection<JobOfferDto>? Offers { get; set; }

        [JsonIgnore]
        public int? UserId { get; set; }
    }
}
