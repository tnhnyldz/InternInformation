using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class StudentProfileManager
    {
        Repository<Student> repoStudent = new Repository<Student>();
        Repository<Teacher> repoTeacher = new Repository<Teacher>();
        Repository<Intern> repoIntern = new Repository<Intern>();
        //maıle gore ogrencıyı yolluyor maılı atıyoruz ogrencı gelıyor
        public List<Student> GetStudentByMail(string p)
        {
            return repoStudent
                .List()
                .Where(x => x.StudentMail == p)
                .ToList();
        }
        //ogrencı numarasını yolladık bıze ona aıt stajlar verdı
        public List<Intern> GetInternByStudentID(int id)
        {
            return repoIntern.List()
                .Where(x => x.StudentID == id)
                .ToList();
        }
        //maıle gore ogretmenı yolluyor maılı atıyoruz ögretmen gelıyor
        public List<Teacher> GetTeacherByMail(string p)
        {
            return repoTeacher
                .List()
                .Where(x => x.TeacherMail == p)
                .ToList();
        }
        public List<Student> GetStudentsByTeacher(int p)
        {
            return repoStudent
                .List()
                .Where(x => x.TeacherID == p)
                .ToList();
        }
    }
}
