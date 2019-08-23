using deepp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace deepp.Service
{
    public interface IRightsService
    {
        IEnumerable<string> UserRights(int userId);
        IEnumerable<Right> GetRights(int instituteId);
        Right GetRightsById(int id);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, Right right);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, Right right);
    }
    public class RightsService : IRightsService
    {
        private readonly IStoredProcedures storedProcedures;
        private readonly IRepositoryAsync<Right> _redeeppitory;

        public RightsService(IStoredProcedures storedProcedures, IRepositoryAsync<Right> redeeppitory)
        {
            this.storedProcedures = storedProcedures;
            _redeeppitory = redeeppitory;
        }

        public IEnumerable<string> UserRights(int userId)
        {
            return storedProcedures.SprGetRights(userId);
        }

        /// <summary>
        /// Gets the rights.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        public IEnumerable<Right> GetRights(int instituteId)
        { 
            return _redeeppitory.Query().Select();
        }
        /// <summary>
        /// Gets the rights by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Right GetRightsById(int id)
        {
            return _redeeppitory.Query(r=>r.Id.Equals(id)).Select().SingleOrDefault();
        }
        /// <summary>
        /// Inserts the specified unit of work asynchronous for right.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="right">The right.</param>
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, Right right)
        {

            _redeeppitory.Insert(right);
            unitOfWorkAsync.SaveChanges();

        }

        /// <summary>
        /// Updates the specified unit of work asynchronous for right.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="right">The right.</param>
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, Right right)
        {
               _redeeppitory.Update(right);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
