using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;

namespace BL.Entities.Dto
{
    public class CompanyDto
    {
        public string Name { get; set; }

        public ICollection<JobOffer> Offers { get; set; }
    }
}
