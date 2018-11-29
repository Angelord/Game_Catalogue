using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GameC.Data.Entities
{
    public class Rating
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Rating")]
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Value { get; set; }

        [StringLength(400, MinimumLength = 1)]
        public string Description { get; set; }
    }
}
