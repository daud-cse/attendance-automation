using System.ComponentModel.DataAnnotations;
using deepp.Entities.Models;
using deepp.utility;
using deepp.utility.Resource;
using Repository.Pattern.Infrastructure;
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

    /// <summary>
    /// Gallery Service
    /// </summary>
    public interface IGalleryService
    {
        IEnumerable<Gallery> GetAllGallery(int instituteId);
        IEnumerable<Gallery> GetGalleryByInstituteId(int instituteId);
        List<Gallery> GetActiveGalleryByInstituteId(int instituteId);
        List<Gallery> GetGlobalGalleryByCurrentDateWithenStartEndDate(int instituteId, bool isActive);
        IEnumerable<Gallery> GetGlobalGalleryByCurrentDateWithenStartEndDateTop10(int instituteId, bool isActive);
        Gallery GetGalleryById(int instituteId, int id);
        Gallery NewGallery(int instituteId);
        Gallery CreateGallery(IUnitOfWorkAsync unitOfWorkAsync, int instituteId, Gallery gallery, List<Image> images);
        Gallery UpdateGallery(IUnitOfWorkAsync unitOfWorkAsync, int instituteId, Gallery gallery, List<Image> images);
        Gallery GetGalleryDefault(int instituteId, bool IsActive);
        List<Gallery> GetActiveGallery(int instituteId);
    }
    /// <summary>
    /// Gallery Service
    /// </summary>
    public class GalleryService : Service<Gallery>, IGalleryService
    {
        private readonly IRepositoryAsync<Gallery> _redeeppitory;
        private readonly IImageService _iImageService;
        private readonly IEventService _eventService;


        /// <summary>
        /// Initializes a new instance of the <see cref="GalleryService"/> class.
        /// </summary>
        /// <param name="redeeppitory">The redeeppitory.</param>
        /// <param name="iImageService">The i image service.</param>
        /// <param name="eventService">The event service.</param>
        public GalleryService(IRepositoryAsync<Gallery> redeeppitory, IImageService iImageService, IEventService eventService)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
            _iImageService = iImageService;
            _eventService = eventService;

        }

        /// <summary>
        /// the New  gallery.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        public Gallery NewGallery(int instituteId)
        {
            var gallery = new Gallery();
            gallery.EventList =
                _eventService.GetEventByInstituteId(instituteId)
                    .Select(e => new KeyValuePair<int, string>(e.Id, e.EventTitle))
                    .ToList();
            gallery.IsActive = true;
            gallery.StartDate = DateTime.Now;
            gallery.EndDate = DateTime.Now;
            return gallery;
        }
        /// <summary>
        /// Creates the gallery.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="gallery">The gallery.</param>
        /// <param name="images">The images.</param>
        /// <returns></returns>
        public Gallery CreateGallery(IUnitOfWorkAsync unitOfWorkAsync, int instituteId, Gallery gallery, List<Image> images)
        {
            gallery.InstituteId = instituteId;
            gallery.LastUpdateTime = DateTime.Now;

            _redeeppitory.Insert(gallery);
            unitOfWorkAsync.SaveChanges();

            if (images != null)
            {
                foreach (var image in images)
                {
                    image.RefPrimaryKey = gallery.Id;
                    _iImageService.Insert(image);

                    _iImageService.Insert(image);

                }
                // save default Image
                var defaultImg = images.FirstOrDefault();
                if (defaultImg != null)
                {
                    _iImageService.Insert(new Image() { RefTypeId = (int)utility.RefCode.Galley_Default, ImageBinaryData = defaultImg.ImageBinaryData, RefPrimaryKey = defaultImg.RefPrimaryKey });
                }
                unitOfWorkAsync.SaveChanges();
            }
            return gallery;
        }
        /// <summary>
        /// Updates the gallery.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="gallery">The gallery.</param>
        /// <param name="images">The images.</param>
        /// <returns></returns>
        public Gallery UpdateGallery(IUnitOfWorkAsync unitOfWorkAsync, int instituteId, Gallery gallery, List<Image> images)
        {
            gallery.InstituteId = instituteId;
            gallery.LastUpdateTime = DateTime.Now;

            _redeeppitory.Update(gallery);
            unitOfWorkAsync.SaveChanges();

            // existing gallery images
            var imagesOld = _iImageService.GetImageByRefTypeIdAndRefPrimaryKey(((int)RefCode.Gallery), gallery.Id);
            if (gallery.Images != null)
            {
                foreach (var imgold in imagesOld)
                {
                    var gimg = gallery.Images.FirstOrDefault(s => s.Id == imgold.Id);
                    if (gimg != null)
                    {
                        imgold.ImageCaption = gimg.ImageCaption;
                        _iImageService.Update(imgold);
                    }
                    else
                    {
                        _iImageService.Delete(imgold);
                    }

                }

                unitOfWorkAsync.SaveChanges();
            }
            // new gallery images
            if (images != null)
            {
                foreach (var image in images)
                {
                    image.RefPrimaryKey = gallery.Id;
                    _iImageService.Insert(image);
                    unitOfWorkAsync.SaveChanges();
                }

            }
            // default image
            var defaultImages = _iImageService.GetImageByRefTypeIdAndRefPrimaryKey(((int)RefCode.Galley_Default), gallery.Id).FirstOrDefault();
            if (defaultImages != null)
            {
                if (defaultImages.Id != gallery.Image.Id)
                {
                    defaultImages.ImageBinaryData = _iImageService.GetImageById(gallery.Image.Id).ImageBinaryData;

                    _iImageService.Update(defaultImages);
                    unitOfWorkAsync.SaveChanges();
                }
            }
            else
            {
                if (gallery.Image != null)
                {
                    var newDefaultImage = _iImageService.GetImageById(gallery.Image.Id);
                    if (newDefaultImage != null)
                    {
                        //newDefaultImage.Id = 0;
                        newDefaultImage.RefTypeId = (int)RefCode.Galley_Default;
                        _iImageService.Insert(newDefaultImage);
                        unitOfWorkAsync.SaveChanges();
                    }
                }
            }

            return gallery;
        }
        /// <summary>
        /// Gets all gallery.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Gallery> GetAllGallery(int instituteId)
        {

            var gallery = _redeeppitory.Query(x => x.InstituteId == instituteId).Select();
            return gallery;
        }
        /// <summary>
        /// Gets the gallery by institute identifier.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        public IEnumerable<Gallery> GetGalleryByInstituteId(int instituteId)
        {

            var gallery = _redeeppitory.Query(x => x.InstituteId == instituteId).Select();//.Where(x => x.InstituteId == InstituteId);
            return gallery;
        }
        /// <summary>
        /// Gets the gallery by identifier.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ComponentModel.DataAnnotations.ValidationException"></exception>
        public Gallery GetGalleryById(int instituteId, int id)
        {
            List<Image> images;

            var gallery = _redeeppitory.Query(x => x.InstituteId == instituteId && x.Id == id).Select().FirstOrDefault();
            if (gallery == null)
            {
                throw new ValidationException(Errors.InvalidGallery);
            }

            images = _iImageService.GetImageByRefTypeIdAndRefPrimaryKey(((int)RefCode.Gallery), gallery.Id).ToList();
            gallery.Images = images;
            // gallery.Images.ForEach(s => s.ImageBinaryData = null);
            gallery.Image = _iImageService.GetImageByRefTypeIdAndRefPrimaryKey((int)RefCode.Galley_Default, gallery.Id).FirstOrDefault();
            if (gallery.Image != null)
                gallery.Image.ImageBinaryData = new byte[] { };
            gallery.EventList =
                _eventService.GetEventByInstituteId(instituteId)
                    .Select(e => new KeyValuePair<int, string>(e.Id, e.EventTitle))
                    .ToList();
            return gallery;
        }




        public Gallery GetGalleryDefault(int instituteId, bool IsActive)
        {

           // List<Gallery> galleryList = new List<Gallery>();
            Gallery gallery = new Gallery();
            gallery.Images = new List<Image>();
            var galleryList = _redeeppitory.Query(x => x.InstituteId == instituteId && x.IsActive == IsActive).Select().ToList().Take(9).OrderByDescending(x=>x.LastUpdateTime);          
            foreach (Gallery item in galleryList)
            {
                var image = _iImageService.GetImageByRefTypeIdAndRefPrimaryKey(((int)RefCode.Galley_Default), item.Id).ToList().FirstOrDefault();
                gallery.Images.Add(image);
            }



            return gallery;
        }

        /// <summary>
        /// Gets the global gallery by current date withen start end date.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        public List<Gallery> GetGlobalGalleryByCurrentDateWithenStartEndDate(int instituteId, bool isActive)
        {

            Image image = new Image();

            // var notice
            var gallery = _redeeppitory.Query().Select()
                //.Where(x => x.InstituteId == instituteId && x.StartDate <= DateTime.Now.Date && x.EndDate >= DateTime.Now.Date && x.IsActive == isActive)
                .ToList();//;


            foreach (Gallery item in gallery)
            {
                image = _iImageService.GetImageByRefTypeIdAndRefPrimaryKey(((int)RefCode.Gallery), item.Id).ToList().FirstOrDefault();
                item.Image = image;
            }
            return gallery;
        }


        public List<Gallery> GetActiveGallery(int instituteId)
        {

            Image image = new Image();
            var gallery = _redeeppitory.Query(x => x.InstituteId == instituteId && x.IsActive==true).Select().ToList();
            foreach (Gallery item in gallery)
            {
                image = _iImageService.GetImageByRefTypeIdAndRefPrimaryKey(((int)RefCode.Gallery), item.Id).ToList().FirstOrDefault();
                item.Image = image;
            }
            return gallery;
        }
        /// <summary>
        /// Get top ten Gallery
        /// </summary>
        /// <param name="instituteId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public IEnumerable<Gallery> GetGlobalGalleryByCurrentDateWithenStartEndDateTop10(int instituteId, bool isActive)
        {

            Image image = new Image();

            // var notice
            var gallery = _redeeppitory.Query().Select()
               // .Where(x => x.InstituteId == instituteId && x.StartDate <= DateTime.Now.Date && x.EndDate >= DateTime.Now.Date && x.IsActive == isActive)
                .Take(12);//;


            foreach (Gallery item in gallery)
            {
                image = _iImageService.GetImageByRefTypeIdAndRefPrimaryKey(((int)RefCode.Gallery), item.Id).ToList().FirstOrDefault();
                item.Image = image;
            }
            return gallery;
        }
        /// <summary>
        /// Gets the active gallery by institute identifier.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        public List<Gallery> GetActiveGalleryByInstituteId(int instituteId)
        {
            // List<Gallery> GalleryList = new List<Gallery>();
            var gallery = _redeeppitory.Query(x => x.InstituteId == instituteId && x.IsActive == true).Select();
            return gallery.Count() <= 0 ? new List<Gallery>() : gallery.ToList();
        }


    }

}
