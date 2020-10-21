using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Company : BaseEntity
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        public virtual ICollection<JobOffer> Offers { get; set; }
    }
}
