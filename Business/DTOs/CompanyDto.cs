using System.Collections.Generic;

namespace Business.DTOs
{
    public class CompanyDto : BaseDTO
    {
        public string? Name { get; set; }

        public ICollection<JobOfferDto>? Offers { get; set; }
    }
}
