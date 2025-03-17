using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingMovieCore.Domain.Entities
{
    public class Category
    {
        [Key]
        public int? CategoryId { get; set; }
        public string? Categoryname { get; set; }
        public ICollection<Movies>? Movies { get; set; }
    }
}
