using deepp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.ViewModels
{
    public class VmOnlineAdmission
    {
        public AdmissionForm AdmissionForm { get; set; }
        public IEnumerable<AdmissionFormAddress> AdmissionFormAddress{ get; set; }
        public AdmissionFormAddress AdmissionFormAddres { get; set; }
        public AdmissionFormGuardian AdmissionFormGuardians { get; set; }
        public IEnumerable<AdmissionFormGuardian> AdmissionFormGuardian { get; set; }
    }

}
