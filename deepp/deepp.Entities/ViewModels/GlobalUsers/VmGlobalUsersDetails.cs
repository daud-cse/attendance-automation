using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.ViewModels.GlobalUsers
{
   public class VmGlobalUsersDetails
    {
        public int GlobalUsersId { get; set; }
        public string BranchName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int InstituteId { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
    }
}
