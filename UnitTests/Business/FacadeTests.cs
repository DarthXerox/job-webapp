using System;
using System.Linq;
using AutoMapper;
using Business;
using Business.DTOs;
using Business.Facades;
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
    public class FacadeTests
    {
        private static int counter = 0;
        private DbContextOptions<JobDbContext> GetInMemoryOptions() =>
            new DbContextOptionsBuilder<JobDbContext>()
                .UseInMemoryDatabase(databaseName: $"FacadeTest{++counter}")
                .Options;

        private IMapper mapper = new Mapper(new MapperConfiguration(GeneralMappingConfig.ConfigureMapping));

        [Fact]
        public void CompanyFacadeTest()
        {
            var unit = new UnitOfWork(GetInMemoryOptions());
            Seeder.Seed(unit);

            var userService = new UserService(unit, new UserQueryObject(unit));
            var compService = new CompanyService(unit, new CompanyQueryObject(unit));
            var companyFacade = new CompanyFacade(unit, mapper, compService, userService);

            // Null ID edit/update
            companyFacade.AddAsync(new CompanyDto() { Name = "Lol" }).Wait();
            //unit.SaveChanges();
            var lol = compService.ListCompaniesByNameAsync("Lol").Result.First();
            Assert.NotNull(lol);
            Assert.NotNull(lol.Id);
            lol.Id = null;
            lol.Name = "new lol";
            var excp2 = Assert.Throws<AggregateException>(() =>
                companyFacade.EditInfoAsync(mapper.Map<CompanyDto>(lol)).Wait());
            Assert.Contains("The property 'Id' on entity type 'Company' is part of a key " +
                            "and so cannot be modified or marked as modified.",
                excp2.Message);


            // Addition with conflicting ID
            // This invalidates the database
            var id1Company = unit.CompanyRepository.GetById(1);
            Assert.NotNull(id1Company); // makes sure company with id 1 is already in database
            var excp = Assert.Throws<AggregateException>(() =>
                companyFacade.AddAsync(new CompanyDto() { Id = 1 }).Wait());
            Assert.Contains("The instance of entity type 'Company' cannot be tracked " +
                            "because another instance with the same key value for {'Id'} is already being tracked.",
                excp.Message);

            unit.Dispose();
        }


        [Fact]
        public void JobSeekerFacadeTest()
        {
            var unit = new UnitOfWork(GetInMemoryOptions());
            Seeder.Seed(unit);

            var userService = new UserService(unit, new UserQueryObject(unit));
            var jobSeekerService = new JobSeekerService(unit);
            var jobSeekerFacade = new JobSeekerFacade(unit, mapper, jobSeekerService, userService);

            var s = new JobSeekerDto() { Name = "Lol" };
            jobSeekerFacade.RegisterAsync(s).Wait();
            Assert.NotNull(s.Id);
            Assert.NotNull(jobSeekerService.GetJobSeeker(s.Id ?? -1).Result);
            s.Id = null;
            s.Name = "new lol";
            // Null ID get
            var excp1 = Assert.Throws<AggregateException>(() =>
                jobSeekerFacade.GetInfoAsync(s).Wait());
            Assert.Contains("JobSeekerDto.Id can't be null!", excp1.Message);

            // Null ID edit/update
            var excp2 = Assert.Throws<AggregateException>(() =>
                jobSeekerFacade.EditInfoAsync(mapper.Map<JobSeekerDto>(s)).Wait());
            Assert.Contains("Attempted to update or delete an entity that does not exist in the store.",
                excp2.Message);

            // Addition with conflicting ID
            // This invalidates the database
            var id1 = unit.JobSeekerRepository.GetById(1);
            Assert.NotNull(id1); // makes sure company with id 1 is already in database
            var excp = Assert.Throws<AggregateException>(() =>
                jobSeekerFacade.RegisterAsync(new JobSeekerDto() { Id = 1 }).Wait());
            Assert.Contains("The instance of entity type 'JobSeeker' cannot be tracked " +
                            "because another instance with the same key value for {'Id'} is already being tracked.",
                excp.Message);

            unit.Dispose();
        }

        [Fact]
        public void JobOfferFacadeTest()
        {
            var unit = new UnitOfWork(GetInMemoryOptions());
            Seeder.Seed(unit);

            var jobOfferService = new JobOfferService(unit, new JobOfferQueryObject(unit));
            var jobOfferFacade = new JobOfferFacade(jobOfferService, mapper);

            var o = new JobOfferDto() { Name = "Lol" };
            jobOfferFacade.CreateAsync(o).Wait();
            var s = unit.JobOfferQuery.FilterByName("Lol").ExecuteAsync().Result.First();
            Assert.NotNull(s.Id);
            Assert.NotNull(jobOfferService.GetByIdAsync(s.Id ?? -1).Result);
            s.Id = null;
            s.Name = "new lol";
            // Null ID get IMPOSSIBLE due to GetById accepting only int

            // Null ID edit/update
            var excp2 = Assert.Throws<AggregateException>(() =>
                jobOfferFacade.Update(mapper.Map<JobOfferDto>(s)).Wait());
            Assert.Contains("The property 'Id' on entity type 'JobOffer' is part of a key " +
                            "and so cannot be modified or marked as modified.",
                excp2.Message);

            // Null ID delete
            excp2 = Assert.Throws<AggregateException>(() =>
                jobOfferFacade.Delete(mapper.Map<JobOfferDto>(s)).Wait());
            Assert.Contains("JobOfferDto.Id can't be null!",
                excp2.Message);

            // Addition with conflicting ID
            // This invalidates the database
            var id1 = unit.JobOfferRepository.GetById(1);
            Assert.NotNull(id1); // makes sure company with id 1 is already in database
            var excp = Assert.Throws<AggregateException>(() =>
                jobOfferFacade.CreateAsync(new JobOfferDto() { Id = 1 }).Wait());
            Assert.Contains("The instance of entity type 'JobOffer' cannot be tracked " +
                            "because another instance with the same key value for {'Id'} is already being tracked.",
                excp.Message);

            unit.Dispose();
        }


        [Fact]
        public void JobApplicationFacadeTest()
        {
            var unit = new UnitOfWork(GetInMemoryOptions());
            Seeder.Seed(unit);

            var jobApplicationService = new JobApplicationService(unit, new JobApplicationQueryObject(unit));
            var jobApplicationFacade = new JobApplicationFacade(unit, mapper,
                new JobApplicationService(unit, new JobApplicationQueryObject(unit)));

            var jason = unit.JobSeekerRepository.GetById(3);
            Assert.Equal("Jason", jason.Name);
            var application = jobApplicationService.GetByApplicantIdAsync(jason.Id ?? -1).Result.First();


            // Null ID get
            var excp1 = Assert.Throws<AggregateException>(() =>
                jobApplicationFacade.GetByApplicantIdAsync(new JobApplicationDto()).Wait());
            Assert.Contains("ApplicantId can't be null!",
                excp1.Message);

            excp1 = Assert.Throws<AggregateException>(() =>
                jobApplicationFacade.GetByApplicantIdAndStatusAsync(new JobApplicationDto(), Status.Rejected).Wait());
            Assert.Contains("ApplicantId can't be null!",
                excp1.Message);

            excp1 = Assert.Throws<AggregateException>(() =>
                jobApplicationFacade.GetByJobOfferIdAsync(new JobApplicationDto()).Wait());
            Assert.Contains("JobOfferId can't be null!",
                excp1.Message);

            excp1 = Assert.Throws<AggregateException>(() =>
                jobApplicationFacade.GetByJobOfferIdAndStatusAsync(new JobApplicationDto(), Status.Rejected).Wait());
            Assert.Contains("JobOfferId can't be null!",
                excp1.Message);

            // Null ID edit/update
            var excp2 = Assert.Throws<AggregateException>(() =>
                jobApplicationFacade.UpdateStatusAsync(mapper
                    .Map<JobApplicationDto>(new JobApplication()), Status.Rejected).Wait());
            Assert.Contains("JobApplicationId can't be null!",
                excp2.Message);

            // Null ID delete
            excp2 = Assert.Throws<AggregateException>(() =>
                jobApplicationFacade.DeleteAsync(mapper
                    .Map<JobApplicationDto>(new JobApplication())).Wait());
            Assert.Contains("JobApplicationId can't be null!",
                excp2.Message);

            // Addition with conflicting ID
            // This invalidates the database
            var id1 = unit.JobApplicationRepository.GetById(1);
            Assert.NotNull(id1); // makes sure company with id 1 is already in database
            var excp = Assert.Throws<AggregateException>(() =>
                jobApplicationFacade.ApplyToJobOfferAsync(new JobApplicationDto() { Id = 1 }).Wait());
            Assert.Contains("The instance of entity type 'JobApplication' cannot be tracked " +
                            "because another instance with the same key value for {'Id'} is already being tracked.",
                excp.Message);

            unit.Dispose();
        }
    }

}
