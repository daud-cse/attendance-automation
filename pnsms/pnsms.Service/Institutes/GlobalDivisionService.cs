﻿using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service.Institutes
{


    /// <summary>
    /// 
    /// </summary>
    public interface IGlobalDivisionService : IService<GlobalDivision>
    {

        /// <summary>
        /// Gets the global division by institute identifier.
        /// </summary>
        /// <param name="isActive">The is active.</param>
        /// <returns></returns>
        IEnumerable<GlobalDivision> GetGlobalDivisions(bool? isActive = null);
        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="GlobalDivision">The global division.</param>
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, GlobalDivision globalDivision);
        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="GlobalDivision">The global division.</param>
        void Update(IUnitOfWorkAsync unitOfWorkAsync, GlobalDivision globalDivision);

    }
    public class GlobalDivisionService : Service<GlobalDivision>, IGlobalDivisionService
    {


        private readonly IRepositoryAsync<GlobalDivision> _repository;


        public GlobalDivisionService(IRepositoryAsync<GlobalDivision> repository)
            : base(repository)
        {
            _repository = repository;
        }



        /// <summary>
        /// Gets the global division by institute identifier.
        /// </summary>
        /// <param name="isActive">The is active.</param>
        /// <returns></returns>
        public IEnumerable<GlobalDivision> GetGlobalDivisions(bool? isActive = null)
        {
            return isActive != null ? _repository.Query(d => d.IsActive == isActive).Select() : _repository.Query().Select();
        }

        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="globalDivision"></param>
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, GlobalDivision globalDivision)
        {

            _repository.Insert(globalDivision);
            unitOfWorkAsync.SaveChanges();

        }

        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="globalDivision"></param>
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, GlobalDivision globalDivision)
        {

            _repository.Update(globalDivision);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
