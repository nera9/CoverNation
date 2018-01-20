using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CoverNation.Models
{
    public class Playlist
    {      
        public int PlaylistId { get; set; }
        public string Title { get; set; }
        public int NumOfCovers { get;set; }
        public DateTime DateOfCreating { get; set; }        
        public int UserId { get; set; }
        public  User User { get;set; }
    }
}