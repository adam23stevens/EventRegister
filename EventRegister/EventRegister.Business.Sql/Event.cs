using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRegister.Business.Sql
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public DateTime UkStartTime { get; set; }
        public DateTime UkEndTime { get; set; }
        public List<string> RegisteredEmails { get; set; }
    }
}
