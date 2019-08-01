using pnsms.Entities.Models;
using pnsms.Entities.ViewModels;
using pnsms.utility;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service.ViewModels
{
    public interface IVmVoucherService
    {
        VmVoucher CreateNew(int institutionId);
        VmVoucher GetDetailsById(int voucherId, int institutionId);
        bool Save(VmVoucher vmVoucherModel, IUnitOfWorkAsync unitOfWorkAsync);
        void Update(VmVoucher vmVoucherModel, IUnitOfWorkAsync unitOfWorkAsync);
    }

    public class VmVoucherService : IVmVoucherService
    {
        private readonly IVoucherService _voucherService;
        private readonly IVoucherDetailService _voucherDetailService;
        private readonly IChartOfAccountService _chartOfAccountService;
        private readonly IAcademicBranchService _branchService;

        public VmVoucherService(
              IVoucherService voucherService
            , IVoucherDetailService voucherDetailService
            , IChartOfAccountService chartOfAccountService
            , IAcademicBranchService branchService
            )
        {

            _voucherService = voucherService;
            _voucherDetailService = voucherDetailService;
            _chartOfAccountService = chartOfAccountService;
            _branchService = branchService;
        }


        public VmVoucher CreateNew(int institutionId)
        {

            VmVoucher vmVoucherModel = new VmVoucher();

            vmVoucherModel.Voucher = new Voucher();
            var branchList = _branchService.GetKVP(institutionId);
            vmVoucherModel.Voucher.AcademicBranchList = branchList;
            vmVoucherModel.Voucher.AcademicBranchId = branchList.FirstOrDefault().Key;

            vmVoucherModel.Voucher.InstituteId = institutionId;
            vmVoucherModel.Voucher.VoucherDate = DateTime.Today;

            vmVoucherModel.Voucher.IsActive = true;
            vmVoucherModel.Voucher.TotalAmount = 0;


            var incomeList = _chartOfAccountService.GetAllIncomeHeads(institutionId, true);

            var incomeHeadList = new List<VoucherDetail>();

            if (incomeList.Count() > 0)
            {
                foreach (ChartOfAccount item in incomeList)
                {
                    VoucherDetail entity = new VoucherDetail();
                    entity.HeadName = item.Name;
                    entity.ChartOfAccountId = item.Id;
                    entity.Amount = 0;
                    incomeHeadList.Add(entity);

                }

                vmVoucherModel.IncomeList = incomeHeadList;
            }

            var expenseList = _chartOfAccountService.GetAllExpenseHeads(institutionId, true);

            var expenseHeadList = new List<VoucherDetail>();

            if (expenseList.Count() > 0)
            {
                foreach (ChartOfAccount item in expenseList)
                {
                    VoucherDetail entity = new VoucherDetail();
                    entity.HeadName = item.Name;
                    entity.ChartOfAccountId = item.Id;
                    entity.Amount = 0;
                    expenseHeadList.Add(entity);

                }

                vmVoucherModel.ExpenseList = expenseHeadList;
            }

            return vmVoucherModel;

        }

        public bool Save(VmVoucher vmVoucherModel, IUnitOfWorkAsync unitOfWorkAsync) {

            //insert data to voucher Table
            vmVoucherModel.Voucher.LastUpdateTime=DateTime.Today;
            vmVoucherModel.Voucher.TotalAmount=0;

            _voucherService.Insert(vmVoucherModel.Voucher);
            unitOfWorkAsync.SaveChanges();

            int voucherId = vmVoucherModel.Voucher.Id;

            if (voucherId != 0) {

                    if (vmVoucherModel.IncomeList != null) {
                        foreach (var details in vmVoucherModel.IncomeList)
                        {
                                VoucherDetail newEntity= new VoucherDetail();
                                newEntity.VoucherId=voucherId;
                                newEntity.Amount=details.Amount;
                                newEntity.ChartOfAccountId=details.ChartOfAccountId;
                                _voucherDetailService.Insert(newEntity); 
                        }
                    }

                     if (vmVoucherModel.ExpenseList != null) {
                            foreach (var details in vmVoucherModel.ExpenseList)
                            {
                                    VoucherDetail newEntity= new VoucherDetail();
                                    newEntity.VoucherId=voucherId;
                                    newEntity.Amount=details.Amount;
                                    newEntity.ChartOfAccountId=details.ChartOfAccountId;
                                    _voucherDetailService.Insert(newEntity); 
                            }
                        }

                unitOfWorkAsync.SaveChanges();
                return true;
            }

            return false;
       }

        public VmVoucher GetDetailsById(int voucherId, int institutionId)
        {
            VmVoucher voucherModel = new VmVoucher();
            voucherModel.Voucher = new Voucher();
            voucherModel.Voucher= _voucherService.GetById(voucherId);
            voucherModel.Voucher.BranchName=voucherModel.Voucher.AcademicBranch.Name;

            var branchList = _branchService.GetKVP(institutionId);
            voucherModel.Voucher.AcademicBranchList = branchList;


            var List = _voucherDetailService.GetAll(voucherModel.Voucher.Id);

            var incomeList = List.Where(p=>p.ChartOfAccount.IsIncome==true);

            var incomeHeadList = new List<VoucherDetail>();

            if (List.Count() > 0)
            {
                foreach (VoucherDetail item in incomeList)
                {
                    VoucherDetail entity = new VoucherDetail();
                    entity.Id = item.Id;
                    entity.HeadName = item.ChartOfAccount.Name;
                    entity.Amount = item.Amount;
                    entity.ChartOfAccountId = item.ChartOfAccountId;
                    entity.Narration = item.Narration;
                    entity.VoucherId = item.VoucherId;
                    incomeHeadList.Add(entity);

                }

                voucherModel.IncomeList = incomeHeadList;
            }

            var expenseList = List.Where(p => p.ChartOfAccount.IsExpense == true);

            var expenseHeadList = new List<VoucherDetail>();

            if (expenseList.Count() > 0)
            {
                foreach (VoucherDetail item in expenseList)
                {
                    VoucherDetail entity = new VoucherDetail();
                    entity.Id = item.Id;
                    entity.HeadName = item.ChartOfAccount.Name;
                    entity.Amount = item.Amount;
                    entity.ChartOfAccountId = item.ChartOfAccountId;
                    entity.Narration = item.Narration;
                    entity.VoucherId = item.VoucherId;
                    expenseHeadList.Add(entity);

                }

                voucherModel.ExpenseList = expenseHeadList;
            }

            return voucherModel;
        
        }

        public void Update(VmVoucher vmVoucherModel, IUnitOfWorkAsync unitOfWorkAsync)
        {
            Voucher newVoucher = new Voucher();
            newVoucher.Id = vmVoucherModel.Voucher.Id;
            newVoucher.InstituteId = vmVoucherModel.Voucher.InstituteId;
            newVoucher.AcademicBranchId = vmVoucherModel.Voucher.AcademicBranchId;
            newVoucher.VoucherDate = vmVoucherModel.Voucher.VoucherDate;
            newVoucher.Narration = vmVoucherModel.Voucher.Narration;
            newVoucher.TotalAmount = vmVoucherModel.Voucher.TotalAmount;
            newVoucher.IsActive = vmVoucherModel.Voucher.IsActive;
            newVoucher.IsIncome = null;
            newVoucher.IsExpense = null;
            newVoucher.LastUpdateTime = DateTime.Now;

            _voucherService.Update(newVoucher);
            unitOfWorkAsync.SaveChanges();

                if (vmVoucherModel.IncomeList != null)
                {
                    foreach (var details in vmVoucherModel.IncomeList)
                    {
                        VoucherDetail newEntity = new VoucherDetail();
                        newEntity.Id = details.Id;
                        newEntity.VoucherId = details.VoucherId;
                        newEntity.Amount = details.Amount;
                        newEntity.ChartOfAccountId = details.ChartOfAccountId;
                        newEntity.Narration = details.Narration;
                        _voucherDetailService.Update(newEntity);
                    }
                }

                if (vmVoucherModel.ExpenseList != null)
                {
                    foreach (var details in vmVoucherModel.ExpenseList)
                    {
                        VoucherDetail newEntity = new VoucherDetail();
                        newEntity.Id = details.Id;
                        newEntity.VoucherId = details.VoucherId;
                        newEntity.Amount = details.Amount;
                        newEntity.ChartOfAccountId = details.ChartOfAccountId;
                        newEntity.Narration = details.Narration;
                        _voucherDetailService.Update(newEntity);
                    }
                }

                unitOfWorkAsync.SaveChanges();
            }
      }
}

