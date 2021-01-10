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

namespace MVC.Controllers
{
    public class JobOfferController : BaseController
    {
        private readonly JobOfferFacade jobOfferFacade; 

        public JobOfferController(JobOfferFacade jobOfferFacade)
        {
            this.jobOfferFacade = jobOfferFacade;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var model = await jobOfferFacade.GetAllAsync(PageSize, page);

            var pagedModel = new PagedListViewModel<JobOfferDto>(
                new PaginationViewModel(page, model.totalCount, PageSize), model.offers);
            
            return View(pagedModel);
        }

        [HttpGet]
        public IActionResult AddJobOffer()
        {
            return View(new JobOfferDto { RelevantSkills = new List<string>{ "" } });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddJobOffer(JobOfferDto jobOffer)
        {
            if (ModelState.IsValid)
            {
                await jobOfferFacade.CreateAsync(jobOffer);
                return RedirectToAction(nameof(Index));
            }

            throw new ArgumentException();
        }

        public ActionResult AddNewSkill(JobOfferDto jobOffer)
        {
            jobOffer.RelevantSkills.Add("");
            return View("AddJobOffer", jobOffer);
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
