using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class InternName
    {
        [Key]
        public int InternNameID { get; set; }
        [StringLength(30)]
        public string InternNamee { get; set; }
        [StringLength(150)]
        public string InternNameDesc { get; set; }
        public bool InternStatus { get; set; }
        //Relations 
        public ICollection<Intern> Interns { get; set; }

    }
}
