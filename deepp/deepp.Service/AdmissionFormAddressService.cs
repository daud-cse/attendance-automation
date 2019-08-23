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
    public interface IAdmissionFormAddressService : IService<AdmissionFormAddress>
    {
        IEnumerable<AdmissionFormAddress> GetAddressByApplicantFormId(int applicantFormId);
        AdmissionFormAddress newAdmissionFormAddress(int instituteId);
    }
    public class AdmissionFormAddressService : Service<AdmissionFormAddress>, IAdmissionFormAddressService
    {
        private readonly IRepositoryAsync<AdmissionFormAddress> _redeeppitory;
        private readonly IDistrictOrStateService _districtOrStateService;
        public AdmissionFormAddressService(
            IRepositoryAsync<AdmissionFormAddress> redeeppitory, IDistrictOrStateService districtOrStateService
            )
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
            _districtOrStateService = districtOrStateService;

        }

        public IEnumerable<AdmissionFormAddress> GetAddressByApplicantFormId(int applicantFormId)
        {
            return _redeeppitory.Query(x => x.AdmissionFormId == applicantFormId).Include(x => x.DistrictOrState).Select();

        }
        public AdmissionFormAddress newAdmissionFormAddress( int instituteId)
        {
             var admissionFormAddress=new AdmissionFormAddress();
            admissionFormAddress.DistrictOrStateList = new List<KeyValuePair<int, string>>();
            var districtOrStateList = _districtOrStateService.GetActiveDistrictOrStateByinstituteId(instituteId).ToList();
            if (districtOrStateList.Count!=0) {
                districtOrStateList.ForEach(item => admissionFormAddress.DistrictOrStateList.Add(new KeyValuePair<int, string>(item.Id, item.Name)));
            }
            

            return admissionFormAddress;
        }

    }
}
