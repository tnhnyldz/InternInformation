using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternInformation.Controllers
{
    public class HelpController : Controller
    {
        // GET: Help
        //help duzenleme anasayfa
        HelpManager hm = new HelpManager();
        public ActionResult Index()
        {
            var helps = hm.GetAll();
            return View(helps);
        }
        public ActionResult HelpPage()
        {
            var helps = hm.GetAll();
            return View(helps);
        }
        [HttpGet]
        public ActionResult UpdateHelp(int id)
        {
            var help = hm.GetByID(id);
            return View(help);
        }
        [HttpPost]
        public ActionResult UpdateHelp(Help p)
        {
        
            return View();
        }
        public ActionResult AddHelp()
        {
            return View();
        }
    }
}