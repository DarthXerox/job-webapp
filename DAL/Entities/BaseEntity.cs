using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
