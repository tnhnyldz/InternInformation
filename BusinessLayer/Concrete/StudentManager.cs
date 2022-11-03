using DataAccessLayer;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class StudentManager
    {
        Repository<Student> repoStudent = new Repository<Student>();
        //bütün öğrenciler getiren metot
        public List<Student> GetAll()
        {
            return repoStudent.List();
        }
        //öğrenciyi idsine göre bulan metot
        public Student FindStudent(int id)
        {
            return repoStudent.Find(x => x.StudentID == id);
        }
        //öğrenciyi pasif yapan metot
        public int StudentStatusFalse(int id)
        {
            Student student = repoStudent.Find(x => x.StudentID == id);
            student.StudentStatus = false;
            return repoStudent.Update(student);
        }
        //öğrenciyi aktif yapan metot
        public int StudentStatusTrue(int id)
        {
            Student student = repoStudent.Find(x => x.StudentID == id);
            student.StudentStatus = true;
            return repoStudent.Update(student);
        }
        //öğrenciyi güncelleyen metot
        public int UpdateStudent(Student p)
        {
            Student student = new Student();
            student = repoStudent.Find(x => x.StudentID == p.StudentID);
            student.StudentName = p.StudentName;
            student.StudentSurname = p.StudentSurname;
            student.StudentMail = p.StudentMail;
            student.StudentNumber = p.StudentNumber;
            student.StudentPhoneNumber = p.StudentPhoneNumber;
            student.StudentGrade = p.StudentGrade;
            student.TeacherID = p.TeacherID;
            student.StudentPassword= p.StudentPassword;
            return repoStudent.Update(student);
        }
        //öğrenciyi ekleyen metot
        public int AddStudentBusiness(Student p)
        {
            if (p.StudentName == "" || 
                p.StudentSurname == "" || 
                p.StudentNumber.Length!=9 ||
                p.StudentPhoneNumber.Length != 10 ||
                p.StudentGrade.Length!=1)
            {
                return -1;
            }
            return repoStudent.Insert(p);
        }
    }
}
