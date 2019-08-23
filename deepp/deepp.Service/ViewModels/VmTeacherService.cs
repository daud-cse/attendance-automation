using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using deepp.Entities.ViewModels.Teacher;
using deepp.utility.Resource;
using Repository.Pattern.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using deepp.utility;

namespace deepp.Service.ViewModels
{
    public interface IVmTeacherService
    {
        VmTeacher GetNewVmTeacher(int instituteId,int userId);
        VmTeacher GetVmTeacherById(int instituteId, int userId, int teacherId);
        VmTeacher CreateTeacher(IUnitOfWorkAsync unitOfWorkAsync, int instituteId, VmTeacher vmTeacher);
        VmTeacher UpdateTeacher(IUnitOfWorkAsync unitOfWorkAsync, VmTeacher vmTeacher);
        IEnumerable<VmTeacherDetails> GetAllTeacherDetails(int instituteId);
        IEnumerable<VmTeacherDetails> GetAllTeacherDetails(int instituteId, Teacher teacher);
    }
    public class VmTeacherService : IVmTeacherService
    {

        private readonly ITeacherService _teacherService;
        private readonly IUserInfoService _userInfoService;
        private readonly IAddressService _addressService;
        private readonly ICountryService _countryService;
        private readonly IDistrictOrStateService _districtOrStateService;
        private readonly IAcademicBranchesOfUserInfoService _academicBranchesOfUserInfoService;
        private readonly IImageService _imageService;
        private readonly IRolesOfUserInfoService _rolesOfUserInfoService;
        private readonly IRoleService _roleService;
        private readonly IAcademicBranchService _academicBranchService;

        public VmTeacherService(
            ITeacherService teacherService,
            IUserInfoService userInfoService,
            IAddressService addressService,
            ICountryService countryService,
            IDistrictOrStateService districtOrStateService,
            IAcademicBranchesOfUserInfoService academicBranchesOfUserInfoService,
            IImageService imageService,
            IRolesOfUserInfoService rolesOfUserInfoService,
            IRoleService roleService,
            IAcademicBranchService academicBranchService
            )
        {

            _teacherService = teacherService;

            _userInfoService = userInfoService;
            _addressService = addressService;
            _countryService = countryService;
            _districtOrStateService = districtOrStateService;
            _academicBranchesOfUserInfoService = academicBranchesOfUserInfoService;
            _imageService = imageService;
            _rolesOfUserInfoService = rolesOfUserInfoService;
            _roleService = roleService;
            _academicBranchService = academicBranchService;
        }

        public VmTeacher GetNewVmTeacher(int instituteId, int userId)
        {
            var obVmTeacher = new VmTeacher();

            var lstCountryListKv = new List<KeyValuePair<int, string>>();
            _countryService.GetCountryByInstituteId(instituteId).ToList().ForEach(item => lstCountryListKv.Add(new KeyValuePair<int, string>(item.Id, item.Name)));

            obVmTeacher.CountryList = lstCountryListKv;
            obVmTeacher.DistrictOrStateList = _districtOrStateService.GetActiveDistrictOrStateByinstituteId(instituteId).Select(s => new DistrictOrState() { Id = s.Id, CountryId = s.CountryId, Name = s.Name }).ToList();
            obVmTeacher.UserInfo = _userInfoService.NewUserInfo(instituteId);
            // roles for teacher
            obVmTeacher.UserInfo.RoleList = _roleService.GetRolesForTeacher(instituteId).Select(r => new KeyValuePair<int, string>(r.Id, r.Name));
            
            obVmTeacher.UserInfo.Teacher = _teacherService.NewTeacher(instituteId,userId);
            obVmTeacher.SingleAddresses = _addressService.NewAddress(instituteId);
            return obVmTeacher;
        }
        public VmTeacher GetVmTeacherById(int instituteId,int userId, int teacherId)
        {

            var teacher = _teacherService.GetTeacherById(teacherId);
            if (teacher == null)
            {
                throw new ValidationException(Errors.InvalidTeacher);
            }
            var vmTeacher = new VmTeacher();
            var lstCountryListKv = new List<KeyValuePair<int, string>>();
            _countryService.GetCountryByInstituteId(instituteId).ToList().ForEach(item => lstCountryListKv.Add(new KeyValuePair<int, string>(item.Id, item.Name)));

            // country list
            vmTeacher.CountryList = lstCountryListKv;
            vmTeacher.DistrictOrStateList = _districtOrStateService.GetDistrictOrStates().Select(s => new DistrictOrState() { Id = s.Id, CountryId = s.CountryId, Name = s.Name }).ToList();

            // New objects 
            vmTeacher.SingleAddresses = _addressService.NewAddress(instituteId);

            vmTeacher.UserInfo = _userInfoService.NewUserInfo(instituteId);
            vmTeacher.UserInfo.Teacher = _teacherService.NewTeacher(instituteId, userId);
            // Mapping teacher and userinfo 
            var userInfo = _userInfoService.GetUserById(teacherId);
            vmTeacher.UserInfo = MapUserInfo(vmTeacher.UserInfo, userInfo);
            vmTeacher.UserInfo.Teacher = MapTeacher(vmTeacher.UserInfo.Teacher, teacher);
            var lstBranch = _academicBranchesOfUserInfoService.GetAcademicBranchesOfUserInfo(teacherId).Select(s => s.AcademicBranchId);
            if (!lstBranch.Any())
            {
                lstBranch= _academicBranchService.GetAcademicBranchs(instituteId).Select(s => s.Id);
            }
            vmTeacher.UserInfo.Teacher.AcademicBranches = lstBranch.ToList();

            // Role
            var rolesId = _rolesOfUserInfoService.GetRolesOfUserInfoByUserId(teacherId).Select(s => s.RoleId);


            //if (!rolesId.Any())
            //{
               

                vmTeacher.UserInfo.RoleList=_roleService.GetRolesForTeacher(instituteId).Select(r => new KeyValuePair<int, string>(r.Id, r.Name));
           // }
            vmTeacher.UserInfo.UserRoles = rolesId.ToList();
            // address 
            vmTeacher.Addresses = _addressService.GetAddressesByUserId(teacherId).Select(MapAddress).ToList();

            // image
            //var image = _imageService.GetImageByRefTypeIdAndRefPrimaryKey((int)utility.RefCode.UserInfo_Photo,
            //    teacherId).FirstOrDefault();
            //if (image != null)
            //    vmTeacher.ProfileImage = image.ImageBinaryData;

            return vmTeacher;
        }

        public IEnumerable<VmTeacherDetails> GetAllTeacherDetails(int instituteId)
        {
            var teacher = _teacherService.GetAllTeacher(instituteId).Select(MapTeacherDetails);
            teacher = teacher.OrderBy(x => x.DesignationOrdering);
            return teacher;
        }
        public IEnumerable<VmTeacherDetails> GetAllTeacherDetails(int instituteId, Teacher teacher)
        {

            var searchText = teacher.UserInfo != null ? teacher.UserInfo.Name : "";
            var teachers = _teacherService.GetAllTeacher(instituteId, searchText, teacher.CurrentAcademicBranchId).Select(MapTeacherDetails);
            teachers = teachers.OrderBy(x => x.DesignationOrdering);
            return teachers;
        }

        public VmTeacher CreateTeacher(IUnitOfWorkAsync unitOfWorkAsync, int instituteId, VmTeacher vmTeacher)
        {
            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);
                vmTeacher.UserInfo.InstituteId = instituteId;
                vmTeacher.UserInfo.IsActive = true;
                vmTeacher.UserInfo.ContactNumber1 = MobileNumber.Rectify(vmTeacher.UserInfo.ContactNumber1);
                vmTeacher.UserInfo.ContactNumber2 = MobileNumber.Rectify(vmTeacher.UserInfo.ContactNumber2);

                vmTeacher.UserInfo.UserInfoTypeId = (int)utility.UserInfoType.Teacher;
                // save teacher & user
                _userInfoService.Insert(vmTeacher.UserInfo);
                _teacherService.Insert(vmTeacher.UserInfo.Teacher);
                unitOfWorkAsync.SaveChanges();
                // save branch
                if (vmTeacher.UserInfo.Teacher.AcademicBranches != null)
                {
                    foreach (var bid in vmTeacher.UserInfo.Teacher.AcademicBranches)
                    {
                        _academicBranchesOfUserInfoService.Insert(new AcademicBranchesOfUserInfo() { UserInfoId = vmTeacher.UserInfo.Id, AcademicBranchId = bid });
                    }
                    unitOfWorkAsync.SaveChanges();
                }
                // save address 
                if (vmTeacher.Addresses != null)
                {
                    foreach (var address in vmTeacher.Addresses)
                    {
                        address.RefPrimaryKey = vmTeacher.UserInfo.Id;
                        address.IsActive = true;
                        _addressService.Insert(address);
                    }
                    unitOfWorkAsync.SaveChanges();
                }

                // save image
                if (vmTeacher.ProfileImage != null && vmTeacher.ProfileImage.Length > 0)
                {
                    var image = new Image()
                    {
                        RefTypeId = (int)utility.RefCode.UserInfo_Photo,
                        RefPrimaryKey = vmTeacher.UserInfo.Id,
                        ImageBinaryData = vmTeacher.ProfileImage
                    };
                    var imageSmall = new Image()
                    {
                        RefTypeId = (int)utility.RefCode.UserInfo_Photo_Small,
                        RefPrimaryKey = vmTeacher.UserInfo.Id,
                        ImageBinaryData = vmTeacher.ProfileImageSmall
                    };

                    _imageService.Insert(image);
                    _imageService.Insert(imageSmall);
                    unitOfWorkAsync.SaveChanges();
                }
                // save role
                if (vmTeacher.UserInfo.UserRoles != null)
                {
                    foreach (var bid in vmTeacher.UserInfo.UserRoles)
                    {
                        _rolesOfUserInfoService.Insert(new RolesOfUserInfo() { UserInfoId = vmTeacher.UserInfo.Id, RoleId = bid });
                    }
                    unitOfWorkAsync.SaveChanges();
                }
                unitOfWorkAsync.Commit();
            }
            catch(Exception exp)
            {
                unitOfWorkAsync.Rollback();
                throw new Exception(exp.Message);
            }
            return vmTeacher;
        }
        public VmTeacher UpdateTeacher(IUnitOfWorkAsync unitOfWorkAsync, VmTeacher vmTeacher)
        {

            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);
                
                vmTeacher.UserInfo.ContactNumber1 = MobileNumber.Rectify(vmTeacher.UserInfo.ContactNumber1);
                vmTeacher.UserInfo.ContactNumber2 = MobileNumber.Rectify(vmTeacher.UserInfo.ContactNumber2);

                _userInfoService.Update(vmTeacher.UserInfo);
                _teacherService.Update(vmTeacher.UserInfo.Teacher);
                unitOfWorkAsync.SaveChanges();

                //address 
                if (vmTeacher.Addresses != null)
                {
                    foreach (var address in vmTeacher.Addresses)
                    {
                        // address.RefPrimaryKey = VmTeacher.UserInfo.Id;
                        if (address.Id == 0)
                        {
                            address.RefPrimaryKey = vmTeacher.UserInfo.Id;
                            address.IsActive = true;
                            _addressService.Insert(address);
                        }
                        else
                        {
                            _addressService.Update(address);
                        }

                    }
                    unitOfWorkAsync.SaveChanges();
                }

                // delete 
                _academicBranchesOfUserInfoService.DeleteAcademicBranchesOfUserInfo(vmTeacher.UserInfo.Id);
                // save branch
                if (vmTeacher.UserInfo.Teacher.AcademicBranches != null)
                {
                    foreach (var bid in vmTeacher.UserInfo.Teacher.AcademicBranches)
                    {
                        _academicBranchesOfUserInfoService.Insert(new AcademicBranchesOfUserInfo() { UserInfoId = vmTeacher.UserInfo.Id, AcademicBranchId = bid });
                    }
                    unitOfWorkAsync.SaveChanges();
                }

                // update image
                if (vmTeacher.ProfileImage != null && vmTeacher.ProfileImage.Length > 0)
                {
                    var image = _imageService.GetImageByRefTypeIdAndRefPrimaryKey((int)utility.RefCode.UserInfo_Photo,
                        vmTeacher.UserInfo.Id).FirstOrDefault();
                    var imageSmall = _imageService.GetImageByRefTypeIdAndRefPrimaryKey((int)utility.RefCode.UserInfo_Photo_Small,
                        vmTeacher.UserInfo.Id).FirstOrDefault();

                    if (image != null)
                    {
                        image.ImageBinaryData = vmTeacher.ProfileImage;
                        imageSmall.ImageBinaryData = vmTeacher.ProfileImageSmall;
                        _imageService.Update(image);
                        _imageService.Update(imageSmall);

                    }

                    else
                    {
                        image = new Image()
                        {
                            RefTypeId = (int)utility.RefCode.UserInfo_Photo,
                            RefPrimaryKey = vmTeacher.UserInfo.Id,
                            ImageBinaryData = vmTeacher.ProfileImage
                        };
                        imageSmall = new Image()
                        {
                            RefTypeId = (int)utility.RefCode.UserInfo_Photo_Small,
                            RefPrimaryKey = vmTeacher.UserInfo.Id,
                            ImageBinaryData = vmTeacher.ProfileImageSmall
                        };
                        _imageService.Insert(image);
                        _imageService.Insert(imageSmall);
                    }

                    unitOfWorkAsync.SaveChanges();
                }
                // delete role
                _rolesOfUserInfoService.DeleteByUserId(unitOfWorkAsync, vmTeacher.UserInfo.Id);
                // save role
                if (vmTeacher.UserInfo.UserRoles != null)
                {
                    foreach (var bid in vmTeacher.UserInfo.UserRoles)
                    {
                        _rolesOfUserInfoService.Insert(new RolesOfUserInfo() { UserInfoId = vmTeacher.UserInfo.Id, RoleId = bid });
                    }
                    unitOfWorkAsync.SaveChanges();
                }
                unitOfWorkAsync.Commit();
                return vmTeacher;
            }
            catch
            {
                unitOfWorkAsync.Rollback();
                throw new Exception("Error");
            }

        }


        #region Helper Method

        private UserInfo MapUserInfo(UserInfo newuserInfo, UserInfo sourceUserInfo)
        {
            newuserInfo.Id = sourceUserInfo.Id;
            newuserInfo.UserInfoTypeId = sourceUserInfo.UserInfoTypeId;
            newuserInfo.InstituteId = sourceUserInfo.InstituteId;
            newuserInfo.PIN = sourceUserInfo.PIN;
            newuserInfo.FirstName = sourceUserInfo.FirstName;
            newuserInfo.MiddleName = sourceUserInfo.MiddleName;
            newuserInfo.LastName = sourceUserInfo.LastName;
            newuserInfo.Name = sourceUserInfo.Name;
            newuserInfo.ContactNumber1 = sourceUserInfo.ContactNumber1;
            newuserInfo.ContactNumber2 = sourceUserInfo.ContactNumber2;
            newuserInfo.EmailAddress = sourceUserInfo.EmailAddress;
            newuserInfo.SSN = sourceUserInfo.SSN;
            newuserInfo.PassportNo = sourceUserInfo.PassportNo;
            newuserInfo.DOB = sourceUserInfo.DOB;
            newuserInfo.GenderId = sourceUserInfo.GenderId;
            newuserInfo.NationalityId = sourceUserInfo.NationalityId;
            newuserInfo.ReligionId = sourceUserInfo.ReligionId;
            newuserInfo.BloodGroupId = sourceUserInfo.BloodGroupId;
            newuserInfo.GoogleId = sourceUserInfo.GoogleId;
            newuserInfo.FacebookId = sourceUserInfo.FacebookId;
            newuserInfo.MicrosoftId = sourceUserInfo.MicrosoftId;
            newuserInfo.TwitterId = sourceUserInfo.TwitterId;
            newuserInfo.IsActive = sourceUserInfo.IsActive;
            newuserInfo.AboutUser = sourceUserInfo.AboutUser;
            return newuserInfo;
        }
        /// <summary>
        /// Maps the teacher.
        /// </summary>
        /// <param name="newTeacher">The new teacher.</param>
        /// <param name="sourceTeacher">The source teacher.</param>
        /// <returns></returns>
        private Teacher MapTeacher(Teacher newTeacher, Teacher sourceTeacher)
        {
            newTeacher.TeacherId = sourceTeacher.TeacherId;
            newTeacher.LastUpdateTime = sourceTeacher.LastUpdateTime;

            newTeacher.CurrentAcademicBranchId = sourceTeacher.CurrentAcademicBranchId;
            newTeacher.DefaultAcademicClassId = sourceTeacher.DefaultAcademicClassId;
            newTeacher.DefaultAcademicSectionId = sourceTeacher.DefaultAcademicSectionId;
            newTeacher.MaritalStatusId = sourceTeacher.MaritalStatusId;
            newTeacher.DesignationId = sourceTeacher.DesignationId;
            newTeacher.DepartmentId = sourceTeacher.DepartmentId;
            newTeacher.FatherName = sourceTeacher.FatherName;
            newTeacher.MotherName = sourceTeacher.MotherName;
            newTeacher.AboutTeacher = sourceTeacher.AboutTeacher;
            return newTeacher;
        }
        /// <summary>
        /// Maps the teacher details.
        /// </summary>
        /// <param name="teacher">The teacher.</param>
        /// <returns></returns>
        private VmTeacherDetails MapTeacherDetails(Teacher teacher)
        {
            return new VmTeacherDetails()
            {
                InstituteId = teacher.UserInfo.InstituteId.Value,
                TeacherId = teacher.TeacherId,
                Designation = teacher.Designation != null ? teacher.Designation.Name : string.Empty,
                AcademicBranch = teacher.UserInfo.AcademicBranchesOfUserInfoes.Any() ? string.Join(", ", teacher.UserInfo.AcademicBranchesOfUserInfoes.Select(s => s.AcademicBranch.Name)) : "",
                Name = teacher.UserInfo.Name,
                DesignationOrdering = teacher.Designation.Ordering,
                IsActive = teacher.UserInfo.IsActive,
                LastUpdateTime = teacher.LastUpdateTime,
            };
        }
        private Address MapAddress(Address address)
        {
            address.AddressType = null;
            address.DistrictOrState = null;
            return address;
        }
        #endregion



    }

}
