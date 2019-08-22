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


    public interface INotificationTagService : IService<NotificationTag>
    {

        IEnumerable<NotificationTag> GetNotificationTags();
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, NotificationTag notificationTag);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, NotificationTag notificationTag);

    }
    public class NotificationTagService : Service<NotificationTag>, INotificationTagService
    {


        private readonly IRepositoryAsync<NotificationTag> _repository;


        public NotificationTagService(IRepositoryAsync<NotificationTag> repository)
            : base(repository)
        {
            _repository = repository;
        }

 

        public IEnumerable<NotificationTag> GetNotificationTags()
        {
            return   _repository.Query().Include(s=>s.NotificationTagGroup).Select();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, NotificationTag notificationTag)
        {
            
            _repository.Insert(notificationTag);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, NotificationTag notificationTag)
        {
            
            _repository.Update(notificationTag);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
