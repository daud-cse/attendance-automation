using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data;
using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using deepp.Entities.ViewModels.Student;
using deepp.utility;
using deepp.utility.Resource;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInfoType = deepp.utility.UserInfoType;

namespace deepp.Service.ViewModels
{
    public interface IVmStudentService
    {
        VmStudent GetNewVmStudent(int instituteId, int userId);
        VmStudent GetVmStudentById(int instituteId, int userId, int studentId);
        VmStudent CreateStudent(IUnitOfWorkAsync unitOfWorkAsync, int instituteId, VmStudent vmStudent);
        VmStudent UpdateStudent(IUnitOfWorkAsync unitOfWorkAsync, VmStudent vmStudent);
        IEnumerable<VmStudentDetails> GetAllStudentDetails(int instituteId, Student student = null);
    }
    public class VmStudentService : IVmStudentService
    {
    
        private readonly IStudentService _studentService;
        private readonly IUserInfoService _userInfoService;
        private readonly IAddressService _addressService;
        private readonly IGuardianService _guardianService;
        private readonly ICountryService _countryService;
        private readonly IGuardiansOfStudentService _guardianOfStudentService;
        private readonly IDistrictOrStateService _districtOrStateService;
        private readonly IImageService _imageService;
        private readonly ICoCurricularActivitiesOfStudentService _coCurricularActivitiesOfStudentService;
        private readonly IScholarshipOfStudentService _scholarshipOfStudentService;

        public VmStudentService( IStudentService studentService,
            IUserInfoService userInfoService,
            IAddressService addressService,
            IGuardianService guardianService,
            ICountryService countryService, 
            IGuardiansOfStudentService guardianOfStudentService, 
            IDistrictOrStateService districtOrStateService,
            IImageService imageService,
            ICoCurricularActivitiesOfStudentService coCurricularActivitiesOfStudentService,
            IScholarshipOfStudentService scholarshipOfStudentService)
        {
         
            _studentService = studentService;
            _userInfoService = userInfoService;
            _addressService = addressService;
            _guardianService = guardianService;
            _countryService = countryService;
            _guardianOfStudentService = guardianOfStudentService;
            _districtOrStateService = districtOrStateService;
            _imageService = imageService;
            _coCurricularActivitiesOfStudentService = coCurricularActivitiesOfStudentService;
            _scholarshipOfStudentService = scholarshipOfStudentService;
        }

        /// <summary>
        /// Gets the new vm student.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public VmStudent GetNewVmStudent(int instituteId, int userId)
        {
            var vmStudent = new VmStudent();
            // country list
            vmStudent.CountryList = _countryService.GetCountryByInstituteId(instituteId)
                .Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList();
            // District Or State List
            vmStudent.DistrictOrStateList = _districtOrStateService.GetActiveDistrictOrStateByinstituteId(instituteId)
                .Select(s => new DistrictOrState {Id = s.Id, CountryId = s.CountryId, Name = s.Name})
                .ToList();
            // Student
            vmStudent.Student = _studentService.NewStudent(instituteId, userId,0);
            // UserInfo
            vmStudent.UserInfo = _userInfoService.NewUserInfo(instituteId);
            // SingleAddresses
            vmStudent.SingleAddresses = _addressService.NewAddress(instituteId);
            // SingleGuardian
            vmStudent.SingleGuardian = _guardianService.NewGuardian(instituteId);

            return vmStudent;
        }

        /// <summary>
        /// Gets the vm student by identifier.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="userId"></param>
        /// <param name="studentId">The student identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ComponentModel.DataAnnotations.ValidationException"></exception>
        public VmStudent GetVmStudentById(int instituteId,int userId, int studentId)
        {
            Student student = _studentService.GetStudentById(studentId);
            if (student == null)
            {
                throw new ValidationException(Errors.InvalidStudent);
            }

            var vmStudent = new VmStudent();

            vmStudent.CountryList = _countryService.GetCountryByInstituteId(instituteId)
                .Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList();

            vmStudent.DistrictOrStateList =
                _districtOrStateService.GetActiveDistrictOrStateByinstituteId(instituteId)
                    .Select(s => new DistrictOrState {Id = s.Id, CountryId = s.CountryId, Name = s.Name})
                    .ToList();

            // New objects 
            vmStudent.SingleAddresses = _addressService.NewAddress(instituteId);
            vmStudent.SingleGuardian = _guardianService.NewGuardian(instituteId);
            var CurrentAcademicClassId=student.CurrentAcademicClassId==null?0:Convert.ToInt16(student.CurrentAcademicClassId);
            vmStudent.Student = _studentService.NewStudent(instituteId, userId, CurrentAcademicClassId);
            vmStudent.UserInfo = _userInfoService.NewUserInfo(instituteId);

            // Mapping Student and userinfo
            vmStudent.Student = MapStudent(vmStudent.Student, student);
            //CoCurricularActivities
            vmStudent.Student.CoCurricularActivities =
                _coCurricularActivitiesOfStudentService.GetCoCurricularActivityByStudentId(studentId)
                    .Select(s => s.CoCurricularActivityId).ToList();
            //Scholarships
            vmStudent.Student.Scholarships =
                _scholarshipOfStudentService.GetScholarshipOfStudentByStudentId(studentId)
                    .Select(s => s.ScholarshipId).ToList();

            UserInfo userInfo = _userInfoService.GetUserById(studentId);

            vmStudent.UserInfo = MapUserInfo(vmStudent.UserInfo, userInfo);

            // guardian
            vmStudent.Guardians = _guardianService.GetGuardiansByStudentId(studentId).Select(MapGuardian).ToList();

            // address 
            vmStudent.Addresses = _addressService.GetAddressesByUserId(studentId).Select(MapAddress).ToList();

            return vmStudent;
        }

        /// <summary>
        /// Gets all student details.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="student">The student.</param>
        /// <returns></returns>
        public IEnumerable<VmStudentDetails> GetAllStudentDetails(int instituteId, Student student = null)
        {
            var students = _studentService.GetAllStudent(instituteId, student).Select(MapStudentDetails);
            return students;
        }

        /// <summary>
        /// Creates the student.
        /// </summary>
        /// <param name="unitOfWorkAsync"></param>
        /// <param name="instituteId"></param>
        /// <param name="vmStudent">The vm student.</param>
        /// <returns></returns>
        public VmStudent CreateStudent(IUnitOfWorkAsync unitOfWorkAsync, int instituteId, VmStudent vmStudent)
        {
            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: IsolationLevel.Unspecified);
                vmStudent.UserInfo.InstituteId = instituteId;
                
                vmStudent.UserInfo.IsActive = true;
                vmStudent.UserInfo.ContactNumber1 = MobileNumber.Rectify(vmStudent.UserInfo.ContactNumber1);
                vmStudent.UserInfo.ContactNumber2 = MobileNumber.Rectify(vmStudent.UserInfo.ContactNumber2);

                vmStudent.UserInfo.UserInfoTypeId = (int) UserInfoType.Student;
                vmStudent.UserInfo.LastUpdateTime = DateTime.Now;
                vmStudent.Student.LastUpdateTime = DateTime.Now;
                // save student & user
                _userInfoService.Insert(vmStudent.UserInfo);
                _studentService.Insert(vmStudent.Student);
                unitOfWorkAsync.SaveChanges();

                // save address 
                if (vmStudent.Addresses != null)
                    foreach (Address address in vmStudent.Addresses)
                    {
                        address.RefPrimaryKey = vmStudent.UserInfo.Id;
                        address.IsActive = true;
                        address.LastUpdateTime = DateTime.Now;
                        _addressService.Insert(address);
                    }
                unitOfWorkAsync.SaveChanges();

                // save guardians
                if (vmStudent.Guardians != null)
                    foreach (Guardian guardian in vmStudent.Guardians)
                    {
                        // dont have any sibling 
                        if (guardian.GuardianId == 0)
                        {
                            guardian.UserInfo.InstituteId = instituteId;
                            guardian.UserInfo.IsActive = true;
                            guardian.UserInfo.UserInfoTypeId = (int) UserInfoType.Guardian;
                            guardian.UserInfo.LastUpdateTime = DateTime.Now;
                            guardian.LastUpdateTime = DateTime.Now;
                            _userInfoService.Insert(guardian.UserInfo);
                            _guardianService.Insert(guardian);
                            unitOfWorkAsync.SaveChanges();
                            // guardian of student
                            _guardianOfStudentService.Insert(new GuardiansOfStudent
                            {
                                GuardianId = guardian.GuardianId,
                                StudentId = vmStudent.Student.StudentId
                            });
                            unitOfWorkAsync.SaveChanges();
                        }
                        else
                        {
                            //   for  sibling 
                            _guardianOfStudentService.Insert(new GuardiansOfStudent
                            {
                                GuardianId = guardian.GuardianId,
                                StudentId = vmStudent.Student.StudentId
                            });
                            unitOfWorkAsync.SaveChanges();
                        }
                    }

                // save image
                if (vmStudent.ProfileImage != null && vmStudent.ProfileImage.Length > 0)
                {
                    var image = new Image
                    {
                        RefTypeId = (int) RefCode.UserInfo_Photo,
                        RefPrimaryKey = vmStudent.Student.StudentId,
                        ImageBinaryData = vmStudent.ProfileImage,
                        LastUpdatedTime = DateTime.Now
                    };
                    var imageSmall = new Image
                    {
                        RefTypeId = (int) RefCode.UserInfo_Photo_Small,
                        RefPrimaryKey = vmStudent.Student.StudentId,
                        ImageBinaryData = vmStudent.ProfileImageSmall,
                        LastUpdatedTime = DateTime.Now
                    };

                    _imageService.Insert(image);
                    _imageService.Insert(imageSmall);
                    unitOfWorkAsync.SaveChanges();
                }

                // save CoCurricularActivities
                if (vmStudent.Student.CoCurricularActivities != null)
                {
                   _coCurricularActivitiesOfStudentService.SaveCoCurricularActivitiesOfStudent
                    (unitOfWorkAsync, vmStudent.Student.StudentId, vmStudent.Student.CoCurricularActivities);
                }
                // save CoCurricularActivities
                if (vmStudent.Student.Scholarships != null)
                {
                    _scholarshipOfStudentService.SaveScholarshipOfStudent
                     (unitOfWorkAsync, vmStudent.Student.StudentId,(int)vmStudent.Student.CurrentAcademicSessionId, vmStudent.Student.Scholarships);
                }
                unitOfWorkAsync.Commit();
                return vmStudent;
            }
            catch (Exception exception)
            {
                unitOfWorkAsync.Rollback();
                throw;
            }
           
        }
        /// <summary>
        /// Updates the student.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="vmStudent">The vm student.</param>
        /// <returns></returns>
        public VmStudent UpdateStudent(IUnitOfWorkAsync unitOfWorkAsync, VmStudent vmStudent)
        {
            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);
                
                vmStudent.UserInfo.ContactNumber1 = MobileNumber.Rectify(vmStudent.UserInfo.ContactNumber1);
                vmStudent.UserInfo.ContactNumber2 = MobileNumber.Rectify(vmStudent.UserInfo.ContactNumber2);

                _userInfoService.Update(vmStudent.UserInfo);
                _studentService.Update(vmStudent.Student);
                unitOfWorkAsync.SaveChanges();

                //address 
                if (vmStudent.Addresses != null)
                {
                    foreach (var address in vmStudent.Addresses)
                    {
                        if (address.Id == 0)
                        {
                            address.RefPrimaryKey = vmStudent.UserInfo.Id;
                            address.IsActive = true;
                            address.LastUpdateTime = DateTime.Now;
                            _addressService.Insert(address);
                        }
                        else
                        {
                            address.LastUpdateTime = DateTime.Now;
                            _addressService.Update(address);
                        }

                    }
                    unitOfWorkAsync.SaveChanges();
                }
                // guardians
                if (vmStudent.Guardians != null)
                {
                    foreach (var guardian in vmStudent.Guardians)
                    {
                        if (guardian.GuardianId == 0)
                        {
                            guardian.UserInfo.InstituteId = vmStudent.UserInfo.InstituteId;
                            guardian.UserInfo.UserInfoTypeId = (int)utility.UserInfoType.Guardian;
                            guardian.UserInfo.IsActive = true;
                            guardian.UserInfo.LastUpdateTime = DateTime.Now;
                            guardian.LastUpdateTime = DateTime.Now;

                            _userInfoService.Insert(guardian.UserInfo);
                            _guardianService.Insert(guardian);
                            unitOfWorkAsync.SaveChanges();

                            _guardianOfStudentService.Insert(new GuardiansOfStudent() { GuardianId = guardian.GuardianId, StudentId = vmStudent.Student.StudentId });
                            unitOfWorkAsync.SaveChanges();
                        }
                        else
                        {
                            guardian.UserInfo.LastUpdateTime = DateTime.Now;
                            guardian.LastUpdateTime = DateTime.Now;

                            _userInfoService.Update(guardian.UserInfo);
                            _guardianService.Update(guardian);
                            unitOfWorkAsync.SaveChanges();
                        }


                    }
                }
                // update image
                if (vmStudent.ProfileImage != null && vmStudent.ProfileImage.Length > 0)
                {
                    var image = _imageService.GetImageByRefTypeIdAndRefPrimaryKey((int)utility.RefCode.UserInfo_Photo,
                        vmStudent.Student.StudentId).FirstOrDefault();
                    var imageSmall = _imageService.GetImageByRefTypeIdAndRefPrimaryKey((int)utility.RefCode.UserInfo_Photo_Small,
                        vmStudent.Student.StudentId).FirstOrDefault();

                    if (image != null)
                    {
                        image.ImageBinaryData = vmStudent.ProfileImage;
                        imageSmall.ImageBinaryData = vmStudent.ProfileImageSmall;
                        image.LastUpdatedTime = DateTime.Now;
                        imageSmall.LastUpdatedTime = DateTime.Now;
                        _imageService.Update(image);
                        _imageService.Update(imageSmall);

                    }

                    else
                    {
                        image = new Image()
                        {
                            RefTypeId = (int)utility.RefCode.UserInfo_Photo,
                            RefPrimaryKey = vmStudent.Student.StudentId,
                            ImageBinaryData = vmStudent.ProfileImage,
                            LastUpdatedTime = DateTime.Now
                        };
                        imageSmall = new Image()
                        {
                            RefTypeId = (int)utility.RefCode.UserInfo_Photo_Small,
                            RefPrimaryKey = vmStudent.Student.StudentId,
                            ImageBinaryData = vmStudent.ProfileImageSmall,
                            LastUpdatedTime = DateTime.Now
                        };
                        _imageService.Insert(image);
                        _imageService.Insert(imageSmall);
                    }

                    unitOfWorkAsync.SaveChanges();
                }
                // save CoCurricularActivities
                _coCurricularActivitiesOfStudentService.DeleteCoCurricularActivitiesOfStudent(
                    vmStudent.Student.StudentId);

                if (vmStudent.Student.CoCurricularActivities != null)
                {
                    _coCurricularActivitiesOfStudentService.SaveCoCurricularActivitiesOfStudent
                     (unitOfWorkAsync, vmStudent.Student.StudentId, vmStudent.Student.CoCurricularActivities);
                }

                // save Scholarships
                _scholarshipOfStudentService.DeleteScholarshipOfStudent(
                  vmStudent.Student.StudentId);

                if (vmStudent.Student.Scholarships != null)
                {
                    _scholarshipOfStudentService.SaveScholarshipOfStudent
                     (unitOfWorkAsync, vmStudent.Student.StudentId, (int)vmStudent.Student.CurrentAcademicSessionId, vmStudent.Student.Scholarships);
                }
                unitOfWorkAsync.Commit();
                return vmStudent;

            }
            catch (Exception)
            {
                unitOfWorkAsync.Rollback();
                throw;
 
            }
          
        }


        #region Helper Method
        /// <summary>
        /// Map Guardian
        /// </summary>
        /// <param name="guardian"></param>
        /// <returns></returns>
        private Guardian MapGuardian(Guardian guardian)
        {
            if (guardian == null)
                return null;
            var newGuardian = new Guardian
            {
                UserInfo =
                    new UserInfo
                    {
                        Id = guardian.UserInfo.Id,
                        InstituteId = guardian.UserInfo.InstituteId,
                        UserInfoTypeId = guardian.UserInfo.UserInfoTypeId,
                        Name = guardian.UserInfo.Name,
                        FirstName = guardian.UserInfo.FirstName,
                        LastName = guardian.UserInfo.LastName,
                        MiddleName = guardian.UserInfo.MiddleName,
                        ContactNumber1 = guardian.UserInfo.ContactNumber1,
                        ContactNumber2 = guardian.UserInfo.ContactNumber2,
                        IsActive = guardian.UserInfo.IsActive
                    },
                GuardianId = guardian.GuardianId,
                GuardianTypeId = guardian.GuardianTypeId,
                MonthlyIncome = guardian.MonthlyIncome,
                ProfessionId = guardian.ProfessionId,
                EducationalQualificationId = guardian.EducationalQualificationId
            };


            return newGuardian;
        }

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

        private Student MapStudent(Student newStudent, Student sourceStudent)
        {
            newStudent.StudentId = sourceStudent.StudentId;
            newStudent.LastUpdateTime = sourceStudent.LastUpdateTime;
            newStudent.CurrentRollNo = sourceStudent.CurrentRollNo;
            newStudent.CurrentAcademicBranchId = sourceStudent.CurrentAcademicBranchId;
            newStudent.CurrentAcademicClassId = sourceStudent.CurrentAcademicClassId;
            newStudent.CurrentAcademicGroupId = sourceStudent.CurrentAcademicGroupId;
            newStudent.CurrentAcademicSectionId = sourceStudent.CurrentAcademicSectionId;
            newStudent.CurrentAcademicSessionId = sourceStudent.CurrentAcademicSessionId;
            newStudent.CurrentAcademicShiftId = sourceStudent.CurrentAcademicShiftId;
            newStudent.CurrentAcademicVerssionId = sourceStudent.CurrentAcademicVerssionId;

            return newStudent;
        }
        private VmStudentDetails MapStudentDetails(Student s)
        {
            return new VmStudentDetails()
            {
                StudentId = s.StudentId,
                SessionName = s.AcademicSession != null ? s.AcademicSession.Name : string.Empty,
                BranchName = s.AcademicBranch != null ? s.AcademicBranch.Name : string.Empty,
                ClassName = s.AcademicClass != null ? s.AcademicClass.Name : string.Empty,
                ShiftName = s.AcademicShift != null ? s.AcademicShift.Name : string.Empty,
                SectionName = s.AcademicClassSectionMapping.AcademicSection != null ? s.AcademicClassSectionMapping.AcademicSection.Name : string.Empty,
                VerssionName = s.AcademicVersion != null ? s.AcademicVersion.Name : string.Empty,
                GroupName = s.AcademicGroup != null ? s.AcademicGroup.Name : string.Empty,
                CurrentRollNo = s.CurrentRollNo,
                LastUpdateTime = s.LastUpdateTime,
                InstituteId = s.UserInfo.InstituteId.Value,
                FirstName = s.UserInfo.FirstName,
                MiddleName = s.UserInfo.MiddleName,
                LastName = s.UserInfo.LastName,
                Name = s.UserInfo.Name,
             
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
