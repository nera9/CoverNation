using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoverNation.Models
{
    [Table("PreferredGenres")]
    public class PreferredGenre
    {
        public int PreferredGenreId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
    }
}