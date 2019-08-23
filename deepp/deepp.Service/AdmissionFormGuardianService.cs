using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using deepp.utility;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service
{
    public interface IAdmissionFormGuardianService : IService<AdmissionFormGuardian>
    {
        IEnumerable<AdmissionFormGuardian> GetGuadianByApplicantFormId(int applicantFormId);
        AdmissionFormGuardian newAdmissionFormGuardian(int instituteId);
    }
    public class AdmissionFormGuardianService : Service<AdmissionFormGuardian>, IAdmissionFormGuardianService
    {
        private readonly IRepositoryAsync<AdmissionFormGuardian> _redeeppitory;
        private readonly IGuardianTypeService _guardianTypeService;
        private readonly IProfessionService _professionService;
        private readonly IEducationalQualificationService _educationalQualificationService;
        public AdmissionFormGuardianService(
            IRepositoryAsync<AdmissionFormGuardian> redeeppitory
            , IGuardianTypeService guardianTypeService
            , IProfessionService professionService
            , IEducationalQualificationService educationalQualificationService
            )
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
            _guardianTypeService = guardianTypeService;
            _professionService = professionService;
            _educationalQualificationService = educationalQualificationService;

        }

        public IEnumerable<AdmissionFormGuardian> GetGuadianByApplicantFormId(int applicantFormId)
        {
            return _redeeppitory.Query(x => x.AdmissionFormId == applicantFormId)
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
