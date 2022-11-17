using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternInformation.Controllers
{
    public class InternStatusController : Controller
    {
        InternStatusManager Ism = new InternStatusManager();
        public ActionResult Index()
        {
            var status=Ism.GetAll();
            return View(status);
        }
        [HttpGet]
        public ActionResult AddNewType()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewType(InternStatus p)
        {
            Ism.AddInternStatus(p);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UpdateType(int id)
        {
            var Type = Ism.GetByID(id);
            return View(Type);
        }
        [HttpPost]
        public ActionResult UpdateType(InternStatus p)
        {
            Ism.UpdateType(p);
            return RedirectToAction("Index");
        }
        public ActionResult Active(int id)
        {
            Ism.InternNameAktif(id);
            return RedirectToAction("Index");
        }
        public ActionResult Pasif(int id)
        {
            Ism.InternNamePasif(id);
            return RedirectToAction("Index");
        }
    }
}