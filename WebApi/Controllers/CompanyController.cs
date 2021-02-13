using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Business.DTOs;
using Business.Facades;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyFacade companyFacade;
        private readonly UserFacade userFacade;

        public CompanyController(CompanyFacade companyFacade, UserFacade userFacade)
        {
            this.companyFacade = companyFacade;
            this.userFacade = userFacade;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync()
            => await companyFacade.GetAllAsync();
    }
}
