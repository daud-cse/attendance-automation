using pnsms.Entities.Models;
using pnsms.Service.Subjects;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service.InstituteSubjects
{
    //class InstituteInstituteSubjectService
    //{
    //}
    /// <summary>
    /// InstituteSubjects interface Service
    /// </summary>
    public interface IInstituteSubjectService
    {


        /// <summary>
        /// Gets the InstituteSubjects.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        List<InstituteSubject> Get(int instituteId, bool? isActive = null);

        /// <summary>
        /// Gets the InstituteSubject by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        InstituteSubject Get(int id);

        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="InstituteSubject">The InstituteSubject.</param>
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, List<InstituteSubject> lstInstituteSubject, int InstituteId);
        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="InstituteSubject">The InstituteSubject.</param>
        void Update(IUnitOfWorkAsync unitOfWorkAsync, InstituteSubject InstituteSubject);
        List<KeyValuePair<int, string>> GetKVP(int instituteId);
    }
    /// <summary>
    /// InstituteSubjects Service
    /// </summary>
    public class InstituteSubjectService : IInstituteSubjectService
    {

        #region "  -  [  Constractor  ]  -  "

        private readonly IRepositoryAsync<InstituteSubject> _repository;

        private readonly ISubjectService _subjectService;


        public InstituteSubjectService(IRepositoryAsync<InstituteSubject> repository, ISubjectService subjectService)
        {
            _repository = repository;
            _subjectService = subjectService;

        }

        #endregion

        #region "  -  [  Crud  ]  -  "



        public List<InstituteSubject> Get(int instituteId, bool? isActive = null)
        {

            var subjectlst = _subjectService.GetAll(true).ToList();
            var lstInstituteSubject = new List<InstituteSubject>();
            var oldlstInstituteSubject = _repository.Query(x => x.InstituteId == instituteId).Select().ToList();
            if (oldlstInstituteSubject.Count == 0)
            {
                subjectlst.ToList().ForEach(delegate(Subject item)
                {
                    InstituteSubject objInstituteSubject = new InstituteSubject();
                    objInstituteSubject.InstituteId = instituteId;
                    objInstituteSubject.Subject = new Subject();
                    objInstituteSubject.SubjectId = item.Id;
                    objInstituteSubject.Subject.Name = item.Name;
                    lstInstituteSubject.Add(objInstituteSubject);
                });
            }
            else if (oldlstInstituteSubject.Count >= subjectlst.Count)
            {
                lstInstituteSubject = oldlstInstituteSubject;
                return lstInstituteSubject;
            }
            else if (subjectlst.Count > oldlstInstituteSubject.Count)
            {
                lstInstituteSubject = oldlstInstituteSubject;
                subjectlst.ToList().ForEach(delegate(Subject item)
               {
                   int vIndex = 0;
                   vIndex = oldlstInstituteSubject.ToList().FindIndex(x => x.SubjectId == item.Id);
                   if (vIndex == -1)
                   {
                       InstituteSubject objInstituteSubject = new InstituteSubject();
                       objInstituteSubject.InstituteId = instituteId;
                       objInstituteSubject.Subject = new Subject();
                       objInstituteSubject.SubjectId = item.Id;
                       objInstituteSubject.Subject.Name = item.Name;
                       lstInstituteSubject.Add(objInstituteSubject);
                   }
               });

            }


            return lstInstituteSubject;
        }



        public InstituteSubject Get(int id)
        {
            var InstituteSubjects = _repository.Query(r => r.Id.Equals(id)).Select().SingleOrDefault();
            if (InstituteSubjects == null)
                throw new ValidationException("Invalid InstituteSubjects");
            return InstituteSubjects;
        }



        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, List<InstituteSubject> lstInstituteSubject, int InstituteId)
        {
            try
            {



                lstInstituteSubject.ForEach(delegate(InstituteSubject item)
                {

                    item.InstituteId = InstituteId;
                    if (item.Id == 0)
                    {
                        item.Subject = null;
                        _repository.Insert(item);
                    }
                    else
                    {
                        _repository.Update(item);
                    }

                    unitOfWorkAsync.SaveChanges();

                });





            }
            catch (Exception)
            {
                unitOfWorkAsync.Rollback();
                throw;

            }

        }

        private void ValidateInstituteSubject(InstituteSubject InstituteSubject)
        {
            var shortNameExist = _repository.Query(s => s.Id != InstituteSubject.Id && s.InstituteId == InstituteSubject.InstituteId).Select().Any();
            if (shortNameExist)
                throw new ValidationException("Duplicate Short Name");
            var codeExist = _repository.Query(s => s.Id != InstituteSubject.Id && s.InstituteId == InstituteSubject.InstituteId).Select().Any();
            if (codeExist)
                throw new ValidationException("Duplicate Code");
        }


        public void Update(IUnitOfWorkAsync unitOfWorkAsync, InstituteSubject InstituteSubject)
        {
            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);
                ValidateInstituteSubject(InstituteSubject);
                _repository.Update(InstituteSubject);
                unitOfWorkAsync.SaveChanges();

                unitOfWorkAsync.Commit();
            }
            catch (Exception)
            {
                unitOfWorkAsync.Rollback();
                throw;

            }
        }

        public List<KeyValuePair<int, string>> GetKVP(int instituteId)
        {
            var data = _repository.Query(r => r.InstituteId == instituteId && r.IsActive).Select().ToList();
            var list = new List<KeyValuePair<int, string>>();
            data.ForEach(c => list.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

            return list;
        }

        #endregion

    }
}
