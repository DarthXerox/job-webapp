using System.Collections.Generic;
using DAL.Entities;
using DAL.Enums;

namespace Business.DTOs
{
    public class JobApplicationDto : BaseDTO
    {
        public int? ApplicantId { get; set; }

        public JobSeekerDto? Applicant { get; set; }

        public int? JobOfferId { get; set; }

        public JobOfferDto? JobOffer { get; set; }

        public Status? Status { get; set; }

        public string? Text { get; set; }

        public ICollection<JobApplicationAnswerDto>? Answers { get; set; }
    }
}
