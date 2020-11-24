namespace Business.DTOs
{
    public class JobSeekerSkillDto
    {
        public int JobSeekerId { get; set; }

        public JobSeekerDto JobSeeker { get; set; }

        public int SkillId { get; set; }

        public SkillDto Skill { get; set; }
    }
}
