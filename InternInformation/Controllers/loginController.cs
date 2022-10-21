﻿using DataAccessLayer;
using EntityLayer.Concrete;
using InternInformation.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace InternInformation.Controllers
{
    public class loginController : Controller
    {
        // GET: login
        Context c = new Context();
        HelperClass hc = new HelperClass();
        public ActionResult Index()
        {
            return View();
        }
        //admın gırısı
        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Admin p)
        {
            var admınSıfre = "396383";
            //giriş yapmaya çalışan adminin mail
            //adresinden md5 şifresini aldık
            var adminPassword = c.Admins
                .Where(x => x.AdminMail == p.AdminMail)
                .Select(x => x.Password)
                .FirstOrDefault();
            //Şifreyi normal hale getırdık
            var decryptedAdminPassword = hc.decrypt(adminPassword);

            //admın bılgılerını aldık
            var adminInfo = c.Admins.
                FirstOrDefault(x => x.AdminMail == p.AdminMail && decryptedAdminPassword == p.Password);
            if (adminInfo != null)
            {
                //adminInfo.Password = decryptedAdminPassword;
                FormsAuthentication.SetAuthCookie(adminInfo.AdminMail,false);
                Session["AdminMail"] = adminInfo.AdminMail;
                return RedirectToAction("Index", "Intern");

            }
            else
            {
                return RedirectToAction("AdminLogin", "login");
            }
        }
        //ogrencı gırısı
        [HttpGet]
        public ActionResult StudentLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StudentLogin(Student p)
        {
            var ogrencı ="tunahanyildiz@outlook.it";
            var sifre=893884;
            var ogrencı1 = "tunahandevv@gmail.com";
            var sifre1 = 794113;
            //giriş yapmaya çalışan ogrencının mail
            //adresinden md5 şifresini aldık
            var studentPassword = c.Students
                .Where(x => x.StudentMail == p.StudentMail)
                .Select(x => x.StudentPassword)
                .FirstOrDefault();
            //Şifreyi normal hale getırdık
            var decryptedStudentPassword = hc.decrypt(studentPassword);

            //ogrencı bılgılerını aldık
            var studentInfo = c.Students.
                FirstOrDefault(x => x.StudentMail == p.StudentMail && decryptedStudentPassword == p.StudentPassword);
            if (studentInfo != null)
            {
                //adminInfo.Password = decryptedAdminPassword;
                FormsAuthentication.SetAuthCookie(studentInfo.StudentMail, false);
                Session["StudentMail"] = studentInfo.StudentMail;
                return RedirectToAction("StudentPage", "Student");

            }
            else
            {
                return RedirectToAction("StudentLogin", "login");
            }
        }
        //ogretmen girisi
        [HttpGet]
        public ActionResult TeacherLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TeacherLogin(Teacher p)
        {
            var adminInfo = c.Teachers.FirstOrDefault(x => x.TeacherMail == p.TeacherMail && x.TeacherPassword == p.TeacherPassword);
            if (adminInfo != null)
            {

            }
            else
            {

            }
            return View();
        }
        //şifre yenileme için kullanılacak
        //[HttpGet]
        //public ActionResult TeacherLogin()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult TeacherLogin(Teacher p)
        //{
        //    var adminInfo = c.Teachers.FirstOrDefault(x => x.TeacherMail == p.TeacherMail && x.TeacherPassword == p.TeacherPassword);
        //    if (adminInfo != null)
        //    {

        //    }
        //    else
        //    {

        //    }
        //    return View();
        //}
        public ActionResult LogOutAdmin()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("AdminLogin", "login");
        }
        public ActionResult LogOutStudent()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("StudentLogin", "login");
        }
    }
}