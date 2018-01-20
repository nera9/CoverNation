using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoverNation.Models
{
    [Table("MusicianEducations")]
    public class MusicianEducation
    {
        [Key]
        public int MusicianEducationId { get; set; }
        
        public int MusicianId { get; set; }
        [ForeignKey("MusicianId")]
        public virtual Musician Musician { get; set; }

        public int InstrumentId { get; set; }
        public virtual Instrument Instrument { get; set; }

        public DateTime DateOfLearning { get; set; }
        public string InstitutionName { get; set; }
        public string DetailsAboutEducation { get; set; }

    }
}