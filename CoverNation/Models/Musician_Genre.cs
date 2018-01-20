using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoverNation.Models
{
    [Table("Musician_Genres")]
    public class Musician_Genre
    {
        [Column(Order = 0), Key]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Column(Order = 1), Key]
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
    }
}