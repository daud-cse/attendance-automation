using pnsms.Entities.StoredProcedures.Models;
using pnsms.Entities.ViewModels.DashBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service.DashBoard
{
    public interface IMaleFemaleRatioService
    {
        MaleFemaleRatio GetMaleFemaleRatioByInstituteId(int instituteId);
        MaleFemaleRatio GetMaleFemaleRatio(int instituteId, int upaThanaId, int unionId);
    }
    public class MaleFemaleRatioService : IMaleFemaleRatioService
    {
        private readonly IStudentService _studentService;
        public MaleFemaleRatioService(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public MaleFemaleRatio GetMaleFemaleRatioByInstituteId(int instituteId)
        {
            MaleFemaleRatio maleFemaleRatio = new MaleFemaleRatio();
            return maleFemaleRatio;
        }
        public MaleFemaleRatio GetMaleFemaleRatio(int instituteId, int upaThanaId, int unionId)
        {
            MaleFemaleRatio maleFemaleRatio = new MaleFemaleRatio();
            var student = _studentService.GetAllStudent(upaThanaId);
            //maleFemaleRatio.TotalSchool = student.ToList().Count(x => x.UserInfo.Institute.GlobalSubDistrictId == upaThanaId);
            //maleFemaleRatio.TotalNumberOfMale = student.ToList().Count(x => x.UserInfo.Institute.GlobalSubDistrictId == upaThanaId && x.UserInfo.Gender.Id == 2);
            //maleFemaleRatio.TotalNumberOfFeMale = student.ToList().Count(x => x.UserInfo.Institute.GlobalSubDistrictId == upaThanaId && x.UserInfo.Gender.Id == 3);
            return maleFemaleRatio;
        }
    }
}
