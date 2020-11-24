using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Enums;

namespace DAL.Entities
{
    public class JobApplication : BaseEntity
    {
        public int ApplicantId { get; set; }

        [ForeignKey(nameof(ApplicantId))]
        public virtual JobSeeker? Applicant { get; set; }

        public int JobOfferId { get; set; }

        [ForeignKey(nameof(JobOfferId))]
        public virtual JobOffer? JobOffer { get; set; }

        public Status Status { get; set; }

        [MaxLength(1024)]
        public string? Text { get; set; }

        public virtual ICollection<JobApplicationAnswer>? Answers { get; set; }
    }
}
