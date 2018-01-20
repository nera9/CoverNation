using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoverNation.Models
{
    public class CoversToPlaylist
    {
        [Key]
        public int CoversToPlaylistId { get; set; }

        public int CoverId { get; set; }
        public virtual Cover Cover { get; set; }


        public int PlaylistId { get; set; }
        public virtual Playlist Playlist { get; set; }

    }
}