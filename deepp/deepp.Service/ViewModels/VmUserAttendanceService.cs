using deepp.Entities.AttendanceMachineModel;
using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using deepp.Service.DashBoard;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service.ViewModels
{
    public interface IVmUserAttendanceService
    {
        //teacher
        VmUserAttendance CreateVmTeacherAttendance(int instituteId, int branchId);
        VmSearchAttendance GetTeacherAttendanceList(VmSearchAttendance vmSearchAttendance, int instituteId);
        //employee
        VmUserAttendance CreateVmEmployeeAttendance(int instituteId, int branchId);
        VmSearchAttendance GetEmployeeAttendanceList(VmSearchAttendance vmSearchAttendance, int instituteId);
        VmUserAttendance SaveUpdateTeacherAttendanceFromDevice(int InstituteId, List<MachineInfo> lstMachineInfoTeacher, IUnitOfWorkAsync unitOfWorkAsync);
        VmUserAttendance SaveUpdateTeacherAttendanceFromExcel(int InstituteId, List<AttendanceExcel> lstMachineInfoTeacher, IUnitOfWorkAsync unitOfWorkAsync);

        VmUserAttendance SaveTeacherAttendance(VmUserAttendance vmUserAttendance, IUnitOfWorkAsync unitOfWorkAsync);
        VmUserAttendance SaveEmployeeAttendance(VmUserAttendance vmUserAttendance, IUnitOfWorkAsync unitOfWorkAsync);
        VmUserAttendance UpdateUserAttendance(VmUserAttendance vmUserAttendance, IUnitOfWorkAsync unitOfWorkAsync);
        UserAttendanceDetail updateTeacherattendanceDetails(UserAttendanceDetail objUserAttendanceDetail, IUnitOfWorkAsync unitOfWorkAsync,int instituteId);
        VmUserAttendance GetVmTeacherDetailsById(int attendanceId, int instituteId);
        VmUserAttendance GetVmEmployeeDetailsById(int attendanceId, int instituteId);
        VmSearchAttendance newTeacherAttendance(int instituteId, int UserId, int UserInfoTypeId);
        

        VmSearchAttendance GetTeacherAttendanceList(VmSearchAttendance vmSearchAttendance);


    }

    public class VmUserAttendanceService : IVmUserAttendanceService
    {
        private readonly IAcademicBranchService _branchService;
        private readonly ITeacherService _teacherService;
        private readonly IEmployeeService _employeeService;
        private readonly IUserInfoService _userInfoService;
        private readonly IAttendanceTypeService _attendanceTypeService;
        private readonly IUserAttendanceService _userAttendanceService;
        private readonly IInstituteService _instituteService;
        private readonly IUserAttendanceDetailService _userAttendanceDetailService;
        private readonly IDashboardService _dashboardService;
        public VmUserAttendanceService(
              IAcademicBranchService branchService,
              ITeacherService teacherService,
              IEmployeeService employeeService,
              IUserInfoService userInfoService,
              IAttendanceTypeService attendanceTypeService,
              IUserAttendanceService userAttendanceService,
              IUserAttendanceDetailService userAttendanceDetailService,
              IInstituteService instituteService,
              IDashboardService dashboardService
            )
        {
            _branchService = branchService;
            _teacherService = teacherService;
            _employeeService = employeeService;
            _userInfoService = userInfoService;
            _attendanceTypeService = attendanceTypeService;
            _userAttendanceService = userAttendanceService;
            _userAttendanceDetailService = userAttendanceDetailService;
            _instituteService = instituteService;
            _dashboardService = dashboardService;
        }

        public VmUserAttendance SaveUpdateTeacherAttendanceFromDevice(int InstituteId, List<MachineInfo> lstMachineInfoTeacher, IUnitOfWorkAsync unitOfWorkAsync)
        {
            VmUserAttendance vmUserAttendance = new VmUserAttendance();
            List<vmMachineInfo> lstvmMachineInfo = new List<vmMachineInfo>();
            var lstAttendanceType = _attendanceTypeService.GetAttendanceTypes(InstituteId);
            var branch = _branchService.GetAcademicBranchsByInstituteId(InstituteId);
            var lstLastAttendanceDistinctValue = lstMachineInfoTeacher.GroupBy(test => test.DateOnlyRecord).Select(grp => grp.First());//lstLastAttendanceData.Distinct().Select(x => x.DateOnlyRecord);   //GroupBy(x => x.DateOnlyRecord).Select(y=>y.);

            int RefTypeId = (int)utility.UserInfoType.Teacher;

            lstLastAttendanceDistinctValue.ToList().ForEach(delegate (MachineInfo item)
            {
                vmMachineInfo objvmMachineInfo = new vmMachineInfo();
                MachineInfo objnewMachineInfo = new MachineInfo();
                List<MachineInfo> lstnewMachineInfo = new List<MachineInfo>();
                objnewMachineInfo = item;
                var lstAttenuser = lstMachineInfoTeacher.Where(x => x.DateOnlyRecord == item.DateOnlyRecord);
                lstnewMachineInfo = lstAttenuser.ToList();
                objvmMachineInfo.objMachineInfo = objnewMachineInfo;
                objvmMachineInfo.lstMachineInfo = lstnewMachineInfo;
                lstvmMachineInfo.Add(objvmMachineInfo);
            });
            lstvmMachineInfo.ToList().ForEach(delegate (vmMachineInfo item)
            {

                vmUserAttendance.UserAttendance = new UserAttendance();
                vmUserAttendance.UserAttendance.AcademicBranchId = branch.FirstOrDefault().Id;
                vmUserAttendance.UserAttendance.AttendanceDate = item.objMachineInfo.DateOnlyRecord;
                vmUserAttendance.UserAttendance.InstituteId = InstituteId;

                var objAttendance = _userAttendanceService.GetTeacherAttendance(vmUserAttendance);

                if (objAttendance == null)
                {
                    var lstteacher = _teacherService.GetAllTeacher(InstituteId, "", null, true);
                    vmUserAttendance.UserAttendance.InstituteId = InstituteId;
                    vmUserAttendance.UserAttendance.UserInfoTypeId = RefTypeId;
                    vmUserAttendance.UserAttendance.AttendanceDate = item.objMachineInfo.DateOnlyRecord;
                    vmUserAttendance.UserAttendance.LastAttendanceSynDate = item.lstMachineInfo.Max(x => Convert.ToDateTime(x.DateTimeRecord));
                    _userAttendanceService.Insert(vmUserAttendance.UserAttendance);
                    //unitOfWorkAsync.SaveChanges();
                    lstteacher.ToList().ForEach(delegate (Teacher objteacher)
                       {
                           UserAttendanceDetail objUserAttendanceDetail = new UserAttendanceDetail();
                           objUserAttendanceDetail.UserAttendanceId = vmUserAttendance.UserAttendance.Id;
                           objUserAttendanceDetail.LastUpdateTime = DateTime.Now;
                           objUserAttendanceDetail.UserInfoId = objteacher.UserInfo.Id;
                           DateTime? convertDateTimeRecordMax = null;
                           DateTime? convertDateTimeRecordMin = null;
                           var lstAtten = item.lstMachineInfo.Where(x => x.IndRegID == objteacher.UserInfo.Id);
                           if (lstAtten.Any())
                           {

                               if (lstAtten.Count() > 1)
                               {
                                   objUserAttendanceDetail.AttendanceTypeId = lstAttendanceType.Where(x => x.IsPresent == true).FirstOrDefault().Id;
                                   convertDateTimeRecordMin = lstAtten.Min(x => Convert.ToDateTime(x.DateTimeRecord));
                                   convertDateTimeRecordMax = lstAtten.Max(x => Convert.ToDateTime(x.DateTimeRecord));
                                   objUserAttendanceDetail.InTime = convertDateTimeRecordMin;
                                   objUserAttendanceDetail.OutTime = convertDateTimeRecordMax;
                               }
                               else
                               {
                                   objUserAttendanceDetail.AttendanceTypeId = lstAttendanceType.Where(x => x.IsPresent == true).FirstOrDefault().Id;
                                   convertDateTimeRecordMin = lstAtten.Min(x => Convert.ToDateTime(x.DateTimeRecord));
                                   objUserAttendanceDetail.InTime = convertDateTimeRecordMin;
                                   objUserAttendanceDetail.OutTime = null;
                               }
                           }
                           else
                           {
                               objUserAttendanceDetail.AttendanceTypeId = lstAttendanceType.Where(x => x.IsPresent == false).FirstOrDefault().Id;
                               objUserAttendanceDetail.UserInfoId = objteacher.TeacherId;
                               objUserAttendanceDetail.InTime = convertDateTimeRecordMin;
                               objUserAttendanceDetail.OutTime = convertDateTimeRecordMax;
                           }
                           _userAttendanceDetailService.Insert(objUserAttendanceDetail);
                       });

                    unitOfWorkAsync.SaveChanges();
                }
                else
                {
                    objAttendance.UserInfoTypeId = RefTypeId;
                    objAttendance.InstituteId = InstituteId;
                    objAttendance.LastAttendanceSynDate = item.lstMachineInfo.Max(x => Convert.ToDateTime(x.DateTimeRecord));
                    _userAttendanceService.Update(objAttendance);


                    if (objAttendance.UserAttendanceDetails != null)
                    {
                        foreach (var details in objAttendance.UserAttendanceDetails)
                        {
                            details.LastUpdateTime = DateTime.Now;
                            DateTime? convertDateTimeRecordMax = null;
                            DateTime? convertDateTimeRecordMin = null;
                            details.UserAttendanceId = objAttendance.Id;
                            details.UserInfoId = details.UserInfoId;
                            //var objuserInfo = vmUserAttendance.AttendanceDetails.Where(x => x.UserInfoId == details.UserInfoId).FirstOrDefault();
                            var lstAtten = item.lstMachineInfo.Where(x => x.IndRegID == details.UserInfoId);
                            if (lstAtten.Any())
                            {
                                if (details.InTime == null && details.OutTime == null)
                                {
                                    if (lstAtten.Count() > 1)
                                    {
                                        details.AttendanceTypeId = lstAttendanceType.Where(x => x.IsPresent == true).FirstOrDefault().Id;
                                        convertDateTimeRecordMin = lstAtten.Min(x => Convert.ToDateTime(x.DateTimeRecord));
                                        convertDateTimeRecordMax = lstAtten.Max(x => Convert.ToDateTime(x.DateTimeRecord));
                                        details.InTime = convertDateTimeRecordMin;
                                        details.OutTime = convertDateTimeRecordMax;
                                    }
                                    else
                                    {
                                        details.AttendanceTypeId = lstAttendanceType.Where(x => x.IsPresent == true).FirstOrDefault().Id;
                                        details.InTime = lstAtten.Min(x => Convert.ToDateTime(x.DateTimeRecord));
                                        details.OutTime = null;
                                    }
                                }
                                else if (details.InTime != null)
                                {
                                    details.AttendanceTypeId = lstAttendanceType.Where(x => x.IsPresent == true).FirstOrDefault().Id;
                                    details.InTime = details.InTime;
                                    details.OutTime = lstAtten.Max(x => Convert.ToDateTime(x.DateTimeRecord));

                                }
                                else if (details.OutTime == null)
                                {
                                    details.AttendanceTypeId = lstAttendanceType.Where(x => x.IsPresent == true).FirstOrDefault().Id;
                                    details.OutTime = lstAtten.Max(x => Convert.ToDateTime(x.DateTimeRecord));
                                }

                            }
                            else
                            {
                                details.AttendanceTypeId = details.AttendanceTypeId; //lstAttendanceType.Where(x => x.IsPresent == false).FirstOrDefault().Id;
                                details.InTime = details.InTime; //convertDateTimeRecordMin;
                                details.OutTime = details.OutTime; //convertDateTimeRecordMax;
                            }
                            _userAttendanceDetailService.Update(details);
                        }
                    }
                    unitOfWorkAsync.SaveChanges();
                }
                if (objAttendance != null)
                {
                    IEnumerable<UserAttendanceDetail> attDetailList = _userAttendanceDetailService.GetListwithTeacherAttendanceTypeById(objAttendance.Id);
                    int presentCount = attDetailList.Where(d => d.AttendanceType.IsPresent == true).Count();
                    int absentCount = attDetailList.Where(d => d.AttendanceType.IsPresent == false).Count();
                    int totalCount = (objAttendance.UserAttendanceDetails != null) ? objAttendance.UserAttendanceDetails.Count() : 0;
                    decimal presentPercent = (totalCount != 0) ? (presentCount * 100) / totalCount : 0;
                    decimal absentPercent = (totalCount != 0) ? (absentCount * 100) / totalCount : 0;
                    objAttendance.TotalCount = totalCount;
                    objAttendance.PresentCount = presentCount;
                    objAttendance.AbsentCount = absentCount;
                    objAttendance.PresentPercentage = presentPercent;
                    objAttendance.AbsentPercentage = absentPercent;
                    // objAttendance.UserAttendanceDetails = null;
                    _userAttendanceService.Update(objAttendance);
                    unitOfWorkAsync.SaveChanges();
                }
                else
                {

                    IEnumerable<UserAttendanceDetail> attDetailList = _userAttendanceDetailService.GetListwithTeacherAttendanceTypeById(vmUserAttendance.UserAttendance.Id);
                    int presentCount = attDetailList.Where(d => d.AttendanceType.IsPresent == true).Count();
                    int absentCount = attDetailList.Where(d => d.AttendanceType.IsPresent == false).Count();
                    int totalCount = (attDetailList != null) ? attDetailList.Count() : 0;
                    decimal presentPercent = (totalCount != 0) ? (presentCount * 100) / totalCount : 0;
                    decimal absentPercent = (totalCount != 0) ? (absentCount * 100) / totalCount : 0;

                    vmUserAttendance.UserAttendance.TotalCount = totalCount;
                    vmUserAttendance.UserAttendance.PresentCount = presentCount;
                    vmUserAttendance.UserAttendance.AbsentCount = absentCount;
                    vmUserAttendance.UserAttendance.PresentPercentage = presentPercent;
                    vmUserAttendance.UserAttendance.AbsentPercentage = absentPercent;
                    //vmUserAttendance.UserAttendance.UserAttendanceDetails = null;
                    _userAttendanceService.Update(vmUserAttendance.UserAttendance);
                    unitOfWorkAsync.SaveChanges();
                }

            });

            return vmUserAttendance;

        }

        public VmUserAttendance SaveUpdateTeacherAttendanceFromExcel(int InstituteId, List<AttendanceExcel> lstAttendanceExcel, IUnitOfWorkAsync unitOfWorkAsync)
        {
            VmUserAttendance vmUserAttendance = new VmUserAttendance();
            try
            {
                List<vmAttendanceExcel> lstvmMachineInfo = new List<vmAttendanceExcel>();
                var lstAttendanceType = _attendanceTypeService.GetAttendanceTypes(InstituteId);
                var branch = _branchService.GetAcademicBranchsByInstituteId(InstituteId);
                var lstLastAttendanceDistinctValue = lstAttendanceExcel.GroupBy(test => test.DateOnlyRecord).Select(grp => grp.First());//lstLastAttendanceData.Distinct().Select(x => x.DateOnlyRecord);   //GroupBy(x => x.DateOnlyRecord).Select(y=>y.);

                int RefTypeId = (int)utility.UserInfoType.Teacher;
                var LastAttendanceSynDate = lstAttendanceExcel.Max(x => Convert.ToDateTime(x.InTime));
                if (LastAttendanceSynDate == null)
                {
                    LastAttendanceSynDate = lstAttendanceExcel.Max(x => Convert.ToDateTime(x.OutTime));
                }
                // vmAttendanceExcel
                var lstteacher = _teacherService.GetAllTeacher(InstituteId, "", null, true);
                vmUserAttendance.lstNotEntryUser = new List<KeyValuePair<int, string>>();
                vmUserAttendance.lsInvalidUserEntry = new List<KeyValuePair<int, string>>();
                int InvalidUserCheck = 0;
                lstteacher.ToList().ForEach(delegate (Teacher item)
                {
                    var checkInvalidExcel= lstAttendanceExcel.Where(x => x.IndRegID == item.TeacherId); ;
                    if (!checkInvalidExcel.Any())
                    {
                        InvalidUserCheck = InvalidUserCheck + 1;
                        vmUserAttendance.lstNotEntryUser.Add(new KeyValuePair<int, string>(item.TeacherId, item.UserInfo.Name));
                        //vmUserAttendance.lsInvalidUserEntry.Add(new KeyValuePair<int, string>(checkInvalidExcel.FirstOrDefault().IndRegID, checkInvalidExcel.FirstOrDefault().Name));                        
                    }
                });

                if (InvalidUserCheck>3)
                {
                    vmUserAttendance.Message = "Some Invalid User Found check List";
                    
                    return vmUserAttendance;
                }
                lstLastAttendanceDistinctValue.ToList().ForEach(delegate (AttendanceExcel item)
                {
                    vmAttendanceExcel objvmMachineInfo = new vmAttendanceExcel();
                    AttendanceExcel objnewMachineInfo = new AttendanceExcel();
                    List<AttendanceExcel> lstnewMachineInfo = new List<AttendanceExcel>();
                    objnewMachineInfo = item;
                    var lstAttenuser = lstAttendanceExcel.Where(x => x.DateOnlyRecord == item.DateOnlyRecord);

                    

                    lstnewMachineInfo = lstAttenuser.ToList();
                    objvmMachineInfo.objAttendanceExcel = objnewMachineInfo;
                    objvmMachineInfo.lstAttendanceExcel = lstnewMachineInfo;
                    lstvmMachineInfo.Add(objvmMachineInfo);
                });
                

                


                lstvmMachineInfo.ToList().ForEach(delegate (vmAttendanceExcel item)
                {

                    vmUserAttendance.UserAttendance = new UserAttendance();
                    vmUserAttendance.UserAttendance.AcademicBranchId = branch.FirstOrDefault().Id;
                    vmUserAttendance.UserAttendance.AttendanceDate = item.objAttendanceExcel.DateOnlyRecord;
                    vmUserAttendance.UserAttendance.InstituteId = InstituteId;

                    var objAttendance = _userAttendanceService.GetTeacherAttendance(vmUserAttendance);
                    if (objAttendance == null)
                    {
                        vmUserAttendance.UserAttendance.InstituteId = InstituteId;
                        vmUserAttendance.UserAttendance.UserInfoTypeId = RefTypeId;
                        vmUserAttendance.UserAttendance.AttendanceDate = item.objAttendanceExcel.DateOnlyRecord;
                        vmUserAttendance.UserAttendance.LastAttendanceSynDate = LastAttendanceSynDate;
                        _userAttendanceService.Insert(vmUserAttendance.UserAttendance);
                        lstteacher.ToList().ForEach(delegate (Teacher objTeacher)
                    {
                        UserAttendanceDetail objUserAttendanceDetail = new UserAttendanceDetail();
                        KeyValuePair<int, string> objkvpInvalidUser = new KeyValuePair<int, string>();
                        objUserAttendanceDetail.UserAttendanceId = vmUserAttendance.UserAttendance.Id;
                        objUserAttendanceDetail.LastUpdateTime = DateTime.Now;
                        var objAttendanceExcel = item.lstAttendanceExcel.Where(x => x.IndRegID == objTeacher.TeacherId).FirstOrDefault();
                        if (objAttendanceExcel == null)
                        {
                            objAttendanceExcel = new AttendanceExcel();
                            objUserAttendanceDetail.UserInfoId = objTeacher.TeacherId;
                            objUserAttendanceDetail.InTime = objAttendanceExcel.InTime;
                            objUserAttendanceDetail.OutTime = objAttendanceExcel.OutTime;
                            objUserAttendanceDetail.AttendanceTypeId = lstAttendanceType.Where(x => x.IsPresent == false).FirstOrDefault().Id;
                            vmUserAttendance.lstNotEntryUser.Add(new KeyValuePair<int, string>(objTeacher.TeacherId, objTeacher.UserInfo.Name));
                        }
                        if (objAttendanceExcel.IndRegID == objTeacher.TeacherId)
                        {
                            objUserAttendanceDetail.UserInfoId = objTeacher.TeacherId;
                            objUserAttendanceDetail.InTime = objAttendanceExcel.InTime;
                            objUserAttendanceDetail.OutTime = objAttendanceExcel.OutTime;

                            if (objUserAttendanceDetail.InTime != null && objUserAttendanceDetail.OutTime != null)
                            {
                                objUserAttendanceDetail.AttendanceTypeId = lstAttendanceType.Where(x => x.IsPresent == true).FirstOrDefault().Id;
                            }
                            else if (objUserAttendanceDetail.InTime != null && objUserAttendanceDetail.OutTime == null)
                            {
                                objUserAttendanceDetail.AttendanceTypeId = lstAttendanceType.Where(x => x.IsPresent == true).FirstOrDefault().Id;
                            }
                            else if (objUserAttendanceDetail.InTime == null && objUserAttendanceDetail.OutTime == null)
                            {
                                objUserAttendanceDetail.AttendanceTypeId = lstAttendanceType.Where(x => x.IsPresent == false).FirstOrDefault().Id;
                            }
                            else if (objUserAttendanceDetail.InTime == null && objUserAttendanceDetail.OutTime!= null)
                            {
                                objUserAttendanceDetail.AttendanceTypeId = lstAttendanceType.Where(x => x.IsPresent == true).FirstOrDefault().Id;
                            }
                        }
                        _userAttendanceDetailService.Insert(objUserAttendanceDetail);
                    });

                        unitOfWorkAsync.SaveChanges();
                    }
                    else
                    {
                        objAttendance.InstituteId = InstituteId;
                        objAttendance.UserInfoTypeId = RefTypeId;
                        objAttendance.AttendanceDate = item.objAttendanceExcel.DateOnlyRecord;
                        objAttendance.LastAttendanceSynDate = LastAttendanceSynDate;
                        _userAttendanceService.Update(objAttendance);
                        if (objAttendance.UserAttendanceDetails != null)
                        {
                            foreach (var objUserAttendanceDetail in objAttendance.UserAttendanceDetails)
                            {
                               // UserAttendanceDetail objUserAttendanceDetail = new UserAttendanceDetail();
                                KeyValuePair<int, string> objkvpInvalidUser = new KeyValuePair<int, string>();
                                objUserAttendanceDetail.UserAttendanceId = objAttendance.Id;
                                objUserAttendanceDetail.LastUpdateTime = DateTime.Now;
                                var objAttendanceExcel = item.lstAttendanceExcel.Where(x => x.IndRegID == objUserAttendanceDetail.UserInfoId).FirstOrDefault();
                                if (objAttendanceExcel == null)
                                {
                                    objAttendanceExcel = new AttendanceExcel();
                                    objUserAttendanceDetail.UserInfoId = objUserAttendanceDetail.UserInfoId;
                                    objUserAttendanceDetail.InTime = objAttendanceExcel.InTime;
                                    objUserAttendanceDetail.OutTime = objAttendanceExcel.OutTime;
                                    objUserAttendanceDetail.AttendanceTypeId = lstAttendanceType.Where(x => x.IsPresent == false).FirstOrDefault().Id;
                                    //vmUserAttendance.lstNotEntryUser.Add(new KeyValuePair<int, string>(objTeacher.TeacherId, objTeacher.UserInfo.Name));
                                }
                                if (objAttendanceExcel.IndRegID == objUserAttendanceDetail.UserInfoId)
                                {
                                    objUserAttendanceDetail.UserInfoId = objUserAttendanceDetail.UserInfoId;
                                    objUserAttendanceDetail.InTime = objAttendanceExcel.InTime;
                                    objUserAttendanceDetail.OutTime = objAttendanceExcel.OutTime;

                                    if (objUserAttendanceDetail.InTime != null && objUserAttendanceDetail.OutTime != null)
                                    {
                                        objUserAttendanceDetail.AttendanceTypeId = lstAttendanceType.Where(x => x.IsPresent == true).FirstOrDefault().Id;
                                    }
                                    else if (objUserAttendanceDetail.InTime != null && objUserAttendanceDetail.OutTime == null)
                                    {
                                        objUserAttendanceDetail.AttendanceTypeId = lstAttendanceType.Where(x => x.IsPresent == true).FirstOrDefault().Id;
                                    }
                                    else if (objUserAttendanceDetail.InTime == null && objUserAttendanceDetail.OutTime == null)
                                    {
                                        objUserAttendanceDetail.AttendanceTypeId = lstAttendanceType.Where(x => x.IsPresent == false).FirstOrDefault().Id;
                                    }
                                    else if (objUserAttendanceDetail.InTime == null && objUserAttendanceDetail.OutTime != null)
                                    {
                                        objUserAttendanceDetail.AttendanceTypeId = lstAttendanceType.Where(x => x.IsPresent == true).FirstOrDefault().Id;
                                    }
                                }
                                _userAttendanceDetailService.Update(objUserAttendanceDetail);

                            }

                            unitOfWorkAsync.SaveChanges();
                        }


                    }                   
                    if (objAttendance != null)
                    {
                        IEnumerable<UserAttendanceDetail> attDetailList = _userAttendanceDetailService.GetListwithTeacherAttendanceTypeById(objAttendance.Id);
                        int presentCount = attDetailList.Where(d => d.AttendanceType.IsPresent == true).Count();
                        int absentCount = attDetailList.Where(d => d.AttendanceType.IsPresent == false).Count();
                        int totalCount = (objAttendance.UserAttendanceDetails != null) ? objAttendance.UserAttendanceDetails.Count() : 0;
                        decimal presentPercent = (totalCount != 0) ? (presentCount * 100) / totalCount : 0;
                        decimal absentPercent = (totalCount != 0) ? (absentCount * 100) / totalCount : 0;
                        objAttendance.TotalCount = totalCount;
                        objAttendance.PresentCount = presentCount;
                        objAttendance.AbsentCount = absentCount;
                        objAttendance.PresentPercentage = presentPercent;
                        objAttendance.AbsentPercentage = absentPercent;
                        // objAttendance.UserAttendanceDetails = null;
                        _userAttendanceService.Update(objAttendance);
                        unitOfWorkAsync.SaveChanges();
                    }
                    else
                    {
                        IEnumerable<UserAttendanceDetail> attDetailList = _userAttendanceDetailService.GetListwithTeacherAttendanceTypeById(vmUserAttendance.UserAttendance.Id);
                        int presentCount = attDetailList.Where(d => d.AttendanceType.IsPresent == true).Count();
                        int absentCount = attDetailList.Where(d => d.AttendanceType.IsPresent == false).Count();
                        int totalCount = (attDetailList != null) ? attDetailList.Count() : 0;
                        decimal presentPercent = (totalCount != 0) ? (presentCount * 100) / totalCount : 0;
                        decimal absentPercent = (totalCount != 0) ? (absentCount * 100) / totalCount : 0;

                        vmUserAttendance.UserAttendance.TotalCount = totalCount;
                        vmUserAttendance.UserAttendance.PresentCount = presentCount;
                        vmUserAttendance.UserAttendance.AbsentCount = absentCount;
                        vmUserAttendance.UserAttendance.PresentPercentage = presentPercent;
                        vmUserAttendance.UserAttendance.AbsentPercentage = absentPercent;
                        //vmUserAttendance.UserAttendance.UserAttendanceDetails = null;
                        _userAttendanceService.Update(vmUserAttendance.UserAttendance);
                        unitOfWorkAsync.SaveChanges();
                    }
                });
                return vmUserAttendance;
            }
            catch (Exception ex)
            {
                vmUserAttendance.Message = ex.Message.ToString();
                return vmUserAttendance;
            }
        }
        public VmSearchAttendance newTeacherAttendance(int instituteId,int UserId,int UserInfoTypeId)
        {
            VmSearchAttendance objVmSearchAttendance = new VmSearchAttendance();
            // objVmSearchAttendance.TeacherList = _teacherService.GetKVP(instituteId);
            var dashboard = _dashboardService.GetDashboard(UserId, UserInfoTypeId);
            var instituteList = new List<KeyValuePair<int, string>>();
            dashboard.lstInstitute.ForEach(c => instituteList.Add(new KeyValuePair<int, string>(c.InstituteId, c.InstituteName)));
            objVmSearchAttendance.InstituteList = instituteList;
            

            return objVmSearchAttendance;
        }
        public VmUserAttendance SaveTeacherAttendance(VmUserAttendance vmUserAttendance, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _userAttendanceService.Insert(vmUserAttendance.UserAttendance);
            unitOfWorkAsync.SaveChanges();

            if (vmUserAttendance.AttendanceDetails != null)
                foreach (var details in vmUserAttendance.AttendanceDetails)
                {
                    UserAttendanceDetail entityUser = new UserAttendanceDetail();
                    entityUser.UserAttendanceId = vmUserAttendance.UserAttendance.Id;
                    entityUser.UserInfoId = details.UserInfoId;
                    entityUser.AttendanceTypeId = details.AttendanceTypeId;
                    entityUser.LastUpdateTime = DateTime.Now;
                    entityUser.InTime = details.InTime;
                    entityUser.Comments = details.Comments;
                    entityUser.OutTime = details.OutTime;
                    _userAttendanceDetailService.Insert(entityUser);
                    unitOfWorkAsync.SaveChanges();
                }


            IEnumerable<UserAttendanceDetail> attDetailList = _userAttendanceDetailService.GetListwithTeacherAttendanceTypeById(vmUserAttendance.UserAttendance.Id);
            int presentCount = attDetailList.Where(d => d.AttendanceType.IsPresent == true).Count();
            int absentCount = attDetailList.Where(d => d.AttendanceType.IsPresent == false).Count();
            int totalCount = (vmUserAttendance.AttendanceDetails != null) ? vmUserAttendance.AttendanceDetails.Count() : 0;
            decimal presentPercent = (totalCount != 0) ? (presentCount * 100) / totalCount : 0;
            decimal absentPercent = (totalCount != 0) ? (absentCount * 100) / totalCount : 0;

            vmUserAttendance.UserAttendance.Id = vmUserAttendance.UserAttendance.Id;
            vmUserAttendance.UserAttendance.TotalCount = totalCount;
            vmUserAttendance.UserAttendance.PresentCount = presentCount;
            vmUserAttendance.UserAttendance.AbsentCount = absentCount;
            vmUserAttendance.UserAttendance.PresentPercentage = presentPercent;
            vmUserAttendance.UserAttendance.AbsentPercentage = absentPercent;
            _userAttendanceService.Update(vmUserAttendance.UserAttendance);
            unitOfWorkAsync.SaveChanges();


            return vmUserAttendance;

        }

        public VmUserAttendance SaveEmployeeAttendance(VmUserAttendance vmUserAttendance, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _userAttendanceService.Insert(vmUserAttendance.UserAttendance);
            unitOfWorkAsync.SaveChanges();

            if (vmUserAttendance.AttendanceDetails != null)
                foreach (var details in vmUserAttendance.AttendanceDetails)
                {
                    UserAttendanceDetail entityUser = new UserAttendanceDetail();
                    entityUser.UserAttendanceId = vmUserAttendance.UserAttendance.Id;
                    entityUser.UserInfoId = details.UserInfoId;
                    entityUser.AttendanceTypeId = details.AttendanceTypeId;
                    entityUser.LastUpdateTime = DateTime.Now;
                    entityUser.InTime = details.InTime;
                    entityUser.OutTime = details.OutTime;
                    _userAttendanceDetailService.Insert(entityUser);
                    unitOfWorkAsync.SaveChanges();
                }


            IEnumerable<UserAttendanceDetail> attDetailList = _userAttendanceDetailService.GetListwithEmployeeAttendanceTypeById(vmUserAttendance.UserAttendance.Id);
            int presentCount = attDetailList.Where(d => d.AttendanceType.IsPresent == true).Count();
            int absentCount = attDetailList.Where(d => d.AttendanceType.IsPresent == false).Count();
            int totalCount = (vmUserAttendance.AttendanceDetails != null) ? vmUserAttendance.AttendanceDetails.Count() : 0;
            decimal presentPercent = (totalCount != 0) ? (presentCount * 100) / totalCount : 0;
            decimal absentPercent = (totalCount != 0) ? (absentCount * 100) / totalCount : 0;

            vmUserAttendance.UserAttendance.Id = vmUserAttendance.UserAttendance.Id;
            vmUserAttendance.UserAttendance.TotalCount = totalCount;
            vmUserAttendance.UserAttendance.PresentCount = presentCount;
            vmUserAttendance.UserAttendance.AbsentCount = absentCount;
            vmUserAttendance.UserAttendance.PresentPercentage = presentPercent;
            vmUserAttendance.UserAttendance.AbsentPercentage = absentPercent;
            _userAttendanceService.Update(vmUserAttendance.UserAttendance);
            unitOfWorkAsync.SaveChanges();


            return vmUserAttendance;

        }
        public UserAttendanceDetail updateTeacherattendanceDetails(UserAttendanceDetail objUserAttendanceDetail, IUnitOfWorkAsync unitOfWorkAsync,int instituteId)
        {


            var objdatabase= _userAttendanceDetailService.Find(objUserAttendanceDetail.Id);

            if (objdatabase.InTime== objdatabase.OutTime)
            {
                objdatabase.InTime = null;
                objdatabase.OutTime = null;
            }

            objdatabase.AttendanceTypeId = objUserAttendanceDetail.AttendanceTypeId;
            objdatabase.Comments = objUserAttendanceDetail.Comments;
            objdatabase.LastUpdateTime = DateTime.Now;
            objdatabase.InTime = objUserAttendanceDetail.InTime;
            objdatabase.OutTime = objUserAttendanceDetail.OutTime;
            _userAttendanceDetailService.Update(objdatabase);
            unitOfWorkAsync.SaveChanges();
            var UserAttendance = _userAttendanceService.GetUserAttendanceForDetailsById(objUserAttendanceDetail.UserAttendanceId, 0);
            IEnumerable<UserAttendanceDetail> tecAttDetailList = _userAttendanceDetailService.GetListwithEmployeeAttendanceTypeById(objUserAttendanceDetail.UserAttendanceId);
            int presentCount = tecAttDetailList.Where(d => d.AttendanceType.IsPresent == true).Count();
            int absentCount = tecAttDetailList.Where(d => d.AttendanceType.IsPresent == false).Count();
            int totalCount = (UserAttendance.UserAttendanceDetails != null) ? UserAttendance.UserAttendanceDetails.Count() : 0;
            decimal presentPercent = (totalCount != 0) ? (presentCount * 100) / totalCount : 0;
            decimal absentPercent = (totalCount != 0) ? (absentCount * 100) / totalCount : 0;

            UserAttendance.Id = UserAttendance.Id;
            UserAttendance.TotalCount = totalCount;
            UserAttendance.PresentCount = presentCount;
            UserAttendance.AbsentCount = absentCount;
            UserAttendance.PresentPercentage = presentPercent;
            UserAttendance.AbsentPercentage = absentPercent;
          
            //  UserAttendance.UserAttendanceDetails = new List<UserAttendanceDetail>();
            _userAttendanceService.Update(UserAttendance);

            unitOfWorkAsync.SaveChanges();


            return objUserAttendanceDetail;
        }
        public VmUserAttendance UpdateUserAttendance(VmUserAttendance vmUserAttendance, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _userAttendanceService.Update(vmUserAttendance.UserAttendance);
            unitOfWorkAsync.SaveChanges();

            if (vmUserAttendance.AttendanceDetails != null)
                foreach (var details in vmUserAttendance.AttendanceDetails)
                {
                    UserAttendanceDetail entityUser = new UserAttendanceDetail();
                    entityUser.UserAttendanceId = vmUserAttendance.UserAttendance.Id;
                    entityUser.UserInfoId = details.UserInfoId;
                    entityUser.AttendanceTypeId = details.AttendanceTypeId;
                    //entityUser.CreateDate = details.create;
                    entityUser.InTime = details.InTime;
                    entityUser.OutTime = details.OutTime;
                    _userAttendanceDetailService.Update(entityUser);
                    unitOfWorkAsync.SaveChanges();
                }


            IEnumerable<UserAttendanceDetail> tecAttDetailList = _userAttendanceDetailService.GetListwithEmployeeAttendanceTypeById(vmUserAttendance.UserAttendance.Id);
            int presentCount = tecAttDetailList.Where(d => d.AttendanceType.IsPresent == true).Count();
            int absentCount = tecAttDetailList.Where(d => d.AttendanceType.IsPresent == false).Count();
            int totalCount = (vmUserAttendance.AttendanceDetails != null) ? vmUserAttendance.AttendanceDetails.Count() : 0;
            decimal presentPercent = (totalCount != 0) ? (presentCount * 100) / totalCount : 0;
            decimal absentPercent = (totalCount != 0) ? (absentCount * 100) / totalCount : 0;

            vmUserAttendance.UserAttendance.Id = vmUserAttendance.UserAttendance.Id;
            vmUserAttendance.UserAttendance.TotalCount = totalCount;
            vmUserAttendance.UserAttendance.PresentCount = presentCount;
            vmUserAttendance.UserAttendance.AbsentCount = absentCount;
            vmUserAttendance.UserAttendance.PresentPercentage = presentPercent;
            vmUserAttendance.UserAttendance.AbsentPercentage = absentPercent;
            _userAttendanceService.Update(vmUserAttendance.UserAttendance);
            unitOfWorkAsync.SaveChanges();


            return vmUserAttendance;

        }

        public VmUserAttendance GetVmTeacherDetailsById(int attendanceId, int instituteId)
        {

            VmUserAttendance vmUserAttendance = new VmUserAttendance();

            var tempAttendace = _userAttendanceService.GetUserAttendanceForDetailsById(attendanceId, instituteId);

            vmUserAttendance.UserAttendance = new UserAttendance();
            vmUserAttendance.UserAttendance.Id = tempAttendace.Id;
            vmUserAttendance.UserAttendance.TotalCount = tempAttendace.TotalCount;
            vmUserAttendance.UserAttendance.PresentCount = tempAttendace.PresentCount;
            vmUserAttendance.UserAttendance.PresentPercentage = tempAttendace.PresentPercentage;
            vmUserAttendance.UserAttendance.AbsentCount = tempAttendace.AbsentCount;
            vmUserAttendance.UserAttendance.AbsentPercentage = tempAttendace.AbsentPercentage;
            vmUserAttendance.UserAttendance.AttendanceDate = tempAttendace.AttendanceDate;
            vmUserAttendance.UserAttendance.BranchName = tempAttendace.AcademicBranch.Name;

            var branchList = _branchService.GetKVP(instituteId);
            vmUserAttendance.UserAttendance.AcademicBranchList = branchList;

            var attendanceTypeList = _attendanceTypeService.GetAttendanceTypesForStudent(instituteId).Select(c => new UserAttendanceTypeModel { AttTypeId = c.Id, IsDefaultAsSelected = c.IsDefault == true ? true : false, Flag = c.Flag, colorCode = c.Colour.ColorCode });

            IEnumerable<UserAttendanceDetail> userAttendanceDetailList = _userAttendanceDetailService.GetListwithTeacherAttendanceTypeById(attendanceId);

            var userAttendanceDetail = new List<UserAttendanceDetail>();

            if (userAttendanceDetailList.Count() > 0)
            {
                foreach (UserAttendanceDetail item in userAttendanceDetailList)
                {
                    UserAttendanceDetail entityUser = new UserAttendanceDetail();
                    entityUser.Id = item.Id;
                    entityUser.UserName = item.UserInfo.Name;
                    entityUser.UserPin = item.UserInfo.PIN;
                    entityUser.UserDesignation = item.UserInfo.Teacher.Designation.Name;
                    entityUser.UserDesignationOrderBy = item.UserInfo.Teacher.Designation.Ordering;
                    entityUser.AttendanceTypeId = item.AttendanceTypeId;
                    entityUser.UserInfoId = item.UserInfoId;
                    entityUser.UserAttendanceId = item.UserAttendanceId;

                  
                    entityUser.InTime = item.InTime;
                    if (entityUser.InTime == null)
                    {
                        entityUser.InTime = vmUserAttendance.UserAttendance.AttendanceDate.Add(TimeSpan.Zero);
                    }

                    entityUser.OutTime = item.OutTime;
                    if (entityUser.OutTime == null)
                    {
                        entityUser.OutTime = vmUserAttendance.UserAttendance.AttendanceDate.Add(TimeSpan.Zero);
                    }
                    if (entityUser.InTime== entityUser.OutTime)
                    {
                        entityUser.InTime = null;
                        entityUser.OutTime = null;
                    }
                    
                    entityUser.Comments = item.Comments;
                    entityUser.Status = item.AttendanceType.Flag;
                    entityUser.StatusColor = item.AttendanceType.Colour.ColorCode;
                    entityUser.AttendanceTypes = attendanceTypeList;

                    userAttendanceDetail.Add(entityUser);

                }
                vmUserAttendance.AttendanceDetails = userAttendanceDetail.OrderBy(x => x.UserDesignationOrderBy);
            }

            return vmUserAttendance;
        }

        //teacher
        public VmUserAttendance CreateVmTeacherAttendance(int instituteId, int branchId)
        {
            VmUserAttendance vmUserAttendance = new VmUserAttendance();

            vmUserAttendance.UserAttendance = new UserAttendance();
            var branchList = _branchService.GetKVP(instituteId);
            vmUserAttendance.UserAttendance.AcademicBranchList = branchList;
            vmUserAttendance.UserAttendance.AcademicBranchId = (branchId > 0) ? branchId : branchList.FirstOrDefault().Key;
            int RefTypeId = (int)utility.UserInfoType.Teacher;
            vmUserAttendance.UserAttendance.UserInfoTypeId = RefTypeId;

            IEnumerable<Teacher> teacherList = _teacherService.GetAllTeacher(instituteId, "", vmUserAttendance.UserAttendance.AcademicBranchId, true).ToList();
            var attendanceTypeList = _attendanceTypeService.GetAttendanceTypesForTeacher(instituteId).Select(c => new UserAttendanceTypeModel { AttTypeId = c.Id, IsDefaultAsSelected = c.IsDefault == true ? true : false, Flag = c.Flag, colorCode = c.Colour.ColorCode });
            int selectedAttendanceId = 0;
            foreach (var item1 in attendanceTypeList)
            {
                if (item1.IsDefaultAsSelected == true) { selectedAttendanceId = item1.AttTypeId; }
            }

            var userAttendanceDetail = new List<UserAttendanceDetail>();

            if (teacherList.Count() > 0)
            {
                foreach (Teacher item in teacherList)
                {
                    UserAttendanceDetail entityUser = new UserAttendanceDetail();
                    entityUser.UserName = item.UserInfo.Name;
                    entityUser.UserPin = item.UserInfo.PIN;
                    entityUser.UserDesignation = item.UserInfo.Teacher.Designation.Name;
                    entityUser.AttendanceTypeId = selectedAttendanceId;
                    entityUser.UserInfoId = item.TeacherId;
                    entityUser.UserAttendanceId = 0;
                    entityUser.InTime = DateTime.Now;
                    entityUser.OutTime = DateTime.Now;
                    entityUser.AttendanceTypes = attendanceTypeList;
                    userAttendanceDetail.Add(entityUser);

                }

                vmUserAttendance.AttendanceDetails = userAttendanceDetail;
            }
            vmUserAttendance.UserAttendance.AttendanceDate = DateTime.Now;

            return vmUserAttendance;
        }

        public VmSearchAttendance GetTeacherAttendanceList(VmSearchAttendance vmSearchAttendance, int instituteId)
        {

            vmSearchAttendance = vmSearchAttendance ?? new VmSearchAttendance();

            var branchList = _branchService.GetKVP(instituteId);
            vmSearchAttendance.BranchList = branchList;
            vmSearchAttendance.InstituteList = _instituteService.GetKVP();
            vmSearchAttendance.BranchId = vmSearchAttendance.BranchId > 0 ? vmSearchAttendance.BranchId : branchList.FirstOrDefault().Key;


            var attList = _userAttendanceService.GetAllTeacherAttendance(vmSearchAttendance);
            var teacherAttendanclist = new List<UserAttendance>();
            if (attList.Count() > 0)
                foreach (var item in attList)
                {
                    UserAttendance entityAttendance = new UserAttendance();
                    entityAttendance.Id = item.Id;
                    entityAttendance.AttendanceDate = item.AttendanceDate;
                    entityAttendance.PresentCount = item.PresentCount;
                    entityAttendance.AbsentCount = item.AbsentCount;
                    entityAttendance.TotalCount = item.TotalCount;
                    entityAttendance.PresentPercentage = item.PresentPercentage;
                    entityAttendance.AbsentPercentage = item.AbsentPercentage;
                    teacherAttendanclist.Add(entityAttendance);
                }
            vmSearchAttendance.SearchTeacherAttendanceData = teacherAttendanclist;

            return vmSearchAttendance;
        }
        public VmSearchAttendance GetTeacherAttendanceList(VmSearchAttendance vmSearchAttendance)
        {

            vmSearchAttendance = vmSearchAttendance ?? new VmSearchAttendance();
            vmSearchAttendance.InstituteList = _instituteService.GetKVP();
            //  var branchList = _branchService.GetKVP(instituteId);
            //vmSearchAttendance.BranchList = branchList;
            //   vmSearchAttendance.BranchId = vmSearchAttendance.BranchId > 0 ? vmSearchAttendance.BranchId : branchList.FirstOrDefault().Key;


            var attList = _userAttendanceService.GetAllTeacherAttendanceForGlobal(vmSearchAttendance);
            var teacherAttendanclist = new List<UserAttendance>();
            if (attList.Count() > 0)
                foreach (var item in attList)
                {
                    UserAttendance entityAttendance = new UserAttendance();
                    entityAttendance.Id = item.Id;
                    entityAttendance.AttendanceDate = item.AttendanceDate;
                    entityAttendance.PresentCount = item.PresentCount;
                    entityAttendance.AbsentCount = item.AbsentCount;
                    entityAttendance.TotalCount = item.TotalCount;
                    entityAttendance.PresentPercentage = item.PresentPercentage;
                    entityAttendance.AbsentPercentage = item.AbsentPercentage;
                    teacherAttendanclist.Add(entityAttendance);
                }
            vmSearchAttendance.SearchTeacherAttendanceData = teacherAttendanclist.OrderByDescending(x=>x.AttendanceDate);

            return vmSearchAttendance;
        }
        //employee

        public VmUserAttendance GetVmEmployeeDetailsById(int attendanceId, int instituteId)
        {

            VmUserAttendance vmUserAttendance = new VmUserAttendance();

            var tempAttendace = _userAttendanceService.GetUserAttendanceForDetailsById(attendanceId, instituteId);

            vmUserAttendance.UserAttendance = new UserAttendance();
            vmUserAttendance.UserAttendance.Id = tempAttendace.Id;
            vmUserAttendance.UserAttendance.TotalCount = tempAttendace.TotalCount;
            vmUserAttendance.UserAttendance.PresentCount = tempAttendace.PresentCount;
            vmUserAttendance.UserAttendance.PresentPercentage = tempAttendace.PresentPercentage;
            vmUserAttendance.UserAttendance.AbsentCount = tempAttendace.AbsentCount;
            vmUserAttendance.UserAttendance.AbsentPercentage = tempAttendace.AbsentPercentage;
            vmUserAttendance.UserAttendance.AttendanceDate = tempAttendace.AttendanceDate;
            vmUserAttendance.UserAttendance.BranchName = tempAttendace.AcademicBranch.Name;

            var branchList = _branchService.GetKVP(instituteId);
            vmUserAttendance.UserAttendance.AcademicBranchList = branchList;

            var attendanceTypeList = _attendanceTypeService.GetAttendanceTypesForEmployee(instituteId).Select(c => new UserAttendanceTypeModel { AttTypeId = c.Id, IsDefaultAsSelected = c.IsDefault == true ? true : false, Flag = c.Flag, colorCode = c.Colour.ColorCode });

            IEnumerable<UserAttendanceDetail> userAttendanceDetailList = _userAttendanceDetailService.GetListwithEmployeeAttendanceTypeById(attendanceId);

            var userAttendanceDetail = new List<UserAttendanceDetail>();

            if (userAttendanceDetailList.Count() > 0)
            {
                foreach (UserAttendanceDetail item in userAttendanceDetailList)
                {
                    UserAttendanceDetail entityUser = new UserAttendanceDetail();
                    entityUser.UserName = item.UserInfo.Name;
                    entityUser.UserPin = item.UserInfo.PIN;
                    entityUser.UserDesignation = item.UserInfo.Employee.Designation.Name;
                    entityUser.AttendanceTypeId = item.AttendanceTypeId;
                    entityUser.UserInfoId = item.UserInfoId;
                    entityUser.UserAttendanceId = item.UserAttendanceId; ;
                    entityUser.InTime = item.InTime;
                    entityUser.OutTime = item.OutTime;
                    entityUser.Status = item.AttendanceType.Flag;
                    entityUser.StatusColor = item.AttendanceType.Colour.ColorCode;
                    entityUser.AttendanceTypes = attendanceTypeList;

                    userAttendanceDetail.Add(entityUser);

                }

                vmUserAttendance.AttendanceDetails = userAttendanceDetail;
            }

            return vmUserAttendance;
        }

        public VmUserAttendance CreateVmEmployeeAttendance(int instituteId, int branchId)
        {
            VmUserAttendance vmUserAttendance = new VmUserAttendance();

            vmUserAttendance.UserAttendance = new UserAttendance();
            var branchList = _branchService.GetKVP(instituteId);
            vmUserAttendance.UserAttendance.AcademicBranchList = branchList;
            vmUserAttendance.UserAttendance.AcademicBranchId = (branchId > 0) ? branchId : branchList.FirstOrDefault().Key;
            int RefTypeId = (int)utility.UserInfoType.Employee;
            vmUserAttendance.UserAttendance.UserInfoTypeId = RefTypeId;

            IEnumerable<Employee> employeeList = _employeeService.GetAllEmployee(instituteId, "", vmUserAttendance.UserAttendance.AcademicBranchId, true).ToList();
            var attendanceTypeList = _attendanceTypeService.GetAttendanceTypesForEmployee(instituteId).Select(c => new UserAttendanceTypeModel { AttTypeId = c.Id, IsDefaultAsSelected = c.IsDefault == true ? true : false, Flag = c.Flag, colorCode = c.Colour.ColorCode });
            int selectedAttendanceId = 0;
            foreach (var item1 in attendanceTypeList)
            {
                if (item1.IsDefaultAsSelected == true) { selectedAttendanceId = item1.AttTypeId; }
            }

            var userAttendanceDetail = new List<UserAttendanceDetail>();

            if (employeeList.Count() > 0)
            {
                foreach (Employee item in employeeList)
                {
                    UserAttendanceDetail entityUser = new UserAttendanceDetail();
                    entityUser.UserName = item.UserInfo.Name;
                    entityUser.UserPin = item.UserInfo.PIN;
                    entityUser.UserDesignation = item.UserInfo.Employee.Designation.Name;
                    entityUser.AttendanceTypeId = selectedAttendanceId;
                    entityUser.UserInfoId = item.EmployeeId;
                    entityUser.UserAttendanceId = 0;
                    entityUser.InTime = DateTime.Now;
                    entityUser.OutTime = DateTime.Now;
                    entityUser.AttendanceTypes = attendanceTypeList;
                    userAttendanceDetail.Add(entityUser);

                }

                vmUserAttendance.AttendanceDetails = userAttendanceDetail;
            }
            vmUserAttendance.UserAttendance.AttendanceDate = DateTime.Now;

            return vmUserAttendance;
        }

        public VmSearchAttendance GetEmployeeAttendanceList(VmSearchAttendance vmSearchAttendance, int instituteId)
        {

            vmSearchAttendance = vmSearchAttendance ?? new VmSearchAttendance();

            var branchList = _branchService.GetKVP(instituteId);
            vmSearchAttendance.BranchList = branchList;
            vmSearchAttendance.BranchId = vmSearchAttendance.BranchId > 0 ? vmSearchAttendance.BranchId : branchList.FirstOrDefault().Key;


            var attList = _userAttendanceService.GetAllEmployeeAttendance(vmSearchAttendance);
            var teacherAttendanclist = new List<UserAttendance>();
            if (attList.Count() > 0)
                foreach (var item in attList)
                {
                    UserAttendance entityAttendance = new UserAttendance();
                    entityAttendance.Id = item.Id;
                    entityAttendance.AttendanceDate = item.AttendanceDate;
                    entityAttendance.PresentCount = item.PresentCount;
                    entityAttendance.AbsentCount = item.AbsentCount;
                    entityAttendance.TotalCount = item.TotalCount;
                    entityAttendance.PresentPercentage = item.PresentPercentage;
                    entityAttendance.AbsentPercentage = item.AbsentPercentage;
                    teacherAttendanclist.Add(entityAttendance);
                }
            vmSearchAttendance.SearchTeacherAttendanceData = teacherAttendanclist;

            return vmSearchAttendance;
        }
        //public DateTime GetLastTakeAttendacneDate(int instituteId)
        //{
        //    _userAttendanceService.GetLastTakeAttendacneDate(int instituteId);
        //}


    }
}
