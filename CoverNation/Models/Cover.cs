using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoverNation.Models
{
    [Table("Covers")]
    public class Cover
    {
        public int CoverId { get; set; }
        public string CoverName { get; set; }
        public DateTime DateOfPosting { get; set; }
        public string VideoURL { get; set; }
        public string Thumbnail { get; set; }
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }

        [ForeignKey("Musician")]
        public int MusicianId { get; set; }
        public virtual Musician Musician { get; set; }

    }
}
