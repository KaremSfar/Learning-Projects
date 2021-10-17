using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(300),MinLength(3)]
        [Display(Name="Name of the Movie")]
        public string Name { get; set; }

        [Required]
        public DateTime? ReleaseDate { get; set; }

        public DateTime? DateAdded { get; set; }

        [Required]
        [Range(1,20)]
        public int Stock { get; set; }

        public int Available { get; set; }

        [Required]
        public Genre Genre { get; set; }
    }
}