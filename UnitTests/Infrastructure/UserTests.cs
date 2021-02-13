using System.Linq;
using Business.QueryObjects;
using Business.Services;
using DAL;
using DAL.Entities;
using DAL.Enums;
using Infrastructure;
using Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests.Infrastructure
{
    public class UserTests
    {
        private static int counter = 0;
        private DbContextOptions<JobDbContext> GetInMemoryOptions() =>
            new DbContextOptionsBuilder<JobDbContext>()
                .UseInMemoryDatabase(databaseName: $"UserTest{++counter}")
                .Options;

        [Fact]
        public void UserQueryTest()
        {
            var unit = new UnitOfWork(GetInMemoryOptions());
            /**var context = new JobDbContext(GetInMemoryOptions());
            var companyQuery = new CompanyQuery(context);
            var userQuery = new UserQuery(context);**/
            var userService = new UserService(unit, new UserQueryObject(unit));
            Seeder.Seed(unit);
            userService.RegisterUserAsync("custom", "1234", Roles.JobSeeker);

            /**var u = unit.UserRepository.GetById(1);
            Assert.Equal("custom", u.Name);
            Assert.Equal(Roles.JobSeeker, u.Role);**/

            //unit.UserQuery.FilterByName("custom");
            //            var y = userQuery.ExecuteAsync().Result;
            //var x = userQuery.FilterByName("custom").ExecuteAsync().Result;
            //var a = unit.CompanyQuery.FilterByName("Tesla").Execute();
            var x = unit.UserQuery.FilterByName("custom").Execute();

            Assert.Equal(Roles.JobSeeker, x.First().Role);
        }
    }
}
