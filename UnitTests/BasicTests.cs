using Xunit;
using DAL;
using DAL.Entities;
using System.Collections.Generic;

namespace UnitTests
{
    public class BasicTests
    {
        /**
         * Just checks if the current user can login to database
         */
        [Fact]
        public void DatabaseAccess()
        {
            new UnitOfWork();
        }

        [Fact]
        public void Insert()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                // Seed
                JobSeeker person = new JobSeeker { Name = "Neo", Surname = "Anderson", Email = "neo@matrix.com"};
                var cSharp = new Skill { Tag = "C#" };
                var seekerSkill = new JobSeekerSkill
                {
                    JobSeeker = person,
                    Skill = cSharp,
                    JobSeekerId = person.Id,
                    SkillId = cSharp.Id
                };
                person.Skills = new List<JobSeekerSkill> { seekerSkill };

                unit.SkillRepository.Add(cSharp);
                unit.JobSeekerRepository.Add(person);
                unit.JobSeekerSkillRepository.Add(seekerSkill);
                
                unit.SaveChanges();

                // Testing
                JobSeeker seeker = unit.JobSeekerRepository.GetById(person.Id);
                Assert.NotNull(seeker); // verify reference
                Assert.Equal("Neo", seeker.Name);
                Assert.Equal("Anderson", seeker.Surname);
                Assert.Equal("neo@matrix.com", seeker.Email);

                Skill skill = unit.SkillRepository.GetById(cSharp.Id);
                Assert.NotNull(skill);
                Assert.Equal("C#", skill.Tag);

                JobSeekerSkill jskill = unit.JobSeekerSkillRepository.GetById(seekerSkill.Id);
                Assert.NotNull(jskill);
                Assert.Equal(seeker, jskill.JobSeeker);
                Assert.Equal(seeker.Id, jskill.JobSeekerId);
                Assert.Equal(skill, jskill.Skill);
                Assert.Equal(skill.Id, jskill.SkillId);
            }
        }

        [Fact]
        public void InsertAsync()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                // Seed
                JobSeeker person = new JobSeeker { Name = "Neo", Surname = "Anderson", Email = "neo@matrix.com" };
                var cSharp = new Skill { Tag = "C#" };
                var seekerSkill = new JobSeekerSkill
                {
                    JobSeeker = person,
                    Skill = cSharp,
                    JobSeekerId = person.Id,
                    SkillId = cSharp.Id
                };
                person.Skills = new List<JobSeekerSkill> { seekerSkill };

                unit.SkillRepository.AddAsync(cSharp);
                unit.JobSeekerRepository.AddAsync(person);
                unit.JobSeekerSkillRepository.AddAsync(seekerSkill);
                unit.SaveChanges();

                // Testing
                JobSeeker seeker = unit.JobSeekerRepository.GetById(person.Id);
                Assert.NotNull(seeker); // verify reference
                Assert.Equal("Neo", seeker.Name);
                Assert.Equal("Anderson", seeker.Surname);
                Assert.Equal("neo@matrix.com", seeker.Email);

                Skill skill = unit.SkillRepository.GetById(cSharp.Id);
                Assert.NotNull(skill);
                Assert.Equal("C#", skill.Tag);

                JobSeekerSkill jskill = unit.JobSeekerSkillRepository.GetById(seekerSkill.Id);
                Assert.NotNull(jskill);
                Assert.Equal(seeker, jskill.JobSeeker);
                Assert.Equal(seeker.Id, jskill.JobSeekerId);
                Assert.Equal(skill, jskill.Skill);
                Assert.Equal(skill.Id, jskill.SkillId);
            }
        }
    }
}
