using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Business.DTOs
{
    public class JobOfferDto : BaseDto
    {
        [StringLength(64)]
        public string? Name { get; set; }

        [StringLength(64)]
        public string? City { get; set; }

        [JsonIgnore]
        public int? CompanyId { get; set; }

        public CompanyDto? Company { get; set; }

        [StringLength(1024)]
        public string? Description { get; set; }

        public IList<string>? RelevantSkills { get; set; }

        public IList<JobOfferQuestionDto>? Questions { get; set; }
    }
}
