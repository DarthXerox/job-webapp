using DAL.Enums;

namespace Business.DTOs
{
    public class UserShowDto : BaseDto
    {
        public string Name { get; set; }

        public Roles Role { get; set; }

        public int? JobSeekerId { get; set; }

        public int? CompanyId { get; set; }
    }
}
