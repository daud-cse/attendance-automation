using pnsms.Entities.Models;
using pnsms.Service;
using pnsms.utility;
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

namespace pnsms.Protal.Controllers
{
    public class ImageController : Controller
    {

        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {

            _imageService = imageService;

        }
        /// <summary>
        /// Get Single Image
        /// daud
        /// </summary>
        /// <param name="id"></param>
        /// <param name="refCode"></param>
        /// <returns></returns>
        public ActionResult GetImageById(int id)
        {

            var img = _imageService.GetImageById(id);
            if (img == null)
            {
                img = new pnsms.Entities.Models.Image();
            }
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
                pnsms.Entities.Models.Image image = new pnsms.Entities.Models.Image();
                img = image;
                return File(img.ImageBinaryData = img.ImageBinaryData == null ? new byte[] { } : img.ImageBinaryData, "image/jpg");
            }


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
    }
}
