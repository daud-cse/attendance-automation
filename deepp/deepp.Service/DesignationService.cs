using deepp.Entities.Models;
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


        private readonly IRepositoryAsync<Designation> _redeeppitory;


        public DesignationService(IRepositoryAsync<Designation> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }


        public IEnumerable<Designation> GetDesignations(int instituteId, bool? isActive = null)
        {
            return isActive != null ? _redeeppitory.Query(d => d.InstituteId.Equals(instituteId) && d.IsActive == isActive).Select() : _redeeppitory.Query(d=>d.InstituteId==instituteId).Select();
        }

        public IEnumerable<Designation> GetDesignations(bool isActive)
        {
            if (isActive)
            {
                return _redeeppitory.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _redeeppitory.Query().Select();
        }

        public IEnumerable<Designation> GetActiveDesignation()
        {
            return _redeeppitory.Query().Select().Where(d => d.IsActive == true);
        }
        public Designation GetDesignationById(int id)
        {
            return _redeeppitory.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, Designation designation)
        {
            designation.LastUpdateTime = DateTime.Now;
            _redeeppitory.Insert(designation);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, Designation designation)
        {
            designation.LastUpdateTime = DateTime.Now;
            _redeeppitory.Update(designation);
            unitOfWorkAsync.SaveChanges();

        }

        public IEnumerable<Designation> VacancyAnalysis(int instituteId)
        {
            return _redeeppitory.Query(d => d.InstituteId == instituteId)
                .Include(p => p.Employees)
                .Include(p => p.Teachers)
                .Select();
        }
    }
}
