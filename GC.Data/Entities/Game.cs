using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameC.Data.Entities
{
    public class Game
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Release Year")]
        public string ReleaseYear { get; set; }

        [Display(Name = "Genre")]
        [ForeignKey("Genre")]
        public int? GenreId { get; set; }
        public virtual Genre Genre { get; set; }

        [Display(Name = "Rating")]
        [ForeignKey("Rating")]
        public int? RatingId { get; set; }
        public virtual Rating Rating { get; set; }
    }
}
