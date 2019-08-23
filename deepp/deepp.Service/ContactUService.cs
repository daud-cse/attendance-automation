using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service
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
        private readonly IRepositoryAsync<ContactU> _redeeppitory;


        public ContactUService(IRepositoryAsync<ContactU> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }


        public IEnumerable<ContactU> GetContactUS()
        {

            return _redeeppitory.Query().Select();
        }

        public IEnumerable<ContactU> GetContactUS(bool isActive)
        {
            if (isActive)
            {
                return _redeeppitory.Query().Select();
            }

            return _redeeppitory.Query().Select();
        }

        public IEnumerable<ContactU> GetActiveContactUS()
        {
            return _redeeppitory.Query().Select();
        }
        public ContactU GetContactUSById(int id)
        {
            return _redeeppitory.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<ContactU> GetContactUSByInstituteId(int instituteId)
        {
            return _redeeppitory.Query().Select().Where(d=>d.InstituteId == instituteId);
        }

        public IEnumerable<ContactU> GetAllBySearch(VmSearch<ContactU> mcontactModel)
        {
            DateTime start = mcontactModel.startDateModel;
            DateTime end = mcontactModel.endDateModel.AddDays(1);

            return _redeeppitory.Query(p => 
                p.InstituteId == mcontactModel.InstituteId
                && p.CreateDate >= start.Date && p.CreateDate < end).Select();



        }
    }
}
