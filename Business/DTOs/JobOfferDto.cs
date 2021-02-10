using System.Collections.Generic;
using Newtonsoft.Json;

namespace Business.DTOs
{
    public class JobOfferDto : BaseDto
    {
        public string? Name { get; set; }

        public string? City { get; set; }

        [JsonIgnore]
        public int? CompanyId { get; set; }

        public CompanyDto? Company { get; set; }

        public string? Description { get; set; }

        public IList<string>? RelevantSkills { get; set; }

        public IList<JobOfferQuestionDto>? Questions { get; set; }
    }
}
