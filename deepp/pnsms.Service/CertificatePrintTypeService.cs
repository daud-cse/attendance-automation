using pnsms.Entities.Models;
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
    public interface ICertificatePrintTypeService : IService<CertificatePrintType>
    {
        List<KeyValuePair<int, string>> GetKVP(int instituteId);
        IEnumerable<CertificatePrintType> GetCertificatePrintTypes(int instituteId, bool? isActive);
        CertificatePrintType GetCertificatePrintTypeById(int id);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, CertificatePrintType certificatePrintType);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, CertificatePrintType certificatePrintType);
    }

    public class CertificatePrintTypeService : Service<CertificatePrintType>, ICertificatePrintTypeService
    {
        private readonly IRepositoryAsync<CertificatePrintType> _repository;

        public CertificatePrintTypeService(IRepositoryAsync<CertificatePrintType> repository)
            : base(repository)
        {
            _repository = repository;

        }
        public List<KeyValuePair<int, string>> GetKVP(int instituteId)
        {
            var data = _repository.Query(r => r.InstituteId == instituteId && r.IsActive).Select().ToList();
            var certificateList = new List<KeyValuePair<int, string>>();
            data.ForEach(c => certificateList.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

            return certificateList;
        }

        public IEnumerable<CertificatePrintType> GetCertificatePrintTypes(int instituteId, bool? isActive)
        {
            if (isActive==true)
            {
                return _repository.Query(d => d.IsActive.Equals(true) && d.InstituteId == instituteId).Select();
            }

            return _repository.Query(d => d.InstituteId == instituteId).Select();
        }

        public CertificatePrintType GetCertificatePrintTypeById(int id)
        {
            return _repository.Query(x => x.Id == id).Select().FirstOrDefault();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, CertificatePrintType certificatePrintType)
        {
            certificatePrintType.LastUpdateTime = DateTime.Now;
            _repository.Insert(certificatePrintType);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, CertificatePrintType certificatePrintType)
        {
            certificatePrintType.LastUpdateTime = DateTime.Now;
            _repository.Update(certificatePrintType);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
