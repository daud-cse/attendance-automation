using pnsms.Entities.Models;
using pnsms.utility;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service.Teachers
{

    public interface ITeacherSubjectAcademicMappingService : IService<TeacherSubjectAcademicMapping>
    {
        IEnumerable<TeacherSubjectAcademicMapping> Get(string searchText, int instituteId, int teacherId, int branchId, int sessionId);
        IEnumerable<TeacherSubjectAcademicMapping> Get(int instituteId, int teacherId, int branchId, int sessionId);
        TeacherSubjectAcademicMapping GetById(int id);
        IEnumerable<TeacherSubjectAcademicMapping> Get(int instituteId, int academicBranchId, int? academicShiftId, int teacherId);
        TeacherSubjectAcademicMapping Get(int instituteId, int academicBranchId, int classId, int sectionId, int? academicShiftId, int subjectId);
        TeacherSubjectAcademicMapping GetBySubGrpId(int subGrpId);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, TeacherSubjectAcademicMapping TeacherSubjectAcademicMapping);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, TeacherSubjectAcademicMapping TeacherSubjectAcademicMapping);
        void Delete(IUnitOfWorkAsync unitOfWorkAsync, int id);


        IEnumerable<TeacherSubjectAcademicMapping> GetAll(int instituteId, int academicBranchId, int? academicShiftId, int academicClassId, int? academicSectionId = null);
    }
    public class TeacherSubjectAcademicMappingService : Service<TeacherSubjectAcademicMapping>, ITeacherSubjectAcademicMappingService
    {
        private readonly IRepositoryAsync<TeacherSubjectAcademicMapping> _repository;

        public TeacherSubjectAcademicMappingService(
            IRepositoryAsync<TeacherSubjectAcademicMapping> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<TeacherSubjectAcademicMapping> Get(string searchText, int instituteId, int teacherId, int branchId, int sessionId)
        {
            return _repository.Query(p => p.InstituteId == instituteId
               && p.TeacherId == teacherId
               && p.AcademicBranchId == branchId
               && p.Description.Contains(searchText.Trim()))
               .Select().Take(10);
        }

        public IEnumerable<TeacherSubjectAcademicMapping> Get(int instituteId, int teacherId, int branchId, int sessionId)
        {
            var bb = _repository.Query(p => p.InstituteId == instituteId
               && p.TeacherId == teacherId
               && p.AcademicBranchId == branchId)
               .Select();

            return bb;
        }

        public TeacherSubjectAcademicMapping GetById(int id)
        {
            return _repository.Query(p => p.Id == id).Include(p => p.AcademicBranch).Include(p => p.AcademicClass).Include(p => p.Subject).Select().FirstOrDefault();
        }

        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, TeacherSubjectAcademicMapping TeacherSubjectAcademicMapping)
        {
            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);

                _repository.Insert(TeacherSubjectAcademicMapping);
                unitOfWorkAsync.SaveChanges();

                unitOfWorkAsync.Commit();
            }
            catch (Exception)
            {
                unitOfWorkAsync.Rollback();
                throw;

            }

        }

        public void Update(IUnitOfWorkAsync unitOfWorkAsync, TeacherSubjectAcademicMapping TeacherSubjectAcademicMapping)
        {
            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);
                _repository.Update(TeacherSubjectAcademicMapping);
                unitOfWorkAsync.SaveChanges();
                unitOfWorkAsync.Commit();
            }
            catch (Exception)
            {
                unitOfWorkAsync.Rollback();
                throw;

            }
        }

        public void Delete(IUnitOfWorkAsync unitOfWorkAsync, int id)
        {
            var item = _repository.Query(s => s.Id == id).Select().FirstOrDefault();
            if (item != null)
            {
                _repository.Delete(item);
                unitOfWorkAsync.SaveChanges();
            }

        }

        public IEnumerable<TeacherSubjectAcademicMapping> Get(int instituteId, int academicBranchId, int? academicShiftId, int teacherId)
        {
            if (academicBranchId != 0)
            {

                var result = _repository.Query(c => c.InstituteId == instituteId && c.AcademicBranchId == academicBranchId && c.AcademicShiftId == academicShiftId && c.TeacherId == teacherId)
            .Select();

                return result;

            }
            else
            {

                var result = _repository.Query(c => c.InstituteId == instituteId).Select();
                return result;
            }
        }

        public TeacherSubjectAcademicMapping Get(int id)
        {
            var teacherSubjectAcademicMappings = _repository.Query(r => r.Id.Equals(id)).Select().SingleOrDefault();
            if (teacherSubjectAcademicMappings == null)
                throw new ValidationException("Invalid TeacherSubjectAcademicMappings");
            return teacherSubjectAcademicMappings;
        }

        public TeacherSubjectAcademicMapping Get(int instituteId, int academicBranchId, int classId, int sectionId, int? academicShiftId, int subjectId)
        {
            return _repository.Query(p => p.InstituteId == instituteId
               && p.AcademicClassId == classId
               && p.AcademicBranchId == academicBranchId
               && p.AcademicSectionId == sectionId
               && p.AcademicShiftId == academicShiftId
               && p.SubjectId == subjectId
               )
               .Select().FirstOrDefault();
        }

        public TeacherSubjectAcademicMapping GetBySubGrpId(int subGrpId)
        {
            return _repository.Query(p => p.SubjectGroupId.Value == subGrpId)
            .Select().FirstOrDefault();
        }


        public IEnumerable<TeacherSubjectAcademicMapping> GetAll(int instituteId, int academicBranchId, int? academicShiftId, int academicClassId, int? academicSectionId = null)
        {
            var predicate = PredicateBuilder.True<TeacherSubjectAcademicMapping>();

            predicate = predicate.And(c => c.InstituteId == instituteId);

            if (academicBranchId > 0)
            {
                predicate = predicate.And(c => c.AcademicBranchId == academicBranchId);
            }
            if (academicSectionId > 0)
            {
                predicate = predicate.And(c => c.AcademicSectionId == academicSectionId);
            }

            if (academicClassId > 0)
            {
                predicate = predicate.And(c => c.AcademicClassId == academicClassId);
            }
            if (academicShiftId > 0)
            {
                predicate = predicate.And(c => c.AcademicShiftId == academicShiftId);
            }



            return _repository.Query(predicate).Select();
            //return _repository.Query(p => p.InstituteId == instituteId && p.AcademicBranchId == academicBranchId && p.AcademicShiftId == academicShiftId && p.AcademicClassId == academicClassId).Select();
        }
    }
}
