using pnsms.Entities.Models;
using pnsms.Entities.ViewModels;
using pnsms.Service;
using pnsms.Service.ViewModels;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace pnsms.erp.Api
{
    public class VoucherEntryController : ApiController
    {
        private readonly IVmVoucherService _vmVoucherService;
        private readonly IAcademicBranchService _branchService;
        private readonly IVoucherService _voucherService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        int institutionId = Sessions.InstituteId;

        public VoucherEntryController(
              IVmVoucherService vmVoucherService
            , IAcademicBranchService branchService
            , IVoucherService voucherService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _vmVoucherService = vmVoucherService;
            _branchService = branchService;
            _voucherService = voucherService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("api/voucherEntry/new")]
        [HttpPost]
        public VmVoucher AddVoucher()
        {
            var voucherCreate = _vmVoucherService.CreateNew(institutionId);
            return voucherCreate;
        }

        [Route("api/voucherEntry/list")]
        [HttpPost]
        public VmSearch<Voucher> GetAllCertificatePrints([FromBody]VmSearch<Voucher> voucherListModel)
        {

            voucherListModel = voucherListModel ?? new VmSearch<Voucher>();
            voucherListModel.InstituteId = institutionId;
            var branchList = _branchService.GetKVP(voucherListModel.InstituteId);
            voucherListModel.DropDownList1 = branchList;
            voucherListModel.DropDownId1 = voucherListModel.DropDownId1 > 0 ? voucherListModel.DropDownId1 : branchList.FirstOrDefault().Key;

            int branchId= voucherListModel.DropDownId1;
            DateTime start = voucherListModel.startDateModel;
            DateTime end = voucherListModel.endDateModel;
            IEnumerable<Voucher> resultlist = _voucherService.GetAll(institutionId, branchId, start, end);
            voucherListModel.SearchData = resultlist;
            return voucherListModel;

        }

        [Route("api/voucherEntry/save")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]VmVoucher vmVoucherModel)
        {

            bool status = _vmVoucherService.Save(vmVoucherModel, _unitOfWorkAsync);

            if (status)
            {

                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            else { return new HttpResponseMessage(HttpStatusCode.Conflict); }
        }

        [Route("api/voucherEntry/update")]
        [HttpPost]
        public HttpResponseMessage UpdateVoucher([FromBody]VmVoucher vmVoucherModel)
        {

            _vmVoucherService.Update(vmVoucherModel, _unitOfWorkAsync);

            return new HttpResponseMessage(HttpStatusCode.Created); 
        }

        [Route("api/voucherEntry/getsingle")]
        public VmVoucher GetSingleCertificatePrintById(int id)
        {
            VmVoucher vmVoucherModel = _vmVoucherService.GetDetailsById(id, institutionId);

            return vmVoucherModel; ;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
