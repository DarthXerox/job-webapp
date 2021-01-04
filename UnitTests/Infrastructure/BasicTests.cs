using System.Collections.Generic;
using System.Linq;
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
            JobSeeker person = new JobSeeker { Name = "Neo", Surname = "Anderson", Email = "neo@matrix.com", Skills = new List<string>{"C#"}};

            unit.JobSeekerRepository.Add(person);

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
            Assert.Equal("C#", seekers[0]?.Skills.First());
        }

        [Fact]
        public void DeleteTests()
        {
            using UnitOfWork unit = new UnitOfWork(inMemoryOptions);

            // Seed
            Seed(unit);

            var seekers = unit.JobSeekerRepository.GetAll().ToListAsync().Result;

            // Check for repo size
            Assert.Single(seekers);
            Assert.NotNull(seekers[0]);

            // Delete
            unit.JobSeekerRepository.Delete(seekers[0]);
            unit.SaveChanges();

            // Updated database
            seekers = unit.JobSeekerRepository.GetAll().ToListAsync().Result;
            Assert.Empty(seekers);
        }
    }
}
