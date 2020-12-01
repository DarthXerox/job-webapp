using System.Collections.Generic;
using DAL;
using DAL.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests.Infrastructure
{
    public class BasicTests
    {
        /// <summary>
        ///     Context changes are saved in memory(ram) instead of in database
        /// </summary>
        private readonly DbContextOptions<JobDbContext> inMemoryOptions = new DbContextOptionsBuilder<JobDbContext>()
            .UseInMemoryDatabase(databaseName: "Test")
            .Options;

        private static void Seed(UnitOfWork unit)
        {
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

            unit.SkillRepository.Add(cSharp);
            unit.JobSeekerRepository.Add(person);
            unit.JobSeekerSkillRepository.Add(seekerSkill);

            unit.SaveChanges();
        }

        /**
        * Just checks if the current user can login to database
        */
        [Fact]
        public void DatabaseAccess()
        {
            Assert.NotNull(new UnitOfWork(inMemoryOptions));
        }


        [Fact]
        public void Insert()
        {
            using UnitOfWork unit = new UnitOfWork(inMemoryOptions);

            // Seed
            Seed(unit);

            var seekers = unit.JobSeekerRepository.GetAll().ToListAsync().Result;
            Assert.Single(seekers);
            Assert.NotNull(seekers[0]); // verify reference
            Assert.Equal("Neo", seekers[0].Name);
            Assert.Equal("Anderson", seekers[0].Surname);
            Assert.Equal("neo@matrix.com", seekers[0].Email);

            var skills = unit.SkillRepository.GetAll().ToListAsync().Result;
            Assert.Single(skills);
            Assert.NotNull(skills[0]);
            Assert.Equal("C#", skills[0].Tag);

            var jskills = unit.JobSeekerSkillRepository.GetAll().ToListAsync().Result;
            Assert.Single(skills);
            Assert.NotNull(jskills[0]);
            Assert.Equal(seekers[0], jskills[0].JobSeeker);
            Assert.Equal(seekers[0].Id, jskills[0].JobSeekerId);
            Assert.Equal(skills[0], jskills[0].Skill);
            Assert.Equal(skills[0].Id, jskills[0].SkillId);
        }

        [Fact]
        public void DeleteTests()
        {
            using UnitOfWork unit = new UnitOfWork(inMemoryOptions);

            // Seed
            Seed(unit);

            var seekers = unit.JobSeekerRepository.GetAll().ToListAsync().Result;
            var skills = unit.SkillRepository.GetAll().ToListAsync().Result;
            var jskills = unit.JobSeekerSkillRepository.GetAll().ToListAsync().Result;

            // Check for repo size
            Assert.Single(jskills);
            Assert.NotNull(jskills[0]);
            Assert.Single(skills);
            Assert.NotNull(skills[0]);
            Assert.Single(seekers);
            Assert.NotNull(seekers[0]);

            // Delete
            unit.JobSeekerSkillRepository.Delete(jskills[0].Id);
            unit.SkillRepository.Delete(skills[0].Id);
            unit.JobSeekerRepository.Delete(seekers[0].Id);
            unit.SaveChanges();

            // Updated database
            jskills = unit.JobSeekerSkillRepository.GetAll().ToListAsync().Result;
            Assert.Empty(jskills);
            skills = unit.SkillRepository.GetAll().ToListAsync().Result;
            Assert.Empty(skills);
            seekers = unit.JobSeekerRepository.GetAll().ToListAsync().Result;
            Assert.Empty(seekers);
        }
    }
}
