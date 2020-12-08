using System.Collections.Generic;

namespace Business.DTOs
{
    public class JobSeekerDto : BaseDTO
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Email { get; set; }

        public ICollection<string>? Skills { get; set; }
    }
}
