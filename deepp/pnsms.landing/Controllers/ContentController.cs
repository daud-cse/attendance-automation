using Microsoft.Practices.Unity;
using pnsms.Entities.Models;
using pnsms.Service;
//using pnsms.Service.Contents;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace pnsms.landing.Controllers
{
    public class ContentController : Controller
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
        /// Gets or sets the Content service.
        /// </summary>
        /// <value>
        /// The Content service.
        /// </value>
        //[Dependency]
        //public IContentService ContentService { get; set; }
        /// <summary>
        /// Gets or sets the image service.
        /// </summary>
        /// <value>
        /// The image service.
        /// </value>
        [Dependency]
        public IImageService ImageService { get; set; }

        #endregion

        #region "  -  [  CRUD  ]  -  "

        // GET api/Content

        //public IEnumerable<Content> Get(bool isActive = false)
        //{

        //    return ContentService.GetContents(Sessions.InstituteId);
        //}

        //public ActionResult ContentList()
        //{
        //    var content = ContentService.GetContents(Sessions.InstituteId,true);
        //   // if (content.Images != null) content.Images.ForEach(s => s.ImageBinaryData = null);
        //    return View(content);
        //}

        //public ActionResult Details(int id)
        //{

        //    var content = ContentService.GetContentById(id);
        //    if (content.Images != null) content.Images.ForEach(s => s.ImageBinaryData = null);
        //    return View(content);
        //}
        [HttpGet]
        public ActionResult ContentDownload(int id)
        {
            //Image myClass = new Image();
            //byte[] img = myClass.ImageBinaryData.ToArray();
            //Response.AppendHeader("Content-Disposition", "inline; filename=xyz.pdf");
            //return File(img, "application/pdf");
            //
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var img = ImageService.GetImageById(id);
            if (img != null && img.ImageBinaryData != null)
            {

                var stream = new MemoryStream(img.ImageBinaryData);
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentType =
                    new MediaTypeHeaderValue(img.FileExt);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = img.ImageCaption
                };
                //return Files(result);
            }
            else
            {
                result = new HttpResponseMessage(HttpStatusCode.NotFound);

            }
            return File(img.ImageBinaryData.ToArray(), result.Content.Headers.ContentType.ToString(), img.ImageCaption);
           // return result;
        }
        #endregion

        #region "  -  [  Others  ]  -  "

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