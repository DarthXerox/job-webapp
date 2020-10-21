using System.Collections;
using System.Collections.Generic;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public static class DataInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var CSharp = new Skill() { Id = 1, Tag = "C#" };
            var Python = new Skill() { Id = 2, Tag = "Python" };

            var JohnWick = new JobSeeker()
            {
                Id = 1,
                Email = "uco@mail.muni.cz",
                Name = "John",
                Surname = "Wick"
            };

            var JohnWickSkill = new JobSeekerSkill() 
            { 
                Id = 1, 
                JobSeeker = JohnWick, 
                Skill = CSharp,
                JobSeekerId = JohnWick.Id, 
                SkillId = CSharp.Id
            };

            JohnWick.Abilities = new List<JobSeekerSkill>() { JohnWickSkill };

            modelBuilder.Entity<Company>().HasData(
               new Company { Id = 1, Name = "Apple" });
        }
    }
}
