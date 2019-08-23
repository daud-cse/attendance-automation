using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using deepp.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;
using deepp.utility;

namespace deepp.Service
{
    public interface IUserInfoService : IService<UserInfo>
    {
        UserInfo NewUserInfo(int instituteId);
        UserInfo GetUserById(int id);
        UserInfo GetUserAndInstituteInfoByUserId(int id);
        
        /// <summary>
        /// by Anirban
        /// </summary>
        /// <param name="pin"></param>
        ///  <param name="contactNo"></param>
        /// <param name="instituteId"></param>
        /// <param name="userTypeId"></param>
        /// <returns>UserInfo Table</returns>
        UserInfo GetUserDetailsByPinOrMobile(string pin, string contactNo, int instituteId, int userTypeId);

        /// <summary>
        /// 
        /// Developed by Akram
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <returns>List of UserInfo</returns>
        IEnumerable<UserInfo> GetByMobileNumber(string mobileNumber, int instituteId);
    }

    public class UserInfoService:Service<UserInfo>, IUserInfoService
    {
        private readonly IRepositoryAsync<UserInfo> _redeeppitory;
        private readonly IBloodGroupService _bloodGroupService;
        private readonly IGenderService _genderService;
        private readonly INationalityService _nationalityService;
        private readonly IReligionService _religionService;
        private readonly IRoleService _roleService;

        public UserInfoService(IRepositoryAsync<UserInfo> redeeppitory, 
            IBloodGroupService bloodGroupService,
            IGenderService genderService,
            INationalityService nationalityService,
            IReligionService religionService,
            IRoleService roleService)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
            _bloodGroupService = bloodGroupService;
            _genderService = genderService;
            _nationalityService = nationalityService;
            _religionService = religionService;
            _roleService = roleService;
        }

        public UserInfo NewUserInfo(int instituteId)
        {
            var userInfo = new UserInfo();
            userInfo.IsActive = true;
            userInfo.GenderList =_genderService.GetGenderByInstituteId(instituteId).Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList();
            userInfo.NationalityList = _nationalityService.GetNationalityByInstituteId(instituteId).Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList();
            userInfo.ReligionList = _religionService.GetReligionByInstituteId(instituteId).Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList();
            userInfo.BloodGroupList = _bloodGroupService.GetBloodGroupsByInstituteId(instituteId).Select(x=> new KeyValuePair<int,string>(x.Id,x.Name));
            //userInfo.RoleList= _roleService.GetRoles(instituteId, true).Select(r => new KeyValuePair<int, string>(r.Id, r.Name));
            return userInfo;
        }

        public UserInfo GetUserById(int id)
        {
            var userInfo = _redeeppitory.Query(x => x.Id == id).Select().FirstOrDefault();
            return userInfo;
        }

        public UserInfo GetUserAndInstituteInfoByUserId(int id)
        {
            var userInfo = _redeeppitory.Query(x => x.Id == id)
                .Include(x=> x.Institute)
                .Include(x=>x.Institute.AcademicSessions)
                .Select().FirstOrDefault();
            return userInfo;
        }

        public UserInfo GetUserDetailsByPinOrMobile(string pin, string contactNo, int instituteId, int userTypeId)
        {
            var predicate = PredicateBuilder.True<UserInfo>();
            predicate = predicate.And(p => p.IsActive && p.InstituteId == instituteId && p.UserInfoTypeId == userTypeId);

            if (pin != null)
                predicate = predicate.And(p => p.PIN == pin);

            if (contactNo != null)
                predicate = predicate.And(p => p.ContactNumber1 == contactNo || p.ContactNumber2 == contactNo);

            return _redeeppitory.Query(predicate).Select().FirstOrDefault();
            
        }

        public IEnumerable<UserInfo> GetByMobileNumber(string mobileNumber, int instituteId)
        {
            return _redeeppitory.Query(x => x.InstituteId==instituteId
                &&(x.ContactNumber1 == mobileNumber || x.ContactNumber2 == mobileNumber))
                .Include(x => x.UserInfoSecurity)                
                .Select()
                ;
        }
    }
}
