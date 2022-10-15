using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class InternManager
    {
        Repository<Intern> repoIntern = new Repository<Intern>();
        //bütün stajları Getiren metot
        public List<Intern> GetAll()
        {
            return repoIntern.List();
        }
        //Onay bekleyen stajları Getiren metot
        public List<Intern> confirmRequiredBL()
        {
            return repoIntern.List()
                .Where(x=>x.InternStatusID==3)
                .ToList(); 
        }
        //Tamamlanan stajları Getiren metot
        public List<Intern> CompletedBl()
        {
            return repoIntern.List()
                .Where(x => x.InternStatusID == 1)
                .ToList();
        }
        //Onaylanan stajları Getiren metot
        public List<Intern> confirmedBl()
        {
            return repoIntern.List()
                .Where(x => x.InternStatusID == 8)
                .ToList();
        }
        //Onaylanmayan stajları Getiren metot
        public List<Intern> UnconfirmedBl()
        {
            return repoIntern.List()
                .Where(x => x.InternStatusID == 4)
                .ToList();
        }
        public Intern Findintern(int id)
        {
            return repoIntern.Find(x => x.InternID == id);
        }
        //Staj ekleyen metot
        public int AddInternBusiness(Intern p)
        {
            if (p.CompanyName==null)
            {
                return -1;
            }
            //her staj default olarak onay gereklı tıpıne eklenır
            p.InternStatusID = 3;
            return repoIntern.Insert(p);
        }
        //öğrenciyi güncelleyen metot
        public int UpdateIntern(Intern p)
        {
            Intern ıntern = new Intern();
            ıntern = repoIntern.Find(x => x.InternID == p.InternID);
            ıntern.CompanyName = p.CompanyName;
            ıntern.CompanyResponsible = p.CompanyResponsible;
            ıntern.CompanyResponsibleNumber = p.CompanyResponsibleNumber;
            ıntern.StartDate = p.StartDate;
            ıntern.FinishDate = p.FinishDate;
            ıntern.InternStatusID = p.InternStatusID;
            ıntern.InternNameID = p.InternNameID;
            return repoIntern.Update(ıntern);
        }
        //Stajı onaylayan metot
        public int confirmIntern(int id)
        {
            Intern intern = repoIntern.Find(x => x.InternID == id);
            intern.InternStatusID = 8;
            return repoIntern.Update(intern);
        } 
        //Stajı onaylamayan metot
        public int unconfirmIntern(int id)
        {
            Intern intern = repoIntern.Find(x => x.InternID == id);
            intern.InternStatusID = 4;
            return repoIntern.Update(intern);
        }
        //Stajı onay bekleyen durumuna getiren metot
        public int confirmWaiting(int id)
        {
            Intern intern = repoIntern.Find(x => x.InternID == id);
            intern.InternStatusID = 3;
            return repoIntern.Update(intern);
        }
        //Stajı tamamlayan metot
        public int completeBl(int id)
        {
            Intern intern = repoIntern.Find(x => x.InternID == id);
            intern.InternStatusID = 1;
            return repoIntern.Update(intern);
        }
        
    }
}
