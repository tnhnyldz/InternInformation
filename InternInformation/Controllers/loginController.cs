using DataAccessLayer;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternInformation.Controllers
{
    public class loginController : Controller
    {
        // GET: login

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Admin p)
        {
            Context c = new Context();
            var adminInfo = c.Admins.FirstOrDefault(x => x.Username == p.Username && x.Password == p.Password);
            if (adminInfo != null)
            {

            }
            else
            {

            }
            return View();
        }

    }
}