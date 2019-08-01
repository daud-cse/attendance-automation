using System.ComponentModel.DataAnnotations;
using pnsms.Entities.Models;
using pnsms.Entities.ViewModels.Employee;
using pnsms.utility.Resource;
using Repository.Pattern.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using pnsms.utility;

namespace pnsms.Service.ViewModels
{

    public interface IVmEmployeeService
    {
        VmEmployee GetNewVmEmployee(int instituteId,int userId);
        VmEmployee GetVmEmployeeById(int instituteId,int userId, int employeeId);
        VmEmployee CreateEmployee(IUnitOfWorkAsync unitOfWorkAsync, int instituteId, VmEmployee vmEmployee);
        VmEmployee UpdateEmployee(IUnitOfWorkAsync unitOfWorkAsync, VmEmployee vmEmployee);
        IEnumerable<VmEmployeeDetails> GetAllEmployeeDetails(int instituteId, string searchText = "", int? branchId = null);
    }
    public class VmEmployeeService : IVmEmployeeService
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


        public VmEmployeeService(
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
        
        public VmEmployee GetNewVmEmployee(int instituteId,int userId)
        {
            var obVmEmployee = new VmEmployee();

            var lstCountryListKv = new List<KeyValuePair<int, string>>();
            _countryService.GetCountryByInstituteId(instituteId).ToList().ForEach(item => lstCountryListKv.Add(new KeyValuePair<int, string>(item.Id, item.Name)));

            obVmEmployee.CountryList = lstCountryListKv;
            obVmEmployee.DistrictOrStateList = _districtOrStateService.GetActiveDistrictOrStateByinstituteId(instituteId).Select(s => new DistrictOrState() { Id = s.Id, CountryId = s.CountryId, Name = s.Name }).ToList();
            obVmEmployee.UserInfo = _userInfoService.NewUserInfo(instituteId);
            // role for employee
            obVmEmployee.UserInfo.RoleList = _roleService.GetRolesForEmployee(instituteId).Select(r => new KeyValuePair<int, string>(r.Id, r.Name));
            obVmEmployee.UserInfo.Employee = _employeeService.NewEmployee(instituteId,userId);
            obVmEmployee.SingleAddresses = _addressService.NewAddress(instituteId);
            return obVmEmployee;
        }
        public VmEmployee GetVmEmployeeById(int instituteId,int userId, int employeeId)
        {
            var employee = _employeeService.GetEmployeeById(employeeId);
            if (employee == null)
            {
                throw new ValidationException(Errors.InvalidEmployee);
            }
            var vmEmployee = new VmEmployee();

            var lstCountryListKv = new List<KeyValuePair<int, string>>();
            _countryService.GetCountryByInstituteId(instituteId).ToList().ForEach(item => lstCountryListKv.Add(new KeyValuePair<int, string>(item.Id, item.Name)));


            vmEmployee.CountryList = lstCountryListKv;
            vmEmployee.DistrictOrStateList = _districtOrStateService.GetDistrictOrStates().Select(s => new DistrictOrState() { Id = s.Id, CountryId = s.CountryId, Name = s.Name }).ToList();

            // New objects 
            vmEmployee.SingleAddresses = _addressService.NewAddress(instituteId);

            vmEmployee.UserInfo = _userInfoService.NewUserInfo(instituteId);
            vmEmployee.UserInfo.Employee = _employeeService.NewEmployee(instituteId, userId);
            // Mapping employee and userinfo

            var userInfo = _userInfoService.GetUserById(employeeId);
            vmEmployee.UserInfo = MapUserInfo(vmEmployee.UserInfo, userInfo);
            vmEmployee.UserInfo.Employee = MapEmployee(vmEmployee.UserInfo.Employee, employee);
            // branch
            var lstBranch = _academicBranchesOfUserInfoService.GetAcademicBranchesOfUserInfo(employeeId).Select(s => s.AcademicBranchId);
            vmEmployee.UserInfo.Employee.AcademicBranches = lstBranch.ToList();
            // Role
            var rolesId = _rolesOfUserInfoService.GetRolesOfUserInfoByUserId(employeeId).Select(s => s.RoleId);


            vmEmployee.UserInfo.RoleList = _roleService.GetRolesForEmployee(instituteId).Select(r => new KeyValuePair<int, string>(r.Id, r.Name));

            vmEmployee.UserInfo.UserRoles = rolesId.ToList();

            // address 
            vmEmployee.Addresses = _addressService.GetAddressesByUserId(employeeId).Select(MapAddress).ToList();
             
            return vmEmployee;
        }

        public IEnumerable<VmEmployeeDetails> GetAllEmployeeDetails(int instituteId, string searchText = "", int? branchId = null)
        {
            var employees = _employeeService.GetAllEmployee(instituteId, searchText, branchId).Select(MapEmployeeDetails);
            return employees;
        }

        public VmEmployee CreateEmployee(IUnitOfWorkAsync unitOfWorkAsync, int instituteId, VmEmployee vmEmployee)
        {
            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);


                vmEmployee.UserInfo.InstituteId = instituteId;
                vmEmployee.UserInfo.IsActive = true;
                vmEmployee.UserInfo.ContactNumber1 = MobileNumber.Rectify(vmEmployee.UserInfo.ContactNumber1);
                vmEmployee.UserInfo.ContactNumber2 = MobileNumber.Rectify(vmEmployee.UserInfo.ContactNumber2);

                vmEmployee.UserInfo.UserInfoTypeId = (int)utility.UserInfoType.Employee;
                // save employee & user
                _userInfoService.Insert(vmEmployee.UserInfo);
                _employeeService.Insert(vmEmployee.UserInfo.Employee);
                unitOfWorkAsync.SaveChanges();
                // save branch
                if (vmEmployee.UserInfo.Employee.AcademicBranches != null)
                {
                    foreach (var bid in vmEmployee.UserInfo.Employee.AcademicBranches)
                    {
                        _academicBranchesOfUserInfoService.Insert(new AcademicBranchesOfUserInfo() { UserInfoId = vmEmployee.UserInfo.Id, AcademicBranchId = bid });
                    }
                    unitOfWorkAsync.SaveChanges();
                }


                // save address 
                if (vmEmployee.Addresses != null)
                {
                    foreach (var address in vmEmployee.Addresses)
                    {
                        address.RefPrimaryKey = vmEmployee.UserInfo.Id;
                        address.IsActive = true;
                        _addressService.Insert(address);
                    }
                    unitOfWorkAsync.SaveChanges();
                }

                // save image
                if (vmEmployee.ProfileImage != null && vmEmployee.ProfileImage.Length > 0)
                {
                    var image = new Image()
                    {
                        RefTypeId = (int)utility.RefCode.UserInfo_Photo,
                        RefPrimaryKey = vmEmployee.UserInfo.Id,
                        ImageBinaryData = vmEmployee.ProfileImage
                    };
                    var imageSmall = new Image()
                    {
                        RefTypeId = (int)utility.RefCode.UserInfo_Photo_Small,
                        RefPrimaryKey = vmEmployee.UserInfo.Id,
                        ImageBinaryData = vmEmployee.ProfileImageSmall
                    };

                    _imageService.Insert(image);
                    _imageService.Insert(imageSmall);
                    unitOfWorkAsync.SaveChanges();
                }

                // save role
                if (vmEmployee.UserInfo.UserRoles != null)
                {
                    foreach (var bid in vmEmployee.UserInfo.UserRoles)
                    {
                        _rolesOfUserInfoService.Insert(new RolesOfUserInfo() { UserInfoId = vmEmployee.UserInfo.Id, RoleId = bid });
                    }
                    unitOfWorkAsync.SaveChanges();
                }
                unitOfWorkAsync.Commit();
            }
            catch
            {
                unitOfWorkAsync.Rollback();
            }
            return vmEmployee;
        }
        public VmEmployee UpdateEmployee(IUnitOfWorkAsync unitOfWorkAsync, VmEmployee vmEmployee)
        {

            vmEmployee.UserInfo.ContactNumber1 = MobileNumber.Rectify(vmEmployee.UserInfo.ContactNumber1);
            vmEmployee.UserInfo.ContactNumber2 = MobileNumber.Rectify(vmEmployee.UserInfo.ContactNumber2);

            _userInfoService.Update(vmEmployee.UserInfo);
            _employeeService.Update(vmEmployee.UserInfo.Employee);
            unitOfWorkAsync.SaveChanges();
            // delete 
            _academicBranchesOfUserInfoService.DeleteAcademicBranchesOfUserInfo(vmEmployee.UserInfo.Id);
            // save branch
            if (vmEmployee.UserInfo.Employee.AcademicBranches != null)
            {
                foreach (var bid in vmEmployee.UserInfo.Employee.AcademicBranches)
                {
                    _academicBranchesOfUserInfoService.Insert(new AcademicBranchesOfUserInfo() { UserInfoId = vmEmployee.UserInfo.Id, AcademicBranchId = bid });
                }
                unitOfWorkAsync.SaveChanges();
            }

            //address 
            if (vmEmployee.Addresses != null)
            {
                foreach (var address in vmEmployee.Addresses)
                {
                    // address.RefPrimaryKey = VmEmployee.UserInfo.Id;
                    if (address.Id == 0)
                    {
                        address.RefPrimaryKey = vmEmployee.UserInfo.Id;
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
            if (vmEmployee.ProfileImage != null && vmEmployee.ProfileImage.Length > 0)
            {
                var image = _imageService.GetImageByRefTypeIdAndRefPrimaryKey((int)utility.RefCode.UserInfo_Photo,
                    vmEmployee.UserInfo.Id).FirstOrDefault();
                var imageSmall = _imageService.GetImageByRefTypeIdAndRefPrimaryKey((int)utility.RefCode.UserInfo_Photo_Small,
                    vmEmployee.UserInfo.Id).FirstOrDefault();

                if (image != null)
                {
                    image.ImageBinaryData = vmEmployee.ProfileImage;
                    imageSmall.ImageBinaryData = vmEmployee.ProfileImageSmall;
                    _imageService.Update(image);
                    _imageService.Update(imageSmall);

                }

                else
                {
                    image = new Image()
                    {
                        RefTypeId = (int)utility.RefCode.UserInfo_Photo,
                        RefPrimaryKey = vmEmployee.UserInfo.Id,
                        ImageBinaryData = vmEmployee.ProfileImage
                    };
                    imageSmall = new Image()
                    {
                        RefTypeId = (int)utility.RefCode.UserInfo_Photo_Small,
                        RefPrimaryKey = vmEmployee.UserInfo.Id,
                        ImageBinaryData = vmEmployee.ProfileImageSmall
                    };
                    _imageService.Insert(image);
                    _imageService.Insert(imageSmall);
                }
              
                unitOfWorkAsync.SaveChanges();
            }
            // delete role
            _rolesOfUserInfoService.DeleteByUserId(unitOfWorkAsync, vmEmployee.UserInfo.Id);
            // save role
            if (vmEmployee.UserInfo.UserRoles != null)
            {
                foreach (var bid in vmEmployee.UserInfo.UserRoles)
                {
                    _rolesOfUserInfoService.Insert(new RolesOfUserInfo() { UserInfoId = vmEmployee.UserInfo.Id, RoleId = bid });
                }
                unitOfWorkAsync.SaveChanges();
            }
            return vmEmployee;
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
        private VmEmployeeDetails MapEmployeeDetails(Employee employee)
        {
            return new VmEmployeeDetails()
            {

                InstituteId = employee.UserInfo.InstituteId.Value,
                EmployeeId = employee.EmployeeId,
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
