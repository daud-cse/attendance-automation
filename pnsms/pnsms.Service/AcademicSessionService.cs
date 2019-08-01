using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service
{
    
    public interface IAcademicSessionService: IService<AcademicSession>
    {
        IEnumerable<AcademicSession> GetAcademicSessions(int instituteId);
        AcademicSession GetAcademicSessionById(int id);
        IEnumerable<AcademicSession> GetAcademicSessionByInstituteId(int instituteId);

        AcademicSession GetAcademicSessionByInstituteId(int instituteId, int sessionid, bool isrunning);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, AcademicSession academicSession);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, AcademicSession academicSession);
        List<KeyValuePair<int, string>> GetKVP(int instituteId);
        List<KeyValuePair<int, string>> GetKVP(int instituteId, int sessionid, bool isrunning);
    }
    public class AcademicSessionService : Service<AcademicSession>, IAcademicSessionService
    {


        private readonly IRepositoryAsync<AcademicSession> _repository;


        public AcademicSessionService(IRepositoryAsync<AcademicSession> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public IEnumerable<AcademicSession> GetAcademicSessions(int instituteId)
        {
            return _repository.Query(s=>s.InstituteId==instituteId).Select();
        }

       
        public AcademicSession GetAcademicSessionById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<AcademicSession> GetAcademicSessionByInstituteId(int instituteId)
        {
            return _repository.Query().Select().Where(x => x.InstituteId == instituteId && x.IsCompleted == false);
        }
        public AcademicSession GetAcademicSessionByInstituteId(int instituteId, int sessionid,bool isrunning)
        {
            return _repository.Query(x => x.InstituteId == instituteId && x.Id == sessionid && x.IsRunning == isrunning && x.IsCompleted == false).Select().FirstOrDefault();
        }

        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="academicSession">The academic session.</param>
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, AcademicSession academicSession)
        {
            academicSession.LastUpdateTime = DateTime.Now;
            _repository.Insert(academicSession);
            unitOfWorkAsync.SaveChanges();

        }
        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="academicSession">The academic session.</param>
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, AcademicSession academicSession)
        {
            academicSession.LastUpdateTime = DateTime.Now;
            _repository.Update(academicSession);
            unitOfWorkAsync.SaveChanges();

        }

        public List<KeyValuePair<int, string>> GetKVP(int instituteId)
        {
            var data = _repository.Query(c=> c.InstituteId == instituteId).Select().ToList();
            var sessList = new List<KeyValuePair<int, string>>();
            data.ForEach(c => sessList.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

            return sessList;
        }
        public List<KeyValuePair<int, string>> GetKVP(int instituteId, int sessionid, bool isrunning)
        {
            var data = _repository.Query(x => x.InstituteId == instituteId && x.Id == sessionid && x.IsRunning == isrunning && x.IsCompleted == false).Select().ToList();
            var sessList = new List<KeyValuePair<int, string>>();
            data.ForEach(c => sessList.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

            return sessList;
        }
    }
}
