using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Company : BaseEntity
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        public virtual ICollection<JobOffer> Offers { get; set; }

        public static Company Create(int id, string name)
        {
            return new Company()
            {
               Id = id,
               Name = name
            };
        }
    }
}
