using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Intern
    {
        [Key]
        public int InternID { get; set; }
        [StringLength(50)]
        public string CompanyName { get; set; }
        [StringLength(30)]
        public string CompanyResponsible { get; set; }
        [StringLength(15)]
        public string CompanyResponsibleNumber { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public bool InternStatuss { get; set; }
        //Relations 
        public int InternNameID { get; set; }
        public virtual InternName InternName { get; set; }
        //Relations 
        public int InternStatusID { get; set; }
        public virtual InternStatus InternStatus { get; set; }
        //Relations 
        public int StudentID { get; set; }
        public virtual Student Student { get; set; }
    }
}
