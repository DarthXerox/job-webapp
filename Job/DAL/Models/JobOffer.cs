using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class JobOffer : BaseEntity
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        public int AddressId { get; set; }

        [ForeignKey(nameof(AddressId))]
        public virtual Address Address { get; set; }

        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public virtual Company Company { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }
    }
}
