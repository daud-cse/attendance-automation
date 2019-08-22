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
  
    public interface IDesignationService : IService<Designation>
    {
        IEnumerable<Designation> GetDesignations(int instituteId, bool? isActive=null);
        IEnumerable<Designation> GetDesignations(bool isActive);
        IEnumerable<Designation> GetActiveDesignation();
        Designation GetDesignationById(int id);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, Designation designation);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, Designation designation);
        IEnumerable<Designation> VacancyAnalysis(int instituteId);

    }
    public class DesignationService : Service<Designation>, IDesignationService
    {


        private readonly IRepositoryAsync<Designation> _repository;


        public DesignationService(IRepositoryAsync<Designation> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public IEnumerable<Designation> GetDesignations(int instituteId, bool? isActive = null)
        {
            return isActive != null ? _repository.Query(d => d.InstituteId.Equals(instituteId) && d.IsActive == isActive).Select() : _repository.Query(d=>d.InstituteId==instituteId).Select();
        }

        public IEnumerable<Designation> GetDesignations(bool isActive)
        {
            if (isActive)
            {
                return _repository.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _repository.Query().Select();
        }

        public IEnumerable<Designation> GetActiveDesignation()
        {
            return _repository.Query().Select().Where(d => d.IsActive == true);
        }
        public Designation GetDesignationById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, Designation designation)
        {
            designation.LastUpdateTime = DateTime.Now;
            _repository.Insert(designation);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, Designation designation)
        {
            designation.LastUpdateTime = DateTime.Now;
            _repository.Update(designation);
            unitOfWorkAsync.SaveChanges();

        }

        public IEnumerable<Designation> VacancyAnalysis(int instituteId)
        {
            return _repository.Query(d => d.InstituteId == instituteId)
                .Include(p => p.Employees)
                .Include(p => p.Teachers)
                .Select();
        }
    }
}
