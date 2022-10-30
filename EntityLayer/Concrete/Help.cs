using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Help
    {
        [Key]
        public int HelpID { get; set; }
        public string HelpTitle { get; set; }
        public string HelpTitle2 { get; set; }
        public string HelpLogo { get; set; }
        public string HelpContent1 { get; set; }
        public string HelpContent2 { get; set; }
        public string HelpContent3 { get; set; }
    }
}
