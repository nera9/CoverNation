using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoverNation.Models
{
    [Table("Instruments")]
    public class Instrument
    {
        [Key]
        public int InstrumentId { get; set; }
        public string Name { get; set; }
        public string PictureURL { get; set; }

    }
}