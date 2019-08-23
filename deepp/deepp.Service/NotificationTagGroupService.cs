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


    public interface INotificationTagGroupService : IService<NotificationTagGroup>
    {

        IEnumerable<NotificationTagGroup> GetNotificationTagGroups();
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, NotificationTagGroup NotificationTagGroup);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, NotificationTagGroup NotificationTagGroup);

    }
    public class NotificationTagGroupService : Service<NotificationTagGroup>, INotificationTagGroupService
    {


        private readonly IRepositoryAsync<NotificationTagGroup> _redeeppitory;


        public NotificationTagGroupService(IRepositoryAsync<NotificationTagGroup> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }

 

        public IEnumerable<NotificationTagGroup> GetNotificationTagGroups()
        {
            return   _redeeppitory.Query().Select();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, NotificationTagGroup NotificationTagGroup)
        {
            
            _redeeppitory.Insert(NotificationTagGroup);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, NotificationTagGroup NotificationTagGroup)
        {
            
            _redeeppitory.Update(NotificationTagGroup);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
