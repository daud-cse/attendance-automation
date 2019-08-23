using deepp.Entities.Models;
using deepp.Entities.ViewModels;
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
  public interface INoticeService
   {

      IEnumerable<Notice> GetAllNotice(int instituteId);
      IEnumerable<Notice> GetNoticeByEmployeeId(int instituteId, int EmployeeId);
      IEnumerable<Notice> GetNoticeByInstituteId(int InstituteId);
      IEnumerable<Notice> GetGlobalNotice();
      IEnumerable<Notice> GetGlobalNoticeByStartEndDate(int instituteId, DateTime StartDate, DateTime EndDate);
      IEnumerable<Notice> GetGlobalNoticeByCurrentDateWithenStartEndDate(int InstituteId,bool IsActive);
      IEnumerable<Notice> GetGlobalNoticeByCurrentDateWithenStartEndDateTop10(int InstituteId, bool IsActive);
      Notice GetNoticeById(int id);
      //anirban
      Notice AddNewNotice(int instituteId);
      IEnumerable<Notice> GetAllNoticeListBySearch(VmSearch<Notice> noticeModel);
      void SaveNotice(IUnitOfWorkAsync unitOfWorkAsync, List<byte[]> imageList, Notice noticeModel);
      void UpdateNotice(IUnitOfWorkAsync unitOfWorkAsync, List<byte[]> imageList, Notice noticeModel);
      IEnumerable<Notice> GetActiveNotice(int instituteId);
      IEnumerable<Notice> GetActiveNoticeByTypeId(int instituteId, int typeId);
   }

   public class NoticeService : Service<Notice>, INoticeService
   {
       private readonly IRepositoryAsync<Notice> _redeeppitory;
       private readonly IImageService _imageService;
       private readonly INoticeTypeService _noticeTypeService;
       List<Notice> Notice = new List<Notice>();


       public NoticeService(IRepositoryAsync<Notice> redeeppitory, IImageService imageService, INoticeTypeService noticeTypeService)
           : base(redeeppitory)
       {
           _redeeppitory = redeeppitory;
           _imageService = imageService;
           _noticeTypeService = noticeTypeService;
          
       }

       public IEnumerable<Notice> GetAllNotice(int instituteId)
        {
            var notice = _redeeppitory.Query(x => x.InstituteId == instituteId).Select();
           return notice;
       }

       public IEnumerable<Notice> GetNoticeByEmployeeId(int instituteId, int EmployeeId)
        {
           var notice = _redeeppitory.Query().Select();
           return notice;
       }
       public IEnumerable<Notice> GetNoticeByInstituteId(int InstituteId)
        {
           var notice = _redeeppitory.Query().Select().Where(x => x.InstituteId == InstituteId);
           return notice;
       }
       public IEnumerable<Notice> GetGlobalNotice()
        {
           var notice = _redeeppitory.Query().Select();
           return notice;
       }
       public IEnumerable<Notice> GetGlobalNoticeByStartEndDate(int instituteId, DateTime StartDate, DateTime EndDate)
        {
           var notice = _redeeppitory.Query().Select();
           return notice;
       }
       public IEnumerable<Notice> GetGlobalNoticeByCurrentDateWithenStartEndDate(int InstituteId, bool IsActive)
       {
           return _redeeppitory.Query().Select();
               //.Where(x => x.InstituteId == InstituteId && x.StartDate <= DateTime.Now.Date && x.EndDate >= DateTime.Now.Date && x.IsActive == IsActive);//;
       }

       public IEnumerable<Notice> GetGlobalNoticeByCurrentDateWithenStartEndDateTop10(int InstituteId, bool IsActive)
       {
           return _redeeppitory.Query().Select();
               // .Where(x => x.InstituteId == InstituteId && x.StartDate < DateTime.Now.Date && x.EndDate >= DateTime.Now.Date).ToList();//;
       }

       public Notice GetNoticeById(int id)
        {
            var notice = _redeeppitory.Query(x => x.Id == id).Select().FirstOrDefault();
            return notice;
        }


       //anirban
       public Notice AddNewNotice(int instituteId)
        {
           Notice noticeModel = new Notice();
           noticeModel.InstituteId = instituteId;
           noticeModel.StartDate = DateTime.Now.Date;
           noticeModel.EndDate = DateTime.Now.Date;
           noticeModel.IsActive = true;
           noticeModel.NoticeTypeList = _noticeTypeService.GetKVP();
           noticeModel.NoticeTypeId = null;
           return noticeModel;
       }

       public IEnumerable<Notice> GetAllNoticeListBySearch(VmSearch<Notice> noticeModel)
       {

           DateTime start = noticeModel.startDateModel;
           DateTime end = noticeModel.endDateModel;
           var noticeList = _redeeppitory.Query(c => c.InstituteId == noticeModel.InstituteId 
               &&
               ((start >= c.StartDate && start <=c.EndDate) 
               || ( end >= c.StartDate && end <=  c.EndDate) 
               || (start <= c.StartDate && end >=c.EndDate)))
               .Select();
           return noticeList;
       }

       public void SaveNotice(IUnitOfWorkAsync unitOfWorkAsync, List<byte[]> imageList, Notice noticeModel)
       {
               noticeModel.LastUpdateTime = DateTime.Now;
               _redeeppitory.Insert(noticeModel);
               unitOfWorkAsync.SaveChanges();

               if (imageList.Count() > 0)
               {
                   for (int i = 0; i < imageList.Count; i++)
                   {
                       Image image = new Image();
                      
                       image.RefTypeId = (int)utility.RefCode.Notice;
                       image.RefPrimaryKey = noticeModel.Id;
                       image.ImageBinaryData = imageList[i];
                       image.IsActive = true;
                       image.LastUpdatedTime = System.DateTime.Now;
                    
                       _imageService.Insert(image);
                       unitOfWorkAsync.SaveChanges();
                   }
               }
       }

       public void UpdateNotice(IUnitOfWorkAsync unitOfWorkAsync, List<byte[]> imageList, Notice noticeModel)
       {
           noticeModel.LastUpdateTime = DateTime.Now;
           var entity = GetNoticeById(noticeModel.Id);

           entity.StartDate = noticeModel.StartDate;
           entity.EndDate = noticeModel.EndDate;
           entity.InstituteId = noticeModel.InstituteId;
           entity.NoticeTitle = noticeModel.NoticeTitle;
           entity.NoticeBody = noticeModel.NoticeBody;
           entity.NoticeTypeId = noticeModel.NoticeTypeId;
           entity.IsActive = noticeModel.IsActive;

           _redeeppitory.Update(entity);
           unitOfWorkAsync.SaveChanges();


               List<int> storedImageIdList = new List<int>();

               int RefTypeId = (int)utility.RefCode.Notice;
               IEnumerable<Image> storedImgList = _imageService.GetImageByRefTypeIdAndRefPrimaryKey(RefTypeId, noticeModel.Id);
               if (storedImgList.Count() > 0)
               {
                   foreach (var image in storedImgList)
                   { storedImageIdList.Add(image.Id); }
               }

               var newImageIdList = storedImageIdList.Except(noticeModel.ExtImageIdList);

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

                   image.RefTypeId = (int)utility.RefCode.Notice;
                   image.RefPrimaryKey = noticeModel.Id;
                   image.ImageBinaryData = imageList[i];
                   image.IsActive = true;
                   image.LastUpdatedTime = System.DateTime.Now;

                   _imageService.Insert(image);
                   unitOfWorkAsync.SaveChanges();
               }
           }
          
       }
       
       public IEnumerable<Notice> GetActiveNotice(int instituteId)
       {
            var notic = _redeeppitory.Query(x => x.InstituteId == instituteId && x.IsActive == true).Select();
            return notic;
        }
       
       public IEnumerable<Notice> GetActiveNoticeByTypeId(int instituteId, int typeId)
        {
            var notic = _redeeppitory.Query(x => x.InstituteId == instituteId && x.IsActive == true && x.NoticeTypeId == typeId).Select();
           return notic;
       }
       
   }

   
}
