using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;

namespace BL.Entities.Dto
{
    public class SkillDto
    {
        public string Tag { get; set; }

        public ICollection<JobSeekerSkill> JobSeekerSkills { get; set; }

        public ICollection<JobOfferSkill> JobOfferSkills { get; set; }
    }
}
