using deepp.Entities.Models;
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
    public interface IVoucherService : IService<Voucher>
    {
        IEnumerable<Voucher> GetAll(int instituteId, int academicBranchId, bool? isActive = null);
        IEnumerable<Voucher> GetAll(int instituteId, int academicBranchId, DateTime startDate, DateTime endDate);
        Voucher GetById(int id);
    }
    public class VoucherService : Service<Voucher>, IVoucherService
    {
        private readonly IRepositoryAsync<Voucher> _redeeppitory;

        public VoucherService(IRepositoryAsync<Voucher> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }

        public IEnumerable<Voucher> GetAll(int instituteId, int academicBranchId, bool? isActive = null)
        {
            if (isActive != null)
            {
                return _redeeppitory.Query(c => c.InstituteId == instituteId
                    && c.AcademicBranchId == academicBranchId
                    && c.IsActive == isActive)
                    .Select();
            }
            return _redeeppitory.Query(c => c.InstituteId == instituteId
                    && c.AcademicBranchId == academicBranchId)
                    .Select();
        }

        public IEnumerable<Voucher> GetAll(int instituteId, int academicBranchId, DateTime startDate, DateTime endDate)
        {
            DateTime start = startDate;
            DateTime end = endDate.AddDays(1);

            return _redeeppitory.Query(c => c.InstituteId == instituteId
                    && c.AcademicBranchId == academicBranchId
                    && c.VoucherDate >= start && c.VoucherDate < end)
                    .Select();
        }

        public Voucher GetById(int id)
        {
            return _redeeppitory.Query()
                .Include(p=>p.AcademicBranch).Select().FirstOrDefault(x => x.Id == id);
        }

       
        
    }
}
