using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class JobOfferQuestion : BaseEntity
    {
        [MaxLength(1024)]
        public string Text { get; set; }
    }
}
