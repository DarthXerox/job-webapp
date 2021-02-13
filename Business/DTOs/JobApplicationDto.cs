using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Entities;
using DAL.Enums;

namespace Business.DTOs
{
    public class JobApplicationDto : BaseDto
    {
        public int? ApplicantId { get; set; }

        public JobSeekerDto? Applicant { get; set; }

        public int? JobOfferId { get; set; }

        public JobOfferDto? JobOffer { get; set; }

        [Required]
        public Status? Status { get; set; }

        [Required]
        [StringLength(1024)]
        public string? Text { get; set; }

        public IList<JobApplicationAnswerDto>? Answers { get; set; }
    }
}
