using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using deepp.utility;
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
   
    public interface IEventService
    {
        IEnumerable<Event> GetAllEvent();
        IEnumerable<Event> GetAllEvent(int instituteId);
        //IEnumerable<Event> GetEventByBrancheId(int instituteId,int BrancheId);
        //IEnumerable<Event> GetEventByStudentId(int instituteId,int StudentId);
        IEnumerable<Event> GetEventByEmployeeId(int instituteId, int EmployeeId);
        IEnumerable<Event> GetEventByInstituteId(int InstituteId);
        //IEnumerable<Event> GetEventByAcademicClassId(int instituteId,int AcademicClassId);
        //IEnumerable<Event> GetEventByAcademicSectionId(int instituteId,int AcademicSectionId);
        IEnumerable<Event> GetGlobalEvent();
        IEnumerable<Event> GetGlobalEventByStartEndDate(int instituteId, DateTime StartDate, DateTime EndDate);
        List<Event> GetGlobalEventByCurrentDateWithenStartEndDate(int InstituteId, bool IsActive);
        IEnumerable<Event> GetGlobalEventByCurrentDateWithenStartEndDateTop10(int InstituteId, bool IsActive);
        Event GetEventById(int id);

        //anirban
        Event AddNewEvent(int instituteId);
        IEnumerable<Event> GetAllEventListBySearch(VmSearch<Event> eventModel);
        void SaveEvent(IUnitOfWorkAsync unitOfWorkAsync, List<byte[]> imageList, Event eventModel);
        void UpdateEvent(IUnitOfWorkAsync unitOfWorkAsync, List<byte[]> imageList, Event eventModel);
        IEnumerable<Event> AllActiveEvent(int instituteId);
    }

    public class EventService : Service<Event>, IEventService
    {
        private readonly IRepositoryAsync<Event> _redeeppitory;
        private readonly IImageService _imageService;
        List<Event> Event = new List<Event>();

        public EventService(
            IRepositoryAsync<Event> redeeppitory,
            IImageService imageService)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
            _imageService = imageService;

        }

        public IEnumerable<Event> GetAllEvent()
        {

            var Event = _redeeppitory.Query().Select();
            return Event;
        }
        public IEnumerable<Event> GetAllEvent(int instituteId)
        {
            var Event = _redeeppitory.Query(x => x.InstituteId == instituteId).Select();
            return Event;
        }
        public IEnumerable<Event> GetEventByEmployeeId(int instituteId, int EmployeeId)
        {
            var Event = _redeeppitory.Query().Select();
            return Event;
        }
        public IEnumerable<Event> GetEventByInstituteId(int InstituteId)
        {
            var Event = _redeeppitory.Query().Select().Where(x => x.InstituteId == InstituteId);
            return Event;
        }
        public IEnumerable<Event> GetGlobalEvent()
        {
            var Event = _redeeppitory.Query().Select();
            return Event;
        }
        public IEnumerable<Event> GetGlobalEventByStartEndDate(int instituteId, DateTime StartDate, DateTime EndDate)
        {
            var Event = _redeeppitory.Query().Select();
            return Event;
        }
        public List<Event> GetGlobalEventByCurrentDateWithenStartEndDate(int InstituteId,bool IsActive)
        {

           var Event = _redeeppitory.Query().Select()
               // .Where(x => x.InstituteId == InstituteId && x.StartDate <= DateTime.Now.Date && x.EndDate >= DateTime.Now.Date && x.IsActive==IsActive)
                .ToList();//;
            return Event;
        }


        public IEnumerable<Event> GetGlobalEventByCurrentDateWithenStartEndDateTop10(int InstituteId, bool IsActive)
        {

            return _redeeppitory.Query().Select();
                //.Where(x => x.InstituteId == InstituteId && x.StartDate <= DateTime.Now.Date && x.EndDate >= DateTime.Now.Date && x.IsActive == IsActive).Take(10);//;
          
        }




        public Event GetEventById(int id)
        {

            List<Image> Images = new List<Image>();
            var Event = _redeeppitory.Query().Include(x=>x.Institute).Select().Where(x => x.Id == id).SingleOrDefault();
          //  Event.Images=Images;
            Event.Image = _imageService.GetImageByRefTypeIdAndRefPrimaryKey(((int)RefCode.Events), id).ToList().FirstOrDefault();          
            return Event;
        }

        //anirban
        public Event AddNewEvent(int instituteId)
        {
            Event eventModel = new Event();
            eventModel.InstituteId = instituteId;
            eventModel.StartDate = DateTime.Now.Date;
            eventModel.EndDate = DateTime.Now.Date;
            eventModel.EventStartAt = DateTime.Now.Date;
            eventModel.EventEndAt = DateTime.Now.Date;
            eventModel.IsActive = true;
            return eventModel;
        }

        public IEnumerable<Event> GetAllEventListBySearch(VmSearch<Event> eventModel)
        {

            DateTime start = eventModel.startDateModel;
            DateTime end = eventModel.endDateModel;
            var eventList = _redeeppitory.Query()
                .Select()
                .Where(c => c.InstituteId == eventModel.InstituteId
                &&
                (
                (start <= c.StartDate && c.EndDate <= end)
                )
                )
                ;
            return eventList;
        }

        public void SaveEvent(IUnitOfWorkAsync unitOfWorkAsync, List<byte[]> imageList, Event eventModel)
        {
            eventModel.LastUpdateTime = DateTime.Now;
            _redeeppitory.Insert(eventModel);
            unitOfWorkAsync.SaveChanges();

            if (imageList.Count() > 0)
            {
                for (int i = 0; i < imageList.Count; i++)
                {
                    Image image = new Image();

                    image.RefTypeId = (int)utility.RefCode.Events;
                    image.RefPrimaryKey = eventModel.Id;
                    image.ImageBinaryData = imageList[i];
                    image.IsActive = true;
                    image.LastUpdatedTime = System.DateTime.Now;

                    _imageService.Insert(image);
                    unitOfWorkAsync.SaveChanges();
                }
            }
        }

        public void UpdateEvent(IUnitOfWorkAsync unitOfWorkAsync, List<byte[]> imageList, Event eventModel)
        {
            eventModel.LastUpdateTime = DateTime.Now;
            _redeeppitory.Update(eventModel);
            unitOfWorkAsync.SaveChanges();


            List<int> storedImageIdList = new List<int>();

            int RefTypeId = (int)utility.RefCode.Events;
            IEnumerable<Image> storedImgList = _imageService.GetImageByRefTypeIdAndRefPrimaryKey(RefTypeId, eventModel.Id);
            if (storedImgList.Count() > 0)
            {
                foreach (var image in storedImgList)
                { storedImageIdList.Add(image.Id); }
            }

            var newImageIdList = storedImageIdList.Except(eventModel.ExtImageIdList);

            foreach (int id in newImageIdList)
            {
                _imageService.DeleteImage(id);
                unitOfWorkAsync.SaveChanges();
            }

            if (imageList.Count() > 0)
            {
                for (int i = 0; i < imageList.Count; i++)
                {
                    Image image = new Image();

                    image.RefTypeId = (int)utility.RefCode.Events;
                    image.RefPrimaryKey = eventModel.Id;
                    image.ImageBinaryData = imageList[i];
                    image.IsActive = true;
                    image.LastUpdatedTime = System.DateTime.Now;

                    _imageService.Insert(image);
                    unitOfWorkAsync.SaveChanges();
                }
            }
        }
        public IEnumerable<Event> AllActiveEvent(int instituteId)
        {
            var Event = _redeeppitory.Query(x => x.InstituteId == instituteId && x.IsActive==true).Select();
            return Event;
        }

    }
}
