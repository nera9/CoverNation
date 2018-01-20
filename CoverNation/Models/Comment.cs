using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoverNation.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public int CoverId { get; set; }
        public Cover Cover { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string CommentText { get; set; }
        public DateTime Date { get; set; }
    }
}