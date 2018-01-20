using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoverNation.Models
{
    [Table("Subscribers")]
    public class Subscriber
    {
        [Key]
        public int SubscriberId { get; set; }

        public int BasicUserId { get; set; }
        [ForeignKey("BasicUserId")]
        public virtual BasicUser BasicUser { get; set; }

        public int MusicianId { get; set; }
        [ForeignKey("MusicianId")]
        public virtual Musician Musician { get; set; }

        public DateTime DateOfSubscription { get; set; }
    }
}