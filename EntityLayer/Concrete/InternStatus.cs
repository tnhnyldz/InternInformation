using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class InternStatus
    {
        [Key]
        public int InternStatusID { get; set; }
        [StringLength(30)]
        public string InternStatusName { get; set; }
        [StringLength(150)]
        public string InternStatusDesc { get; set; }
        public bool InternStatuss { get; set; }
        //Relations 
        public ICollection<Intern> Interns { get; set; }

    }
}
