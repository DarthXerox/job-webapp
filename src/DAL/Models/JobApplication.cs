using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class JobApplication : BaseEntity
    {
        public int ApplicantId { get; set; }

        [ForeignKey(nameof(ApplicantId))]
        public virtual JobSeeker Applicant { get; set; }

        public int JobOfferId { get; set; }

        [ForeignKey(nameof(JobOffer))]
        public virtual JobOffer JobOffer { get; set; }

        public Status Status { get; set; }

        [MaxLength(1024)]
        public string Text { get; set; }
    }
}
