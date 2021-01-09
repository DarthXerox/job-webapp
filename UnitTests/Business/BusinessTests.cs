using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.QueryObjects;
using DAL;
using DAL.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests.Business
{
    /// <summary>
    /// These tests are working with 20210107002834_SkillChangedToString.cs migration
    ///
    /// !!! It is necessary to make sure infrastructure is tested !!!
    ///
    /// Check if queries work right - filter correctly and in correct order
    /// look for null id?
    /// </summary>
    public class BusinessTests
    {
        private readonly DbContextOptions<JobDbContext> inMemoryOptions = new DbContextOptionsBuilder<JobDbContext>()
            .UseInMemoryDatabase(databaseName: "Test")
            .Options;

        [Fact]
        public void QueryObjectsCorrect()
        {
            // Companies
            var unitOfWork = new UnitOfWork(inMemoryOptions);
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
            var unit = new UnitOfWork(inMemoryOptions);
            Seeder.Seed(unit);

            unit.JobOfferQuery.FilterByCompanyName("Apple"); // Apple should have 2 applications
            var offers = unit.JobOfferQuery.ExecuteAsync().Result;
            Assert.Equal(2, offers.Count());
            var offer = offers.First();
            Assert.NotNull(offer);
            Assert.Equal("Well-paid position at Apple", offer.Name);

            var queryObject = new JobApplicationQueryObject(unit);
            Assert.Equal(2, queryObject.GetByJobOfferIdAsync(offer.Id ?? -1)
                    .Result
                    .Count());
            Assert.Equal(new List<string>() {"Jason", "Neo"},
                queryObject.GetByJobOfferIdAsync(offer.Id ?? -1)
                    .Result
                    .Select(a => a.Applicant?.Name));
            unit.Dispose();

        }
    }
}
