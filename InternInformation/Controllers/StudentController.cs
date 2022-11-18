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
        HelperClass helper = new HelperClass();
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
        [HttpGet]
        public ActionResult StudentPage()
        {
            
            var p = (string)Session["StudentMail"];
            var studentProfile = spm.GetStudentByMail(p);
            var Password = studentProfile[0].StudentPassword;
            var DecryptedPassword= helper.decrypt(Password);
            studentProfile[0].StudentPassword = DecryptedPassword;
            return View(studentProfile);
        }
        [HttpPost]
        public ActionResult StudentPage(Student student)
        {

            var p = (string)Session["StudentMail"];

            var studentProfile = spm.GetStudentByMail(p);

            var teacherIDold = studentProfile[0].TeacherID;

            var newPassword = student.StudentPassword;

            var EncryptedPassword = helper.encrypt(newPassword);

            student.StudentPassword = EncryptedPassword;

            student.TeacherID = teacherIDold;
            
            sm.UpdateStudent(student);
            return RedirectToAction("StudentPage");
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
            List<SelectListItem> InternTypes = (from x in c.InternNames.Where(x=>x.InternStatus).ToList()
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
            ////dosyayı alıp proje ıcıne kaydedecek
            //string fileName = Path.GetFileNameWithoutExtension(p.UploadFile.FileName);
            //string extension = Path.GetExtension(p.UploadFile.FileName);
            //fileName = fileName + extension;
            //p.Filepath = "~/StajBelgeleri/" + fileName;
            //fileName = Path.Combine(Server.MapPath("~/StajBelgeleri/"), fileName);
            //p.UploadFile.SaveAs(fileName);

            ////dosyayı alıp proje ıcıne kaydedecek
            ////buraya if koyulup default belge adı verılecek

            //string fileName2 = "Staj_Defteri_Yok";
            //string extension2 = ".pdf";
            //if (p.UploadFile.FileName!=null)
            //{
            //    string fileName3 = Path.GetFileNameWithoutExtension(p.UploadFile.FileName);
            //    string extension3 = Path.GetExtension(p.UploadFile.FileName);
            //    fileName3 = fileName3 + extension3;
            //    p.FilepathStajDefteri = "~/StajDefterleri/" + fileName3;
            //    fileName3 = Path.Combine(Server.MapPath("~/StajDefterleri/"), fileName3);
            //    p.UploadFile.SaveAs(fileName3);
            //    ım.AddInternBusiness(p);
            //}
            //fileName2 = fileName2 + extension2;
            //p.FilepathStajDefteri = "~/StajDefterleri/" + fileName2;
            //fileName2 = Path.Combine(Server.MapPath("~/StajDefterleri/"), fileName2);
            //p.UploadFile.SaveAs(fileName2);
            //ım.AddInternBusiness(p);
            if (Request.Files.Count > 0)
            {
                if (Request.Files[1].FileName == "")
                {
                    string dosyaadi2 = "Staj_Defteri_Yok";
                    string uzanti2 = ".pdf";
                    string yol2 = "~/StajDefterleri2/" + dosyaadi2 + uzanti2;
                    Request.Files[1].SaveAs(Server.MapPath(yol2));
                    p.StajDefteri = "/StajDefterleri2/" + dosyaadi2 + uzanti2;
                }
                else
                {
                    string dosyaadi2 = Path.GetFileNameWithoutExtension(Request.Files[1].FileName);
                    string uzanti2 = Path.GetExtension(Request.Files[1].FileName);
                    string yol2 = "~/StajDefterleri2/" + dosyaadi2 + uzanti2;
                    Request.Files[1].SaveAs(Server.MapPath(yol2));
                    p.StajDefteri = "/StajDefterleri2/" + dosyaadi2 + uzanti2;
                }
                string dosyaadi = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/StajBelgeleri2/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.StajBelgesi = "/StajBelgeleri2/" + dosyaadi + uzanti;
            }
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
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(Student p)
        {
            return View();
        }
        [HttpGet]
        public ActionResult InternUpdateStudentSide(int id)
        {
            Context c = new Context();
            List<SelectListItem> InternTypes = (from x in c.InternNames.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = x.InternNamee,
                                                    Value = x.InternNameID.ToString()
                                                }).ToList();
            ViewBag.InternTypes = InternTypes;
            List<SelectListItem> InternStatus = (from x in c.InternStatuss.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.InternStatusName,
                                                     Value = x.InternStatusID.ToString()
                                                 }).ToList();
            ViewBag.InternStatus = InternStatus;
            List<SelectListItem> Students = (from x in c.Students.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.StudentName + " " + x.StudentSurname,
                                                 Value = x.StudentID.ToString()
                                             }).ToList();
            ViewBag.Students = Students;
            var student = ım.Findintern(id);
            return View(student);
        }
        [HttpPost]
        public ActionResult InternUpdateStudentSide(Intern p)
        {
            ım.UpdateIntern(p);
            return RedirectToAction("StudentPage");
        }

    }
}