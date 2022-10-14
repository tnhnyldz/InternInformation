using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;

namespace InternInformation.Controllers
{
    public class TeacherController : Controller
    {
        TeacherManager tm = new TeacherManager();
        //öğretmenleri getırıp sayfaya yazan actıon
        public ActionResult Index()
        {
            var Teachers = tm.GetAll();
            return View(Teachers);
        }
        //öğretmenleri guncellemek ıcın gereken actıon 
        [HttpGet]
        public ActionResult TeacherUpdate(int id)
        {
            Teacher teacher = tm.FindTeacher(id);
            return View(teacher);
        }
        [HttpPost]
        public ActionResult TeacherUpdate(Teacher p)
        {
            tm.UpdateTeacher(p);
            return RedirectToAction("Index");
        }
        //yenı ogretmen ekleme
        [HttpGet]
        public ActionResult AddNewTeacher()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewTeacher(Teacher p)
        {
            tm.AddTeacherBusiness(p);
            return RedirectToAction("Index");
        }
        //ogretmeni pasif yapar
        public ActionResult TeacherFalse(int id)
        {
            tm.TeacherStatusFalse(id);
            return RedirectToAction("Index");
        }
        //ogretmeni aktıf yapar
        public ActionResult TeacherTrue(int id)
        {
            tm.TeacherStatusTrue(id);
            return RedirectToAction("Index");
        }
        //ogretmenı komısyon yapar
        public ActionResult Commision(int id)
        {
            tm.TeacherCommision(id);
            return RedirectToAction("Index");
        }
        //komısyonu ogretmen yapar
        public ActionResult Teacher(int id)
        {
            tm.TeacherTeacher(id);
            return RedirectToAction("Index");
        }
        //sadece komısyon sayfası
        public ActionResult CommisionPage()
        {
           var commision= tm.GetAllCommision();
            return View(commision);
        }
        //sadece ogretmenler sayfası
        public ActionResult Teacherpage()
        {
            var teacher = tm.GetAllTeachers();
            return View(teacher);
        }
    }
}