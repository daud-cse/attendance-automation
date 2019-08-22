using pnsms.Entities.Models;
using pnsms.Entities.ViewModels.GlobalUsers;
using pnsms.utility;
using pnsms.utility.Resource;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service.GlobalUsers
{
  
    public interface IVmGlobalUsersService
    {
        VmGlobalUsers GetNewVmGlobalUsers(int instituteId, int userId);
        VmGlobalUsers GetVmGlobalUsersById(int instituteId, int userId, int employeeId);
        VmGlobalUsers CreateEmployee(IUnitOfWorkAsync unitOfWorkAsync, int instituteId, VmGlobalUsers VmGlobalUsers);
        VmGlobalUsers UpdateEmployee(IUnitOfWorkAsync unitOfWorkAsync, VmGlobalUsers VmGlobalUsers);
        IEnumerable<VmGlobalUsersDetails> GetAllEmployeeDetails(int instituteId, string searchText = "", int? branchId = null);
    }
    public class VmGlobalUsersService : IVmGlobalUsersService
    {

        private readonly IEmployeeService _employeeService;
        private readonly IUserInfoService _userInfoService;
        private readonly IAddressService _addressService;
        private readonly ICountryService _countryService;
        private readonly IDistrictOrStateService _districtOrStateService;
        private readonly IAcademicBranchesOfUserInfoService _academicBranchesOfUserInfoService;
        private readonly IImageService _imageService;
        private readonly IRolesOfUserInfoService _rolesOfUserInfoService;
        private readonly IRoleService _roleService;


        public VmGlobalUsersService(
            IEmployeeService employeeService,
            IUserInfoService userInfoService,
            IAddressService addressService,
            ICountryService countryService,
            IDistrictOrStateService districtOrStateService,
            IAcademicBranchesOfUserInfoService academicBranchesOfUserInfoService,
            IImageService imageService,
            IRolesOfUserInfoService rolesOfUserInfoService,
            IRoleService roleService)
        {

            _employeeService = employeeService;
            _userInfoService = userInfoService;
            _addressService = addressService;
            _countryService = countryService;
            _districtOrStateService = districtOrStateService;
            _academicBranchesOfUserInfoService = academicBranchesOfUserInfoService;
            _imageService = imageService;
            _rolesOfUserInfoService = rolesOfUserInfoService;
            _roleService = roleService;
        }

        public VmGlobalUsers GetNewVmGlobalUsers(int instituteId, int userId)
        {
            var obVmGlobalUsers = new VmGlobalUsers();

            var lstCountryListKv = new List<KeyValuePair<int, string>>();
            _countryService.GetCountryByInstituteId(instituteId).ToList().ForEach(item => lstCountryListKv.Add(new KeyValuePair<int, string>(item.Id, item.Name)));

            obVmGlobalUsers.CountryList = lstCountryListKv;
            obVmGlobalUsers.DistrictOrStateList = _districtOrStateService.GetActiveDistrictOrStateByinstituteId(instituteId).Select(s => new DistrictOrState() { Id = s.Id, CountryId = s.CountryId, Name = s.Name }).ToList();
            obVmGlobalUsers.UserInfo = _userInfoService.NewUserInfo(instituteId);
            // role for employee
            obVmGlobalUsers.UserInfo.RoleList = _roleService.GetRolesForEmployee(instituteId).Select(r => new KeyValuePair<int, string>(r.Id, r.Name));
            obVmGlobalUsers.UserInfo.Employee = _employeeService.NewEmployee(instituteId, userId);
            obVmGlobalUsers.SingleAddresses = _addressService.NewAddress(instituteId);
            return obVmGlobalUsers;
        }
        public VmGlobalUsers GetVmGlobalUsersById(int instituteId, int userId, int employeeId)
        {
            var employee = _employeeService.GetEmployeeById(employeeId);
            if (employee == null)
            {
                throw new ValidationException(Errors.InvalidEmployee);
            }
            var VmGlobalUsers = new VmGlobalUsers();

            var lstCountryListKv = new List<KeyValuePair<int, string>>();
            _countryService.GetCountryByInstituteId(instituteId).ToList().ForEach(item => lstCountryListKv.Add(new KeyValuePair<int, string>(item.Id, item.Name)));


            VmGlobalUsers.CountryList = lstCountryListKv;
            VmGlobalUsers.DistrictOrStateList = _districtOrStateService.GetDistrictOrStates().Select(s => new DistrictOrState() { Id = s.Id, CountryId = s.CountryId, Name = s.Name }).ToList();

            // New objects 
            VmGlobalUsers.SingleAddresses = _addressService.NewAddress(instituteId);

            VmGlobalUsers.UserInfo = _userInfoService.NewUserInfo(instituteId);
            VmGlobalUsers.UserInfo.Employee = _employeeService.NewEmployee(instituteId, userId);
            // Mapping employee and userinfo

            var userInfo = _userInfoService.GetUserById(employeeId);
            VmGlobalUsers.UserInfo = MapUserInfo(VmGlobalUsers.UserInfo, userInfo);
            VmGlobalUsers.UserInfo.Employee = MapEmployee(VmGlobalUsers.UserInfo.Employee, employee);
            // branch
            var lstBranch = _academicBranchesOfUserInfoService.GetAcademicBranchesOfUserInfo(employeeId).Select(s => s.AcademicBranchId);
            VmGlobalUsers.UserInfo.Employee.AcademicBranches = lstBranch.ToList();
            // Role
            var rolesId = _rolesOfUserInfoService.GetRolesOfUserInfoByUserId(employeeId).Select(s => s.RoleId);


            VmGlobalUsers.UserInfo.RoleList = _roleService.GetRolesForEmployee(instituteId).Select(r => new KeyValuePair<int, string>(r.Id, r.Name));

            VmGlobalUsers.UserInfo.UserRoles = rolesId.ToList();

            // address 
            VmGlobalUsers.Addresses = _addressService.GetAddressesByUserId(employeeId).Select(MapAddress).ToList();

            return VmGlobalUsers;
        }

        public IEnumerable<VmGlobalUsersDetails> GetAllEmployeeDetails(int instituteId, string searchText = "", int? branchId = null)
        {
            var employees = _employeeService.GetAllEmployee(instituteId, searchText, branchId).Select(MapEmployeeDetails);
            return employees;
        }

        public VmGlobalUsers CreateEmployee(IUnitOfWorkAsync unitOfWorkAsync, int instituteId, VmGlobalUsers VmGlobalUsers)
        {
            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);


                VmGlobalUsers.UserInfo.InstituteId = instituteId;
                VmGlobalUsers.UserInfo.IsActive = true;
                VmGlobalUsers.UserInfo.ContactNumber1 = MobileNumber.Rectify(VmGlobalUsers.UserInfo.ContactNumber1);
                VmGlobalUsers.UserInfo.ContactNumber2 = MobileNumber.Rectify(VmGlobalUsers.UserInfo.ContactNumber2);

                VmGlobalUsers.UserInfo.UserInfoTypeId = (int)utility.UserInfoType.Employee;
                // save employee & user
                _userInfoService.Insert(VmGlobalUsers.UserInfo);
                _employeeService.Insert(VmGlobalUsers.UserInfo.Employee);
                unitOfWorkAsync.SaveChanges();
                // save branch
                if (VmGlobalUsers.UserInfo.Employee.AcademicBranches != null)
                {
                    foreach (var bid in VmGlobalUsers.UserInfo.Employee.AcademicBranches)
                    {
                        _academicBranchesOfUserInfoService.Insert(new AcademicBranchesOfUserInfo() { UserInfoId = VmGlobalUsers.UserInfo.Id, AcademicBranchId = bid });
                    }
                    unitOfWorkAsync.SaveChanges();
                }


                // save address 
                if (VmGlobalUsers.Addresses != null)
                {
                    foreach (var address in VmGlobalUsers.Addresses)
                    {
                        address.RefPrimaryKey = VmGlobalUsers.UserInfo.Id;
                        address.IsActive = true;
                        _addressService.Insert(address);
                    }
                    unitOfWorkAsync.SaveChanges();
                }

                // save image
                if (VmGlobalUsers.ProfileImage != null && VmGlobalUsers.ProfileImage.Length > 0)
                {
                    var image = new Image()
                    {
                        RefTypeId = (int)utility.RefCode.UserInfo_Photo,
                        RefPrimaryKey = VmGlobalUsers.UserInfo.Id,
                        ImageBinaryData = VmGlobalUsers.ProfileImage
                    };
                    var imageSmall = new Image()
                    {
                        RefTypeId = (int)utility.RefCode.UserInfo_Photo_Small,
                        RefPrimaryKey = VmGlobalUsers.UserInfo.Id,
                        ImageBinaryData = VmGlobalUsers.ProfileImageSmall
                    };

                    _imageService.Insert(image);
                    _imageService.Insert(imageSmall);
                    unitOfWorkAsync.SaveChanges();
                }

                // save role
                if (VmGlobalUsers.UserInfo.UserRoles != null)
                {
                    foreach (var bid in VmGlobalUsers.UserInfo.UserRoles)
                    {
                        _rolesOfUserInfoService.Insert(new RolesOfUserInfo() { UserInfoId = VmGlobalUsers.UserInfo.Id, RoleId = bid });
                    }
                    unitOfWorkAsync.SaveChanges();
                }
                unitOfWorkAsync.Commit();
            }
            catch
            {
                unitOfWorkAsync.Rollback();
            }
            return VmGlobalUsers;
        }
        public VmGlobalUsers UpdateEmployee(IUnitOfWorkAsync unitOfWorkAsync, VmGlobalUsers VmGlobalUsers)
        {

            VmGlobalUsers.UserInfo.ContactNumber1 = MobileNumber.Rectify(VmGlobalUsers.UserInfo.ContactNumber1);
            VmGlobalUsers.UserInfo.ContactNumber2 = MobileNumber.Rectify(VmGlobalUsers.UserInfo.ContactNumber2);

            _userInfoService.Update(VmGlobalUsers.UserInfo);
            _employeeService.Update(VmGlobalUsers.UserInfo.Employee);
            unitOfWorkAsync.SaveChanges();
            // delete 
            _academicBranchesOfUserInfoService.DeleteAcademicBranchesOfUserInfo(VmGlobalUsers.UserInfo.Id);
            // save branch
            if (VmGlobalUsers.UserInfo.Employee.AcademicBranches != null)
            {
                foreach (var bid in VmGlobalUsers.UserInfo.Employee.AcademicBranches)
                {
                    _academicBranchesOfUserInfoService.Insert(new AcademicBranchesOfUserInfo() { UserInfoId = VmGlobalUsers.UserInfo.Id, AcademicBranchId = bid });
                }
                unitOfWorkAsync.SaveChanges();
            }

            //address 
            if (VmGlobalUsers.Addresses != null)
            {
                foreach (var address in VmGlobalUsers.Addresses)
                {
                    // address.RefPrimaryKey = VmGlobalUsers.UserInfo.Id;
                    if (address.Id == 0)
                    {
                        address.RefPrimaryKey = VmGlobalUsers.UserInfo.Id;
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
            // update image
            if (VmGlobalUsers.ProfileImage != null && VmGlobalUsers.ProfileImage.Length > 0)
            {
                var image = _imageService.GetImageByRefTypeIdAndRefPrimaryKey((int)utility.RefCode.UserInfo_Photo,
                    VmGlobalUsers.UserInfo.Id).FirstOrDefault();
                var imageSmall = _imageService.GetImageByRefTypeIdAndRefPrimaryKey((int)utility.RefCode.UserInfo_Photo_Small,
                    VmGlobalUsers.UserInfo.Id).FirstOrDefault();

                if (image != null)
                {
                    image.ImageBinaryData = VmGlobalUsers.ProfileImage;
                    imageSmall.ImageBinaryData = VmGlobalUsers.ProfileImageSmall;
                    _imageService.Update(image);
                    _imageService.Update(imageSmall);

                }

                else
                {
                    image = new Image()
                    {
                        RefTypeId = (int)utility.RefCode.UserInfo_Photo,
                        RefPrimaryKey = VmGlobalUsers.UserInfo.Id,
                        ImageBinaryData = VmGlobalUsers.ProfileImage
                    };
                    imageSmall = new Image()
                    {
                        RefTypeId = (int)utility.RefCode.UserInfo_Photo_Small,
                        RefPrimaryKey = VmGlobalUsers.UserInfo.Id,
                        ImageBinaryData = VmGlobalUsers.ProfileImageSmall
                    };
                    _imageService.Insert(image);
                    _imageService.Insert(imageSmall);
                }

                unitOfWorkAsync.SaveChanges();
            }
            // delete role
            _rolesOfUserInfoService.DeleteByUserId(unitOfWorkAsync, VmGlobalUsers.UserInfo.Id);
            // save role
            if (VmGlobalUsers.UserInfo.UserRoles != null)
            {
                foreach (var bid in VmGlobalUsers.UserInfo.UserRoles)
                {
                    _rolesOfUserInfoService.Insert(new RolesOfUserInfo() { UserInfoId = VmGlobalUsers.UserInfo.Id, RoleId = bid });
                }
                unitOfWorkAsync.SaveChanges();
            }
            return VmGlobalUsers;
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
            return newuserInfo;
        }
        private Employee MapEmployee(Employee newEmployee, Employee sourceEmployee)
        {
            newEmployee.EmployeeId = sourceEmployee.EmployeeId;
            newEmployee.LastUpdateTime = sourceEmployee.LastUpdateTime;

            //newEmployee.CurrentAcademicBranchId = sourceEmployee.CurrentAcademicBranchId;
            //newEmployee.DefaultAcademicClassId = sourceEmployee.DefaultAcademicClassId;
            //newEmployee.DefaultAcademicSectionId = sourceEmployee.DefaultAcademicSectionId;
            newEmployee.MaritalStatusId = sourceEmployee.MaritalStatusId;
            newEmployee.DesignationId = sourceEmployee.DesignationId;
            newEmployee.DepartmentId = sourceEmployee.DepartmentId;
            newEmployee.FatherName = sourceEmployee.FatherName;
            newEmployee.MotherName = sourceEmployee.MotherName;

            return newEmployee;
        }
        private VmGlobalUsersDetails MapEmployeeDetails(Employee employee)
        {
            return new VmGlobalUsersDetails()
            {

                InstituteId = employee.UserInfo.InstituteId.Value,
                GlobalUsersId = employee.EmployeeId,
                Designation = employee.Designation != null ? employee.Designation.Name : string.Empty,
                Department = employee.Department != null ? employee.Department.Name : string.Empty,
                Name = employee.UserInfo.Name,
                FirstName = employee.UserInfo.FirstName,
                MiddleName = employee.UserInfo.MiddleName,
                LastName = employee.UserInfo.LastName,
                LastUpdateTime = employee.LastUpdateTime,

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
