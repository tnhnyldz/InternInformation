using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using PagedList.Mvc;



namespace BusinessLayer.Concrete
{
    public class InternManager
    {
        Helper2 helper2 = new Helper2();
        Repository<Intern> repoIntern = new Repository<Intern>();
        //bütün stajları Getiren metot
        public List<Intern> GetAll()
        {
            return repoIntern.List();
        }
        //Onay bekleyen stajları Getiren metot
        public List<Intern> confirmRequiredBL()
        {
            return repoIntern.List()
                .Where(x => x.InternStatusID == 3)
                .ToList();
        }
        //Tamamlanan stajları Getiren metot
        public List<Intern> CompletedBl()
        {
            return repoIntern.List()
                .Where(x => x.InternStatusID == 1)
                .ToList();
        }
        //Onaylanan stajları Getiren metot
        public List<Intern> confirmedBl()
        {
            return repoIntern.List()
                .Where(x => x.InternStatusID == 8)
                .ToList();
        }
        //Onaylanmayan stajları Getiren metot
        public List<Intern> UnconfirmedBl()
        {
            return repoIntern.List()
                .Where(x => x.InternStatusID == 4)
                .ToList();
        }
        //staj bulur
        public Intern Findintern(int id)
        {
            return repoIntern.Find(x => x.InternID == id);
        }
        //Staj ekleyen metot
        public int AddInternBusiness(Intern p)
        {
            helper2 = new Helper2();
            //staj1 ve staj2 için bu blok kullanılır
            if (p.InternNameID == 1 || p.InternNameID == 2)
            {
                //helperdakı hesaplama fonk
                var days = helper2.GetBusinessDays(p.StartDate, p.FinishDate);
                if (days != 30.0)
                {
                    return -1;
                }
            }
            //işyeri için bu blok kullanılır
            if (p.InternNameID == 3)
            {
                //helperdakı hesaplama fonk
                var days = helper2.GetBusinessDays(p.StartDate, p.FinishDate);
                if (days != 70.0)
                {
                    return -1;
                }
            }
            if (p.CompanyName == null)
            {
                return -1;
            }
            var ogrID = p.StudentID;
            var interns=repoIntern.List().Where(x=>x.StudentID==ogrID).ToList();
            //if (interns.Count()>=3)
            //{
            //    return -1;

            //}
            //her staj default olarak onay gereklı tıpıne eklenır
            p.InternStatusID = 3;
            return repoIntern.Insert(p);
        }
        //öğrenciyi güncelleyen metot
        public int UpdateIntern(Intern p)
        {
            Intern ıntern = new Intern();
            ıntern = repoIntern.Find(x => x.InternID == p.InternID);
            ıntern.CompanyName = p.CompanyName;
            ıntern.CompanyResponsible = p.CompanyResponsible;
            ıntern.CompanyResponsibleNumber = p.CompanyResponsibleNumber;
            ıntern.StartDate = p.StartDate;
            ıntern.FinishDate = p.FinishDate;
            ıntern.InternStatusID = p.InternStatusID;
            ıntern.InternNameID = p.InternNameID;
            return repoIntern.Update(ıntern);
        }
        //Stajı onaylayan metot
        public int confirmIntern(int id)
        {
            Intern intern = repoIntern.Find(x => x.InternID == id);
            var nameSurname = intern.Student.StudentName + " " + intern.Student.StudentSurname;
            var studentMail = intern.Student.StudentMail;
            //degısıklıgı ogrencıye haber verıyor
            helper2.stajDeğişiklik(nameSurname, studentMail);
            intern.InternStatusID = 8;
            return repoIntern.Update(intern);
        }
        //Stajı onaylamayan metot
        public int unconfirmIntern(int id)
        {
            //HelperClass h = new HelperClass();
            Intern intern = repoIntern.Find(x => x.InternID == id);
            var nameSurname = intern.Student.StudentName + " " + intern.Student.StudentSurname;
            var studentMail = intern.Student.StudentMail;
            //degısıklıgı ogrencıye haber verıyor
            helper2.stajDeğişiklik(nameSurname, studentMail);
            intern.InternStatusID = 4;
            return repoIntern.Update(intern);
        }
        //Stajı onay bekleyen durumuna getiren metot
        public int confirmWaiting(int id)
        {
            Intern intern = repoIntern.Find(x => x.InternID == id);
            var nameSurname = intern.Student.StudentName + " " + intern.Student.StudentSurname;
            var studentMail = intern.Student.StudentMail;
            //degısıklıgı ogrencıye haber verıyor
            helper2.stajDeğişiklik(nameSurname, studentMail);
            intern.InternStatusID = 3;
            return repoIntern.Update(intern);
        }
        //Stajı tamamlayan metot
        public int completeBl(int id)
        {
            Intern intern = repoIntern.Find(x => x.InternID == id);
            var nameSurname = intern.Student.StudentName + " " + intern.Student.StudentSurname;
            var studentMail = intern.Student.StudentMail;
            //degısıklıgı ogrencıye haber verıyor
            helper2.stajDeğişiklik(nameSurname, studentMail);
            intern.InternStatusID = 1;
            return repoIntern.Update(intern);
        }

    }
}
