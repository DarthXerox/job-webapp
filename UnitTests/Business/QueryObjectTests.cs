using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using Business.QueryObjects;
using DAL;
using DAL.Entities;
using DAL.Enums;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests.Business
{
    /// <summary>
    /// These tests are working with 20210109172253_RelationshipFix.cs migration
    /// </summary>
    public class QueryObjectTests
    {
        private readonly DbContextOptions<JobDbContext> inMemoryOptions = new DbContextOptionsBuilder<JobDbContext>()
            .UseInMemoryDatabase(databaseName: "Test")
            .Options;

        private static int counter = 0;
        private DbContextOptions<JobDbContext> GetInMemoryOptions() =>
            new DbContextOptionsBuilder<JobDbContext>()
                .UseInMemoryDatabase(databaseName: $"Test{++counter}")
                .Options;

        [Fact]
        public void CompanyQueryObjectTest()
        {
            // Companies
            var unitOfWork = new UnitOfWork(GetInMemoryOptions());
            Seeder.Seed(unitOfWork);
            var x = unitOfWork.CompanyRepository.GetAll();
            Assert.Equal(4, unitOfWork.CompanyRepository.GetAll().Count());

            var companiesQueryObject = new CompanyQueryObject(unitOfWork);
            Assert.Equal(new List<string>() { "Apple" },
                companiesQueryObject.GetByNameAsync("Apple")
                    .Result
                    .Select(c => c.Name));
            Assert.Equal(new List<string>() { "Tesla" },
                companiesQueryObject.GetByNameAsync("Tesla")
                    .Result
                    .Select(c => c.Name));

            Assert.Equal(new List<string>() { "Apple", "Tesla" },
                companiesQueryObject.GetByNameContainsAsync("l")
                    .Result
                    .Select(c => c.Name));

            Assert.Equal(new List<string>() {  "Tesla", "Apple" },
                companiesQueryObject.GetByNameContainsAsync("l", false)
                    .Result
                    .Select(c => c.Name));
            unitOfWork.Dispose();
        }

        [Fact]
        public void JobApplicationQueryObjectTest()
        {
            var unit = new UnitOfWork(GetInMemoryOptions());
            Seeder.Seed(unit);

            unit.JobOfferQuery.FilterByCompanyName("Apple").OrderBy(o => o.Name, false);
            var offers = unit.JobOfferQuery.ExecuteAsync().Result;
            Assert.Equal(2, offers.Count()); // Apple should have 1 offer
            var offer = offers.First();
            Assert.NotNull(offer);
            Assert.Equal("Well-paid position at Apple", offer.Name);

            var queryObject = new JobApplicationQueryObject(unit);
            Assert.Equal(2, queryObject.GetByJobOfferIdAsync(offer.Id ?? -1)
                    .Result
                    .Count());
            Assert.Equal(new List<string>() { "Jason", "Neo" },
                queryObject.GetByJobOfferIdAsync(offer.Id ?? -1)
                    .Result
                    .Select(a => a.Applicant?.Name));
            Assert.Equal(new List<string>() { "Neo" },
                queryObject.GetByJobOfferIdAndStatusAsync(offer.Id ?? -1, Status.Rejected)
                    .Result
                    .Select(a => a.Applicant?.Name));

            int neoId = queryObject.GetByJobOfferIdAndStatusAsync(offer.Id ?? -1, Status.Rejected)
                .Result
                .Select(a => a.Applicant?.Id)
                .First() ?? -1;

            Assert.Equal(new List<string>() {"Microsoft > Apple", "Blue pill, red pill, I'll eat both"},
                queryObject.GetByApplicantIdAsync(neoId)
                    .Result
                    .Select(a => a.Text));

            Assert.Equal(new List<string>() {"Blue pill, red pill, I'll eat both"},
                queryObject.GetByApplicantIdAndStatusAsync(neoId, Status.Rejected)
                                .Result
                                .Select(a => a.Text));

            unit.Dispose();
        }

        [Fact]
        public void JobOfferQueryObjectTest()
        {
            var unit = new UnitOfWork(GetInMemoryOptions());
            Seeder.Seed(unit);

            unit.CompanyQuery.FilterByName("Apple");
            var apple = unit.CompanyQuery.ExecuteAsync().Result.First();

            var queryObject = new JobOfferQueryObject(unit);
            Assert.Equal(new List<string>()
                {
                    "Cybertruck driving software development at Tesla",
                    "Software development at Apple"
                },
                queryObject.GetByNameContainsAsync("oftware")
                    .Result
                    .Select(o => o.Name));

            Assert.Equal(2, queryObject.GetByCompanyNameAsync("Apple")
                .Result
                .Count());

            Assert.Equal(new List<string>()
                {
                    "Developing new .NET Core SDK",
                    "Well-paid position at Apple"
                },
                queryObject.GetBySkillTagAsync("C#")
                        .Result
                        .Select(o => o.Name));

            Assert.Equal(new List<string>()
                {
                    "Developing new .NET Core SDK",
                    "Pouring coffee and writing names on cups"
                },
                queryObject.GetByCityAsync("Seattle")
                    .Result
                    .Select(o => o.Name));
            unit.Dispose();
        }
    }
}
