﻿using BusinessLayer.Concrete;
using DataAccessLayer;
using EntityLayer.Concrete;
using InternInformation.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternInformation.Controllers
{
    public class StudentController : Controller
    {
        StudentManager sm = new StudentManager();
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
            var encryptedPass =helper.encrypt(password);

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

    }
}