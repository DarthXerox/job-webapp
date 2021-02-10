using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.DTOs;
using Business.Facades;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobOfferListController : ControllerBase
    {
        private readonly JobOfferFacade jobOfferFacade;

        public JobOfferListController(JobOfferFacade jobOfferFacade)
        {
            this.jobOfferFacade = jobOfferFacade;
        }

        [HttpGet]
        [Route("paging")]
        public async Task<(int totalCount, IEnumerable<JobOfferDto> offers)> GetJobOffersAsync(int pgsz, int pgnum)
            => await jobOfferFacade.GetAllAsync(pgsz, pgnum);

        [HttpGet]
        [Route("all")]
        public async Task<(int totalCount, IEnumerable<JobOfferDto> offers)> GetAllJobOffersAsync()
            => await jobOfferFacade.GetAllWithoutPagingAsync();

        [HttpGet]
        [Route("byCompany")]
        //[Authorize(Roles = "User")]
        public async Task<IEnumerable<JobOfferDto>> GetAppleOffers(string name)
            => await jobOfferFacade.GetByCompanyNameAsync(new JobOfferDto() { Company = new CompanyDto() {Name = name}});
    }
}
