using System.ComponentModel.DataAnnotations;

namespace Bussiness.Dto
{
    public class UserCreateDto
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string PasswordHash { get; set; }


    }
}
