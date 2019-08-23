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
       public interface IResultPublicationService
       {
          ResultPublication AddNewResultPublication(int instituteId);
          IEnumerable<ResultPublication> GetAllResultPublicationListBySearch(VmSearch<ResultPublication> resultPublicationeModel);
          void SaveResultPublication(IUnitOfWorkAsync unitOfWorkAsync, List<byte[]> imageList, ResultPublication resultPublicationeModel);
          void UpdateResultPublication(IUnitOfWorkAsync unitOfWorkAsync, List<byte[]> imageList, ResultPublication resultPublicationeModel);
          ResultPublication GetresultPublishById(int id);

          IEnumerable<ResultPublication> Get(int instituteId, bool? isActive = null);
          IEnumerable<ResultPublication> Get(int instituteId,int academicSessionId, bool? isActive = null);
         
       }

       public class ResultPublicationService : Service<ResultPublication>, IResultPublicationService
       {
           private readonly IRepositoryAsync<ResultPublication> _redeeppitory;
           private readonly IImageService _imageService;
           private readonly IAcademicSessionService _sessionService;
           List<ResultPublication> Result = new List<ResultPublication>();


           public ResultPublicationService(IRepositoryAsync<ResultPublication> redeeppitory,
               IImageService imageService,
               IAcademicSessionService sessionService)
               : base(redeeppitory)
           {
               _redeeppitory = redeeppitory;
               _imageService = imageService;
               _sessionService = sessionService;
          
           }
                      
           public IEnumerable<ResultPublication> Get(int instituteId, bool? isActive = null)
           {
              if(isActive==null)
              {
                  return _redeeppitory
                      .Query(x => x.InstituteId == instituteId)
                      .Include(x=> x.AcademicSession)                      
                      .Select();
              }
              else
              {
                  return _redeeppitory
                      .Query(x => x.InstituteId == instituteId && x.IsActive == isActive)
                      .Select();
              }
           }

           public IEnumerable<ResultPublication> Get(int instituteId, int academicSessionId, bool? isActive = null)
           {
               if (isActive == null)
               {
                   return _redeeppitory
                       .Query(x => x.InstituteId == instituteId)
                       .Select();
               }
               else
               {
                   return _redeeppitory
                       .Query(x => x.InstituteId == instituteId && x.AcademicSessionId==academicSessionId && x.IsActive == isActive)
                       .Select();
               }
           }

           public ResultPublication AddNewResultPublication(int instituteId)
           {
               ResultPublication resultPublicationeModel = new ResultPublication();
               resultPublicationeModel.InstituteId = instituteId;
               resultPublicationeModel.IsActive = true;
               resultPublicationeModel.AcademicSessionList=_sessionService.GetKVP(instituteId);
               return resultPublicationeModel;
           }

           public IEnumerable<ResultPublication> GetAllResultPublicationListBySearch(VmSearch<ResultPublication> resultPublicationeModel)
           {
               return _redeeppitory.Query(x => x.InstituteId == resultPublicationeModel.InstituteId && x.AcademicSessionId == resultPublicationeModel.DropDownId1).Select();
           }

           public void SaveResultPublication(IUnitOfWorkAsync unitOfWorkAsync, List<byte[]> imageList, ResultPublication resultPublicationeModel)
           {
               resultPublicationeModel.LastUpdateTime = DateTime.Now;
               _redeeppitory.Insert(resultPublicationeModel);
                   unitOfWorkAsync.SaveChanges();

                   if (imageList.Count() > 0)
                   {
                       for (int i = 0; i < imageList.Count; i++)
                       {
                           Image image = new Image();
                      
                           image.RefTypeId = (int)utility.RefCode.Result_Publish;
                           image.RefPrimaryKey = resultPublicationeModel.Id;
                           image.ImageBinaryData = imageList[i];
                           image.IsActive = true;
                           image.LastUpdatedTime = System.DateTime.Now;
                    
                           _imageService.Insert(image);
                           unitOfWorkAsync.SaveChanges();
                       }
                   }
           }

           public void UpdateResultPublication(IUnitOfWorkAsync unitOfWorkAsync, List<byte[]> imageList, ResultPublication resultPublicationeModel)
           {
               resultPublicationeModel.LastUpdateTime = DateTime.Now;
               var entity = GetresultPublishById(resultPublicationeModel.Id);

               entity.Id = resultPublicationeModel.Id;
               entity.InstituteId = resultPublicationeModel.InstituteId;
               entity.Title = resultPublicationeModel.Title;
               entity.Body = resultPublicationeModel.Body;
               entity.AcademicSessionId = resultPublicationeModel.AcademicSessionId;
               entity.IsActive = resultPublicationeModel.IsActive;

               _redeeppitory.Update(entity);
               unitOfWorkAsync.SaveChanges();


                   List<int> storedImageIdList = new List<int>();

                   int RefTypeId = (int)utility.RefCode.Result_Publish;
                   IEnumerable<Image> storedImgList = _imageService.GetImageByRefTypeIdAndRefPrimaryKey(RefTypeId, resultPublicationeModel.Id);
                   if (storedImgList.Count() > 0)
                   {
                       foreach (var image in storedImgList)
                       { storedImageIdList.Add(image.Id); }
                   }

                   var newImageIdList = storedImageIdList.Except(resultPublicationeModel.ExtImageIdList);

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

                       image.RefTypeId = (int)utility.RefCode.Result_Publish;
                       image.RefPrimaryKey = resultPublicationeModel.Id;
                       image.ImageBinaryData = imageList[i];
                       image.IsActive = true;
                       image.LastUpdatedTime = System.DateTime.Now;

                       _imageService.Insert(image);
                       unitOfWorkAsync.SaveChanges();
                   }
               }
          
           }

           public ResultPublication GetresultPublishById(int id)
           {
               var notice = _redeeppitory.Query(x => x.Id == id).Select().FirstOrDefault();
               return notice;
           }
       
   }

   
}
