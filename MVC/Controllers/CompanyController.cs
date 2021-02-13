using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.DTOs;
using Business.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace MVC.Controllers
{
    public class CompanyController : Controller
    {
        private readonly CompanyFacade companyFacade;
        private readonly UserFacade userFacade;

        public CompanyController(CompanyFacade companyFacade, UserFacade userFacade)
        {
            this.companyFacade = companyFacade;
            this.userFacade = userFacade;
        }

        [HttpGet]
        public IActionResult AddCompany([FromQuery(Name = "userId")] int userId)
        {
            return View(new CompanyDto { UserId = userId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCompany(CompanyDto company)
        {
            if (ModelState.IsValid)
            {
                await companyFacade.AddAsync(company);
                return RedirectToAction("Login", "User");
            }

            throw new ArgumentException();
        }

        [HttpGet]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> EditCompany()
        {
            var user = await userFacade.GetByIdAsync(int.Parse(User.Identity.Name));
            var company = await companyFacade.GetByIdAsync(user.CompanyId.Value);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        [HttpPost]
        [Authorize(Roles = "Company")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCompany(CompanyDto company)
        {
            if (ModelState.IsValid && company.UserId == int.Parse(User.Identity.Name))
            {
                await companyFacade.EditInfoAsync(company);
                return RedirectToAction("Index", "Home");
            }

            throw new ArgumentException();
        }
    }
}
