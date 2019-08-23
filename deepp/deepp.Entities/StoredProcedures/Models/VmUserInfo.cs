using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.StoredProcedures.Models
{
   public class VmUserInfo
    {
        public int UserInforId { get; set; }
        public int UserInfoTypeId { get; set; }
        public string UserName { get; set; }
        public string ContactNumber1 { get; set; }
        public string ContactNumber2 { get; set; }
        public string PIN { get; set; }
        public int SessionsId { get; set; }
        public string SessionsName { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }

    }
}
