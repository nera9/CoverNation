using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoverNation.Models
{
    [Table("Musician_Educations")]
    public class Musician_Education
    {
        [Column(Order = 0), Key]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Column(Order = 1), Key]
        public int EducationId { get; set; }
        public Education Education { get; set; }

        public DateTime DateOfLearning { get; set; }
        public string DetailsAboutEducation { get; set; }
    }
}