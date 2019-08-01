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


    public interface ICoCurricularActivitiesOfStudentService : IService<CoCurricularActivitiesOfStudent>
    {

        IEnumerable<CoCurricularActivitiesOfStudent> GetCoCurricularActivityByStudentId(int studentId);
        bool DeleteCoCurricularActivitiesOfStudent(int studentId);

        void SaveCoCurricularActivitiesOfStudent(IUnitOfWorkAsync unitOfWorkAsync, int studentId,
            List<int> coCurricularActivities);
    }
    public class CoCurricularActivitiesOfStudentService : Service<CoCurricularActivitiesOfStudent>, ICoCurricularActivitiesOfStudentService
    {


        private readonly IRepositoryAsync<CoCurricularActivitiesOfStudent> _repository;


        public CoCurricularActivitiesOfStudentService(IRepositoryAsync<CoCurricularActivitiesOfStudent> repository)
            : base(repository)
        {
            _repository = repository;
        }




        public IEnumerable<CoCurricularActivitiesOfStudent> GetCoCurricularActivityByStudentId(int studentId)
        {
            return   _repository.Query(d => d.StudentId.Equals(studentId)).Select();
        }
        public bool DeleteCoCurricularActivitiesOfStudent(int studentId)
        {

            var deleteItems = GetCoCurricularActivityByStudentId(studentId);
            if (deleteItems != null)
            {
                foreach (var item in deleteItems)
                {
                    _repository.Delete(item);
                }
                return true;
            }
            return false;
        }

        public void SaveCoCurricularActivitiesOfStudent(IUnitOfWorkAsync unitOfWorkAsync ,int studentId, List<int> coCurricularActivities)
        {
            foreach (int bid in coCurricularActivities)
            {
                _repository.Insert(new CoCurricularActivitiesOfStudent
                {
                    StudentId = studentId,
                    CoCurricularActivityId = bid
                });
            }
            unitOfWorkAsync.SaveChanges();
             
        }
    }
}
