using pnsms.Entities.Models;
using pnsms.Entities.ViewModels;
using pnsms.utility;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service
{
    public interface IAdmissionFormGuardianService : IService<AdmissionFormGuardian>
    {
        IEnumerable<AdmissionFormGuardian> GetGuadianByApplicantFormId(int applicantFormId);
        AdmissionFormGuardian newAdmissionFormGuardian(int instituteId);
    }
    public class AdmissionFormGuardianService : Service<AdmissionFormGuardian>, IAdmissionFormGuardianService
    {
        private readonly IRepositoryAsync<AdmissionFormGuardian> _repository;
        private readonly IGuardianTypeService _guardianTypeService;
        private readonly IProfessionService _professionService;
        private readonly IEducationalQualificationService _educationalQualificationService;
        public AdmissionFormGuardianService(
            IRepositoryAsync<AdmissionFormGuardian> repository
            , IGuardianTypeService guardianTypeService
            , IProfessionService professionService
            , IEducationalQualificationService educationalQualificationService
            )
            : base(repository)
        {
            _repository = repository;
            _guardianTypeService = guardianTypeService;
            _professionService = professionService;
            _educationalQualificationService = educationalQualificationService;

        }

        public IEnumerable<AdmissionFormGuardian> GetGuadianByApplicantFormId(int applicantFormId)
        {
            return _repository.Query(x => x.AdmissionFormId == applicantFormId)
                .Include(x => x.GuardianType)
                .Include(x => x.EducationalQualification)
                .Include(x => x.Profession)
                .Select();

        }

        public AdmissionFormGuardian newAdmissionFormGuardian(int instituteId)
        {
            var admissionFormGuardian = new AdmissionFormGuardian();
            var lstguardianTypeKv = new List<KeyValuePair<int, string>>();
            _guardianTypeService.GetGuardianTypeByInstituteId(instituteId).ToList().ForEach(item => lstguardianTypeKv.Add(new KeyValuePair<int, string>(item.Id, item.Name)));
            admissionFormGuardian.GuardianTypesList = lstguardianTypeKv;
            admissionFormGuardian.EducationalQualificationList = _educationalQualificationService.GetEducationalQualificationByInstituteId(instituteId).Select(e => new KeyValuePair<int, string>(e.Id, e.Name)).ToList();
            admissionFormGuardian.ProfessionList = _professionService.GetProfessionByInstituteId(instituteId).Select(e => new KeyValuePair<int, string>(e.Id, e.Name)).ToList();
            return admissionFormGuardian;
        }
    }
}
