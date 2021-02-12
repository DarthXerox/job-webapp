using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Business.DTOs;
using Business.Facades;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace MVC.Controllers
{
    public class JobOfferController : BaseController
    {
        private readonly JobOfferFacade jobOfferFacade;
        private readonly CompanyFacade companyFacade;
        private readonly UserFacade userFacade;

        public JobOfferController(JobOfferFacade jobOfferFacade, CompanyFacade companyFacade, UserFacade userFacade)
        {
            this.jobOfferFacade = jobOfferFacade;
            this.companyFacade = companyFacade;
            this.userFacade = userFacade;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            (int totalCount, IEnumerable<JobOfferDto> offers) model;
            if (User.IsInRole("Company"))
            {
                var user = await userFacade.GetByIdAsync(int.Parse(User.Identity.Name));
                var offers = await jobOfferFacade.GetByCompanyIdAsync(user.CompanyId.Value);
                if (offers == null)
                {
                    model = (0, new List<JobOfferDto>());
                }
                else
                {
                    model = (offers.Count(), offers);
                }
                
            }
            else
            {
                model = await jobOfferFacade.GetAllAsync(PageSize, page);
            }

            var pagedModel = new PagedListViewModel<JobOfferDto>(
                new PaginationViewModel(page, model.totalCount, PageSize), model.offers);
            
            return View(pagedModel);
        }

        [HttpGet]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> AddJobOffer()
        {
            var user = await userFacade.GetByIdAsync(int.Parse(User.Identity.Name));
            var model = new JobOfferDto
            {
                CompanyId = user.CompanyId.Value,
                RelevantSkills = new List<string> { "" },
                Questions = new List<JobOfferQuestionDto> { new JobOfferQuestionDto { Text = "" } }
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Company")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddJobOffer(JobOfferDto jobOffer)
        {
            var user = await userFacade.GetByIdAsync(int.Parse(User.Identity.Name));
            if (ModelState.IsValid && user.CompanyId == jobOffer.CompanyId)
            {
                await jobOfferFacade.CreateAsync(jobOffer);
                return RedirectToAction(nameof(Index));
            }

            throw new ArgumentException();
        }

        [HttpPost]
        [Authorize(Roles = "Company")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewSkill(JobOfferDto jobOffer)
        {
            jobOffer.RelevantSkills.Add("");
            if (jobOffer.Id != null)
            {
                return View("EditJobOffer", jobOffer);
            }
            return View("AddJobOffer", jobOffer);
        }

        [HttpPost]
        [Authorize(Roles = "Company")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewQuestion(JobOfferDto jobOffer)
        {
            jobOffer.Questions.Add(new JobOfferQuestionDto{ Text = "" });
            if (jobOffer.Id != null)
            {
                return View("EditJobOffer", jobOffer);
            }
            return View("AddJobOffer", jobOffer);
        }

        [HttpGet]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> DeleteJobOffer(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            JobOfferDto offer = await jobOfferFacade.GetByIdAsync(id.Value);
            if (offer == null)
            {
                return NotFound();
            }
            var user = await userFacade.GetByIdAsync(int.Parse(User.Identity.Name));
            if (user.CompanyId != offer.CompanyId)
            {
                return Forbid();
            }
            return View(offer);
        }

        [HttpPost]
        [Authorize(Roles = "Company")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteJobOffer(JobOfferDto offer)
        {
            if (offer == null)
            {
                return BadRequest();
            }
            await jobOfferFacade.DeleteAsync(offer);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> JobOfferDetail(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            JobOfferDto offer = await jobOfferFacade.GetByIdWithQuestionsAsync(id.Value);
            if (offer == null)
            {
                return NotFound();
            }
            return View(offer);
        }

        [HttpGet]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> EditJobOffer(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var user = await userFacade.GetByIdAsync(int.Parse(User.Identity.Name));
            var jobOffer = await jobOfferFacade.GetByIdWithQuestionsAsync(id.Value);
            if (jobOffer == null)
            {
                return NotFound();
            }
            if (jobOffer.CompanyId != user.CompanyId)
            {
                return Forbid();
            }
            return View(jobOffer);
        }

        [HttpPost]
        [Authorize(Roles = "Company")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditJobOffer(JobOfferDto jobOffer)
        {
            var user = await userFacade.GetByIdAsync(int.Parse(User.Identity.Name));
            if (ModelState.IsValid && jobOffer.CompanyId == user.CompanyId)
            {
                await jobOfferFacade.UpdateAsync(jobOffer);
                return RedirectToAction(nameof(Index));
            }

            throw new ArgumentException();
        }
    }
}
