using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoverNation.Models
{
    [Table("Artists")]
    public class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}