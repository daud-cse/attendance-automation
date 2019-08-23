using deepp.Entities.Models;
using deepp.utility;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service
{
    /// <summary>
    /// user authentication service
    /// </summary>
    public interface IUserInfoSecurityService : IService<UserInfoSecurity>
    {
        /// <summary>
        /// Developed by Akram
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        UserInfoSecurity GetByUserId(int Id);

        /// <summary>
        /// Developed by Akram
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="instituteId"></param>
        /// <returns></returns>
        UserInfoSecurity GetByUserLoginName(string userName, int instituteId);
        UserInfoSecurity GetByUserLoginName(string userName);



        /// <summary>
        /// Developed by Akram
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="instituteId"></param>
        /// <returns></returns>
        ErrorCode Insert(IUnitOfWork unitOfWork, string userName, string password, int instituteId, int userInfoId);

        /// <summary>
        /// Developed by Akram
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        ErrorCode UpdatePassword(IUnitOfWork unitOfWork, int userInfoId, string password);
        /// <summary>
        /// Developed by daud
        /// </summary>
        /// <param name="userLoginId"></param>
        /// <param name="instituteId"></param>
        /// <returns></returns>
        UserInfoSecurity GetByUserLoginId(int userLoginId, int instituteId);
    }

    public class UserInfoSecurityService : Service<UserInfoSecurity>, IUserInfoSecurityService
    {
        readonly IRepositoryAsync<UserInfoSecurity> redeeppitory;

        public UserInfoSecurityService(IRepositoryAsync<UserInfoSecurity> redeeppitory)
            : base(redeeppitory)
        {
            this.redeeppitory = redeeppitory;
        }

        public UserInfoSecurity GetByUserId(int Id)
        {
            return redeeppitory.Query(c => c.UserInfoId==Id)
                .Select()
                .FirstOrDefault();            
        }

        public UserInfoSecurity GetByUserLoginName(string userName, int instituteId)
        {
            return redeeppitory.Query(c => c.UserName == userName && c.InstituteId == instituteId)
                .Include(c=>c.UserInfo)
                .Select()
                .FirstOrDefault();
        }
        public UserInfoSecurity GetByUserLoginName(string userName)
        {
            return redeeppitory.Query(c => c.UserName == userName)
                .Include(c => c.UserInfo)
                .Select()
                .FirstOrDefault();
        }
        public UserInfoSecurity GetByUserLoginId(int serLoginId, int instituteId)
        {
            return redeeppitory.Query(c => c.UserInfoId == serLoginId && c.InstituteId == instituteId)
                .Select()
                .FirstOrDefault();
        }

        public ErrorCode UpdatePassword(IUnitOfWork unitOfWork, int userInfoId, string password)
        {
            try
            {
                var user = GetByUserId(userInfoId);
                user.PasswordHash = EncryptionDecreption.EncryptToMD5(password);
                user.LastPasswordChangedAt = DateTime.Now;

                redeeppitory.Update(user);
                unitOfWork.SaveChanges();
            }
            catch
            {
                return ErrorCode.UserSecurity_PasswordNotChanged;
            }

            return ErrorCode.NoError;
        }


        public ErrorCode Insert(IUnitOfWork unitOfWork, string userName, string password, int instituteId, int userInfoId)
        {
            UserInfoSecurity entity = new UserInfoSecurity();

            // line comments for Global User Checking
            // var userIfExists = GetByUserLoginName(userName, instituteId); 
            var userIfExists = GetByUserLoginName(userName);
            if (userIfExists != null)
            {
                return ErrorCode.UserSecurity_UserNameExists;
            }

            try
            {
                entity.UserName = userName;
                entity.PasswordHash = EncryptionDecreption.EncryptToMD5(password);
                entity.InstituteId = instituteId;
                entity.UserInfoId = userInfoId;
                entity.IsActive = true;
                entity.IsLockout = false;

                redeeppitory.Insert(entity);
                unitOfWork.SaveChanges();
            }
            catch
            {
                return ErrorCode.UserSecurity_UserNameExists;
            }


            return ErrorCode.NoError;
        }
        
        
    }
}
