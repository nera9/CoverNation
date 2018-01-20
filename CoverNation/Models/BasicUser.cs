using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoverNation.Models
{
    [Table("BasicUsers")]
    public class BasicUser:User
    {
        public virtual List<Subscriber> SubscribedTo { get; set; }
        public string PersonalInfo { get; set; }
    }
}