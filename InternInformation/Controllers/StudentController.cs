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

namespace InternInformation.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        StudentProfileManager spm = new StudentProfileManager();
        StudentManager sm = new StudentManager();
        InternManager ım = new InternManager();

        //ogrencılerı getırıp sayfaya yazan actıon
        public ActionResult Index()
        {
            var students = sm.GetAll();
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
        public ActionResult StudentPage(string p)
        {

            p = (string)Session["StudentMail"];
            var studentProfile = spm.GetStudentByMail(p);
            return View(studentProfile);
        }

        //ogrencıye aıt stajları getırır
        public ActionResult Interns(string p)
        {
            p = (string)Session["StudentMail"];
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
            var stajMetni = studentProfile[0].StudentName + "_" + studentProfile[0].StudentSurname+"-Başvuru_Belgesi";
            ViewBag.stajMetni = stajMetni;
            return View();
        }
        [HttpPost]
        public ActionResult apply(Intern p)
        {
            //dosyayı alıp proje ıcıne kaydedecek
            string fileName = Path.GetFileNameWithoutExtension(p.UploadFile.FileName);
            string extension = Path.GetExtension(p.UploadFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            p.Filepath = "~/StajBelgeleri/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/StajBelgeleri/"), fileName);
            p.UploadFile.SaveAs(fileName);
            ım.AddInternBusiness(p);
            return RedirectToAction("StudentPage");
        }
       
    }
}