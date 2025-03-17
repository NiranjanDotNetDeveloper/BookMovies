using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingMovieCore.Domain.Entities
{
    public class Movies
    {
        [Key]
        public int? MovieId { get; set; }
        public string? Title { get; set; }
        public string? Director { get; set; }
        public string? Actor { get; set; }
        public string? Actress { get; set; }
        public int? ReleasedDate { get; set; }

        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

    }
}
