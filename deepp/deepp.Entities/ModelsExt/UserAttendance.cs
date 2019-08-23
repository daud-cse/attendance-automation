using deepp.Entities.ModelsExt;
using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.Models
{
    [MetadataType(typeof(UserAttendanceMetadata))]
    public partial class UserAttendance : Entity
    {
        public List<KeyValuePair<int, string>> AcademicBranchList { get; set; }
        public string BranchName;
    }

    public class UserAttendanceListModel : Entity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int? TotalStudent { get; set; }
        public int? PresentCount { get; set; }
        public int? AbsentCount { get; set; }
        public int? AbscondingCount { get; set; }
        public decimal? PresentPercent { get; set; }
        public decimal? AbsentPercent { get; set; }
        public DateTime Date { get; set; }
    }
}
