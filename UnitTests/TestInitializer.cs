using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Entities;

namespace UnitTests
{
    public class TestInitializer
    {
        protected TestInitializer(DbContextOptions<JobDbContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Seed();
        }

        protected DbContextOptions<JobDbContext> ContextOptions { get; }

        private void Seed()
        {
            using (var unit = new UnitOfWork(ContextOptions))
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
        }
    }
}
