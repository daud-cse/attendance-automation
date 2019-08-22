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


    public interface INotificationTagGroupService : IService<NotificationTagGroup>
    {

        IEnumerable<NotificationTagGroup> GetNotificationTagGroups();
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, NotificationTagGroup NotificationTagGroup);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, NotificationTagGroup NotificationTagGroup);

    }
    public class NotificationTagGroupService : Service<NotificationTagGroup>, INotificationTagGroupService
    {


        private readonly IRepositoryAsync<NotificationTagGroup> _repository;


        public NotificationTagGroupService(IRepositoryAsync<NotificationTagGroup> repository)
            : base(repository)
        {
            _repository = repository;
        }

 

        public IEnumerable<NotificationTagGroup> GetNotificationTagGroups()
        {
            return   _repository.Query().Select();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, NotificationTagGroup NotificationTagGroup)
        {
            
            _repository.Insert(NotificationTagGroup);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, NotificationTagGroup NotificationTagGroup)
        {
            
            _repository.Update(NotificationTagGroup);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
