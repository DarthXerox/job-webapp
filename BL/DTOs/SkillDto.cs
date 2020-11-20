using System.Collections.Generic;

namespace BL.Entities.Dto
{
    public class SkillDto
    {
        public string Tag { get; set; }

        public ICollection<JobSeekerSkillDto> JobSeekerSkills { get; set; }

        public ICollection<JobOfferSkillDto> JobOfferSkills { get; set; }
    }
}
