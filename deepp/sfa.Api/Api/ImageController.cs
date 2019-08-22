using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Web;
using pnsms.Entities.Models;
using pnsms.Service;
using pnsms.utility;
using pnsms.utility.Resource;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace sfa.Api.Api
{
    public class ImageController : ApiController
    {


        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IImageService _imageService;


        public ImageController(IUnitOfWorkAsync unitOfWorkAsync, IImageService imageService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _imageService = imageService;
        }

        // image upload
        [HttpPost]
        [Route("api/image")]
        public HttpResponseMessage ImageUpload()
        {
            try
            {
                var httpPostedFile = HttpContext.Current.Request.Files;
                ImageResize img = new ImageResize();
                var imgBig = ImageToByte(img.GetBitmap(httpPostedFile[0].InputStream, 100, 100, 50));
                var imgSmall = ImageToByte(img.GetBitmap(httpPostedFile[0].InputStream, 25, 25, 50));
                var images = new List<byte[]>() { imgBig, imgSmall };
                Sessions.Temp = images;
                var resultBitmap = img.GetBitmap(httpPostedFile[0].InputStream, 160);
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StringContent(Convert.ToBase64String(imgBig));
                return result;
            }
            catch (Exception exception)
            {
                throw new ValidationException(exception.Message);
            }

        }

        [HttpPost]
        [Route("api/image/multiple")]
        public void MultipleImageUpload(bool resize=true)
        {
            var httpPostedFile = HttpContext.Current.Request.Files;
            ImageResize imageResize = new ImageResize();
            var imgBig = (resize?ImageToByte(imageResize.GetBitmap(httpPostedFile[0].InputStream, 600, 400, 50)):ImageToByte(imageResize.GetBitmap(httpPostedFile[0].InputStream,100)));

            List<byte[]> objList = new List<byte[]>();
            var obj = Sessions.Temp;
            if (obj != null)
            {
                objList = (List<byte[]>)obj;
            }
            else
            {
                objList = new List<byte[]>();
            }

            objList.Add(imgBig);
            Sessions.Temp = objList;

        }

        [HttpPost]
        [Route("api/image/institute")]
        public void InstituteImageUpload()
        {
            var httpPostedFile = HttpContext.Current.Request.Files;
            var form = HttpContext.Current.Request.Form;
            var imageType = "";
            ImageResize imageResize = new ImageResize();

            List<byte[]> objList = new List<byte[]>(){null,null};
            var obj = Sessions.Temp;
            if (obj != null)
            {
                objList = (List<byte[]>)obj;
            }
            else
            {
                objList = new List<byte[]>() { null, null };
            }

            
            for (int i = 0; i < form.Count; i++)
            {
                imageType += form[i];
            }

            if (imageType == "logo")
            {
                var imageLogo = ImageToByte(imageResize.GetBitmap(httpPostedFile[0].InputStream,50));
                objList[0] = imageLogo;
            }

            if (imageType == "banner")
            {
                var imageBanner = ImageToByte(imageResize.GetBitmap(httpPostedFile[0].InputStream, 50));
                objList[1] = imageBanner;
            }

           
            Sessions.Temp = objList;

        }
        //[HttpPost]
        //[Route("api/image/ImageUploadNotice")]
        //public void ImageUploadNotice()
        //{
        //        var httpPostedFile = HttpContext.Current.Request.Files;
        //        ImageResize img = new ImageResize();
        //        var imgBig = ImageToByte(img.GetBitmap(httpPostedFile[0].InputStream, 100, 100, 50));

        //        List<byte[]> objList = new List<byte[]>();

        //        var obj = Sessions.Temp;

        //        if (obj != null)
        //        {
        //            objList = (List<byte[]>)obj;
        //        }
        //        else 
        //        {
        //            objList = new List<byte[]>(); 
        //        }

        //        objList.Add(imgBig);
        //        Sessions.Temp = objList;

        //}


        [Route("api/image")]
        public HttpResponseMessage GetImage(int id)
        {

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var img = _imageService.GetImageById(id);
            if (img != null && img.ImageBinaryData != null)
            {
                result.Content = new ByteArrayContent(img.ImageBinaryData);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            }
            else
            {
                result = new HttpResponseMessage(HttpStatusCode.NotFound);

            }

            return result;
        }
        [Route("api/image/refcode")]
        public HttpResponseMessage GetImage(int id, string refCode, bool isThumbnail = false)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);

            try
            {

                if (refCode == null)
                    return new HttpResponseMessage(HttpStatusCode.NotFound);

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
                        result.Content = new ByteArrayContent(thumbImg);

                    }
                    else
                    {
                        result.Content = new ByteArrayContent(img.ImageBinaryData);
                    }

                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                    return result;
                }


                return NoImageContent();
            }
            catch
            {


                return NoImageContent();
            }



            ;
        }
        [Route("api/image/thumb")]
        public HttpResponseMessage GetThumbImage(int id)
        {

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var img = _imageService.GetImageById(id);

            if (img != null && img.ImageBinaryData != null)
            {
                Stream stream = new MemoryStream(img.ImageBinaryData);
                ImageResize imageResize = new ImageResize();
                var thumbImg = ImageToByte(imageResize.GetBitmap(stream, 100, 100, 50));
                result.Content = new ByteArrayContent(thumbImg);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            }
            else
            {
                result = new HttpResponseMessage(HttpStatusCode.NotFound);

            }

            return result;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }
        /// <summary>
        /// Reads the fully.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[input.Length];
            //byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
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

        /// <summary>
        /// Bitmaps to byte array.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <returns></returns>
        public static byte[] BitmapToByteArray(Bitmap bitmap)
        {

            BitmapData bmpdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
            int numbytes = bmpdata.Stride * bitmap.Height;
            byte[] bytedata = new byte[numbytes];
            IntPtr ptr = bmpdata.Scan0;

            Marshal.Copy(ptr, bytedata, 0, numbytes);

            bitmap.UnlockBits(bmpdata);

            return bytedata;

        }

        /// <summary>
        /// Noes the content of the image.
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
    }
}
