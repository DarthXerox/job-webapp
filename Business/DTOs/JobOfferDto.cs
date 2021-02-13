using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Business.DTOs
{
    public class JobOfferDto : BaseDto
    {
        [Required]
        [StringLength(64)]
        public string? Name { get; set; }

        [Required]
        [StringLength(64)]
        public string? City { get; set; }

        [JsonIgnore]
        public int? CompanyId { get; set; }

        public CompanyDto? Company { get; set; }

        [Required]
        [StringLength(1024)]
        public string? Description { get; set; }

        [Required]
        public IList<string>? RelevantSkills { get; set; }

        [Required]
        public IList<JobOfferQuestionDto>? Questions { get; set; }
    }
}
