using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.DTOs;
using Business.Facades;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> EditCompany()
        {
            var user = await userFacade.GetByIdAsync(Int32.Parse(this.User.Identity.Name));
            var company = await companyFacade.GetByIdAsync(user.CompanyId.Value);
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCompany(CompanyDto company)
        {
            if (ModelState.IsValid)
            {
                await companyFacade.EditInfoAsync(company);
                return RedirectToAction("Index", "Home");
            }

            throw new ArgumentException();
        }
    }
}
