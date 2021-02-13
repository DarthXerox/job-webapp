using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class JobSeekerDto : BaseDto
    {
        [Required]
        [StringLength(64)]
        public string? Name { get; set; }

        [Required]
        [StringLength(64)]
        public string? Surname { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(64)]
        public string? Email { get; set; }

        public IList<string>? Skills { get; set; }

        public int? UserId { get; set; }
    }
}
