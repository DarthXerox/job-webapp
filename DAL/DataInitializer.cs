using System.Collections.Generic;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public static class DataInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var cSharp = new Skill { Id = 1, Tag = "C#" };
            var python = new Skill { Id = 2, Tag = "Python" };

            var johnWick = new JobSeeker
            {
                Id = 1,
                Email = "uco@mail.muni.cz",
                Name = "John",
                Surname = "Wick"
            };

            johnWick.Skills = new List<JobSeekerSkill>
            {
                new JobSeekerSkill
                {
                    Id = 1,
                    JobSeeker = johnWick,
                    Skill = cSharp,
                    JobSeekerId = johnWick.Id,
                    SkillId = cSharp.Id
                },
                new JobSeekerSkill
                {
                    Id = 2,
                    JobSeeker = johnWick,
                    Skill = python,
                    JobSeekerId = johnWick.Id,
                    SkillId = python.Id
                }
            };

            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "Apple" }
            );
        }
    }
}
