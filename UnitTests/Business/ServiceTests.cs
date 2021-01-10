using System.Collections.Generic;
using System.Linq;
using Business.QueryObjects;
using Business.Services;
using DAL;
using DAL.Entities;
using DAL.Enums;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests.Business
{
    /// <summary>
    /// These tests rely on functioning QueryObjects, test them first
    /// </summary>
    public class ServiceTests
    {
        private static int counter = 0;
        private DbContextOptions<JobDbContext> GetInMemoryOptions() =>
            new DbContextOptionsBuilder<JobDbContext>()
                .UseInMemoryDatabase(databaseName: $"ServiceTest{++counter}")
                .Options;

        [Fact]
        public void CompanyServiceTests()
        {
            // Addition
            var unit = new UnitOfWork(GetInMemoryOptions());
            var companyService = new CompanyService(unit, new CompanyQueryObject(unit));
            var c1 = new Company()
            {
                Name = "Lol"
            };
            Assert.Null(c1.Id);
            companyService.AddCompany(c1);
            unit.SaveChanges();
            Assert.NotEqual(-1, c1.Id);
            Assert.Equal("Lol", unit.CompanyRepository.GetById(c1.Id ?? -1).Name);

            Seeder.Seed(unit);

            var c2 = new Company()
            {
                Name = "New addition"
            };
            Assert.Null(c2.Id);
            companyService.AddCompany(c2);
            unit.SaveChanges();
            Assert.NotEqual(-1, c2.Id);
            Assert.Equal("New addition", unit.CompanyRepository.GetById(c2.Id ?? -1).Name);

            // Update
            c1.Name = "new Lol";
            companyService.UpdateCompany(c1);
            unit.SaveChanges();
            Assert.Equal("new Lol", unit.CompanyRepository.GetById(c1.Id ?? -1).Name);
            Assert.Equal("new Lol", unit.CompanyQuery
                .FilterByNameContains("Lol")
                .ExecuteAsync()
                .Result
                .First()
                .Name);


            var apple = unit.CompanyQuery.FilterByName("Apple").ExecuteAsync().Result.First();
            Assert.NotNull(apple);
            Assert.Equal("Apple", apple.Name);
            apple.Name = "Not Apple";
            companyService.UpdateCompany(apple);
            unit.SaveChanges();
            Assert.Equal("Not Apple", unit.CompanyRepository.GetById(apple.Id ?? -1).Name);
            Assert.Equal("Not Apple", unit.CompanyQuery
                .FilterByNameContains("Apple")
                .ExecuteAsync()
                .Result
                .First()
                .Name);

            // Listing
            Assert.Equal(new List<string>() { "Tesla" },
                companyService.ListCompaniesByNameAsync("Tesla").Result.Select(c => c.Name));

            Assert.Equal(new List<string>() { "new Lol", "Not Apple", "Tesla" },
                companyService.ListCompaniesByNameContainsAsync("l").Result.Select(c => c.Name));
        }


        [Fact]
        public void JobSeekerServiceTests()
        {
            // Addition
            var unit = new UnitOfWork(GetInMemoryOptions());
            var jobSeekerService = new JobSeekerService(unit);
            var s1 = new JobSeeker()
            {
                Name = "Joey",
                Surname = "Tribbiani"
            };
            Assert.Null(s1.Id);
            jobSeekerService.RegisterJobSeeker(s1);
            unit.SaveChanges();
            Assert.NotEqual(-1, s1.Id);
            Assert.Equal("Joey", unit.JobSeekerRepository.GetById(s1.Id ?? -1).Name);

            Seeder.Seed(unit);

            var s2 = new JobSeeker()
            {
                Name = "Chandler"
            };
            Assert.Null(s2.Id);
            jobSeekerService.RegisterJobSeeker(s2);
            unit.SaveChanges();
            Assert.NotEqual(-1, s2.Id);
            Assert.Equal("Chandler", unit.JobSeekerRepository.GetById(s2.Id ?? -1).Name);


            // Getter
            Assert.Equal(unit.JobSeekerRepository.GetById(1).Name, jobSeekerService.GetJobSeeker(1).Result.Name);
            Assert.Equal(unit.JobSeekerRepository.GetById(3).Name, jobSeekerService.GetJobSeeker(3).Result.Name);
            Assert.Equal(unit.JobSeekerRepository.GetById(2).Name, jobSeekerService.GetJobSeeker(2).Result.Name);
            Assert.Equal(unit.JobSeekerRepository.GetById(2).Surname, jobSeekerService.GetJobSeeker(2).Result.Surname);
            Assert.Equal(unit.JobSeekerRepository.GetById(2).Email, jobSeekerService.GetJobSeeker(2).Result.Email);


            // Update
            s1.Name = "Rachel";
            jobSeekerService.UpdateJobSeeker(s1);
            unit.SaveChanges();
            Assert.Equal("Rachel", unit.JobSeekerRepository.GetById(s1.Id ?? -1).Name);
            Assert.Equal("Tribbiani", unit.JobSeekerRepository.GetById(s1.Id ?? -1).Surname);

            var seeker = unit.JobSeekerRepository.GetById(3);
            Assert.NotNull(seeker);
            var seekerName = seeker.Name;
            var seekerSurname = seeker.Surname;
            seeker.Name = "Ross";
            jobSeekerService.UpdateJobSeeker(seeker);
            unit.SaveChanges();
            Assert.Equal("Ross", unit.JobSeekerRepository.GetById(seeker.Id ?? -1).Name);
            Assert.Equal(seekerSurname, unit.JobSeekerRepository.GetById(seeker.Id ?? -1).Surname);
        }

        /*
         * In the following two test cases I'm only testing functions from services,
         * that are not 1-line calls of QueryObjects, as they are tested in QueryObjectTests.cs
         */
        [Fact]
        public void JobOfferServiceTests()
        {
            // Addition
            var unit = new UnitOfWork(GetInMemoryOptions());
            var jobOfferService = new JobOfferService(unit, new JobOfferQueryObject(unit));
            var c1 = new JobOffer()
            {
                Name = "Lol"
            };
            Assert.Null(c1.Id);
            jobOfferService.CreateAsync(c1).Wait();
            unit.SaveChanges();
            Assert.NotEqual(-1, c1.Id);
            Assert.Equal("Lol", unit.JobOfferRepository.GetById(c1.Id ?? -1).Name);

            Seeder.Seed(unit);

            var c2 = new JobOffer()
            {
                Name = "New addition"
            };
            Assert.Null(c2.Id);
            jobOfferService.CreateAsync(c2).Wait();
            unit.SaveChanges();
            Assert.NotEqual(-1, c2.Id);
            Assert.Equal("New addition", unit.JobOfferRepository.GetById(c2.Id ?? -1).Name);

            // Getter
            Assert.Equal(unit.JobOfferRepository.GetById(1).Name, jobOfferService.GetByIdAsync(1).Result.Name);
            Assert.Equal(unit.JobOfferRepository.GetById(3).Name, jobOfferService.GetByIdAsync(3).Result.Name);
            Assert.Equal(unit.JobOfferRepository.GetById(2).Name, jobOfferService.GetByIdAsync(2).Result.Name);
            Assert.Equal(unit.JobOfferRepository.GetById(2).Description, jobOfferService.GetByIdAsync(2).Result.Description);
            Assert.Equal(unit.JobOfferRepository.GetById(2).CompanyId, jobOfferService.GetByIdAsync(2).Result.CompanyId);

            // Update
            c1.Name = "new Lol";
            jobOfferService.UpdateAsync(c1).Wait();
            unit.SaveChanges();
            Assert.Equal("new Lol", unit.JobOfferRepository.GetById(c1.Id ?? -1).Name);
            Assert.Equal("new Lol", unit.JobOfferQuery
                .FilterByNameContains("Lol")
                .ExecuteAsync()
                .Result
                .First()
                .Name);


            var appleOffer = unit.JobOfferQuery.FilterByNameContains("Apple")
                .OrderBy(o => o.Name)
                .ExecuteAsync()
                .Result
                .First();
            Assert.NotNull(appleOffer);
            Assert.Equal("Software development at Apple", appleOffer.Name);
            appleOffer.Name = "Not Apple";
            jobOfferService.UpdateAsync(appleOffer).Wait();
            unit.SaveChanges();
            Assert.Equal("Not Apple", unit.JobOfferRepository.GetById(appleOffer.Id ?? -1).Name);
            Assert.Equal(new List<string>() { "Not Apple", "Well-paid position at Apple" }, unit.JobOfferQuery
                .FilterByNameContains("Apple")
                .OrderBy(o => o.Name)
                .ExecuteAsync()
                .Result
                .Select(o => o.Name));

            // Delete
            int size = unit.JobOfferRepository.GetAll().Count();
            jobOfferService.DeleteAsync(c1.Id ?? -1).Wait();
            Assert.Equal(size - 1, unit.JobOfferRepository.GetAll().Count());
        }

        [Fact]
        public void JobApplicationServiceTests()
        {
            // Addition
            var unit = new UnitOfWork(GetInMemoryOptions());
            var jobApplicationService = new JobApplicationService(unit, new JobApplicationQueryObject(unit));
            var c1 = new JobApplication()
            {
                Text = "Lol",
                Status = Status.Unresolved
            };
            Assert.Null(c1.Id);
            jobApplicationService.Create(c1);
            unit.SaveChanges();
            Assert.NotEqual(-1, c1.Id);
            Assert.Equal("Lol", unit.JobApplicationRepository.GetById(c1.Id ?? -1).Text);

            Seeder.Seed(unit);

            var c2 = new JobApplication()
            {
                Text = "New addition"
            };
            Assert.Null(c2.Id);
            jobApplicationService.Create(c2);
            unit.SaveChanges();
            Assert.NotEqual(-1, c2.Id);
            Assert.Equal("New addition", unit.JobApplicationRepository.GetById(c2.Id ?? -1).Text);

            // UpdateStaus
            Assert.Equal(Status.Unresolved, unit.JobApplicationRepository.GetById(c1.Id ?? -1).Status);
            jobApplicationService.UpdateStatus(c1.Id ?? -1, Status.Accepted);
            unit.SaveChanges();
            Assert.Equal(Status.Accepted, unit.JobApplicationRepository.GetById(c1.Id ?? -1).Status);

            // Delete
            int size = unit.JobApplicationRepository.GetAll().Count();
            jobApplicationService.Delete(c1.Id ?? -1);
            unit.SaveChanges();
            Assert.Equal(size - 1, unit.JobApplicationRepository.GetAll().Count());
        }
    }
}
