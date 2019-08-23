using deepp.Service;
using deepp.utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using deepp.Entities.Models;
using deepp.utility.Resource;
using System.Drawing.Imaging;

namespace deepp.landing.Controllers
{
    public class ImageController : Controller
    {

        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {

            _imageService = imageService;

        }
        //
        // GET: /Image/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetImageById(int id, string refCode = "")
        {
            RefCode refCodes = (RefCode)0;
            Enum.TryParse(refCode, true, out refCodes);
            //  var result = new HttpResponseMessage(HttpStatusCode.OK);
            var img = _imageService.GetImageById(id);
            return File(img.ImageBinaryData = img.ImageBinaryData == null ? new byte[] { } : img.ImageBinaryData, "image/jpg");
        }
        public ActionResult GetImage(int id, string refCode, bool isThumbnail = false)
        {

            RefCode refCodes = (RefCode)0;
            Enum.TryParse(refCode, true, out refCodes);

            var img = _imageService.GetImageByRefTypeIdAndRefPrimaryKey((int)refCodes, id).FirstOrDefault();

            if (img != null && img.ImageBinaryData != null)
            {
                if (isThumbnail == true)
                {
                    Stream stream = new MemoryStream(img.ImageBinaryData);
                    var imageResize = new ImageResize();
                    var thumbImg = ImageToByte(imageResize.GetBitmap(stream, 100, 100, 50));
                    return File(thumbImg, "image/jpg");

                }
                else
                {
                    return File(img.ImageBinaryData, "image/jpg");
                }

            }
            else
            {

                deepp.Entities.Models.Image image = new deepp.Entities.Models.Image();
                img = image;
                return File(img.ImageBinaryData = img.ImageBinaryData == null ? new byte[] { } : img.ImageBinaryData, "image/jpg");
            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage NoImageContent()
        {
            MemoryStream ms = new MemoryStream();
            Labels.NoImageFound.Save(ms, ImageFormat.Png);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(ms.ToArray());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            return result;

        }
        /// <summary>
        /// Images to byte.
        /// </summary>
        /// <param name="img">The img.</param>
        /// <returns></returns>
        public static byte[] ImageToByte(System.Drawing.Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        //protected override void Dispose(bool disdeepping)
        //{
        //    if (disdeepping)
        //    {
        //        _unitOfWork.Dispose();
        //    }
        //    base.Dispose(disdeepping);
        //}
    }
}
