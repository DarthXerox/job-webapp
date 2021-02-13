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


        [HttpPost("Register")]
        public async Task<string> RegisterAsync([FromBody] UserRegisterDto user)
        {
            if (user == null || (user.Role != Roles.Company && user.Role != Roles.JobSeeker))
            {
                return "-1";
            }

            var userId = await userFacade.RegisterUserAsync(user);
            return "{ \"CreatedUserId\": " + userId.ToString()
                                           + "\"Note\" : \"Continue with Add\" ";
        }
    }
}
