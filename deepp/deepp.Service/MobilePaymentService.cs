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
    public interface IMobilePaymentService
    {

        MobilePayment AddNewMobilePayment(int instituteId);
        MobilePayment GetMobilePaymentById(int id);
        IEnumerable<MobilePayment> GetAllMobilePaymentListBySearch(VmSearch<MobilePayment> mPaymentModel);
        bool SaveMobilePayment(IUnitOfWorkAsync unitOfWorkAsync, MobilePayment mPaymentModel);
        bool UpdateMobilePayment(IUnitOfWorkAsync unitOfWorkAsync, MobilePayment mPaymentModel);
        IEnumerable<MobilePayment> GetPaymentsByStudentId(int studentId);
    }

    public class MobilePaymentService : Service<MobilePayment>, IMobilePaymentService
    {
        private readonly IRepositoryAsync<MobilePayment> _redeeppitory;
        private readonly IPaymentTypeService _paymentTypeService;
        private readonly IUserInfoService _userInfoService;
        List<Notice> Notice = new List<Notice>();


        public MobilePaymentService(IRepositoryAsync<MobilePayment> redeeppitory, IPaymentTypeService paymentTypeService,
            IUserInfoService userInfoService)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
            _paymentTypeService = paymentTypeService;
            _userInfoService = userInfoService;

        }

        public MobilePayment AddNewMobilePayment(int instituteId)
        {
            MobilePayment mPaymentModel = new MobilePayment();
            mPaymentModel.InstituteId = instituteId;
            mPaymentModel.TransactionDate = DateTime.Now.Date;
            mPaymentModel.PaymentTypeList = _paymentTypeService.GetKVP();
            return mPaymentModel;
        }

        public bool SaveMobilePayment(IUnitOfWorkAsync unitOfWorkAsync, MobilePayment mPaymentModel)
        {
            mPaymentModel.LastUpdateTime = DateTime.Now;
            int RefTypeId = (int)utility.UserInfoType.Student;
            UserInfo userDetails = new UserInfo();
            if (mPaymentModel.studentPin != null)
            {
                userDetails = _userInfoService.GetUserDetailsByPinOrMobile(mPaymentModel.studentPin, null, mPaymentModel.InstituteId, RefTypeId);
            }
            else 
            {
                userDetails = _userInfoService.GetUserDetailsByPinOrMobile(null, mPaymentModel.MobileNo, mPaymentModel.InstituteId, RefTypeId);            
            }

            if (userDetails != null)
            {
                mPaymentModel.ReffStudentId = userDetails.Id;


                _redeeppitory.Insert(mPaymentModel);
                unitOfWorkAsync.SaveChanges();

                return true;
            }
            else { return false; }

        }

        public bool UpdateMobilePayment(IUnitOfWorkAsync unitOfWorkAsync, MobilePayment mPaymentModel)
        {
            int RefTypeId = (int)utility.UserInfoType.Student;
            UserInfo userDetails = new UserInfo();
            if (mPaymentModel.studentPin != null)
            {
                userDetails = _userInfoService.GetUserDetailsByPinOrMobile(mPaymentModel.studentPin, null, mPaymentModel.InstituteId, RefTypeId);
            }
            else
            {
                userDetails = _userInfoService.GetUserDetailsByPinOrMobile(null, mPaymentModel.MobileNo, mPaymentModel.InstituteId, RefTypeId);
            }

            if (userDetails != null)
            {
                MobilePayment entrymPaymentModel = new MobilePayment();
                entrymPaymentModel.Id = mPaymentModel.Id;
                entrymPaymentModel.ReffStudentId = userDetails.Id;
                entrymPaymentModel.InstituteId = mPaymentModel.InstituteId;
                entrymPaymentModel.MobileNo = mPaymentModel.MobileNo;
                entrymPaymentModel.TransactionDate = mPaymentModel.TransactionDate;
                entrymPaymentModel.TransactionId = mPaymentModel.TransactionId;
                entrymPaymentModel.PaymentTypeId = mPaymentModel.PaymentTypeId;
                entrymPaymentModel.Payment = mPaymentModel.Payment;
                entrymPaymentModel.LastActionBy = mPaymentModel.LastActionBy;
                entrymPaymentModel.LastUpdateTime = DateTime.Now;

                _redeeppitory.Update(entrymPaymentModel);
                unitOfWorkAsync.SaveChanges();

                return true;
            }
            else { return false; }

        }

        public MobilePayment GetMobilePaymentById(int id)
        {
            var payment = _redeeppitory.Query(x => x.Id == id)
                .Include(x => x.PaymentType)
                .Include(s => s.Student.UserInfo)
                .Select().SingleOrDefault();
            return payment;
        }

        public IEnumerable<MobilePayment> GetAllMobilePaymentListBySearch(VmSearch<MobilePayment> mPaymentModel)
        {
            DateTime start = mPaymentModel.startDateModel;
            DateTime end = mPaymentModel.endDateModel.AddDays(1);
            int RefTypeId = (int)utility.UserInfoType.Student;

            var predicate = PredicateBuilder.True<MobilePayment>();
            predicate = predicate.And(p => p.TransactionDate >= start.Date && p.TransactionDate < end);
            if (mPaymentModel.TextFieldId1 != null || mPaymentModel.TextFieldId2 != null || mPaymentModel.StudentPin != null)
            {

                if (mPaymentModel.StudentPin != null)
                {
                    UserInfo userDetails = _userInfoService.GetUserDetailsByPinOrMobile(mPaymentModel.StudentPin, null, mPaymentModel.InstituteId, RefTypeId);
                    if (userDetails != null)
                    predicate = predicate.And(p => p.ReffStudentId == userDetails.Id);
                }

                if (mPaymentModel.TextFieldId1 != null)
                    predicate = predicate.And(p => p.MobileNo == mPaymentModel.TextFieldId1);

                if (mPaymentModel.TextFieldId2 != null)
                    predicate = predicate.And(p => p.TransactionId == mPaymentModel.TextFieldId2);

            }
            return _redeeppitory.Query(predicate)
                .Include(s => s.PaymentType)
                .Include(s => s.Student.UserInfo)
                .Select();

        }

        public IEnumerable<MobilePayment> GetPaymentsByStudentId(int studentId)
        {
            
            return _redeeppitory.Query(x => x.ReffStudentId == studentId).Select();
        } 

    }
}
