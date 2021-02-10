using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class JobSeekerDto : BaseDto
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public IList<string>? Skills { get; set; }

        public int? UserId { get; set; }
    }
}
