using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class JobOfferQuestion : BaseEntity
    {
        [MaxLength(1024)]
        public string? Text { get; set; }

        public int? JobOfferId { get; set; }

        [ForeignKey(nameof(JobOfferId))]
        public virtual JobOffer? JobOffer { get; set; }
    }
}
