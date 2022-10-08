using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }
        [StringLength(30)]
        public string StudentName { get; set; }
        [StringLength(30)]
        public string StudentSurname { get; set; }
        [StringLength(50)]
        public string StudentMail { get; set; }
        [StringLength(20)]
        public string StudentNumber { get; set; }
        [StringLength(15)]
        public string StudentPhoneNumber { get; set; }
        [StringLength(10)]
        public string StudentGrade { get; set; }
        public bool StudentStatus { get; set; }
        //Relations
        public int TeacherID { get; set; }
        public virtual Teacher Teacher { get; set; }
        //Relations 
        public ICollection<Intern> Interns { get; set; }
    }
}
