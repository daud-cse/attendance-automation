using pnsms.Entities.Models;
using pnsms.Entities.ViewModels;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service
{
    public interface IContactUService:IService<ContactU>
    {
        IEnumerable<ContactU> GetContactUS();
        IEnumerable<ContactU> GetContactUS(bool isActive);
        IEnumerable<ContactU> GetActiveContactUS();
        ContactU GetContactUSById(int id);
        IEnumerable<ContactU> GetContactUSByInstituteId(int instituteId);
        IEnumerable<ContactU> GetAllBySearch(VmSearch<ContactU> mcontactModel);

    }
    public class ContactUService :Service<ContactU>, IContactUService
    {
        private readonly IRepositoryAsync<ContactU> _repository;


        public ContactUService(IRepositoryAsync<ContactU> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public IEnumerable<ContactU> GetContactUS()
        {

            return _repository.Query().Select();
        }

        public IEnumerable<ContactU> GetContactUS(bool isActive)
        {
            if (isActive)
            {
                return _repository.Query().Select();
            }

            return _repository.Query().Select();
        }

        public IEnumerable<ContactU> GetActiveContactUS()
        {
            return _repository.Query().Select();
        }
        public ContactU GetContactUSById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<ContactU> GetContactUSByInstituteId(int instituteId)
        {
            return _repository.Query().Select().Where(d=>d.InstituteId == instituteId);
        }

        public IEnumerable<ContactU> GetAllBySearch(VmSearch<ContactU> mcontactModel)
        {
            DateTime start = mcontactModel.startDateModel;
            DateTime end = mcontactModel.endDateModel.AddDays(1);

            return _repository.Query(p => 
                p.InstituteId == mcontactModel.InstituteId
                && p.CreateDate >= start.Date && p.CreateDate < end).Select();



        }
    }
}
