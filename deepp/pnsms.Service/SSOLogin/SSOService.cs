using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace pnsms.Service.SSOLogin
{
  
    public interface ISSOService
    {
        IEnumerable<SSO> Get();
        SSO Find(int SSOID);
        IQueryable<SSO> SelectQuery(string query, params object[] parameters);
        void Insert(SSO entity);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, SSO SSO);
        void InsertRange(IEnumerable<SSO> entities);
        void InsertOrUpdateGraph(SSO entity);
        void InsertGraphRange(IEnumerable<SSO> entities);
        void Update(SSO entity);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, SSO SSO);
        void Delete(object id);
        void Delete(SSO entity);
        IQueryFluent<SSO> Query();
        IQueryFluent<SSO> Query(IQueryObject<SSO> queryObject);
        IQueryFluent<SSO> Query(Expression<Func<SSO, bool>> query);
        Task<SSO> FindAsync(params object[] keyValues);
        Task<SSO> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
        Task<bool> DeleteAsync(params object[] keyValues);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
        IQueryable<SSO> Queryable();

        SSO GetInstituteIdByToken(string tokenkey);

        SSO IsTokenValid(System.Net.Http.Headers.HttpRequestHeaders headers);
    }

    public class SSOService : Service<SSO>, ISSOService
    {
        private readonly IRepositoryAsync<SSO> _repository;

        public SSOService(IRepositoryAsync<SSO> repository)
            : base(repository)
        {
            _repository = repository;
        }
        public IEnumerable<SSO> Get()
        {
            return _repository.Query().Select();
        }

        public SSO Find(int SSOID)
        {
            return _repository.Query(x => x.Id == SSOID).Select().SingleOrDefault();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, SSO SSO)
        {

            _repository.Insert(SSO);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, SSO SSO)
        {

            _repository.Update(SSO);
            unitOfWorkAsync.SaveChanges();

        }

        public SSO GetInstituteIdByToken(string tokenkey)
        {
            return _repository.Query(x => x.Tokenkey == tokenkey).Select().FirstOrDefault();
        }
        public SSO IsTokenValid(System.Net.Http.Headers.HttpRequestHeaders headers)
        {
            var token = string.Empty;

            SSO objsso = new SSO();
            if (headers.Contains("token"))
            {

                token = headers.GetValues("token").First();
                 objsso =GetInstituteIdByToken(token);
                 return objsso;
            }
            else
            {

                return objsso;
            }
        }
    }
}
