using BusinessLayer.Concrete;
using DataAccessLayer;
using EntityLayer.Concrete;
using InternInformation.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace InternInformation.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        StudentProfileManager spm = new StudentProfileManager();
        StudentManager sm = new StudentManager();
        InternManager ım = new InternManager();
        //ogrencılerı getırıp sayfaya yazan actıon
        public ActionResult Index(int sayfa=1)
        {
            var students = sm.GetAll().ToPagedList(sayfa, 5);
            return View(students);
        }
        //ogrencılerı guncellemek ıcın gereken actıon 
        [HttpGet]
        public ActionResult StudentUpdate(int id)
        {
            Context c = new Context();
            List<SelectListItem> Teachers = (from x in c.Teachers.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.TeacherName + " " + x.TeacherSurname,
                                                 Value = x.TeacherID.ToString()
                                             }).ToList();
            ViewBag.Teachers = Teachers;
            var student = sm.FindStudent(id);
            return View(student);
        }
        [HttpPost]
        public ActionResult StudentUpdate(Student p)
        {
            sm.UpdateStudent(p);
            return RedirectToAction("Index");
        }
        //öğrencılerı ekleyen metot
        [HttpGet]
        public ActionResult AddNewStudent()
        {
            Context c = new Context();
            List<SelectListItem> Teachers = (from x in c.Teachers.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.TeacherName + " " + x.TeacherSurname,
                                                 Value = x.TeacherID.ToString()
                                             }).ToList();
            ViewBag.Teachers = Teachers;
            return View();
        }
        [HttpPost]
        public ActionResult AddNewStudent(Student p)
        {
            //yardımcı classdan bır nesne urettık
            HelperClass helper = new HelperClass();
            //eklenecek ogrencı ıcın rastgele bır sıfre olusturduk
            var password = helper.generatePassword();
            //ogrencinin mailini bir degiskene aldık
            string ogrMail = p.StudentMail;
            //ogrencının maılını ve sıfresını ona maıl atmak ıcın 
            //helper classdakı maıl gonder butonuna yolluyoruz
            helper.SendMailPassword(ogrMail, password);
            //şifreyi md5 yaptık
            var encryptedPass = helper.encrypt(password);

            p.StudentPassword = encryptedPass;


            sm.AddStudentBusiness(p);
            return RedirectToAction("Index");
        }
        //ogrencılerı pasif yapar sm sınıfındaki metodu kullanıyor
        public ActionResult StudentFalse(int id)
        {
            sm.StudentStatusFalse(id);
            return RedirectToAction("Index");
        }
        //ogrencılerı aktif yapar sm sınıfındaki metodu kullanıyor
        public ActionResult StudentTrue(int id)
        {
            sm.StudentStatusTrue(id);
            return RedirectToAction("Index");
        }
        //ÖĞRENCİ TEMPLATE KULLANAN ACTIONLAR
        //ogrencı anasayfa
        public ActionResult StudentPage()
        {

            var p = (string)Session["StudentMail"];
            var studentProfile = spm.GetStudentByMail(p);
            return View(studentProfile);
        }

        //ogrencıye aıt stajları getırır
        public ActionResult Interns()
        {
            var p = (string)Session["StudentMail"];
            var studentProfile = spm.GetStudentByMail(p);
            var Interns = spm.GetInternByStudentID(studentProfile[0].StudentID);
            return View(Interns);
        }
        //ogrencı staj basvuru yapma
        [HttpGet]
        public ActionResult apply()
        {
            var p = (string)Session["StudentMail"];
            var studentProfile = spm.GetStudentByMail(p);
            Context c = new Context();
            ViewBag.isim = studentProfile[0].StudentName;
            ViewBag.soyisim = studentProfile[0].StudentSurname;
            ViewBag.ID = studentProfile[0].StudentID;
            List<SelectListItem> InternTypes = (from x in c.InternNames.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = x.InternNamee,
                                                    Value = x.InternNameID.ToString()
                                                }).ToList();
            ViewBag.InternTypes = InternTypes;
            var stajMetni = studentProfile[0].StudentName + "_" + studentProfile[0].StudentSurname + "-Başvuru_Belgesi";
            ViewBag.stajMetni = stajMetni;
            var stajDefteri = studentProfile[0].StudentName + "_" + studentProfile[0].StudentSurname + "-StajDefteri";
            ViewBag.stajDefteri = stajDefteri;
            return View();
        }
        [HttpPost]
        public ActionResult apply(Intern p)
        {
            //dosyayı alıp proje ıcıne kaydedecek
            string fileName = Path.GetFileNameWithoutExtension(p.UploadFile.FileName);
            string extension = Path.GetExtension(p.UploadFile.FileName);
            fileName = fileName + extension;
            p.Filepath = "~/StajBelgeleri/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/StajBelgeleri/"), fileName);
            p.UploadFile.SaveAs(fileName);

            //dosyayı alıp proje ıcıne kaydedecek
            //buraya if koyulup default belge adı verılecek

            string fileName2 = "Staj_Defteri_Yok";
            string extension2 = ".pdf";
            if (p.UploadFile.FileName!=null)
            {
                string fileName3 = Path.GetFileNameWithoutExtension(p.UploadFile.FileName);
                string extension3 = Path.GetExtension(p.UploadFile.FileName);
                fileName3 = fileName3 + extension3;
                p.FilepathStajDefteri = "~/StajDefterleri/" + fileName3;
                fileName3 = Path.Combine(Server.MapPath("~/StajDefterleri/"), fileName3);
                p.UploadFile.SaveAs(fileName3);
                ım.AddInternBusiness(p);
            }
            fileName2 = fileName2 + extension2;
            p.FilepathStajDefteri = "~/StajDefterleri/" + fileName2;
            fileName2 = Path.Combine(Server.MapPath("~/StajDefterleri/"), fileName2);
            p.UploadFile.SaveAs(fileName2);
            ım.AddInternBusiness(p);

            return RedirectToAction("StudentPage");
        }
      
        [HttpGet]
        public ActionResult addNewDate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addNewDate(Intern p)
        {
            var tarih = p.StartDate;
            var staj= HelperClass3.AddBusinessDays(p.StartDate, 30)
                .ToString("dd MMMM yyyy");
            var İme = HelperClass3.AddBusinessDays(p.StartDate, 70)
                .ToString("dd MMMM yyyy");
            ViewBag.staj = staj;
            ViewBag.İme = İme;
            return View();
        }
     

    }
}