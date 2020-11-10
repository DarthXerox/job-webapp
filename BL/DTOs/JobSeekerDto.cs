using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;

namespace BL.Entities.Dto
{
    public class JobSeekerDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public ICollection<JobSeekerSkill> Skills { get; set; }
    }
}
