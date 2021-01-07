using System;
using System.Collections.Generic;
using System.Linq;
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
            var model = await jobOfferFacade.GetAllAsync(page, PageSize);

            var pagedModel = new PagedListViewModel<JobOfferDto>(
                new PaginationViewModel(page, model.totalCount, PageSize),
                model.offers);
            return View(pagedModel);
        }
    }
}
