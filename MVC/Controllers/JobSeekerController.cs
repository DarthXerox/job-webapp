using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.DTOs;
using Business.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MVC.Models;

namespace MVC.Controllers
{
    public class JobSeekerController : Controller
    {
        private readonly JobSeekerFacade jobSeekerFacade;
        private readonly UserFacade userFacade;

        public JobSeekerController(JobSeekerFacade jobSeekerFacade, UserFacade userFacade)
        {
            this.jobSeekerFacade = jobSeekerFacade;
            this.userFacade = userFacade;
        }

        [HttpGet]
        public IActionResult AddJobSeeker([FromQuery(Name = "userId")] int userId)
        {
            return View(new JobSeekerDto{ UserId = userId, Skills = new List<string> { "" } });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddJobSeeker(JobSeekerDto jobSeeker)
        {
            if (ModelState.IsValid)
            {
                await jobSeekerFacade.RegisterAsync(jobSeeker);
                return RedirectToAction("Login", "User");
            }

            throw new ArgumentException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewSkill(JobSeekerDto jobSeeker)
        {
            jobSeeker.Skills.Add("");
            return View("AddJobSeeker", jobSeeker);
        }

        [HttpGet]
        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> EditJobSeeker()
        {
            var user = await userFacade.GetByIdAsync(int.Parse(User.Identity.Name));
            var jobSeeker = await jobSeekerFacade.GetInfoAsync(new JobSeekerDto {Id = user.JobSeekerId});
            if (jobSeeker == null)
            {
                return NotFound();
            }

            return View(jobSeeker);
        }

        [HttpPost]
        [Authorize(Roles = "JobSeeker")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditJobSeeker(JobSeekerDto jobSeeker)
        {
            if (ModelState.IsValid && jobSeeker.UserId == int.Parse(User.Identity.Name))
            {
                await jobSeekerFacade.EditInfoAsync(jobSeeker);
                return RedirectToAction("Index", "Home");
            }

            throw new ArgumentException();
        }
    }
}
