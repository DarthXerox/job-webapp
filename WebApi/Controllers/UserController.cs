using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using Business.DTOs;
using Business.Facades;
using DAL.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserFacade userFacade;

        public UserController(UserFacade userFacade)
        {
            this.userFacade = userFacade;
        }

        [HttpGet]
        [Route("ById")]
        public async Task<UserShowDto> GetUserByIdAsync([FromQuery(Name = "userId")] int id)
            => await userFacade.GetByIdAsync(id);
    }
}
