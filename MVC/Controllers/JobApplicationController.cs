using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Business.DTOs;
using Business.Facades;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Autofac;
using DAL.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Controllers
{
    public class JobApplicationController : BaseController
    {
        private readonly JobOfferFacade jobOfferFacade;
        private readonly JobApplicationFacade jobApplicationFacade;
        private readonly UserFacade userFacade;
        private readonly JobSeekerFacade jobSeekerFacade;
        private readonly CompanyFacade companyFacade;

        public JobApplicationController(JobOfferFacade jobOfferFacade, JobApplicationFacade jobApplicationFacade, UserFacade userFacade, JobSeekerFacade jobSeekerFacade, CompanyFacade companyFacade)
        {
            this.jobOfferFacade = jobOfferFacade;
            this.jobApplicationFacade = jobApplicationFacade;
            this.userFacade = userFacade;
            this.jobSeekerFacade = jobSeekerFacade;
            this.companyFacade = companyFacade;
        }

        [HttpGet]
        public async Task<IActionResult> AddJobApplication(int jobOfferId)
        {
            var jobOffer = await jobOfferFacade.GetByIdWithQuestionsAsync(jobOfferId);
            var user = await userFacade.GetByIdAsync(Int32.Parse(User.Identity.Name));
            var jobSeeker = await jobSeekerFacade.GetByIdAsync(user.JobSeekerId.Value);
            var model = new AddJobApplicationModel
            {
                JobOffer = jobOffer,
                JobApplication = new JobApplicationDto
                {
                    ApplicantId = jobSeeker.Id,
                    Applicant = jobSeeker,
                    JobOfferId = jobOffer.Id,
                    JobOffer = jobOffer,
                    Status = Status.Unresolved,
                    Answers = new JobApplicationAnswerDto[jobOffer.Questions.Count]
                }
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddJobApplication(AddJobApplicationModel addModel)
        {
            if (!ModelState.IsValid) throw new ArgumentException();
            var jobOffer = await jobOfferFacade.GetByIdAsync(addModel.JobApplication.JobOfferId ?? 0);
            var user = await userFacade.GetByIdAsync(Int32.Parse(User.Identity.Name));
            var jobSeeker = await jobSeekerFacade.GetByIdAsync(user.JobSeekerId.Value);
            var jobApplication = new JobApplicationDto
            {
                ApplicantId = jobSeeker.Id,
                JobOfferId = jobOffer.Id,
                Status = Status.Unresolved,
                Answers = addModel.JobApplication.Answers,
                Text = addModel.JobApplication.Text
            };
            await jobApplicationFacade.ApplyToJobOfferAsync(jobApplication);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var user = await userFacade.GetByIdAsync(Int32.Parse(User.Identity.Name));

            var model = user.Role == Roles.JobSeeker
                ? await jobApplicationFacade.GetByApplicantIdAsync(new JobApplicationDto { ApplicantId = user.JobSeekerId })
                : await jobApplicationFacade.GetByCompanyIdAsync(user.CompanyId ?? 0);

            return View(model);
        }

        public async Task<IActionResult> JobApplicationDetail(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var model = await jobApplicationFacade.GetByIdAsync(id.Value);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JobApplicationDetail(JobApplicationDto changeModel)
        {
            if (changeModel.Id == null)
            {
                return BadRequest();
            }
            var model = await jobApplicationFacade.GetByIdAsync(changeModel.Id.Value);
            await jobApplicationFacade.UpdateStatusAsync(model, changeModel.Status.Value);
            return View(model);
        }
    }
}
