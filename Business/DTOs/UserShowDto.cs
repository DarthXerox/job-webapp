using DAL.Enums;

namespace Business.DTOs
{
    public class UserShowDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Roles Role { get; set; }
    }
}
