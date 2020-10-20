using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class Company : BaseEntity
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        public int AddressId { get; set; }

        [ForeignKey(nameof(AddressId))]
        public virtual Address Address { get; set; }

        public virtual ICollection<JobOffer> Offers { get; set; }
    }
}
