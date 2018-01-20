using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoverNation.Models
{
    [Table("MusicianGenres")]
    public class MusicianGenre
    {
        [Key]
    	public int MusicianGenreId { get; set;}
    	
        public int MusicianId { get; set; }
        [ForeignKey("MusicianId")]
        public virtual Musician Musician { get; set; }

        public int GenreId { get; set; }
        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}