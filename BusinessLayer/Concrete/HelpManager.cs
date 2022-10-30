using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class HelpManager
    {
        Repository<Help> repoHelp = new Repository<Help>();
        public List<Help> GetAll()
        {
            return repoHelp.List();
        }
        public Help GetByID(int id)
        {
            return repoHelp.Find(x => x.HelpID == id);
        }
        //public int UpdateHelp(Student p)
        //{
        //    Student student = new Student();
        //    student = repoStudent.Find(x => x.StudentID == p.StudentID);
        //    student.StudentName = p.StudentName;
        //    student.StudentSurname = p.StudentSurname;
        //    student.StudentMail = p.StudentMail;
        //    student.StudentNumber = p.StudentNumber;
        //    student.StudentPhoneNumber = p.StudentPhoneNumber;
        //    student.StudentGrade = p.StudentGrade;
        //    student.TeacherID = p.TeacherID;
        //    return repoStudent.Update(student);
        //}
    }
}
