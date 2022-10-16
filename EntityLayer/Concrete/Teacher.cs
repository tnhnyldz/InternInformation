using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Teacher
    {
        [Key]
        public int TeacherID { get; set; }
        [StringLength(30)]
        public string TeacherName { get; set; }
        [StringLength(30)]
        public string TeacherSurname { get; set; }
        public string TeacherPassword { get; set; }
        public string TeacherMail { get; set; }
        public bool Commission { get; set; }
        public bool TeacherStatus { get; set; }
        //Relations
        public ICollection<Student> Students { get; set; }

    }
}