﻿using deepp.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service
{
    public interface IShortMessageDetailService
    {



        /// <summary>
        /// Gets the short message details by short message identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IEnumerable<ShortMessageDetail> GetShortMessageDetailsByShortMessageId(int id);

        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="ShortMessageDetail">The short message.</param>
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, ShortMessageDetail shortMessageDetail);

        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="ShortMessageDetail">The short message.</param>
        void Update(IUnitOfWorkAsync unitOfWorkAsync, ShortMessageDetail shortMessageDetail);
    }

    public class ShortMessageDetailService : IShortMessageDetailService
    {
        
        readonly IRepositoryAsync<ShortMessageDetail> redeeppitory;
        

        public ShortMessageDetailService(  IRepositoryAsync<ShortMessageDetail> redeeppitory)
        {
             
            this.redeeppitory = redeeppitory;
            
        }
 
         
        /// <summary>
        /// Gets the short message by institute identifier.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        public IEnumerable<ShortMessageDetail> GetShortMessageDetailsByShortMessageId(int id)
        {
            return redeeppitory.Query(d => d.ShortMessageId == id).Select();
        }

        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="shortMessageDetail">The short message.</param>
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, ShortMessageDetail shortMessageDetail)
        {

            redeeppitory.Insert(shortMessageDetail);
            unitOfWorkAsync.SaveChanges();
        }

        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="shortMessageDetail">The short message.</param>
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, ShortMessageDetail shortMessageDetail)
        {

            redeeppitory.Update(shortMessageDetail);
            unitOfWorkAsync.SaveChanges();
        }
    }
}