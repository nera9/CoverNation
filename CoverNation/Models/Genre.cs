using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoverNation.Models
{
    [Table("Genres")]
    public class Genre
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public string GenreImage { get; set; }

        public virtual ICollection<MusicianGenre> Musicians { get; set; }
    }
}