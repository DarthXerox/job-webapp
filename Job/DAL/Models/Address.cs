using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Address : BaseEntity
    {
        [MaxLength(64)]
        public string City { get; set; }

        [MaxLength(64)]
        public string Street { get; set; }

        [MaxLength(16)]
        public string HouseNumber { get; set; }

        [MaxLength(16)]
        public string ZipCode { get; set; }
    }
}
