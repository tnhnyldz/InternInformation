using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class HelpManager
    {
        Repository<Help> repoHelp = new Repository<Help>();
        public List<Help> GetAll()
        {
            return repoHelp.List();
        }
        public Help GetByID(int id)
        {
            return repoHelp.Find(x => x.HelpID == id);
        }
        public int UpdateHelp(Help p)
        {
            Help help = new Help();
            help = repoHelp.Find(x => x.HelpID == p.HelpID);
            help.HelpID = p.HelpID;
            help.HelpTitle = p.HelpTitle;
            help.HelpTitle2 = p.HelpTitle2;
            help.HelpLogo= p.HelpLogo;
            help.HelpContent1 = p.HelpContent1;
            help.HelpContent2 = p.HelpContent2; 
            help.HelpContent3 = p.HelpContent3; 
            return repoHelp.Update(help);
        }
        public int AddHelpBusiness(Help p)
        {
            return repoHelp.Insert(p);
        }
        public int DeleteHelp(int id)
        {
            Help help = new Help();
            help = repoHelp.Find(x => x.HelpID == id);
            return repoHelp.Delete(help);
        }
    }
}
