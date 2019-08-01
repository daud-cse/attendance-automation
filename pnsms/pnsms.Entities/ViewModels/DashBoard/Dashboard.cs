using pnsms.Entities.Models;
using pnsms.Entities.StoredProcedures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.ViewModels.DashBoard
{
    public class Dashboard
    {
        public int UserId { get; set; }

        public string GlobalPlaceName { get; set; }
        public IEnumerable<DashboardHeader> DashboardHeader { get; set; }

        public List <VmInstitute> lstInstitute { get; set; }

        public   List<MaleFemaleRatioGlobal> lstMaleFemaleRatioGlobal { get; set; }
        public List<MaleFemaleRatioGlobal> lstTeacherMaleFemaleRatioGlobal { get; set; }
        public List<InstituteTotalInfo> lstInstituteTotalInfo { get; set; }

        public List<GlobalHeadmasterList> lstHeadMaster { get; set; }

        public List<cRatio> MaleFemaleRation { get; set; }

        public IEnumerable<cRatio> Result { get; set; }

    }
}
