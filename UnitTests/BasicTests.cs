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
                JobSeeker person = JobSeeker.Create(1, "Neo", "Anderson", "neo@matrix.com");
                var cSharp = new Skill { Id = 11, Tag = "C#" };
                var seekerSkill = new JobSeekerSkill
                {
                    Id = 1,
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
                JobSeeker seeker = unit.JobSeekerRepository.GetById(1);
                Assert.NotNull(seeker); // verify reference
                Assert.Equal(1, seeker.Id);
                Assert.Equal("Neo", seeker.Name);
                Assert.Equal("Anderson", seeker.Surname);
                Assert.Equal("neo@matrix.com", seeker.Email);

                Skill skill = unit.SkillRepository.GetById(11);
                Assert.NotNull(skill);
                Assert.Equal(11, skill.Id);
                Assert.Equal("C#", skill.Tag);

                JobSeekerSkill jskill = unit.JobSeekerSkillRepository.GetById(1);
                Assert.NotNull(jskill);
                Assert.Equal(1, jskill.Id);
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
                JobSeeker person = JobSeeker.Create(1, "Neo", "Anderson", "neo@matrix.com");
                var cSharp = new Skill { Id = 11, Tag = "C#" };
                var seekerSkill = new JobSeekerSkill
                {
                    Id = 1,
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
                JobSeeker seeker = unit.JobSeekerRepository.GetById(1);
                Assert.NotNull(seeker); // verify reference
                Assert.Equal(1, seeker.Id);
                Assert.Equal("Neo", seeker.Name);
                Assert.Equal("Anderson", seeker.Surname);
                Assert.Equal("neo@matrix.com", seeker.Email);

                Skill skill = unit.SkillRepository.GetById(11);
                Assert.NotNull(skill);
                Assert.Equal(11, skill.Id);
                Assert.Equal("C#", skill.Tag);

                JobSeekerSkill jskill = unit.JobSeekerSkillRepository.GetById(1);
                Assert.NotNull(jskill);
                Assert.Equal(1, jskill.Id);
                Assert.Equal(seeker, jskill.JobSeeker);
                Assert.Equal(seeker.Id, jskill.JobSeekerId);
                Assert.Equal(skill, jskill.Skill);
                Assert.Equal(skill.Id, jskill.SkillId);
            }
        }

        [Fact]
        public void CheckDeletion()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                // Seed
                JobSeeker person = JobSeeker.Create(1, "Neo", "Anderson", "neo@matrix.com");
                var cSharp = new Skill { Id = 11, Tag = "C#" };
                var seekerSkill = new JobSeekerSkill
                {
                    Id = 1,
                    JobSeeker = person,
                    Skill = cSharp,
                    JobSeekerId = person.Id,
                    SkillId = cSharp.Id
                };
                person.Skills = new List<JobSeekerSkill> { seekerSkill };
                unit.SkillRepository.Add(cSharp);
                unit.SaveChanges();

                

            }
        }

    }
}
