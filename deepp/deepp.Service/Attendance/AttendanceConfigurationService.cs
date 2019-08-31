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

    public interface IAttendanceConfigurationService : IService<AttendanceConfiguration>
    {
        AttendanceConfiguration GetAttendanceConfiguration(int instituteId);
        IEnumerable<AttendanceConfiguration> GetActiveAttendanceConfiguration();
        IEnumerable<AttendanceConfiguration> GetAttendanceConfiguration(bool IsActive);
        AttendanceConfiguration GetAttendanceConfigurationById(int id);
        IEnumerable<AttendanceConfiguration> GetAttendanceConfigurationesByInstituteId(int instituteId);

        void Insert(IUnitOfWorkAsync unitOfWorkAsync, AttendanceConfiguration AttendanceConfiguration);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, AttendanceConfiguration AttendanceConfiguration);
    }
    public class AttendanceConfigurationService : Service<AttendanceConfiguration>, IAttendanceConfigurationService
    {
        private readonly IRepositoryAsync<AttendanceConfiguration> _repository;

        public AttendanceConfigurationService(IRepositoryAsync<AttendanceConfiguration> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public AttendanceConfiguration GetAttendanceConfiguration(int instituteId)
        {

            return _repository.Query(x => x.InstituteId == instituteId).Include(y => y.AttendanceConfigurationDetails)
                .Select().FirstOrDefault();
        }
        public IEnumerable<AttendanceConfiguration> GetAttendanceConfiguration(bool isActive = false)
        {
            if (isActive)
            {
                return _repository.Query().Select();
            }

            return _repository.Query().Select();
        }
        public IEnumerable<AttendanceConfiguration> GetActiveAttendanceConfiguration()
        {
            return _repository.Query().Select();
        }
        public AttendanceConfiguration GetAttendanceConfigurationById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<AttendanceConfiguration> GetAttendanceConfigurationesByInstituteId(int instituteId)
        {
            return _repository.Query().Select().Where(x => x.InstituteId == instituteId);
        }

        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, AttendanceConfiguration AttendanceConfiguration)
        {
            //AttendanceConfiguration. = DateTime.Now;
            _repository.Insert(AttendanceConfiguration);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, AttendanceConfiguration AttendanceConfiguration)
        {
            //  AttendanceConfiguration.LastUpdateTime = DateTime.Now;
            _repository.Update(AttendanceConfiguration);
            unitOfWorkAsync.SaveChanges();

        }
        /// <summary>
        /// anirban
        /// </summary>
        /// <param name="instituteId"></param>
        /// <returns></returns>

    }
}
