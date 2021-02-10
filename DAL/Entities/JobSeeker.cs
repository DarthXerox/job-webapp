using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class JobSeeker : BaseEntity
    {
        [Required]
        [MaxLength(64)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(64)]
        public string? Surname { get; set; }

        [Required]
        [MaxLength(64)]
        public string? Email { get; set; }

        public virtual ICollection<string>? Skills { get; set; }

        public int? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }
    }
}
