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


    public interface INotificationTagService : IService<NotificationTag>
    {

        IEnumerable<NotificationTag> GetNotificationTags();
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, NotificationTag notificationTag);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, NotificationTag notificationTag);

    }
    public class NotificationTagService : Service<NotificationTag>, INotificationTagService
    {


        private readonly IRepositoryAsync<NotificationTag> _redeeppitory;


        public NotificationTagService(IRepositoryAsync<NotificationTag> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }

 

        public IEnumerable<NotificationTag> GetNotificationTags()
        {
            return   _redeeppitory.Query().Include(s=>s.NotificationTagGroup).Select();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, NotificationTag notificationTag)
        {
            
            _redeeppitory.Insert(notificationTag);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, NotificationTag notificationTag)
        {
            
            _redeeppitory.Update(notificationTag);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
