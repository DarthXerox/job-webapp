using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Company : BaseEntity
    {
        [Required]
        [MaxLength(64)]
        public string? Name { get; set; }

        public virtual ICollection<JobOffer>? Offers { get; set; }

        public int? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }
    }
}
