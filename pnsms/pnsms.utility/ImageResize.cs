using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.utility
{
    public interface IImageResize
    {
        int MinWidth { get; set; }
        int MinHeight { get; set; }
        int MinSize { get; set; }
        int MaxCompressionPercentage { get; set; }

        Bitmap GetBitmap(Stream imgStream, int width, int height, int compressionPercentage);
        Bitmap GetBitmap(Stream imgStream, int compressionPercentage);
        Bitmap GetBitmap(Stream imgStream, int width, int height);
        Bitmap GetBitmap(Stream imgStream);
    }
    public class ImageResize : IImageResize
    {
        int minWidth = 600;
        int minHeight = 750;
        int minSize = 10240; //10KB
        int maxCompressionPercentage = 25;//at percentage

        public int MinWidth { get { return minWidth; } set { minWidth = value; } }
        public int MinHeight { get { return minHeight; } set { minHeight = value; } }
        public int MinSize { get { return minSize; } set { minSize = value; } }
        public int MaxCompressionPercentage { get { return maxCompressionPercentage; } set { maxCompressionPercentage = value; } }


        public Bitmap GetBitmap(Stream imgStream, int width, int height, int compressionPercentage)
        {
            return GetBitmap(imgStream, true, width, height, true, compressionPercentage);
        }

        public Bitmap GetBitmap(Stream imgStream, int compressionPercentage)
        {
            return GetBitmap(imgStream, false, 0, 0, true, compressionPercentage);
        }

        public Bitmap GetBitmap(Stream imgStream, int width, int height)
        {
            return GetBitmap(imgStream, true, width, height, false, 0);
        }

        public Bitmap GetBitmap(Stream imgStream)
        {
            return GetBitmap(imgStream, true, minWidth, minHeight, true, 0);
        }


        #region Private Methods

        Bitmap GetBitmap(Stream imgStream, bool isResize, int width, int height, bool isCompressed, int compressionPercentage)
        {
            long imageLength = imgStream.Length;
            Bitmap bmp= (Bitmap)Bitmap.FromStream(imgStream);
            
            if (isResize)
            {
                bmp = ReSize(bmp, width, height);                
            }
            if (isCompressed)
            {
                if (compressionPercentage == 0)
                {
                    compressionPercentage = imageLength > minSize ? (int)(100 / (imageLength / minSize)) : 100;
                    
                    compressionPercentage = compressionPercentage < maxCompressionPercentage ? maxCompressionPercentage : compressionPercentage;

                }

                if (compressionPercentage < 100 && imageLength > minSize)
                {
                    bmp = Compress(compressionPercentage, bmp);
                }
            }

            /*MemoryStream memoryStream = new MemoryStream();
            bmp.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            
            return memoryStream;*/
            
            return bmp;
        }

        MemoryStream GetStream(Stream imgStream, bool isResize, int width, int height, bool isCompressed, int compressionPercentage)
        {
            long imageLength = imgStream.Length;
            Bitmap bmp = (Bitmap)Bitmap.FromStream(imgStream);

            if (isResize)
            {
                bmp = ReSize(bmp, width, height);
            }
            if (isCompressed)
            {
                if (compressionPercentage == 0)
                {
                    compressionPercentage = imageLength > minSize ? (int)(100 / (imageLength / minSize)) : 100;

                    compressionPercentage = compressionPercentage < maxCompressionPercentage ? maxCompressionPercentage : compressionPercentage;

                }

                if (compressionPercentage < 100 && imageLength > minSize)
                {
                    bmp = Compress(compressionPercentage, bmp);
                }
            }

            MemoryStream memoryStream = new MemoryStream();
            bmp.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            
            return memoryStream;
        }

        
        Bitmap ReSize(Bitmap originalBmp, int width, int height)
        {            
            Double originalWidth = originalBmp.Width;
            Double originalHeight = originalBmp.Height; 
            Bitmap newBmp = null;
            
            
            if (width < originalWidth || height < originalHeight)
            {
                Int32 newWidth = width;
                Int32 newHeight = height;

                newBmp = new Bitmap(newWidth, newHeight, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
                newBmp.SetResolution(72, 72);

                int newX = 0;
                //Set the new top left drawing position on the image canvas
                int newY = 0;
                Double reDuce;

                //Keep the aspect ratio of image the same if not 4:3 and work out the newX and newY positions
                //to ensure the image is always in the centre of the canvas vertically and horizontally


                if (originalWidth > originalHeight)
                {
                    //Landscape picture
                    reDuce = newWidth / originalWidth;
                    //calculate the width percentage reduction as decimal
                    newHeight = ((Int32)(originalHeight * reDuce));
                    //reduce the uploaded image height by the reduce amount
                    newY = ((Int32)((height - newHeight) / 2));
                    //Position the image centrally down the canvas
                    newX = 0;
                    //Picture will be full width
                }
                else if (originalWidth < originalHeight)
                {
                    //Portrait picture
                    reDuce = newHeight / originalHeight;
                    //calculate the height percentage reduction as decimal
                    newWidth = ((Int32)(originalWidth * reDuce));
                    //reduce the uploaded image height by the reduce amount
                    newX = ((Int32)((width - newWidth) / 2));
                    //Position the image centrally across the canvas
                    newY = 0;
                    //Picture will be full hieght
                }
                else if (originalWidth == originalHeight)
                {
                    //square picture
                    reDuce = newHeight / originalHeight;
                    //calculate the height percentage reduction as decimal
                    newWidth = ((Int32)(originalWidth * reDuce));
                    //reduce the uploaded image height by the reduce amount
                    newX = ((Int32)((width - newWidth) / 2));
                    //Position the image centrally across the canvas
                    newY = ((Int32)((height - newHeight) / 2));
                    //Position the image centrally down the canvas
                }

                //Create a new image from the uploaded picture using the Graphics class
                //Clear the graphic and set the background colour to white
                //Use Antialias and High Quality Bicubic to maintain a good quality picture
                //Save the new bitmap image using 'Png' picture format and the calculated canvas positioning
                Graphics newGraphic = Graphics.FromImage(newBmp);

                try
                {
                    newGraphic.Clear(Color.White);
                    newGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                    newGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    newGraphic.DrawImage(originalBmp, newX, newY, newWidth, newHeight);
                }

                catch
                {

                }
                finally
                {
                    newGraphic.Dispose();
                }
            }
            else
            {
                newBmp = originalBmp;
            }

            return newBmp;
        }

        Bitmap Compress(int copressionPercentage, Bitmap bitmap)
        {
            try
            {
                ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);

                // Create an Encoder object based on the GUID 
                // for the Quality parameter category.
                System.Drawing.Imaging.Encoder myEncoder =
                    System.Drawing.Imaging.Encoder.Quality;

                // Create an EncoderParameters object. 
                // An EncoderParameters object has an array of EncoderParameter 
                // objects. In this case, there is only one 
                // EncoderParameter object in the array.
                EncoderParameters myEncoderParameters = new EncoderParameters(1);



                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, copressionPercentage);
                myEncoderParameters.Param[0] = myEncoderParameter;

                //MemoryStream stream = new MemoryStream();
                //bitmap.Save(stream, jgpEncoder, myEncoderParameters);

                return bitmap;
            }
            catch
            {
                return null;
            }
        }

        ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        #endregion
    }
}
