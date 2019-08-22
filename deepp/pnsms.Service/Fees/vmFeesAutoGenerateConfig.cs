using pnsms.Entities.Models;
using pnsms.Entities.ViewModels.Fees;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service.Fees
{
    public interface IvmFeesAutoGenerateConfig
    {
        VmFeesAutoGenerateConfig New(int institutionId, int userId);
        VmFeesAutoGenerateConfig NewEnroll(int institutionId, int userId);
        VmFeesAutoGenerateConfig GetById(int institutionId, int id, int userId);
        VmFeesAutoGenerateConfig Save(IUnitOfWork _unitOfWork, VmFeesAutoGenerateConfig VmFeesGenModel);
        VmFeesAutoGenerateConfig Update(IUnitOfWork _unitOfWork, VmFeesAutoGenerateConfig VmFeesGenModel);
        VmFeesAutoGenerateConfig SaveEnroll(IUnitOfWork _unitOfWork, VmFeesAutoGenerateConfig VmFeesGenModel);
        VmFeesAutoGenerateConfig UpdateEnroll(IUnitOfWork _unitOfWork, VmFeesAutoGenerateConfig VmFeesGenModel);
       // VmSearch<VmShowList> GetAllEnrollments(int institutionId);
        VmFeesAutoGenerateConfig GetEnrollById(int institutionId, int id, int userId);
       // IEnumerable<VmFeesAutoGenConfigEnrollmentDetail> GetFeesEnrollInsPolicyByStId(int institutionId, int studentId);
    }
    public class vmFeesAutoGenerateConfig : IvmFeesAutoGenerateConfig
    {
      //  private readonly _IAcademicBranchesOfUserInfoService _branchService;
        private readonly IAcademicBranchesOfUserInfoService _branchService;
        private readonly IAcademicClassService _classService;
        private readonly IAcademicGroupService _groupService;
        private readonly IAcademicVersionService _versionService;
        private readonly IAcademicShiftService _shiftService;
       // private readonly _IFeesAutoGenerateConfigTypeService _configTypeService;
        private readonly _IFeesAutoGenerateConfigService _feesGenConfService;
        private readonly _IFeesAutoGenerateConfigDetailService _feesGenConfDetailsService;
       // private readonly _IUnfiedMappingService _unifiedMapService;
      //  private readonly _IFeesGenerateHeadService _feesGenerateHeadService;
       // private readonly _IFacilityService _facilityService;
        private readonly IFeesHeadService _feesHeedService;
        private readonly IStudentService _studentService;
        //private readonly _IFeesAutoGenConfigEnrollService _feesAuGenEnrollService;
       // private readonly _IFeesAutoGenerateConfigEnrollmentDetailService _feesAGCEDService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public vmFeesAutoGenerateConfig(
            IAcademicBranchesOfUserInfoService branchService,
            IAcademicClassService classService
            , IAcademicGroupService groupService
            , IAcademicVersionService versionService
            , IAcademicShiftService shiftService
          //  , _IFeesAutoGenerateConfigTypeService configTypeService
            , _IFeesAutoGenerateConfigService feesGenConfService
            , _IFeesAutoGenerateConfigDetailService feesGenConfDetailsService
          //  , _IUnfiedMappingService unifiedMapService
           // , _IFeesGenerateHeadService feesGenerateHeadService
           // , _IFacilityService facilityService
            , IFeesHeadService feesHeedService
            , IStudentService studentService
          //  , _IFeesAutoGenConfigEnrollService feesAuGenEnrollService
           // , _IFeesAutoGenerateConfigEnrollmentDetailService feesAGCEDService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _branchService = branchService;
            _classService = classService;
            _groupService = groupService;
            _versionService = versionService;
            _shiftService = shiftService;
           // _configTypeService = configTypeService;
            _feesGenConfService = feesGenConfService;
            _feesGenConfDetailsService = feesGenConfDetailsService;
            _unitOfWorkAsync = unitOfWorkAsync;
           // _unifiedMapService = unifiedMapService;
         //   _feesGenerateHeadService = feesGenerateHeadService;
         //   _facilityService = facilityService;
           // _feesAuGenEnrollService = feesAuGenEnrollService;
            _feesHeedService = feesHeedService;
           // _feesAGCEDService = feesAGCEDService;
            _studentService = studentService;
        }

        public VmFeesAutoGenerateConfig New(int institutionId, int userId)
        {

            VmFeesAutoGenerateConfig vmConfig = new VmFeesAutoGenerateConfig();
           // vmConfig.feesAutoGenTypeList = _configTypeService.GetAllKVP(institutionId, true);
            vmConfig.BranchList = _branchService.GetKVP(userId);
            vmConfig.VersionList = _versionService.GetKVP(institutionId);
            vmConfig.ClassList = _classService.GetKVP(institutionId);
            vmConfig.GroupList = _groupService.GetKVP(institutionId);
            vmConfig.ShiftList = _shiftService.GetKVP(institutionId);
          //  vmConfig.FacilityList = new List<KeyValuePair<int, string>>();
           // List<Facility> data = new List<Facility>();
           // data = _facilityService.GetAll(institutionId, true).ToList();
           // data.ForEach(c => vmConfig.FacilityList.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

            vmConfig.FeesAutoGenerateConfig = new FeesAutoGenerateConfig();
            vmConfig.FeesAutoGenerateConfig.IsActive = true;
            vmConfig.FeesAutoGenerateConfig.IsAllAcademicBranch = true;
            vmConfig.FeesAutoGenerateConfig.IsAllAcademicVerssion = true;
            vmConfig.FeesAutoGenerateConfig.IsAllAcademicClass = true;
            vmConfig.FeesAutoGenerateConfig.IsAllAcademicGroup = true;
            vmConfig.FeesAutoGenerateConfig.IsAllAcademicShift = true;
            vmConfig.FeesAutoGenerateConfig.IsAllFacility = true;
            vmConfig.FeesAutoGenerateConfig.InstituteId = institutionId;


          //  var feedList = _feesHeedService.GetWithoutEnrollmentFeesHeads(institutionId);

            var feedList = _feesHeedService.GetFeesHeads(institutionId, true);

            var feesGenerateHeadList = new List<FeesAutoGenerateConfigDetail>();

            if (feedList.Count() > 0)
            {
                foreach (FeesHead item in feedList)
                {
                    FeesAutoGenerateConfigDetail entity = new FeesAutoGenerateConfigDetail();
                    entity.HeadName = item.Name;
                    entity.FeesHeadId = item.Id;
                    entity.FeesAutoGenerateConfigId = 0;
                    entity.Amount = 0;
                    entity.VAT =0;// item.VatPercent;
                    feesGenerateHeadList.Add(entity);

                }

                vmConfig.FeesGenerateHeadList = feesGenerateHeadList;
            }

            return vmConfig;
        }

        public VmFeesAutoGenerateConfig NewEnroll(int institutionId, int userId)
        {

            VmFeesAutoGenerateConfig vmConfig = new VmFeesAutoGenerateConfig();
            //vmConfig.feesAutoGenTypeList = _configTypeService.GetAllKVP(institutionId, true);
            //vmConfig.BranchList = _branchService.GetKVPUserWise(userId);
            //vmConfig.VersionList = _versionService.GetKVP(institutionId);
            //vmConfig.ClassList = _classService.GetKVP(institutionId);
            //vmConfig.GroupList = _groupService.GetKVP(institutionId);
            //vmConfig.ShiftList = _shiftService.GetKVP(institutionId);
            //vmConfig.FacilityList = new List<KeyValuePair<int, string>>();
            //List<Facility> data = new List<Facility>();
            //data = _facilityService.GetAll(institutionId, true).ToList();
            //data.ForEach(c => vmConfig.FacilityList.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

            //vmConfig.dayList = utility.MonthInfo.Days();

            //vmConfig.FeesAutoGenerateConfig = new FeesAutoGenerateConfig();
            //vmConfig.FeesAutoGenerateConfig.IsActive = true;
            //vmConfig.FeesAutoGenerateConfig.IsAllAcademicBranch = true;
            //vmConfig.FeesAutoGenerateConfig.IsAllAcademicVerssion = true;
            //vmConfig.FeesAutoGenerateConfig.IsAllAcademicClass = true;
            //vmConfig.FeesAutoGenerateConfig.IsAllAcademicGroup = true;
            //vmConfig.FeesAutoGenerateConfig.IsAllAcademicShift = true;
            //vmConfig.FeesAutoGenerateConfig.IsAllFacility = true;
            //vmConfig.FeesAutoGenerateConfig.InstituteId = institutionId;


            //var feedList = _feesHeedService.GetEnrollmentFeesHeads(institutionId);

            //var feesGenerateHeadList = new List<FeesAutoGenerateConfigDetail>();

            //if (feedList.Count() > 0)
            //{
            //    foreach (QryFeesHead item in feedList)
            //    {
            //        FeesAutoGenerateConfigDetail entity = new FeesAutoGenerateConfigDetail();
            //        entity.HeadName = item.Name;
            //        entity.FeesHeadId = item.Id;
            //        entity.FeesAutoGenerateConfigId = 0;
            //        entity.Amount = 0;
            //        entity.VAT = item.VatPercent;
            //        feesGenerateHeadList.Add(entity);

            //    }

            //    vmConfig.FeesGenerateHeadList = feesGenerateHeadList;
            //}

            return vmConfig;
        }

        public VmFeesAutoGenerateConfig Save(IUnitOfWork _unitOfWork, VmFeesAutoGenerateConfig VmFeesGenModel)
        {

            VmFeesGenModel.FeesAutoGenerateConfig.LastUpdateTime = DateTime.Now;
            _feesGenConfService.Insert(VmFeesGenModel.FeesAutoGenerateConfig);
            _unitOfWork.SaveChanges();

         //   int UnifiedMappingId = (int)utility.UnifiedMappingInfo.FeesAutoGenerateProcesses;

            if (!VmFeesGenModel.FeesAutoGenerateConfig.IsAllAcademicBranch)
            {
                foreach (var item in VmFeesGenModel.BranchList)
                {
                    //UnfiedMappingAcademicBranch us = new UnfiedMappingAcademicBranch();
                    //us.UnifiedMappingId = UnifiedMappingId;
                    //us.PrimaryKeyId = VmFeesGenModel.FeesAutoGenerateConfig.Id;
                    //us.AcademicBranchId = item.Key;
                    //_unifiedMapService.SaveUMABranch(_unitOfWork, us);
                }

            }
            _unitOfWork.SaveChanges();
            if (!VmFeesGenModel.FeesAutoGenerateConfig.IsAllAcademicClass)
            {
                foreach (var item in VmFeesGenModel.ClassList)
                {
                    //UnfiedMappingAcademicClass us = new UnfiedMappingAcademicClass();
                    //us.UnifiedMappingId = UnifiedMappingId;
                    //us.PrimaryKeyId = VmFeesGenModel.FeesAutoGenerateConfig.Id;
                    //us.AcademicClassId = item.Key;
                    //_unifiedMapService.SaveUMAClass(_unitOfWork, us);
                }

            }
            _unitOfWork.SaveChanges();
            if (!VmFeesGenModel.FeesAutoGenerateConfig.IsAllAcademicGroup)
            {
                foreach (var item in VmFeesGenModel.GroupList)
                {
                    //UnfiedMappingAcademicGroup us = new UnfiedMappingAcademicGroup();
                    //us.UnifiedMappingId = UnifiedMappingId;
                    //us.PrimaryKeyId = VmFeesGenModel.FeesAutoGenerateConfig.Id;
                    //us.AcademicGroupId = item.Key;
                    //_unifiedMapService.SaveUMAGroup(_unitOfWork, us);
                }

            }
            _unitOfWork.SaveChanges();
            if (!VmFeesGenModel.FeesAutoGenerateConfig.IsAllAcademicShift)
            {
                foreach (var item in VmFeesGenModel.ShiftList)
                {
                    //UnfiedMappingAcademicShift us = new UnfiedMappingAcademicShift();
                    //us.UnifiedMappingId = UnifiedMappingId;
                    //us.PrimaryKeyId = VmFeesGenModel.FeesAutoGenerateConfig.Id;
                    //us.AcademicShiftId = item.Key;
                    //_unifiedMapService.SaveUMAShift(_unitOfWork, us);
                }

            }
            _unitOfWork.SaveChanges();
            if (!VmFeesGenModel.FeesAutoGenerateConfig.IsAllAcademicVerssion)
            {
                foreach (var item in VmFeesGenModel.VersionList)
                {
                    //UnfiedMappingAcademicVerssion us = new UnfiedMappingAcademicVerssion();
                    //us.UnifiedMappingId = UnifiedMappingId;
                    //us.PrimaryKeyId = VmFeesGenModel.FeesAutoGenerateConfig.Id;
                    //us.AcademicVerssionId = item.Key;
                    //_unifiedMapService.SaveUMAVersion(_unitOfWork, us);
                }

            }
            _unitOfWork.SaveChanges();

            if (VmFeesGenModel.FeesAutoGenerateConfig.Id != 0)
            {
                if (VmFeesGenModel.FeesGenerateHeadList != null)
                    foreach (var details in VmFeesGenModel.FeesGenerateHeadList)
                    {
                        if (details.Amount != 0 || details.Amount > 0)
                        {
                            FeesAutoGenerateConfigDetail newEntity = new FeesAutoGenerateConfigDetail();
                            newEntity.FeesAutoGenerateConfigId = VmFeesGenModel.FeesAutoGenerateConfig.Id;
                            newEntity.FeesHeadId = details.FeesHeadId;
                            newEntity.Amount = details.Amount;
                            _feesGenConfDetailsService.Insert(newEntity);
                        }
                    }

            }

            _unitOfWork.SaveChanges();

            return VmFeesGenModel;

        }

        public VmFeesAutoGenerateConfig Update(IUnitOfWork _unitOfWork, VmFeesAutoGenerateConfig VmFeesGenModel)
        {

            VmFeesGenModel.FeesAutoGenerateConfig.LastUpdateTime = DateTime.Now;
            _feesGenConfService.Update(VmFeesGenModel.FeesAutoGenerateConfig);
            _unitOfWork.SaveChanges();

            //int UnifiedMappingId = (int)utility.UnifiedMappingInfo.FeesAutoGenerateProcesses;
            //int primaryKey = VmFeesGenModel.FeesAutoGenerateConfig.Id;

            ////Delete

            //#region branch

            //List<UnfiedMappingAcademicBranch> branchList = _unifiedMapService.GetUMABranch(primaryKey, UnifiedMappingId).ToList();

            //var branchDeleted =
            //    branchList.Except(branchList.Where(x => VmFeesGenModel.selectedBranches.Any(y => y.Key == x.AcademicBranchId)))
            //        .ToList();

            //foreach (var item in branchDeleted)
            //{
            //    _unifiedMapService.DelBranchByPrKey(item.Id, _unitOfWork);

            //}

            //if (!VmFeesGenModel.FeesAutoGenerateConfig.IsAllAcademicBranch && VmFeesGenModel.FeesAutoGenerateConfig.Id != 0)
            //{
            //    var branchAdded = VmFeesGenModel.selectedBranches.Select(c => new { Id = c.Key })
            //     .Where(n => !branchList.Select(r => new { Id = r.AcademicBranchId }).Contains(n));

            //    foreach (var item in branchAdded)
            //    {
            //        UnfiedMappingAcademicBranch us = new UnfiedMappingAcademicBranch();
            //        us.UnifiedMappingId = UnifiedMappingId;
            //        us.PrimaryKeyId = VmFeesGenModel.FeesAutoGenerateConfig.Id;
            //        us.AcademicBranchId = item.Id;
            //        _unifiedMapService.SaveUMABranch(_unitOfWork, us);
            //    }

            //}
            //else
            //{
            //    foreach (var item in branchList)
            //    {
            //        _unifiedMapService.DelBranchByPrKey(item.Id, _unitOfWork);

            //    }

            //}

            //#endregion

            //#region class

            //List<UnfiedMappingAcademicClass> classList = _unifiedMapService.GetUMAClass(primaryKey, UnifiedMappingId).ToList();

            //var classDeleted =
            //    classList.Except(classList.Where(x => VmFeesGenModel.selectedClasses.Any(y => y.Key == x.AcademicClassId)))
            //        .ToList();

            //foreach (var item in classDeleted)
            //{
            //    _unifiedMapService.DelClassByPrKey(item.Id, _unitOfWork);

            //}

            //if (!VmFeesGenModel.FeesAutoGenerateConfig.IsAllAcademicClass && VmFeesGenModel.FeesAutoGenerateConfig.Id != 0)
            //{
            //    var classAdded = VmFeesGenModel.selectedClasses.Select(c => new { Id = c.Key })
            //        .Where(n => !classList.Select(r => new { Id = r.AcademicClassId }).Contains(n));

            //    foreach (var item in classAdded)
            //    {
            //        UnfiedMappingAcademicClass us = new UnfiedMappingAcademicClass();
            //        us.UnifiedMappingId = UnifiedMappingId;
            //        us.PrimaryKeyId = VmFeesGenModel.FeesAutoGenerateConfig.Id;
            //        us.AcademicClassId = item.Id;
            //        _unifiedMapService.SaveUMAClass(_unitOfWork, us);
            //    }

            //}
            //else
            //{
            //    foreach (var item in classList)
            //    {
            //        _unifiedMapService.DelClassByPrKey(item.Id, _unitOfWork);

            //    }

            //}

            //#endregion

            //#region group

            //List<UnfiedMappingAcademicGroup> groupList = _unifiedMapService.GetUMAGroup(primaryKey, UnifiedMappingId).ToList();

            //var groupDeleted =
            //    groupList.Except(groupList.Where(x => VmFeesGenModel.selectedGroups.Any(y => y.Key == x.AcademicGroupId)))
            //        .ToList();

            //foreach (var item in groupDeleted)
            //{
            //    _unifiedMapService.DelGroupByPrKey(item.Id, _unitOfWork);

            //}

            //if (!VmFeesGenModel.FeesAutoGenerateConfig.IsAllAcademicGroup && VmFeesGenModel.FeesAutoGenerateConfig.Id != 0)
            //{

            //    var groupAdded = VmFeesGenModel.selectedGroups.Select(c => new { Id = c.Key })
            //        .Where(n => !groupList.Select(r => new { Id = r.AcademicGroupId }).Contains(n));

            //    foreach (var item in groupAdded)
            //    {
            //        UnfiedMappingAcademicGroup us = new UnfiedMappingAcademicGroup();
            //        us.UnifiedMappingId = UnifiedMappingId;
            //        us.PrimaryKeyId = VmFeesGenModel.FeesAutoGenerateConfig.Id;
            //        us.AcademicGroupId = item.Id;
            //        _unifiedMapService.SaveUMAGroup(_unitOfWork, us);
            //    }

            //}
            //else
            //{
            //    foreach (var item in groupList)
            //    {
            //        _unifiedMapService.DelGroupByPrKey(item.Id, _unitOfWork);

            //    }

            //}

            //#endregion

            //#region shift

            //List<UnfiedMappingAcademicShift> shiftList = _unifiedMapService.GetUMAShift(primaryKey, UnifiedMappingId).ToList();

            //var shiftDeleted =
            //    shiftList.Except(shiftList.Where(x => VmFeesGenModel.selectedShifts.Any(y => y.Key == x.AcademicShiftId)))
            //        .ToList();

            //foreach (var item in shiftDeleted)
            //{
            //    _unifiedMapService.DelShiftByPrKey(item.Id, _unitOfWork);

            //}

            //if (!VmFeesGenModel.FeesAutoGenerateConfig.IsAllAcademicShift && VmFeesGenModel.FeesAutoGenerateConfig.Id != 0)
            //{

            //    var shiftAdded = VmFeesGenModel.selectedShifts.Select(c => new { Id = c.Key })
            //        .Where(n => !shiftList.Select(r => new { Id = r.AcademicShiftId }).Contains(n));

            //    foreach (var item in shiftAdded)
            //    {
            //        UnfiedMappingAcademicShift us = new UnfiedMappingAcademicShift();
            //        us.UnifiedMappingId = UnifiedMappingId;
            //        us.PrimaryKeyId = VmFeesGenModel.FeesAutoGenerateConfig.Id;
            //        us.AcademicShiftId = item.Id;
            //        _unifiedMapService.SaveUMAShift(_unitOfWork, us);
            //    }

            //}
            //else
            //{
            //    foreach (var item in shiftList)
            //    {
            //        _unifiedMapService.DelShiftByPrKey(item.Id, _unitOfWork);
            //    }

            //}

            //#endregion

            //#region version

            //List<UnfiedMappingAcademicVerssion> versionList = _unifiedMapService.GetUMAVersion(primaryKey, UnifiedMappingId).ToList();

            //var versionDeleted =
            //    versionList.Except(versionList.Where(x => VmFeesGenModel.selectedVersions.Any(y => y.Key == x.AcademicVerssionId)))
            //        .ToList();

            //foreach (var item in versionDeleted)
            //{
            //    _unifiedMapService.DelVersionByPrKey(item.Id, _unitOfWork);
            //}

            //if (!VmFeesGenModel.FeesAutoGenerateConfig.IsAllAcademicVerssion && VmFeesGenModel.FeesAutoGenerateConfig.Id != 0)
            //{

            //    var versionAdded = VmFeesGenModel.selectedVersions.Select(c => new { Id = c.Key })
            //        .Where(n => !versionList.Select(r => new { Id = r.AcademicVerssionId }).Contains(n));

            //    foreach (var item in versionAdded)
            //    {
            //        UnfiedMappingAcademicVerssion us = new UnfiedMappingAcademicVerssion();
            //        us.UnifiedMappingId = UnifiedMappingId;
            //        us.PrimaryKeyId = VmFeesGenModel.FeesAutoGenerateConfig.Id;
            //        us.AcademicVerssionId = item.Id;
            //        _unifiedMapService.SaveUMAVersion(_unitOfWork, us);
            //    }

            //}
            //else
            //{
            //    foreach (var item in versionList)
            //    {
            //        _unifiedMapService.DelVersionByPrKey(item.Id, _unitOfWork);
            //    }

            //}

            //#endregion

            //var detailsList = _feesGenConfDetailsService.GetByConfId(primaryKey).ToList();
            //foreach (var item in detailsList)
            //{
            //    _feesGenConfDetailsService.Delete(item.Id);
            //}

            //if (VmFeesGenModel.FeesAutoGenerateConfig.Id != 0)
            //{
            //    if (VmFeesGenModel.FeesGenerateHeadList != null)
            //        foreach (var details in VmFeesGenModel.FeesGenerateHeadList)
            //        {
            //            if (details.Amount != 0 || details.Amount > 0)
            //            {
            //                FeesAutoGenerateConfigDetail newEntity = new FeesAutoGenerateConfigDetail();
            //                newEntity.FeesAutoGenerateConfigId = VmFeesGenModel.FeesAutoGenerateConfig.Id;
            //                newEntity.FeesHeadId = details.FeesHeadId;
            //                newEntity.Amount = details.Amount;
            //                _feesGenConfDetailsService.Insert(newEntity);
            //            }
            //        }

            //}

            //_unitOfWork.SaveChanges();

            return VmFeesGenModel;

        }

        //public VmSearch<VmShowList> GetAllEnrollments(int institutionId)
        //{
        //    var listModel = new VmSearch<VmShowList>();

        //    var resultlist = _feesAuGenEnrollService.GetAll(institutionId, null);
        //    var returnList = new List<VmShowList>();

        //    foreach (var item in resultlist)
        //    {
        //        VmShowList temp = new VmShowList();
        //        temp.Id = item.Id;
        //        temp.ColumnField1 = item.FeesAutoGenerateConfig.Name;
        //        temp.ColumnField2 = item.FeesAutoGenerateConfig.Description;
        //        temp.ColumnField3 = item.FeesAutoGenerateConfig.IsActive == true ? "Active" : "Inactive";
        //        temp.Status = item.FeesAutoGenerateConfig.IsActive;
        //        returnList.Add(temp);

        //    }

        //    listModel.SearchListData = returnList;

        //    return listModel;
        //}

        public VmFeesAutoGenerateConfig SaveEnroll(IUnitOfWork _unitOfWork, VmFeesAutoGenerateConfig VmFeesGenModel)
        {

            VmFeesGenModel.FeesAutoGenerateConfig.LastUpdateTime = DateTime.Now;
            //_feesGenConfService.Insert(VmFeesGenModel.FeesAutoGenerateConfig);
            //_unitOfWork.SaveChanges();

            //int UnifiedMappingId = (int)utility.UnifiedMappingInfo.FeesAutoGenerateProcesses;

            //if (!VmFeesGenModel.FeesAutoGenerateConfig.IsAllAcademicBranch)
            //{
            //    foreach (var item in VmFeesGenModel.BranchList)
            //    {
            //        UnfiedMappingAcademicBranch us = new UnfiedMappingAcademicBranch();
            //        us.UnifiedMappingId = UnifiedMappingId;
            //        us.PrimaryKeyId = VmFeesGenModel.FeesAutoGenerateConfig.Id;
            //        us.AcademicBranchId = item.Key;
            //        _unifiedMapService.SaveUMABranch(_unitOfWork, us);
            //    }

            //}
            //_unitOfWork.SaveChanges();
            //if (!VmFeesGenModel.FeesAutoGenerateConfig.IsAllAcademicClass)
            //{
            //    foreach (var item in VmFeesGenModel.ClassList)
            //    {
            //        UnfiedMappingAcademicClass us = new UnfiedMappingAcademicClass();
            //        us.UnifiedMappingId = UnifiedMappingId;
            //        us.PrimaryKeyId = VmFeesGenModel.FeesAutoGenerateConfig.Id;
            //        us.AcademicClassId = item.Key;
            //        _unifiedMapService.SaveUMAClass(_unitOfWork, us);
            //    }

            //}
            //_unitOfWork.SaveChanges();
            //if (!VmFeesGenModel.FeesAutoGenerateConfig.IsAllAcademicGroup)
            //{
            //    foreach (var item in VmFeesGenModel.GroupList)
            //    {
            //        UnfiedMappingAcademicGroup us = new UnfiedMappingAcademicGroup();
            //        us.UnifiedMappingId = UnifiedMappingId;
            //        us.PrimaryKeyId = VmFeesGenModel.FeesAutoGenerateConfig.Id;
            //        us.AcademicGroupId = item.Key;
            //        _unifiedMapService.SaveUMAGroup(_unitOfWork, us);
            //    }

            //}
            //_unitOfWork.SaveChanges();
            //if (!VmFeesGenModel.FeesAutoGenerateConfig.IsAllAcademicShift)
            //{
            //    foreach (var item in VmFeesGenModel.ShiftList)
            //    {
            //        UnfiedMappingAcademicShift us = new UnfiedMappingAcademicShift();
            //        us.UnifiedMappingId = UnifiedMappingId;
            //        us.PrimaryKeyId = VmFeesGenModel.FeesAutoGenerateConfig.Id;
            //        us.AcademicShiftId = item.Key;
            //        _unifiedMapService.SaveUMAShift(_unitOfWork, us);
            //    }

            //}
            //_unitOfWork.SaveChanges();
            //if (!VmFeesGenModel.FeesAutoGenerateConfig.IsAllAcademicVerssion)
            //{
            //    foreach (var item in VmFeesGenModel.VersionList)
            //    {
            //        UnfiedMappingAcademicVerssion us = new UnfiedMappingAcademicVerssion();
            //        us.UnifiedMappingId = UnifiedMappingId;
            //        us.PrimaryKeyId = VmFeesGenModel.FeesAutoGenerateConfig.Id;
            //        us.AcademicVerssionId = item.Key;
            //        _unifiedMapService.SaveUMAVersion(_unitOfWork, us);
            //    }

            //}
            //_unitOfWork.SaveChanges();

            //FeesAutoGenerateConfigEnrollment feesEnroll = new FeesAutoGenerateConfigEnrollment();
            //feesEnroll.FeesAutoGenerateConfigId = VmFeesGenModel.FeesAutoGenerateConfig.Id;
            //feesEnroll.InstituteId = VmFeesGenModel.FeesAutoGenerateConfig.InstituteId;
            //feesEnroll.CreateDate = DateTime.Now;
            //_feesAuGenEnrollService.Insert(feesEnroll);

            //_unitOfWork.SaveChanges();


            //if (feesEnroll.Id != 0)
            //{
            //    if (VmFeesGenModel.enrollDetails.Count() > 0)
            //        foreach (var details in VmFeesGenModel.enrollDetails)
            //        {
            //            foreach (var tmp in details.AmountDetails)
            //            {
            //                FeesAutoGenerateConfigEnrollmentDetail newEntity = new FeesAutoGenerateConfigEnrollmentDetail();
            //                newEntity.FeesAutoGenerateConfigEnrollmentId = feesEnroll.Id;
            //                newEntity.InstallmentScheduleId = details.SessionId;
            //                newEntity.InstituteId = VmFeesGenModel.FeesAutoGenerateConfig.InstituteId;
            //                newEntity.ForDay = details.DayId;
            //                newEntity.ForMonth = details.MonthId;
            //                newEntity.DueForDay = details.DueDayId;
            //                newEntity.DueForMonth = details.DueMonthId;
            //                newEntity.DueForDay = details.DueDayId;
            //                newEntity.DueForMonth = details.DueMonthId;
            //                newEntity.FeesHeadId = tmp.HeadId;
            //                newEntity.Amount = tmp.Amount;
            //                _feesAGCEDService.Insert(newEntity);
            //            }
            //        }
            //    _unitOfWork.SaveChanges();
            //}

            return VmFeesGenModel;

        }

        public VmFeesAutoGenerateConfig GetById(int institutionId, int id, int userId)
        {

            VmFeesAutoGenerateConfig vmConfig = new VmFeesAutoGenerateConfig();
           // vmConfig.feesAutoGenTypeList = _configTypeService.GetAllKVP(institutionId, true);
            vmConfig.BranchList = _branchService.GetKVP(institutionId);
            vmConfig.VersionList = _versionService.GetKVP(institutionId);
            vmConfig.ClassList = _classService.GetKVP(institutionId);
            vmConfig.GroupList = _groupService.GetKVP(institutionId);
            vmConfig.ShiftList = _shiftService.GetKVP(institutionId);
           // vmConfig.FacilityList = new List<KeyValuePair<int, string>>();
            //List<Facility> data = new List<Facility>();
          //  data = _facilityService.GetAll(institutionId, true).ToList();
           // data.ForEach(c => vmConfig.FacilityList.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

            vmConfig.FeesAutoGenerateConfig = new FeesAutoGenerateConfig();
            vmConfig.FeesAutoGenerateConfig = _feesGenConfService.GetById(id);

            vmConfig.FeesAutoGenerateConfig.FeesAutoGenerateConfigType.FeesAutoGenerateConfigs = null;


            List<FeesHead> feedList = _feesHeedService.GetFeesHeads(institutionId,true).ToList();
            var getDetails = _feesGenConfDetailsService.GetByConfId(id);
            var feesGenerateHeadList = new List<FeesAutoGenerateConfigDetail>();

            if (feedList.Count() > 0)
            {
                foreach (var item in feedList)
                {
                    FeesAutoGenerateConfigDetail entity = new FeesAutoGenerateConfigDetail();
                    if (getDetails.Where(r => r.FeesHeadId == item.Id).Count() > 0)
                    {
                        entity.Amount = getDetails.Where(r => r.FeesHeadId == item.Id).FirstOrDefault().Amount;
                        entity.FeesAutoGenerateConfigId = getDetails.Where(r => r.FeesHeadId == item.Id).FirstOrDefault().FeesAutoGenerateConfigId;
                      //  entity.VAT = item.VatPercent;
                    }
                    else
                    {
                        entity.Amount = 0;
                        entity.FeesAutoGenerateConfigId = 0;
                       // entity.VAT = item.VatPercent;
                    }
                    entity.HeadName = item.Name;
                    entity.FeesHeadId = item.Id;
                    feesGenerateHeadList.Add(entity);
                }

                vmConfig.FeesGenerateHeadList = feesGenerateHeadList;
            }

            int primaryKey = vmConfig.FeesAutoGenerateConfig.Id;

           // int UnifiedMappingId = (int)utility.UnifiedMappingInfo.FeesAutoGenerateProcesses;

            if (!vmConfig.FeesAutoGenerateConfig.IsAllAcademicBranch)
            {
                //vmConfig.selectedBranches = new List<KeyValuePair<int, string>>();
                //List<UnfiedMappingAcademicBranch> branchList = new List<UnfiedMappingAcademicBranch>();
                //branchList = _unifiedMapService.GetUMABranch(primaryKey, UnifiedMappingId).ToList();
                //branchList.ForEach(c => vmConfig.selectedBranches.Add(new KeyValuePair<int, string>(c.AcademicBranchId, c.AcademicBranch.Name)));

            }

            if (!vmConfig.FeesAutoGenerateConfig.IsAllAcademicVerssion)
            {
                //vmConfig.selectedVersions = new List<KeyValuePair<int, string>>();
                //List<UnfiedMappingAcademicVerssion> versionList = new List<UnfiedMappingAcademicVerssion>();
                //versionList = _unifiedMapService.GetUMAVersion(primaryKey, UnifiedMappingId).ToList();
                //versionList.ForEach(c => vmConfig.selectedVersions.Add(new KeyValuePair<int, string>(c.AcademicVerssionId, c.AcademicVersion.Name)));

            }

            if (!vmConfig.FeesAutoGenerateConfig.IsAllAcademicClass)
            {
                //vmConfig.selectedClasses = new List<KeyValuePair<int, string>>();
                //List<UnfiedMappingAcademicClass> classList = new List<UnfiedMappingAcademicClass>();
                //classList = _unifiedMapService.GetUMAClass(primaryKey, UnifiedMappingId).ToList();
                //classList.ForEach(c => vmConfig.selectedClasses.Add(new KeyValuePair<int, string>(c.AcademicClassId, c.AcademicClass.Name)));

            }
            if (!vmConfig.FeesAutoGenerateConfig.IsAllAcademicGroup)
            {
                //vmConfig.selectedGroups = new List<KeyValuePair<int, string>>();
                //List<UnfiedMappingAcademicGroup> groupList = new List<UnfiedMappingAcademicGroup>();
                //groupList = _unifiedMapService.GetUMAGroup(primaryKey, UnifiedMappingId).ToList();
                //groupList.ForEach(c => vmConfig.selectedGroups.Add(new KeyValuePair<int, string>(c.AcademicGroupId, c.AcademicGroup.Name)));

            }
            if (!vmConfig.FeesAutoGenerateConfig.IsAllAcademicShift)
            {
                vmConfig.selectedShifts = new List<KeyValuePair<int, string>>();
                //List<UnfiedMappingAcademicShift> shiftList = new List<UnfiedMappingAcademicShift>();
                //shiftList = _unifiedMapService.GetUMAShift(primaryKey, UnifiedMappingId).ToList();
                //shiftList.ForEach(c => vmConfig.selectedShifts.Add(new KeyValuePair<int, string>(c.AcademicShiftId, c.AcademicShift.Name)));

            }
            vmConfig.FeesAutoGenerateConfig.FeesAutoGenerateConfigDetails = null;
          //  vmConfig.FeesAutoGenerateConfig.FeesAutoGenerateConfigEnrollments = null;
            //vmConfig.FeesAutoGenerateConfig.FeesAutoGenerateProcessDetails = null;
            return vmConfig;
        }

        public VmFeesAutoGenerateConfig GetEnrollById(int institutionId, int id, int userId)
        {

            VmFeesAutoGenerateConfig vmConfig = new VmFeesAutoGenerateConfig();
            //vmConfig.BranchList = _branchService.GetKVPUserWise(userId);
            //vmConfig.VersionList = _versionService.GetKVP(institutionId);
            //vmConfig.ClassList = _classService.GetKVP(institutionId);
            //vmConfig.GroupList = _groupService.GetKVP(institutionId);
            //vmConfig.ShiftList = _shiftService.GetKVP(institutionId);
            //vmConfig.FacilityList = new List<KeyValuePair<int, string>>();
            //List<Facility> data = new List<Facility>();
            //data = _facilityService.GetAll(institutionId, true).ToList();
            //data.ForEach(c => vmConfig.FacilityList.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

            //vmConfig.dayList = utility.MonthInfo.Days();

            //vmConfig.FeesAutoGenerateConfig = new FeesAutoGenerateConfig();

            //vmConfig.FeesEnrollmentId = _feesAuGenEnrollService.GetById(id).FeesAutoGenerateConfigId;
            //vmConfig.FeesAutoGenerateConfig = _feesGenConfService.GetById(vmConfig.FeesEnrollmentId);

            //List<QryFeesHead> feedList = _feesHeedService.GetEnrollmentFeesHeads(institutionId).ToList();
            //var feesGenerateHeadList = new List<FeesAutoGenerateConfigDetail>();

            //if (feedList.Count() > 0)
            //{
            //    foreach (var item in feedList)
            //    {
            //        FeesAutoGenerateConfigDetail entity = new FeesAutoGenerateConfigDetail();
            //        entity.HeadName = item.Name;
            //        entity.VAT = item.VatPercent;
            //        entity.FeesHeadId = item.Id;
            //        feesGenerateHeadList.Add(entity);
            //    }

            //    vmConfig.FeesGenerateHeadList = feesGenerateHeadList;
            //}

            //vmConfig.enrollDetails = new List<VmFeesEnrollDetails>();

            //var enrollDetails = _feesAGCEDService.GetByConfId(id);

            //if (enrollDetails != null)
            //{
            //    var rslt = enrollDetails.GroupBy(g => new { g.InstallmentScheduleId, g.ForMonth, g.ForDay, g.DueForMonth, g.DueForDay, g.FeesAutoGenerateConfigEnrollmentId, g.InstituteId });

            //    foreach (var item in rslt.ToList())
            //    {
            //        VmFeesEnrollDetails tmp = new VmFeesEnrollDetails();

            //        tmp.SessionId = item.Key.InstallmentScheduleId;
            //        tmp.MonthId = item.Key.ForMonth;
            //        tmp.DayId = item.Key.ForDay;
            //        tmp.DueDayId = item.Key.DueForDay;
            //        tmp.DueMonthId = item.Key.DueForMonth;

            //        var details = new List<FeesEnrollAmount>();

            //        var preList = enrollDetails.Where(r =>
            //            r.FeesAutoGenerateConfigEnrollmentId == item.Key.FeesAutoGenerateConfigEnrollmentId
            //            && r.InstituteId == item.Key.InstituteId
            //            && r.InstallmentScheduleId == item.Key.InstallmentScheduleId
            //            && r.ForDay == tmp.DayId
            //            && r.ForMonth == tmp.MonthId
            //            && r.DueForDay == tmp.DueDayId
            //            && r.DueForMonth == tmp.DueMonthId
            //            );
            //        foreach (var i in preList.ToList())
            //        {
            //            FeesEnrollAmount t = new FeesEnrollAmount();
            //            t.Amount = i.Amount;
            //            t.HeadId = i.FeesHeadId;
            //            var vatPcnt = feedList.Where(r => r.Id == i.FeesHeadId).FirstOrDefault().VatPercent;
            //            t.Vat = ((vatPcnt * t.Amount) / 100);
            //            t.Total = i.Amount + t.Vat;
            //            details.Add(t);
            //        }

            //        tmp.AmountDetails = details;

            //        vmConfig.enrollDetails.Add(tmp);
            //    }

            //}

            //int primaryKey = vmConfig.FeesAutoGenerateConfig.Id;

            //int UnifiedMappingId = (int)utility.UnifiedMappingInfo.FeesAutoGenerateEnrollments;

            //if (!vmConfig.FeesAutoGenerateConfig.IsAllAcademicBranch)
            //{
            //    vmConfig.selectedBranches = new List<KeyValuePair<int, string>>();
            //    List<UnfiedMappingAcademicBranch> branchList = new List<UnfiedMappingAcademicBranch>();
            //    branchList = _unifiedMapService.GetUMABranch(primaryKey, UnifiedMappingId).ToList();
            //    branchList.ForEach(c => vmConfig.selectedBranches.Add(new KeyValuePair<int, string>(c.AcademicBranchId, c.AcademicBranch.Name)));

            //}

            //if (!vmConfig.FeesAutoGenerateConfig.IsAllAcademicVerssion)
            //{
            //    vmConfig.selectedVersions = new List<KeyValuePair<int, string>>();
            //    List<UnfiedMappingAcademicVerssion> versionList = new List<UnfiedMappingAcademicVerssion>();
            //    versionList = _unifiedMapService.GetUMAVersion(primaryKey, UnifiedMappingId).ToList();
            //    versionList.ForEach(c => vmConfig.selectedVersions.Add(new KeyValuePair<int, string>(c.AcademicVerssionId, c.AcademicVersion.Name)));

            //}

            //if (!vmConfig.FeesAutoGenerateConfig.IsAllAcademicClass)
            //{
            //    vmConfig.selectedClasses = new List<KeyValuePair<int, string>>();
            //    List<UnfiedMappingAcademicClass> classList = new List<UnfiedMappingAcademicClass>();
            //    classList = _unifiedMapService.GetUMAClass(primaryKey, UnifiedMappingId).ToList();
            //    classList.ForEach(c => vmConfig.selectedClasses.Add(new KeyValuePair<int, string>(c.AcademicClassId, c.AcademicClass.Name)));

            //}
            //if (!vmConfig.FeesAutoGenerateConfig.IsAllAcademicGroup)
            //{
            //    vmConfig.selectedGroups = new List<KeyValuePair<int, string>>();
            //    List<UnfiedMappingAcademicGroup> groupList = new List<UnfiedMappingAcademicGroup>();
            //    groupList = _unifiedMapService.GetUMAGroup(primaryKey, UnifiedMappingId).ToList();
            //    groupList.ForEach(c => vmConfig.selectedGroups.Add(new KeyValuePair<int, string>(c.AcademicGroupId, c.AcademicGroup.Name)));

            //}
            //if (!vmConfig.FeesAutoGenerateConfig.IsAllAcademicShift)
            //{
            //    vmConfig.selectedShifts = new List<KeyValuePair<int, string>>();
            //    List<UnfiedMappingAcademicShift> shiftList = new List<UnfiedMappingAcademicShift>();
            //    shiftList = _unifiedMapService.GetUMAShift(primaryKey, UnifiedMappingId).ToList();
            //    shiftList.ForEach(c => vmConfig.selectedShifts.Add(new KeyValuePair<int, string>(c.AcademicShiftId, c.AcademicShift.Name)));

            //}
            //vmConfig.FeesAutoGenerateConfig.FeesAutoGenerateConfigDetails = null;
            //vmConfig.FeesAutoGenerateConfig.FeesAutoGenerateConfigEnrollments = null;
            return vmConfig;
        }

        public VmFeesAutoGenerateConfig UpdateEnroll(IUnitOfWork _unitOfWork, VmFeesAutoGenerateConfig VmFeesGenModel)
        {

            VmFeesGenModel.FeesAutoGenerateConfig.LastUpdateTime = DateTime.Now;
            //_feesGenConfService.Update(VmFeesGenModel.FeesAutoGenerateConfig);
            //_unitOfWork.SaveChanges();

            //int UnifiedMappingId = (int)utility.UnifiedMappingInfo.FeesAutoGenerateEnrollments;
            //int primaryKey = VmFeesGenModel.FeesAutoGenerateConfig.Id;

            ////Delete

            //#region branch

            //List<UnfiedMappingAcademicBranch> branchList = _unifiedMapService.GetUMABranch(primaryKey, UnifiedMappingId).ToList();

            //var branchDeleted =
            //    branchList.Except(branchList.Where(x => VmFeesGenModel.selectedBranches.Any(y => y.Key == x.AcademicBranchId)))
            //        .ToList();

            //foreach (var item in branchDeleted)
            //{
            //    _unifiedMapService.DelBranchByPrKey(item.Id, _unitOfWork);

            //}

            //if (!VmFeesGenModel.FeesAutoGenerateConfig.IsAllAcademicBranch && VmFeesGenModel.FeesAutoGenerateConfig.Id != 0)
            //{
            //    var branchAdded = VmFeesGenModel.selectedBranches.Select(c => new { Id = c.Key })
            //     .Where(n => !branchList.Select(r => new { Id = r.AcademicBranchId }).Contains(n));

            //    foreach (var item in branchAdded)
            //    {
            //        UnfiedMappingAcademicBranch us = new UnfiedMappingAcademicBranch();
            //        us.UnifiedMappingId = UnifiedMappingId;
            //        us.PrimaryKeyId = VmFeesGenModel.FeesAutoGenerateConfig.Id;
            //        us.AcademicBranchId = item.Id;
            //        _unifiedMapService.SaveUMABranch(_unitOfWork, us);
            //    }

            //}
            //else
            //{
            //    foreach (var item in branchList)
            //    {
            //        _unifiedMapService.DelBranchByPrKey(item.Id, _unitOfWork);

            //    }

            //}

            //#endregion

            //#region class

            //List<UnfiedMappingAcademicClass> classList = _unifiedMapService.GetUMAClass(primaryKey, UnifiedMappingId).ToList();

            //var classDeleted =
            //    classList.Except(classList.Where(x => VmFeesGenModel.selectedClasses.Any(y => y.Key == x.AcademicClassId)))
            //        .ToList();

            //foreach (var item in classDeleted)
            //{
            //    _unifiedMapService.DelClassByPrKey(item.Id, _unitOfWork);

            //}

            //if (!VmFeesGenModel.FeesAutoGenerateConfig.IsAllAcademicClass && VmFeesGenModel.FeesAutoGenerateConfig.Id != 0)
            //{
            //    var classAdded = VmFeesGenModel.selectedClasses.Select(c => new { Id = c.Key })
            //        .Where(n => !classList.Select(r => new { Id = r.AcademicClassId }).Contains(n));

            //    foreach (var item in classAdded)
            //    {
            //        UnfiedMappingAcademicClass us = new UnfiedMappingAcademicClass();
            //        us.UnifiedMappingId = UnifiedMappingId;
            //        us.PrimaryKeyId = VmFeesGenModel.FeesAutoGenerateConfig.Id;
            //        us.AcademicClassId = item.Id;
            //        _unifiedMapService.SaveUMAClass(_unitOfWork, us);
            //    }

            //}
            //else
            //{
            //    foreach (var item in classList)
            //    {
            //        _unifiedMapService.DelClassByPrKey(item.Id, _unitOfWork);

            //    }

            //}

            //#endregion

            //#region group

            //List<UnfiedMappingAcademicGroup> groupList = _unifiedMapService.GetUMAGroup(primaryKey, UnifiedMappingId).ToList();

            //var groupDeleted =
            //    groupList.Except(groupList.Where(x => VmFeesGenModel.selectedGroups.Any(y => y.Key == x.AcademicGroupId)))
            //        .ToList();

            //foreach (var item in groupDeleted)
            //{
            //    _unifiedMapService.DelGroupByPrKey(item.Id, _unitOfWork);

            //}

            //if (!VmFeesGenModel.FeesAutoGenerateConfig.IsAllAcademicGroup && VmFeesGenModel.FeesAutoGenerateConfig.Id != 0)
            //{

            //    var groupAdded = VmFeesGenModel.selectedGroups.Select(c => new { Id = c.Key })
            //        .Where(n => !groupList.Select(r => new { Id = r.AcademicGroupId }).Contains(n));

            //    foreach (var item in groupAdded)
            //    {
            //        UnfiedMappingAcademicGroup us = new UnfiedMappingAcademicGroup();
            //        us.UnifiedMappingId = UnifiedMappingId;
            //        us.PrimaryKeyId = VmFeesGenModel.FeesAutoGenerateConfig.Id;
            //        us.AcademicGroupId = item.Id;
            //        _unifiedMapService.SaveUMAGroup(_unitOfWork, us);
            //    }

            //}
            //else
            //{
            //    foreach (var item in groupList)
            //    {
            //        _unifiedMapService.DelGroupByPrKey(item.Id, _unitOfWork);

            //    }

            //}

            //#endregion

            //#region shift

            //List<UnfiedMappingAcademicShift> shiftList = _unifiedMapService.GetUMAShift(primaryKey, UnifiedMappingId).ToList();

            //var shiftDeleted =
            //    shiftList.Except(shiftList.Where(x => VmFeesGenModel.selectedShifts.Any(y => y.Key == x.AcademicShiftId)))
            //        .ToList();

            //foreach (var item in shiftDeleted)
            //{
            //    _unifiedMapService.DelShiftByPrKey(item.Id, _unitOfWork);

            //}

            //if (!VmFeesGenModel.FeesAutoGenerateConfig.IsAllAcademicShift && VmFeesGenModel.FeesAutoGenerateConfig.Id != 0)
            //{

            //    var shiftAdded = VmFeesGenModel.selectedShifts.Select(c => new { Id = c.Key })
            //        .Where(n => !shiftList.Select(r => new { Id = r.AcademicShiftId }).Contains(n));

            //    foreach (var item in shiftAdded)
            //    {
            //        UnfiedMappingAcademicShift us = new UnfiedMappingAcademicShift();
            //        us.UnifiedMappingId = UnifiedMappingId;
            //        us.PrimaryKeyId = VmFeesGenModel.FeesAutoGenerateConfig.Id;
            //        us.AcademicShiftId = item.Id;
            //        _unifiedMapService.SaveUMAShift(_unitOfWork, us);
            //    }

            //}
            //else
            //{
            //    foreach (var item in shiftList)
            //    {
            //        _unifiedMapService.DelShiftByPrKey(item.Id, _unitOfWork);
            //    }

            //}

            //#endregion

            //#region version

            //List<UnfiedMappingAcademicVerssion> versionList = _unifiedMapService.GetUMAVersion(primaryKey, UnifiedMappingId).ToList();

            //var versionDeleted =
            //    versionList.Except(versionList.Where(x => VmFeesGenModel.selectedVersions.Any(y => y.Key == x.AcademicVerssionId)))
            //        .ToList();

            //foreach (var item in versionDeleted)
            //{
            //    _unifiedMapService.DelVersionByPrKey(item.Id, _unitOfWork);
            //}

            //if (!VmFeesGenModel.FeesAutoGenerateConfig.IsAllAcademicVerssion && VmFeesGenModel.FeesAutoGenerateConfig.Id != 0)
            //{

            //    var versionAdded = VmFeesGenModel.selectedVersions.Select(c => new { Id = c.Key })
            //        .Where(n => !versionList.Select(r => new { Id = r.AcademicVerssionId }).Contains(n));

            //    foreach (var item in versionAdded)
            //    {
            //        UnfiedMappingAcademicVerssion us = new UnfiedMappingAcademicVerssion();
            //        us.UnifiedMappingId = UnifiedMappingId;
            //        us.PrimaryKeyId = VmFeesGenModel.FeesAutoGenerateConfig.Id;
            //        us.AcademicVerssionId = item.Id;
            //        _unifiedMapService.SaveUMAVersion(_unitOfWork, us);
            //    }

            //}
            //else
            //{
            //    foreach (var item in versionList)
            //    {
            //        _unifiedMapService.DelVersionByPrKey(item.Id, _unitOfWork);
            //    }

            //}

            //#endregion


            //var enrollObj = _feesAuGenEnrollService.Get(primaryKey);
            //enrollObj.CreateDate = DateTime.Now;
            //_feesAuGenEnrollService.Update(enrollObj);

            //var detailsList = _feesAGCEDService.GetByConfId(enrollObj.Id).ToList();
            //foreach (var item in detailsList)
            //{
            //    _feesAGCEDService.Delete(item.Id);
            //}

            //if (VmFeesGenModel.enrollDetails.Count() > 0)
            //    foreach (var details in VmFeesGenModel.enrollDetails)
            //    {
            //        foreach (var tmp in details.AmountDetails)
            //        {
            //            FeesAutoGenerateConfigEnrollmentDetail newEntity = new FeesAutoGenerateConfigEnrollmentDetail();
            //            newEntity.FeesAutoGenerateConfigEnrollmentId = enrollObj.Id;
            //            newEntity.InstallmentScheduleId = details.SessionId;
            //            newEntity.InstituteId = VmFeesGenModel.FeesAutoGenerateConfig.InstituteId;
            //            newEntity.ForDay = details.DayId;
            //            newEntity.ForMonth = details.MonthId;
            //            newEntity.DueForDay = details.DueDayId;
            //            newEntity.DueForMonth = details.DueMonthId;
            //            newEntity.FeesHeadId = tmp.HeadId;
            //            newEntity.Amount = tmp.Amount;
            //            _feesAGCEDService.Insert(newEntity);
            //        }
            //    }

            //_unitOfWork.SaveChanges();

            return VmFeesGenModel;

        }

        //public IEnumerable<VmFeesAutoGenConfigEnrollmentDetail> GetFeesEnrollInsPolicyByStId(int institutionId, int studentId)
        //{
        //    var rtnList = new List<VmFeesAutoGenConfigEnrollmentDetail>();

        //    var studentDetails = _studentService.GetStudentById(studentId);

        //    var enrollConfList = _feesAuGenEnrollService.GetAll(institutionId, true);
        //    int UnifiedMappingId = (int)utility.UnifiedMappingInfo.FeesAutoGenerateEnrollments;

        //    if (enrollConfList != null)
        //    {
        //        var ids = enrollConfList.Select(c => c.FeesAutoGenerateConfigId).ToList();
        //        var genConfList = _feesGenConfService.GetAll(institutionId, ids);

        //        int feesconfgenId = 0;

        //        if ((genConfList.ToList().Where(r => r.IsAllAcademicClass == true).Count()) > 0)
        //        {
        //            //get first id
        //            feesconfgenId = genConfList.ToList().FirstOrDefault().Id;
        //        }
        //        else
        //        {
        //            //var genCnfIds = genConfList.Select(c=>c.Id).ToList();
        //            //var unfMapList = _unifiedMapService.GetUMAClass(genCnfIds, UnifiedMappingId);

        //            //if (unfMapList != null)
        //            //{
        //            //    feesconfgenId = unfMapList.ToList().Where(r => r.AcademicClassId == studentDetails.CurrentAcademicClassId.Value).FirstOrDefault().PrimaryKeyId;
        //            //}
        //            //else
        //            //{
        //            return new List<VmFeesAutoGenConfigEnrollmentDetail>();
        //            // }

        //        }

        //        // get details enroll details
        //        var result = _feesAGCEDService.GetByFeesGenConfId(feesconfgenId);
        //        if (result != null)
        //        {
        //            foreach (var tmp in result.ToList())
        //            {
        //                VmFeesAutoGenConfigEnrollmentDetail i = new VmFeesAutoGenConfigEnrollmentDetail();
        //                i.FeesAutoGenerateConfigId = feesconfgenId;
        //                i.FeesAutoGenerateConfigEnrollmentId = tmp.FeesAutoGenerateConfigEnrollmentId;
        //                i.FeesAutoGenerateConfigEnrollmentDetailId = tmp.Id;
        //                i.FeesHeadId = tmp.FeesHeadId;
        //                i.FeesHeadName = tmp.FeesHead.Name;
        //                i.VAT = tmp.FeesHead.FeesHeadGroup.VatPercent;
        //                i.ForDay = tmp.ForDay;
        //                i.ForMonth = tmp.ForMonth;
        //                i.DueForDay = tmp.DueForDay;
        //                i.DueForMonth = tmp.DueForMonth;
        //                i.Amount = tmp.Amount;
        //                i.InstallmentScheduleId = tmp.InstallmentScheduleId;
        //                i.InstallmentScheduleName = tmp.GlobalVariable.Name;
        //                rtnList.Add(i);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        return new List<VmFeesAutoGenConfigEnrollmentDetail>();
        //    }

        //    return rtnList;
        //}

    }
}
