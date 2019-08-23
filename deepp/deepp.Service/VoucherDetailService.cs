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
    public interface IVoucherDetailService : IService<VoucherDetail>
    {
        IEnumerable<VoucherDetail> GetAll(int voucherId);
        IEnumerable<VoucherDetail> GetAll(int instituteId, int year);


    }
    public class VoucherDetailService : Service<VoucherDetail>, IVoucherDetailService
    {
        private readonly IRepositoryAsync<VoucherDetail> _redeeppitory;

        public VoucherDetailService(IRepositoryAsync<VoucherDetail> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }

        public IEnumerable<VoucherDetail> GetAll(int voucherId)
        {
            return _redeeppitory.Query(c => c.VoucherId == voucherId)
                    .Include(c=>c.ChartOfAccount)
                    .Select();
        }

        public IEnumerable<VoucherDetail> GetAll(int instituteId, int year)
        {
            DateTime frm = DateTime.Parse("1/1/" + year);
            DateTime to = DateTime.Parse("1/1/" + (year + 1));

            var data = _redeeppitory
                .Query(c => c.Voucher.VoucherDate >= frm && c.Voucher.VoucherDate < to)
                .Include(c => c.Voucher)
                .Include(c => c.ChartOfAccount)
                .Select();

            return data;
        }
        
    }
}
