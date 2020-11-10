using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using DAL.Entities.Dto;
using DAL.Enums;

namespace BL.Entities.Dto
{
    public class JobApplicationDto
    {
        public int ApplicantId { get; set; }

        public JobSeekerDto Applicant { get; set; }

        public int JobOfferId { get; set; }

        public JobOfferDto JobOffer { get; set; }

        public Status Status { get; set; }

        public string Text { get; set; }

        public ICollection<JobApplicationAnswer> Answers { get; set; }
    }
}
