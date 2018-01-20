using System.ComponentModel.DataAnnotations.Schema;

namespace CoverNation.Models
{
    [Table("Administrators")]
    public class Administrator:User
    {
        public string temp { get; set; }
        public int nesto2 { get; set; }
    }
}