using System;
using System.Collections.Generic;
using System.Linq;
using BL.Entities.Dto;

namespace DAL.Entities.Dto
{
    public class JobOfferDto
    {
        public string Name { get; set; }

        public string City { get; set; }

        public int CompanyId { get; set; }

        public CompanyDto Company { get; set; }

        public string Description { get; set; }

        public ICollection<JobOfferSkill> RelevantSkills { get; set; }

        public ICollection<JobOfferQuestion> Questions { get; set; }
    }
}
