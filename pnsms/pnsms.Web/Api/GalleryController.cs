﻿using System.IO;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using pnsms.Entities.Models;
using pnsms.Entities.ViewModels.Teacher;
using pnsms.Service;
using pnsms.Service.ViewModels;
using pnsms.erp.Attributes;
using Repository.Pattern.Ef6;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
 

namespace pnsms.erp.Api
{
    /// <summary>
    /// Gallery Controller
    /// </summary>
    public class GalleryController : ApiController
    {
        #region "  -  [  Constractor  ]  -  "
        
        /// <summary>
        /// Gets or sets the unit of work asynchronous.
        /// </summary>
        /// <value>
        /// The unit of work asynchronous.
        /// </value>
        [Dependency]
        public IUnitOfWorkAsync UnitOfWorkAsync { get; set; }

        /// <summary>
        /// Gets or sets the gallery service.
        /// </summary>
        /// <value>
        /// The gallery service.
        /// </value>
        [Dependency]
        public IGalleryService GalleryService { get; set; }
 
        #endregion

        #region "  -  [  CRUD  ]  -  "

        // GET api/gallery

        public IEnumerable<Gallery> Get(bool isActive = false)
        {

            return GalleryService.GetGalleryByInstituteId(Sessions.InstituteId);
        }

        public Gallery Get(int id)
        {
            var gallery = GalleryService.GetGalleryById(Sessions.InstituteId, id);
            gallery.Images.ForEach(s => s.ImageBinaryData = null);
            return gallery;
        }

        public HttpResponseMessage Post([FromBody]Gallery gallery)
        {
            var imageList = new List<Image>();
            if (Sessions.Temp != null)
            {
                var images = (List<byte[]>)Sessions.Temp;
                if (images != null)
                {
                   
                    foreach (var image in images)
                    {
                        var img = new Entities.Models.Image() { RefTypeId = (int)utility.RefCode.Gallery, ImageBinaryData = image};
                        imageList.Add(img);
                        
                    }
                 
                }
                
                Sessions.Temp = null;
            }
            GalleryService.CreateGallery(UnitOfWorkAsync, Sessions.InstituteId, gallery, imageList);

            return new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new JsonContent(new
                {
                    Id = gallery.Id,
                    Message = "Success"
                })
            };
        }

        // PUT api/gallery/5

        public void Put(int id, [FromBody]Gallery gallery)
        {
            var imageList = new List<Image>();
            if (Sessions.Temp != null)
            {
                var images = (List<byte[]>)Sessions.Temp;
                if (images != null)
                {
                     
                    foreach (var image in images)
                    {
                        var img = new Entities.Models.Image() { RefTypeId = (int)utility.RefCode.Gallery, ImageBinaryData = image };
                        imageList.Add(img);

                    }
                     
                }

                Sessions.Temp = null;
            }
            GalleryService.UpdateGallery(UnitOfWorkAsync, Sessions.InstituteId, gallery, imageList);

        }

        // DELETE api/gallery/5

        public void Delete(int id)
        {
        }

         
        #endregion

        #region "  -  [  Others  ]  -  "

        /// <summary>
        /// Gets the new gallery.
        /// </summary>
        /// <returns></returns>
        [Route("api/gallery/new")]
        public Gallery GetNewGallery()
        {

            var gallery = GalleryService.NewGallery(Sessions.InstituteId);
            return gallery;
        }

       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
         
    }


}
