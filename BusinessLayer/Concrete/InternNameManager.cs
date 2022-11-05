using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class InternNameManager
    {
        Repository<InternName> repoInternName = new Repository<InternName>();
        public List<InternName> GetAll()
        {
            return repoInternName.List();
        }
        public InternName GetByID(int id)
        {
            return repoInternName.Find(x => x.InternNameID == id);
        }
        public int AddInternNameBusiness(InternName p)
        {
            if (p.InternNamee == "" ||
                p.InternNameDesc == "")
            {
                return -1;
            }
            return repoInternName.Insert(p);
        }
        public int UpdateType(InternName p)
        {
            InternName In = new InternName();
            In = repoInternName.Find(x => x.InternNameID == p.InternNameID);
            In.InternNameDesc = p.InternNameDesc;   
            In.InternNamee = p.InternNamee;
            return repoInternName.Update(In);
        }
        //staj ismini pasif yapar
        public int InternNamePasif(int id)
        {
            InternName internName = repoInternName.Find(x => x.InternNameID == id);
            internName.InternStatus = false;
            return repoInternName.Update(internName);
        }
        //staj ismini aktif yapar
        public int InternNameAktif(int id)
        {
            InternName internName = repoInternName.Find(x => x.InternNameID == id);
            internName.InternStatus = true;
            return repoInternName.Update(internName);
        }
    }
}
