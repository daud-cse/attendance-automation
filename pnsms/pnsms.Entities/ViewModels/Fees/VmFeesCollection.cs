using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pnsms.Entities.Models;

namespace pnsms.Entities.ViewModels.Fees
{
    public class VmFeesCollection
    {
        public string StudentName { get; set; }
        public string SectionName { get; set; }
        public string ClassName { get; set; }

        public int StudentId { get; set; }
        public int InstituteId { get; set; }
        public string Session { get; set; }
        public int SessionId { get; set; }
        public string Branch { get; set; }
        public int Class { get; set; }
        public int Section { get; set; }
        public string Roll { get; set; }
        public DateTime CollectionDate { get; set; }

        public VmFeesCollection() { }

        //public List<Models.FeesAcademicClass> studentFees { get; set; }
        //public List<FeesCollection> MonthlyAmount { get; set; }

        public List<VmStudentFees> StudentFeesList { get; set; }
    }
    public class VmStudentFees
    {
        public VmStudentFees() { }
        public string MonthName { get; set; }
        public string FeesHeadName { get; set; }
        public int FeesHeadId { get; set; }
        public decimal FeesAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get; set; }
    }
    public class VmStudentFeesCollection
    {
        public int InstituteId { get; set; }
        public string InstituteName { get; set; }
        public int FeesCollectionId { get; set; }
        public string CollectionDate { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int MonthId { get; set; }
        public string AcademicClassName { get; set; }

        public string MonthName { get; set; }
        public string FeesHeadName { get; set; }
        public int FeesHeadId { get; set; }
        public decimal FeesAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public decimal AdvanceAmountPayment { get; set; }
    }
}
