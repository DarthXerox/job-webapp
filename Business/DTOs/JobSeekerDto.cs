using System.Collections.Generic;

namespace Business.DTOs
{
    public class JobSeekerDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public ICollection<JobSeekerSkillDto> Skills { get; set; }
    }
}
