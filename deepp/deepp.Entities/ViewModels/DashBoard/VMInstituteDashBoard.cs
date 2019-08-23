using deepp.Entities.StoredProcedures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.ViewModels.DashBoard
{
   public class VMInstituteDashBoard
    {

       public InstituteTotalInfo objInstituteTotalInfo { get; set; }

       public List<ClassWiseStudent> lstClassWiseStudent { get; set; }
       public List<AttendanceSummary> lstAttendanceSummary { get; set; }

       public List<MaleFemaleRatioInstituteWise> lstMaleFemaleRatio { get; set; }
    }
}
