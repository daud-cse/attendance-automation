﻿using deepp.Entities.Models;
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
    public interface IDepartmentService : IService<Department>
    {
        IEnumerable<Department> GetDepartments(int instituteId, bool? isActive = null);
        Department GetDepartmentById(int id);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, Department department);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, Department department);
        List<KeyValuePair<int, string>> GetKVP(int instituteid);
    }
   public  class DepartmentService: Service<Department>, IDepartmentService
    {
       private readonly IRepositoryAsync<Department> _redeeppitory;

        public DepartmentService(IRepositoryAsync<Department> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }


        public IEnumerable<Department> GetDepartments(int instituteId, bool? isActive=null)
        {
            return isActive!=null ? _redeeppitory.Query(d => d.InstituteId.Equals(instituteId) && d.IsActive==isActive).Select() : _redeeppitory.Query(d => d.InstituteId.Equals(instituteId)).Select();
        }

       public Department GetDepartmentById(int id)
        {
            return _redeeppitory.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, Department department)
        {
            department.LastUpdateTime = DateTime.Now;
            _redeeppitory.Insert(department);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, Department department)
        {
            department.LastUpdateTime = DateTime.Now;
            _redeeppitory.Update(department);
            unitOfWorkAsync.SaveChanges();

        }
        public List<KeyValuePair<int, string>> GetKVP(int instituteid)
        {
            var kvpDepartment = new List<KeyValuePair<int, string>>();
            _redeeppitory.Query(x => x.InstituteId == instituteid && x.IsActive == true).Select().ToList().ForEach(delegate(Department item)
            {
                kvpDepartment.Add(new KeyValuePair<int, string>(item.Id, item.Name));
            });

            return kvpDepartment;
        }
    }
   
}
