using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;


namespace deepp.Service.Attendance
{
   
    public interface IMachineInfoService : IService<MachineInfo>
    {
        IEnumerable<MachineInfo> GetMachineInfos(int instituteId);
        IEnumerable<MachineInfo> GetMachineInfos(bool isActive);
        IEnumerable<MachineInfo> GetActiveMachineInfo();
        IEnumerable<MachineInfo> GetMachineInfoData(int instituteId,string deviceinfo ,DateTime? AttendanceLastSynDate, bool IsProcessDone);
        MachineInfo GetMachineInfoById(int id);
        IEnumerable<MachineInfo> GetMachineInfoByInstituteId(int instituteId);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, MachineInfo MachineInfo);
        Entities.ViewModels.ReturnModel Inserts(int instituteId,int AcademicSessionId,int UserId, IUnitOfWorkAsync unitOfWorkAsync, List<MachineInfo> lstMachineInfo);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, MachineInfo MachineInfo);
    }
    public class MachineInfoService : Service<MachineInfo>, IMachineInfoService
    {


        private readonly IRepositoryAsync<MachineInfo> _repository;
        private readonly IStoredProcedures _storedProcedures;

        public MachineInfoService(IRepositoryAsync<MachineInfo> repository
            , IStoredProcedures storedProcedures)
            : base(repository)
        {
            _repository = repository;
            _storedProcedures = storedProcedures;
        }


        public IEnumerable<MachineInfo> GetMachineInfos(int instituteId)
        {

            return _repository.Query(c => c.InstituteId == instituteId).Select();
        }

        public IEnumerable<MachineInfo> GetMachineInfos(bool isActive)
        {
            if (isActive)
            {
                return _repository.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _repository.Query().Select();
        }

        public IEnumerable<MachineInfo> GetActiveMachineInfo()
        {
            return _repository.Query().Select().Where(d => d.IsActive == true);
        }
        public MachineInfo GetMachineInfoById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.MachineInfoId == id);
        }
        public IEnumerable<MachineInfo> GetMachineInfoByInstituteId(int instituteId)
        {
            return _repository.Query(d => d.IsActive == true && d.InstituteId == instituteId).Select();
        }
        public IEnumerable<MachineInfo> GetMachineInfoData(int instituteId, string deviceinfo , DateTime? AttendanceLastSynDate,bool IsProcessDone)
        {
            string[] formats = new[] { "M/d/yy", "MM/dd/yy", "M/d/yyyy", "MM/dd/yyyy" };
            var lstMachineInfoData = _repository.Query(d => d.IsActive == true && d.InstituteId == instituteId && d.deviceinfo == deviceinfo).Select();// && DateTime.ParseExact(d.DateTimeRecord, "dd-MM-yyyy", CultureInfo.InvariantCulture) > AttendanceLastSynDate).Select();
            return lstMachineInfoData;
        }
        
        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="MachineInfo">The MachineInfo.</param>
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, MachineInfo MachineInfo)
        {
            MachineInfo.LastUpdateTime = DateTime.Now;
            _repository.Insert(MachineInfo);
            unitOfWorkAsync.SaveChanges();

        }
        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="MachineInfo">The MachineInfo.</param>
        public ReturnModel Inserts(int instituteId ,int AcademicSessionId,int UserId, IUnitOfWorkAsync unitOfWorkAsync, List<MachineInfo> lstMachineInfo)
        {

            ReturnModel objReturnModel = new ReturnModel();      
            try
            {
                foreach (var item in lstMachineInfo)
                {
                    item.AcademicSessionId = AcademicSessionId;
                    item.UserId = UserId;
                    item.InstituteId = instituteId;
                    item.LastUpdateTime = DateTime.Now;
                    item.CreateDate = DateTime.Now;
                    item.IsActive = true;
                    item.IsProcess = false;
                    _repository.Insert(item);                  

                }
                unitOfWorkAsync.SaveChanges();   
                            
                objReturnModel.IsSuccess = true;
                objReturnModel.Message = "Data Uploaded Successfully.";
                var deviceinfo = lstMachineInfo.ToList().FirstOrDefault().deviceinfo;
                var objSpstatus = _storedProcedures.AttendanceDataMigration(instituteId, deviceinfo, UserId);
                return objReturnModel;
            }
            catch (Exception ex)
            {
                objReturnModel.IsSuccess = false;
                objReturnModel.Message = ex.Message;
                return objReturnModel;
            }
            

        }
        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="MachineInfo">The MachineInfo.</param>
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, MachineInfo MachineInfo)
        {
            MachineInfo.LastUpdateTime = DateTime.Now;
            _repository.Update(MachineInfo);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
