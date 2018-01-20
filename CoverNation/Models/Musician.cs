using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoverNation.Models
{
    [Table("Musicians")]
    public class Musician : User
    {
        public string Biography { get; set; }

        public virtual ICollection<Subscriber> Subscribers { get; set; }
        public virtual ICollection<MusicianEducation> Educations { get; set; }
        public virtual ICollection<MusicianGenre> Genres { get; set; }
    }
}