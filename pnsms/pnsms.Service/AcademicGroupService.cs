﻿using pnsms.Entities.Models;
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
    //class AcademicGroupService
    //{
    //}


    public interface IAcademicGroupService : IService<AcademicGroup>
    {
        IEnumerable<AcademicGroup> GetAcademicGroups();
        IEnumerable<AcademicGroup> GetActiveGetAcademicGroups();
        AcademicGroup GetGetAcademicGroupById(int id);
        IEnumerable<AcademicGroup> GetAcademicGroups(int instituteId, bool isActive);
        IEnumerable<AcademicGroup> GetActiveGetAcademicGroupsByInstituteId(int instituteId);
        List<KeyValuePair<int, string>> GetKVP(int instituteId);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, AcademicGroup academicGroup);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, AcademicGroup academicGroup);
    }
    public class AcademicGroupService : Service<AcademicGroup>, IAcademicGroupService
    {


        private readonly IRepositoryAsync<AcademicGroup> _repository;


        public AcademicGroupService(IRepositoryAsync<AcademicGroup> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public IEnumerable<AcademicGroup> GetAcademicGroups()
        {
            return _repository.Query().Select();
        }

        public IEnumerable<AcademicGroup> GetAcademicGroups(int instituteId, bool isActive)
        {
            if (isActive)
            {
                return _repository.Query(d => d.IsActive.Equals(true) && d.InstituteId == instituteId).Select();
            }

            return _repository.Query(d=>d.InstituteId == instituteId).Select();
        }

        public IEnumerable<AcademicGroup> GetActiveGetAcademicGroups()
        {
            return _repository.Query().Select().Where(d => d.IsActive == true);
        }
        public AcademicGroup GetGetAcademicGroupById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<AcademicGroup> GetActiveGetAcademicGroupsByInstituteId(int instituteId)
        {
            return _repository.Query(d => d.IsActive == true && d.InstituteId == instituteId).Select();
        }
        public List<KeyValuePair<int, string>> GetKVP(int instituteId)
        {
            var data = _repository.Query(c => c.IsActive && c.InstituteId == instituteId).Select().ToList();
            var groupList = new List<KeyValuePair<int, string>>();
            data.ForEach(c => groupList.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

            return groupList;
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, AcademicGroup academicGroup)
        {
            academicGroup.LastUpdateTime = DateTime.Now;
            _repository.Insert(academicGroup);
            unitOfWorkAsync.SaveChanges();
        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, AcademicGroup academicGroup)
        {
            academicGroup.LastUpdateTime = DateTime.Now;
            _repository.Update(academicGroup);
            unitOfWorkAsync.SaveChanges();

        }
    }
}