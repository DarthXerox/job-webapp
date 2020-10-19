using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Skill : BaseEntity
    {
        [MaxLength(64)]
        public string Tag { get; set; }
    }
}
