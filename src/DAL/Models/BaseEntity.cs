using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
