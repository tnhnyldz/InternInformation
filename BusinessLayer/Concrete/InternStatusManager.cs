using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class InternStatusManager
    {
        Repository<InternStatus> RepoInternSs = new Repository<InternStatus>();
        public List<InternStatus> GetAll()
        {
            return RepoInternSs.List();
        }
        public InternStatus GetByID(int id)
        {
            return RepoInternSs.Find(x => x.InternStatusID == id);
        }
        public int AddInternStatus(InternStatus p)
        {
            return RepoInternSs.Insert(p);
        }
        public int UpdateType(InternStatus p)
        {
            InternStatus status = new InternStatus();
            status = RepoInternSs.Find(x => x.InternStatusID == p.InternStatusID);
            status.InternStatusDesc = p.InternStatusDesc;
            status.InternStatusName = p.InternStatusName;
            return RepoInternSs.Update(status);
        }
        //staj ismini pasif yapar
        public int InternNamePasif(int id)
        {
            InternStatus internName = RepoInternSs.Find(x => x.InternStatusID == id);
            internName.InternStatuss = false;
            return RepoInternSs.Update(internName);
        }
        //staj ismini aktif yapar
        public int InternNameAktif(int id)
        {
            InternStatus internName = RepoInternSs.Find(x => x.InternStatusID == id);
            internName.InternStatuss = true;
            return RepoInternSs.Update(internName);
        }
    }
}
