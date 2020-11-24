using System.Collections.Generic;

namespace Business.DTOs
{
    public class JobOfferDto
    {
        public string Name { get; set; }

        public string City { get; set; }

        public int CompanyId { get; set; }

        public CompanyDto Company { get; set; }

        public string Description { get; set; }

        public ICollection<JobOfferSkillDto> RelevantSkills { get; set; }

        public ICollection<JobOfferQuestionDto> Questions { get; set; }
    }
}
