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
    public interface IChartOfAccountService : IService<ChartOfAccount>
    {
        IEnumerable<ChartOfAccount> GetAllIncomeHeads(int instituteId, bool? isActive=null);
        IEnumerable<ChartOfAccount> GetAllExpenseHeads(int instituteId, bool? isActive=null);
        ChartOfAccount GetById(int id);
        void SaveIncomeHead(IUnitOfWorkAsync unitOfWorkAsync, ChartOfAccount incomeHead);
        void SaveExpenseHead(IUnitOfWorkAsync unitOfWorkAsync, ChartOfAccount expenseHead);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, ChartOfAccount chartOfAccountModel);
    }
    public class ChartOfAccountService : Service<ChartOfAccount>, IChartOfAccountService
    {
        private readonly IRepositoryAsync<ChartOfAccount> _redeeppitory;

        public ChartOfAccountService(IRepositoryAsync<ChartOfAccount> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }

        public IEnumerable<ChartOfAccount> GetAllIncomeHeads(int instituteId, bool? isActive = null)
        {
            if (isActive != null)
            {
                return _redeeppitory.Query(c => c.InstituteId == instituteId
                    && c.IsIncome == true
                    && c.IsActive == isActive)
                    .Select();
            }
            return _redeeppitory.Query(c => c.InstituteId == instituteId
                    && c.IsIncome == true)
                    .Select();
        }

        public IEnumerable<ChartOfAccount> GetAllExpenseHeads(int instituteId, bool? isActive = null)
        {
            if (isActive != null)
            {
                return _redeeppitory.Query(c => c.InstituteId == instituteId
                    && c.IsExpense == true
                    && c.IsActive == isActive)
                    .Select();
            }
            return _redeeppitory.Query(c => c.InstituteId == instituteId
                    && c.IsExpense == true)
                    .Select();
        }

        public ChartOfAccount GetById(int id)
        {
            return _redeeppitory.Query().Select().FirstOrDefault(x => x.Id == id);
        }

        public void SaveIncomeHead(IUnitOfWorkAsync unitOfWorkAsync, ChartOfAccount incomeHead) {

            incomeHead.IsAsset = null;
            incomeHead.IsCapital = null;
            incomeHead.IsLiabilities = null;
            incomeHead.ParentId = null;
            incomeHead.Code = null;
            incomeHead.IsIncome = true;
            incomeHead.IsExpense = null;
            incomeHead.LastUpdateTime = DateTime.Now;

            _redeeppitory.Insert(incomeHead);
            unitOfWorkAsync.SaveChanges();
        
        }

        public void SaveExpenseHead(IUnitOfWorkAsync unitOfWorkAsync, ChartOfAccount expenseHead)
        {
            expenseHead.IsAsset = null;
            expenseHead.IsCapital = null;
            expenseHead.IsLiabilities = null;
            expenseHead.ParentId = null;
            expenseHead.Code = null;
            expenseHead.IsIncome = null;
            expenseHead.IsExpense = true;
            expenseHead.LastUpdateTime = DateTime.Now;

            _redeeppitory.Insert(expenseHead);
            unitOfWorkAsync.SaveChanges();
        }

        public void Update(IUnitOfWorkAsync unitOfWorkAsync, ChartOfAccount chartOfAccountModel)
        {
            chartOfAccountModel.LastUpdateTime = DateTime.Now;
            _redeeppitory.Update(chartOfAccountModel);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
