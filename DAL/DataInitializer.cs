using System.Collections.Generic;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public static class DataInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobSeeker>().HasData(new JobSeeker
            {
                Id = 1,
                Email = "uco@mail.muni.cz",
                Name = "John",
                Surname = "Wick",
                Skills = new List<string> { "C#", "Python" }
            });

            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "Apple" }
            );
        }
    }
}
