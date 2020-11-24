using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class JobOfferQuestion : BaseEntity
    {
        [MaxLength(1024)]
        public string? Text { get; set; }
    }
}
