using pnsms.Entities.Models;
using pnsms.Entities.ViewModels;
using pnsms.utility.Resource;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service.ViewModels
{
    public interface IVmOnlineAdmissionService
    {
        VmSearch<AdmissionForm> GetAllList(VmSearch<AdmissionForm> admissionFormListModel);
        VmOnlineAdmission GetApplicantById(int Id);
        bool SaveOnlineAdmission(IUnitOfWork _unitOfWork, VmOnlineAdmission vmOnlineAdmission);
        VmOnlineAdmission newVmOnlineAdmission(int InstituteId);
    }

    public class VmOnlineAdmissionService : IVmOnlineAdmissionService
    {
        private readonly IAdmissionFormService _admissionFormService;
        private readonly IAdmissionFormAddressService _admissionAddressService;
        private readonly IAdmissionFormGuardianService _admissionGuardianService;
        private readonly IAcademicBranchService _branchService;
        private readonly IAcademicClassService _classService;
        private readonly IAcademicSessionService _sessionService;

        public VmOnlineAdmissionService(
              IAdmissionFormService admissionFormService
            , IAdmissionFormAddressService admissionAddressService
            , IAdmissionFormGuardianService admissionGuardianService
            , IAcademicBranchService branchService
            , IAcademicClassService classService
            , IAcademicSessionService sessionService
            )
        {

            _admissionFormService = admissionFormService;
            _admissionAddressService = admissionAddressService;
            _admissionGuardianService = admissionGuardianService;
            _branchService = branchService;
            _classService = classService;
            _sessionService = sessionService;
        }

        public VmSearch<AdmissionForm> GetAllList(VmSearch<AdmissionForm> admissionFormListModel)
        {
            var branchList = _branchService.GetKVP(admissionFormListModel.InstituteId);
            admissionFormListModel.DropDownList1 = branchList;
            admissionFormListModel.DropDownId1 = admissionFormListModel.DropDownId1 > 0 ? admissionFormListModel.DropDownId1 : branchList.FirstOrDefault().Key;
            var sessList = _sessionService.GetKVP(admissionFormListModel.InstituteId);
            admissionFormListModel.DropDownList2 = sessList;
            admissionFormListModel.DropDownId2 = admissionFormListModel.DropDownId2 > 0 ? admissionFormListModel.DropDownId2 : sessList.FirstOrDefault().Key;
            var classList = _classService.GetKVP(admissionFormListModel.InstituteId);
            admissionFormListModel.DropDownList3 = classList;
            admissionFormListModel.DropDownId3 = admissionFormListModel.DropDownId3 > 0 ? admissionFormListModel.DropDownId3 : classList.FirstOrDefault().Key;
            admissionFormListModel.selectedStatus = admissionFormListModel.selectedStatus ? admissionFormListModel.selectedStatus : false;

            IEnumerable<AdmissionForm> list = _admissionFormService.GetAllBySearch(admissionFormListModel);
            admissionFormListModel.SearchData = list;
            return admissionFormListModel;
        }

        public VmOnlineAdmission GetApplicantById(int applicantId)
        {

            VmOnlineAdmission applicantModel = new VmOnlineAdmission();

            AdmissionForm details = _admissionFormService.GetDetailsByApplicantFormId(applicantId);
            applicantModel.AdmissionForm = details;

            if (applicantModel.AdmissionForm == null)
            {
                throw new ValidationException(Errors.InvalidStudent);
            }

            IEnumerable<AdmissionFormAddress> addressDetailList = _admissionAddressService.GetAddressByApplicantFormId(applicantId);
            applicantModel.AdmissionFormAddress = addressDetailList;

            IEnumerable<AdmissionFormGuardian> gurdianDetailList = _admissionGuardianService.GetGuadianByApplicantFormId(applicantId);
            applicantModel.AdmissionFormGuardian = gurdianDetailList;

            return applicantModel;
        }
        public bool SaveOnlineAdmission(IUnitOfWork _unitOfWork, VmOnlineAdmission vmOnlineAdmission)
        {
            vmOnlineAdmission.AdmissionForm.IsActive = true;
            vmOnlineAdmission.AdmissionForm.IsSelected = false;
            vmOnlineAdmission.AdmissionForm.LastUpdateTime = DateTime.Now;
            vmOnlineAdmission.AdmissionForm.Name = vmOnlineAdmission.AdmissionForm.FirstName + " " + vmOnlineAdmission.AdmissionForm.MiddleName + " " + vmOnlineAdmission.AdmissionForm.LastName;
            _admissionFormService.Insert(vmOnlineAdmission.AdmissionForm);
            _unitOfWork.SaveChanges();
            AdmissionFormAddress admissionFormAddres = new AdmissionFormAddress();
            vmOnlineAdmission.AdmissionFormAddres.AdmissionFormId = vmOnlineAdmission.AdmissionForm.Id;
            admissionFormAddres = vmOnlineAdmission.AdmissionFormAddres;
            _admissionAddressService.Insert(admissionFormAddres);
            //_unitOfWork.SaveChanges();
            AdmissionFormGuardian admissionFormGuardian = new AdmissionFormGuardian();
            admissionFormGuardian.AdmissionFormId = vmOnlineAdmission.AdmissionForm.Id;
            admissionFormGuardian.GuardianTypeId = vmOnlineAdmission.AdmissionFormGuardians.GuardianTypeId;
            _admissionGuardianService.Insert(admissionFormGuardian);
            _unitOfWork.SaveChanges();
            return true;
        }

        public VmOnlineAdmission newVmOnlineAdmission(int InstituteId)
        {
            VmOnlineAdmission vmOnlineAdmission = new VmOnlineAdmission();
            vmOnlineAdmission.AdmissionForm = _admissionFormService.newadmissionForm(InstituteId);
            vmOnlineAdmission.AdmissionFormAddres = _admissionAddressService.newAdmissionFormAddress(InstituteId);
            vmOnlineAdmission.AdmissionFormGuardians = _admissionGuardianService.newAdmissionFormGuardian(InstituteId);
            return vmOnlineAdmission;
        }


    }
}
