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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Controllers
{
    public class JobOfferController : BaseController
    {
        private readonly JobOfferFacade jobOfferFacade;
        private readonly CompanyFacade companyFacade;

        public JobOfferController(JobOfferFacade jobOfferFacade, CompanyFacade companyFacade)
        {
            this.jobOfferFacade = jobOfferFacade;
            this.companyFacade = companyFacade;
        }

        public async Task<IActionResult> Index([FromQuery] int? page = 1, [FromQuery] string? skill = null)
        {
            var model = await jobOfferFacade.GetAllAsync(PageSize, page ?? 1, skill);

            var pagedModel = new PagedListViewModel<JobOfferDto>(
                new PaginationViewModel(page ?? 1, model.totalCount, PageSize), model.offers);

            return View(pagedModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddJobOffer()
        {
            var model = new AddJobOfferModel
            {
                JobOffer = new JobOfferDto
                {
                    RelevantSkills = new List<string> { "" },
                    Questions = new List<JobOfferQuestionDto> { new JobOfferQuestionDto { Text = "" } }
                },
                Companies = (List<CompanyDto>) await companyFacade.GetAllAsync()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddJobOffer(AddJobOfferModel addModel)
        {
            if (ModelState.IsValid)
            {
                await jobOfferFacade.CreateAsync(addModel.JobOffer);
                return RedirectToAction(nameof(Index));
            }

            throw new ArgumentException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewSkill(JobOfferDto jobOffer)
        {
            jobOffer.RelevantSkills.Add("");
            return View("AddJobOffer", new AddJobOfferModel
            {
                JobOffer = jobOffer,
                Companies = (List<CompanyDto>) await companyFacade.GetAllAsync()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewQuestion(JobOfferDto jobOffer)
        {
            jobOffer.Questions.Add(new JobOfferQuestionDto{ Text = "" });
            return View("AddJobOffer", new AddJobOfferModel
            {
                JobOffer = jobOffer,
                Companies = (List<CompanyDto>) await companyFacade.GetAllAsync()
            });
        }

        [HttpGet]
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
            return View(offer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteJobOffer(JobOfferDto offer)
        {
            if (offer == null)
            {
                return BadRequest();
            }
            await jobOfferFacade.Delete(offer);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> JobOfferDetail(int? id)
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
            return View(offer);
        }
    }
}
