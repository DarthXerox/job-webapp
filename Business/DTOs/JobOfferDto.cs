using System.Collections.Generic;

namespace Business.DTOs
{
    public class JobOfferDto : BaseDTO
    {
        public string? Name { get; set; }

        public string? City { get; set; }

        public int? CompanyId { get; set; }

        public CompanyDto? Company { get; set; }

        public string? Description { get; set; }

        public IList<string>? RelevantSkills { get; set; }

        public IList<JobOfferQuestionDto>? Questions { get; set; }
    }
}
