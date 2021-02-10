using System.Collections.Generic;
using Newtonsoft.Json;

namespace Business.DTOs
{
    public class CompanyDto : BaseDto
    {
        public string? Name { get; set; }

        [JsonIgnore]
        public ICollection<JobOfferDto>? Offers { get; set; }

        [JsonIgnore]
        public int? UserId { get; set; }
    }
}
