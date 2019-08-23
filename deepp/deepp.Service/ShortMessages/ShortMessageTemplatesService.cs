using System.Collections.Generic;
using System.Linq;
using deepp.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace deepp.Service.ShortMessages
{
    /// <summary>
    /// 
    /// </summary>
    public interface IShortMessageTemplateService
    {
        /// <summary>
        /// Gets the short message templates by institute identifier.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="isActive">The is active.</param>
        /// <returns></returns>
        IEnumerable<ShortMessageTemplate> GetShortMessageTemplatesByInstituteId(int instituteId, bool? isActive = null);

        /// <summary>
        /// Gets the short message template by identifier.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        ShortMessageTemplate GetShortMessageTemplateById(int instituteId,int id);

        /// <summary>
        /// News the short message template by identifier.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        ShortMessageTemplate NewShortMessageTemplate(int instituteId);
        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="shortMessageTemplate">The short message template.</param>
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, ShortMessageTemplate shortMessageTemplate);

        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="shortMessageTemplate">The short message template.</param>
        void Update(IUnitOfWorkAsync unitOfWorkAsync, ShortMessageTemplate shortMessageTemplate);
    }

    public class ShortMessageTemplateService : IShortMessageTemplateService
    {
        
        readonly IRepositoryAsync<ShortMessageTemplate> redeeppitory;
        private readonly INotificationTagService _notificationTagService;
        private readonly INotificationTagGroupService _notificationTagGroupService;


        /// <summary>
        /// Initializes a new instance of the <see cref="ShortMessageTemplateService" /> class.
        /// </summary>
        /// <param name="redeeppitory">The redeeppitory.</param>
        /// <param name="notificationTagGroupService"></param>
        public ShortMessageTemplateService(  IRepositoryAsync<ShortMessageTemplate> redeeppitory, INotificationTagService notificationTagService, INotificationTagGroupService notificationTagGroupService)
        {
            this.redeeppitory = redeeppitory;
            _notificationTagService = notificationTagService;
            _notificationTagGroupService = notificationTagGroupService;
        }

        /// <summary>
        /// Gets the short message templates by institute identifier.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="isActive">The is active.</param>
        /// <returns></returns>
        public IEnumerable<ShortMessageTemplate> GetShortMessageTemplatesByInstituteId(int instituteId, bool? isActive = null)
        {
            return isActive != null ? redeeppitory.Query(d => d.InstituteId.Equals(instituteId) && d.IsActive == isActive).Select() : redeeppitory.Query(d => d.InstituteId == instituteId).Select();
        }
        /// <summary>
        /// Gets the short message template by identifier.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ShortMessageTemplate GetShortMessageTemplateById(int instituteId,int id)
        {
            var shortMessageTemplate  = redeeppitory.Query(d => d.Id == id).Select().SingleOrDefault();
            if (shortMessageTemplate != null)
            {
                shortMessageTemplate.NotificationTags = _notificationTagService.GetNotificationTags().ToList();
                shortMessageTemplate.NotificationTags.ForEach(s =>
                {
                    s.NotificationGroupName = s.NotificationTagGroup.Name;
                    s.NotificationTagGroup = null;
                });
                shortMessageTemplate.NotificationTagGroupList =
                    _notificationTagGroupService.GetNotificationTagGroups()
                        .Select(d => new KeyValuePair<int, string>(d.Id, d.Name))
                        .ToList();
                return shortMessageTemplate;
            }
            return null;
        }
        /// <summary>
        /// News the short message template by identifier.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        public ShortMessageTemplate NewShortMessageTemplate(int instituteId)
        {
            var smsTemplateService = new ShortMessageTemplate();
            smsTemplateService.IsActive = true;
            smsTemplateService.IsStaticSms = false;
            smsTemplateService.IsForGeneral = true;
            smsTemplateService.NotificationTags = _notificationTagService.GetNotificationTags().ToList();
            smsTemplateService.NotificationTags.ForEach(s =>
            {
                s.NotificationGroupName = s.NotificationTagGroup.Name;
                s.NotificationTagGroup = null;
            });
            smsTemplateService.NotificationTagGroupList =
                _notificationTagGroupService.GetNotificationTagGroups()
                    .Select(d => new KeyValuePair<int, string>(d.Id, d.Name))
                    .ToList();
            return smsTemplateService;
        }
        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="shortMessage">The short message.</param>
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, ShortMessageTemplate shortMessage)
        {

            redeeppitory.Insert(shortMessage);
            unitOfWorkAsync.SaveChanges();
        }

        
        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="shortMessage">The short message.</param>
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, ShortMessageTemplate shortMessage)
        {

            redeeppitory.Update(shortMessage);
            unitOfWorkAsync.SaveChanges();
        }
    }
}
