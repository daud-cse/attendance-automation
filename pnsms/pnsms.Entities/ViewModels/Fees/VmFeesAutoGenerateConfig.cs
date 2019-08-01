using pnsms.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.ViewModels.Fees
{
    public class VmFeesAutoGenerateConfig
    {
        public FeesAutoGenerateConfig FeesAutoGenerateConfig { get; set; }
        public List<KeyValuePair<int, string>> feesAutoGenTypeList { get; set; }
        public List<KeyValuePair<int, string>> FacilityList { get; set; }
        public List<KeyValuePair<int, string>> BranchList { get; set; }
        public List<KeyValuePair<int, string>> VersionList { get; set; }
        public List<KeyValuePair<int, string>> ClassList { get; set; }
        public List<KeyValuePair<int, string>> ShiftList { get; set; }
        public List<KeyValuePair<int, string>> GroupList { get; set; }
        public IEnumerable<FeesAutoGenerateConfigDetail> FeesGenerateHeadList { get; set; }

        public List<KeyValuePair<int, string>> selectedBranches { get; set; }
        public List<KeyValuePair<int, string>> selectedVersions { get; set; }
        public List<KeyValuePair<int, string>> selectedClasses { get; set; }
        public List<KeyValuePair<int, string>> selectedGroups { get; set; }
        public List<KeyValuePair<int, string>> selectedShifts { get; set; }

    }
}
