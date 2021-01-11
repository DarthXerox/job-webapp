using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Models
{
    public class AddJobOfferModel
    {
        public JobOfferDto JobOffer { get; set; }
        public List<CompanyDto> Companies { get; set; }
    }
}
