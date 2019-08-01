using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace pnsms.erp.Api.Fees
{
    public class FeesAutoGenerateConfigController : ApiController
    {

       //private readonly IFeesAutoGenerateConfigBL _vmfeesGenConfService;
       // private readonly _IAcademicBranchService _branchService;
       // private readonly _IAcademicClassService _classService;
       // private readonly _IAcademicGroupService _groupService;
       // private readonly _IAcademicVersionService _versionService;
       // private readonly _IAcademicShiftService _shiftService;
       // private readonly _IFeesAutoGenConfigEnrollService _feesAuGenEnrollService;
       // private readonly IUnitOfWorkAsync _unitOfWorkAsync;



       // public FeesAutoGenerateConfigController(
       //     IFeesAutoGenerateConfigBL vmfeesGenConfService
       //     , _IAcademicBranchService branchService
       //     , _IAcademicClassService classService
       //     , _IAcademicGroupService groupService
       //     , _IAcademicVersionService versionService
       //     , _IAcademicShiftService shiftService
       //     , _IFeesAutoGenConfigEnrollService feesAuGenEnrollService
       //     , IUnitOfWorkAsync unitOfWorkAsync)
       // {
       //     _vmfeesGenConfService = vmfeesGenConfService;
       //     _branchService = branchService;
       //     _classService = classService;
       //     _groupService = groupService;
       //     _versionService = versionService;
       //     _shiftService = shiftService;
       //     _feesAuGenEnrollService = feesAuGenEnrollService;
       //     _unitOfWorkAsync = unitOfWorkAsync;
       // }


       // [Route("api/feesautogenerate/new")]
       // [HttpPost]
       // public VmFeesAutoGenerateConfig Create()
       // {
       //     var newModel = _vmfeesGenConfService.New(authInfo.InstituteId, authInfo.UserId);
       //     return newModel;

       // }

       // [Route("api/feesautogenerate/newEnroll")]
       // [HttpPost]
       // public VmFeesAutoGenerateConfig CreateEnroll()
       // {
       //     var newModel = _vmfeesGenConfService.NewEnroll(authInfo.InstituteId, authInfo.UserId);
       //     return newModel;

       // }

       // [Route("api/feesautogenerate/save")]
       // [HttpPost]
       // public HttpResponseMessage Post([FromBody]VmFeesAutoGenerateConfig VmFeesAutoModel)
       // {

       //     VmFeesAutoModel.FeesAutoGenerateConfig.InstituteId = authInfo.InstituteId;
       //     if (VmFeesAutoModel.FeesAutoGenerateConfig.Id == 0)
       //     {
       //         _vmfeesGenConfService.Save(_unitOfWorkAsync, VmFeesAutoModel);
       //     }
       //     else
       //     {
       //         _vmfeesGenConfService.Update(_unitOfWorkAsync, VmFeesAutoModel);
       //     }
       //     return new HttpResponseMessage(HttpStatusCode.Created);
       // }


       // [Route("api/feesautogenerate/saveEnroll")]
       // [HttpPost]
       // public HttpResponseMessage PostEnroll([FromBody]VmFeesAutoGenerateConfig VmFeesAutoModel)
       // {
       //     VmFeesAutoModel.FeesAutoGenerateConfig.InstituteId = authInfo.InstituteId;
       //     if (VmFeesAutoModel.FeesAutoGenerateConfig.Id == 0)
       //     {
       //         _vmfeesGenConfService.SaveEnroll(_unitOfWorkAsync, VmFeesAutoModel);
       //     }
       //     else
       //     {
       //         _vmfeesGenConfService.UpdateEnroll(_unitOfWorkAsync, VmFeesAutoModel);
       //     }
       //     return new HttpResponseMessage(HttpStatusCode.Created);
       // }

       // [Route("api/feesautogenerate/getsingle")]
       // public VmFeesAutoGenerateConfig GetSingleById(int id)
       // {
       //     var newModel = _vmfeesGenConfService.GetById(authInfo.InstituteId, id, authInfo.UserId);
       //     return newModel;
       // }

       // [Route("api/feesautogenerate/getsingleEnroll")]
       // public VmFeesAutoGenerateConfig GetEnrollSingleById(int id)
       // {
       //     var newModel = _vmfeesGenConfService.GetEnrollById(authInfo.InstituteId, id, authInfo.UserId);
       //     return newModel;
       // }

       // [Route("api/feesautogenerate/listEnroll")]
       // public VmSearch<VmShowList> GetAllEnrolls()
       // {
       //     return _vmfeesGenConfService.GetAllEnrollments(authInfo.InstituteId);
       // }

       // //[Route("api/feesautogenerate/newEnroll")]
       // //[HttpGet]
       // //public VmFeesAutoGenerateConfig NewEnroll()
       // //{
       // //    VmFeesAutoGenerateConfig model = new VmFeesAutoGenerateConfig();
       // //    return _vmfeesGenConfService.New(authInfo.InstituteId, authInfo.UserId);
       // //}

       // //[Route("api/feesautogenerate/getsingleEnroll")]
       // //[HttpGet]
       // //public VmFeesAutoGenerateConfig GetsingleEnrollById(int id)
       // //{

       // //    VmFeesAutoGenerateConfig model = new VmFeesAutoGenerateConfig();

       // //    var existModel = _feesAuGenEnrollService.GetByInsId(authInfo.InstituteId);

       // //    if (existModel != null)
       // //    {
       // //        model = _vmfeesGenConfService.GetById(authInfo.InstituteId, existModel.FeesAutoGenerateConfigId, authInfo.UserId);
       // //    }
       // //    else
       // //    {
       // //        model = _vmfeesGenConfService.New(authInfo.InstituteId, authInfo.UserId);
       // //    }

       // //    return model;
       // //}

       // protected override void Dispose(bool disposing)
       // {
       //     if (disposing)
       //     {
       //         _unitOfWorkAsync.Dispose();
       //     }
       //     base.Dispose(disposing);
       // }
    }
}
