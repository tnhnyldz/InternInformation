using BusinessLayer.Concrete;
using DataAccessLayer;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternInformation.Controllers
{
    [Authorize]
    public class InternController : Controller
    {
        InternManager ım = new InternManager();
        #region[CRUD Kısmı] 

        // Stajları listeler

        public ActionResult Index()
        {
            var interns = ım.GetAll();
            return View(interns);
        }
        //Stajları ekleyen actıon
        [HttpGet]
        public ActionResult AddNewIntern()
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
            return View();
        }
        [HttpPost]
        public ActionResult AddNewIntern(Intern p)
        {
            ım.AddInternBusiness(p);
            return RedirectToAction("Index");
        }
        //stajları guncelleyen actıon
        [HttpGet]
        public ActionResult InternUpdate(int id)
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
        public ActionResult InternUpdate(Intern p)
        {
            ım.UpdateIntern(p);
            return RedirectToAction("Index");
        }

        #endregion

        //onay bekleyen stajları gosteren actıon
        public ActionResult cRequired()
        {
            var interns = ım.confirmRequiredBL();
            return View(interns);
        }
        //Onaylanmayan stajları gosteren actıon
        public ActionResult UnConfirmed()
        {
            var interns = ım.UnconfirmedBl();
            return View(interns);
        }
        //Onaylanan stajları gosteren actıon
        public ActionResult Confirmed()
        {
            var interns = ım.confirmedBl();
            return View(interns);
        }
        //Tamamlanan stajları gosteren actıon
        public ActionResult Completed()
        {
            var interns = ım.CompletedBl();
            return View(interns);
        }
        //Stajları Onaylayan actıon
        public ActionResult Confirm(int id)
        {
            ım.confirmIntern(id);
            return RedirectToAction("Index");
        }
        //Stajlardaki onayı kaldıran actıon
        public ActionResult UnConfirm(int id)
        {
            ım.unconfirmIntern(id);
            return RedirectToAction("Index");
        }
        //Onay bekleyen durumuna dusuren actıon
        public ActionResult confirmWaiting(int id)
        {
            var interns = ım.confirmWaiting(id);
            return RedirectToAction("Index");
        }
        //Stajı Tamamlayan actıon
        public ActionResult Complete(int id)
        {
            var interns = ım.completeBl(id);
            return RedirectToAction("Index");
        }
      
    }
}