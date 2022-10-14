using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class TeacherManager
    {
        Repository<Teacher> repoTchr = new Repository<Teacher>();
        //bütün öğretmenleri getiren metot
        public List<Teacher> GetAll()
        {
            return repoTchr.List();
        }
        //komısyonu getıren metot
        public List<Teacher> GetAllCommision()
        {
            return repoTchr.List()
                .Where(x => x.Commission == true)
                .ToList(); ;
        }
        //ogretmenı getıren metot
        public List<Teacher> GetAllTeachers()
        {
            return repoTchr.List()
                .Where(x => x.Commission == false)
                .ToList(); ;
        }
        //öğretman idsine göre bulan metot
        public Teacher FindTeacher(int id)
        {
            //var deneme =repoTchr.GetById(id);
            return repoTchr.Find(x => x.TeacherID == id);
        }
        //Öğretmeni pasif yapan metot
        public int TeacherStatusFalse(int id)
        {
            Teacher teacher = repoTchr.Find(x => x.TeacherID == id);
            teacher.TeacherStatus = false;
            return repoTchr.Update(teacher);
        }
        //Öğretmeni aktif yapan metot
        public int TeacherStatusTrue(int id)
        {
            Teacher teacher = repoTchr.Find(x => x.TeacherID == id);
            teacher.TeacherStatus = true;
            return repoTchr.Update(teacher);
        }
        //Öğretmeni komisyon yapan metot
        public int TeacherCommision(int id)
        {
            Teacher teacher = repoTchr.Find(x => x.TeacherID == id);
            teacher.Commission = true;
            return repoTchr.Update(teacher);
        }
        //Öğretmeni öğretmen yapan metot
        public int TeacherTeacher(int id)
        {
            Teacher teacher = repoTchr.Find(x => x.TeacherID == id);
            teacher.Commission = false;
            return repoTchr.Update(teacher);
        }
        //Öğretmeni güncelleyen metot
        public int UpdateTeacher(Teacher p)
        {
            Teacher teacher = new Teacher();
            teacher = repoTchr.Find(x => x.TeacherID == p.TeacherID);
            teacher.TeacherID = p.TeacherID;
            teacher.TeacherName = p.TeacherName;
            teacher.TeacherSurname = p.TeacherSurname;
            teacher.Commission = p.Commission;
            teacher.TeacherStatus = p.TeacherStatus;
            return repoTchr.Update(teacher);
        }
        //Öğretmeni ekleyen metot
        public int AddTeacherBusiness(Teacher p)
        {
            if (p.TeacherName == "" ||
                p.TeacherSurname == "")
            {
                return -1;
            }
            return repoTchr.Insert(p);
        }
    }
}
