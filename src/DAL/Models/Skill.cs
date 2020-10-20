using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Skill : BaseEntity
    {
        [MaxLength(64)]
        public string Tag { get; set; }
    }
}
