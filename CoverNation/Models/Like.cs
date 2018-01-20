using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoverNation.Models
{
    [Table("Likes")]
    public class Like
    {
        public int LikeId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int CoverId { get; set; }
        public virtual Cover Cover { get; set; }
        public int Raiting { get; set; }

    }
}
