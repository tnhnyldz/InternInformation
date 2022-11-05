using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternInformation.Controllers
{
    [Authorize]
    public class InternNameController : Controller
    {
        // GET: InternName
        InternNameManager ınameMan = new InternNameManager();
        //Staj Türlerini Listeler
        public ActionResult Index()
        {
            var InternTypes=ınameMan.GetAll();  
            return View(InternTypes);
        }
        [HttpGet]
        public ActionResult AddNewType()
        {
          return View();
        }
        [HttpPost]
        public ActionResult AddNewType(InternName p)
        {
            ınameMan.AddInternNameBusiness(p);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UpdateType(int id)
        {
            var Type = ınameMan.GetByID(id);
            return View(Type);
        }
        [HttpPost]
        public ActionResult UpdateType(InternName p)
        {
            ınameMan.UpdateType(p);
            return RedirectToAction("Index");
        }
        public ActionResult Active(int id)
        {
            ınameMan.InternNameAktif(id);
            return RedirectToAction("Index");
        }
        public ActionResult Pasif(int id)
        {
            ınameMan.InternNamePasif(id);
            return RedirectToAction("Index");
        }
    }
}