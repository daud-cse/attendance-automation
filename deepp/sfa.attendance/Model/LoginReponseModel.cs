using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sfa.attendance.Model
{
    public class LoginReponseModel
    {

        public string Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public object Obj { get; set; }
    }
}
