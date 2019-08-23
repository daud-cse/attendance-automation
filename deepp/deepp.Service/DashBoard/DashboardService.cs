using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using deepp.Entities.ViewModels.DashBoard;
using deepp.Service.GlobalUsers;
using deepp.Entities.Models;

namespace deepp.Service.DashBoard
{
    public interface IDashboardService
    {
        Dashboard GetDashboard(int UserId,int UserInfoType);
       // UserInfo GetUserAndInstituteInfoByUserId(int UserId);
    }
    public class DashboardService : IDashboardService
    {
       // private readonly IInstituteService _instituteService;
        private readonly IStudentService _studentService;
        private readonly IStoredProcedureService _storedProcedureService;
       // private readonly IUserInfoService _userInfoService;
        private readonly IGlobalUserService _globalUserService;

        public DashboardService(
            //IInstituteService instituteService
           // ,
            IStudentService studentService
            , IStoredProcedureService storedProcedureService
          //  , IUserInfoService userInfoService
            , IGlobalUserService globalUserService
            )
        {
           // _instituteService = instituteService;
            _studentService = studentService;
            _storedProcedureService = storedProcedureService;
           // _userInfoService = userInfoService;
            _globalUserService = globalUserService;
        }

        public int getTypeByUserId(int userId)
        {
            return 0;
        }
        //public UserInfo GetUserAndInstituteInfoByUserId(int UserId)
        //{
        //    return _userInfoService.GetUserAndInstituteInfoByUserId(UserId);
        //}
        public Dashboard GetDashboard(int UserId, int UserInfoTypeId)
        {
            string GlobalPlaceName = string.Empty;
            int CountryId=0;
            int DivisionId=0;
            int DistrictId=0;
            int UpaThanaId=0;
            int UnionId=0;
            int userType=0;
            int InstituteId=0;
            int BrancheId=0;
            if (UserInfoTypeId == 15)//Golbal User
            {
                var userInfo = _globalUserService.GetGlobalUserById(UserId);
                CountryId = userInfo.GlobalCountryId==null?0: Convert.ToInt16(userInfo.GlobalCountryId);
                
                DivisionId = userInfo.GlobalDivisionId == null ? 0 : Convert.ToInt16(userInfo.GlobalDivisionId);

                DistrictId = userInfo.GlobalDistrictId == null ? 0 : Convert.ToInt16(userInfo.GlobalDistrictId);
                if (DistrictId!=0)
                {
                    GlobalPlaceName = userInfo.GlobalCountryId == null ? "" : userInfo.GlobalDistrict.Name;
                }

                UpaThanaId = userInfo.GlobalSubDistrictId == null ? 0 : Convert.ToInt16(userInfo.GlobalSubDistrictId);
                if (UpaThanaId!=0)
                {
                    GlobalPlaceName = userInfo.GlobalCountryId == null ? "" : userInfo.GlobalSubDistrict.Name;
                }
                             
            }                     
            var objDashboard = _storedProcedureService.GetGlobalDashBoard(CountryId, DivisionId, DistrictId,UpaThanaId, 0, 0,0,0);
            objDashboard.GlobalPlaceName = GlobalPlaceName;
            return objDashboard;
        }
    }
}
