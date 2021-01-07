using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.DTOs;

namespace MVC.Models
{
    public class JobOfferListModel
    {
        public IEnumerable<JobOfferDto> JobOffers { get; set; }
    }
}
