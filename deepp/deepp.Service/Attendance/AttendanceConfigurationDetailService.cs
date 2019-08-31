using deepp.Entities.Models;

using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service.Attendance
{

    public interface IAttendanceConfigurationDetailService : IService<AttendanceConfigurationDetail>
    {
        IEnumerable<AttendanceConfigurationDetail> GetAttendanceConfigurationDetail(int instituteId);
        IEnumerable<AttendanceConfigurationDetail> GetActiveAttendanceConfigurationDetail();
        IEnumerable<AttendanceConfigurationDetail> GetAttendanceConfigurationDetail(bool IsActive);
        AttendanceConfigurationDetail GetAttendanceConfigurationDetailById(int id);
        IEnumerable<AttendanceConfigurationDetail> GetAttendanceConfigurationDetailesByInstituteId(int instituteId);

        void Insert(IUnitOfWorkAsync unitOfWorkAsync, AttendanceConfigurationDetail AttendanceConfigurationDetail);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, AttendanceConfigurationDetail AttendanceConfigurationDetail);
    }
    public class AttendanceConfigurationDetailService : Service<AttendanceConfigurationDetail>, IAttendanceConfigurationDetailService
    {
        private readonly IRepositoryAsync<AttendanceConfigurationDetail> _repository;

        public AttendanceConfigurationDetailService(IRepositoryAsync<AttendanceConfigurationDetail> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public IEnumerable<AttendanceConfigurationDetail> GetAttendanceConfigurationDetail(int instituteId)
        {

            return _repository.Query(x => x.InstituteId == instituteId).Select();
        }
        public IEnumerable<AttendanceConfigurationDetail> GetAttendanceConfigurationDetail(bool isActive = false)
        {
            if (isActive)
            {
                return _repository.Query().Select();
            }

            return _repository.Query().Select();
        }
        public IEnumerable<AttendanceConfigurationDetail> GetActiveAttendanceConfigurationDetail()
        {
            return _repository.Query().Select();
        }
        public AttendanceConfigurationDetail GetAttendanceConfigurationDetailById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<AttendanceConfigurationDetail> GetAttendanceConfigurationDetailesByInstituteId(int instituteId)
        {
            return _repository.Query().Select().Where(x => x.InstituteId == instituteId);
        }

        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, AttendanceConfigurationDetail AttendanceConfigurationDetail)
        {
            //AttendanceConfigurationDetail. = DateTime.Now;
            _repository.Insert(AttendanceConfigurationDetail);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, AttendanceConfigurationDetail AttendanceConfigurationDetail)
        {
            //  AttendanceConfigurationDetail.LastUpdateTime = DateTime.Now;
            _repository.Update(AttendanceConfigurationDetail);
            unitOfWorkAsync.SaveChanges();

        }
        /// <summary>
        /// anirban
        /// </summary>
        /// <param name="instituteId"></param>
        /// <returns></returns>

    }
}
