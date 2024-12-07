using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessing
{
    partial class Filters
    {
        public Filters() { }

        public Bitmap basicCopy(Bitmap loaded)
        {
            if (loaded == null)
            {
                throw new ArgumentException("Picture Box required is null");
            }

            Bitmap processed = new Bitmap(loaded);

            BitmapData bmpData = processed.LockBits(new Rectangle(0, 0, processed.Width, processed.Height),
                                                    ImageLockMode.ReadWrite, processed.PixelFormat);
            int bytesPerPixel = Image.GetPixelFormatSize(processed.PixelFormat) / 8;
            int byteCount = bmpData.Stride * processed.Height;
            byte[] pixelData = new byte[byteCount];
            IntPtr ptrFirstPixel = bmpData.Scan0;
            Marshal.Copy(ptrFirstPixel, pixelData, 0, byteCount);

            for (int y = 0; y < processed.Height; y++)
            {
                int rowStartIndex = y * bmpData.Stride;
                for (int x = 0; x < processed.Width; x++)
                {
                    int pixelIndex = rowStartIndex + x * bytesPerPixel;

                    pixelData[pixelIndex] = (byte)pixelData[pixelIndex];
                    pixelData[pixelIndex + 1] = (byte)pixelData[pixelIndex + 1];
                    pixelData[pixelIndex + 2] = (byte)pixelData[pixelIndex + 2];
                }
            }

            Marshal.Copy(pixelData, 0, ptrFirstPixel, byteCount);
            processed.UnlockBits(bmpData);

            return processed;
        }

        public Bitmap grayScale(Bitmap loaded)
        {
            if (loaded == null)
            {
                throw new ArgumentException("Picture Box required is null");
            }

            Bitmap processed = new Bitmap(loaded);

            BitmapData bmpData = processed.LockBits(new Rectangle(0, 0, processed.Width, processed.Height),
                                                    ImageLockMode.ReadWrite, processed.PixelFormat);
            int bytesPerPixel = Image.GetPixelFormatSize(processed.PixelFormat) / 8;
            int byteCount = bmpData.Stride * processed.Height;
            byte[] pixelData = new byte[byteCount];
            IntPtr ptrFirstPixel = bmpData.Scan0;
            Marshal.Copy(ptrFirstPixel, pixelData, 0, byteCount);

            for (int y = 0; y < processed.Height; y++)
            {
                int rowStartIndex = y * bmpData.Stride;
                for (int x = 0; x < processed.Width; x++)
                {
                    int pixelIndex = rowStartIndex + x * bytesPerPixel;

                    int grayValue = (pixelData[pixelIndex] + pixelData[pixelIndex + 1] + pixelData[pixelIndex + 2]) / 3;

                    pixelData[pixelIndex] = (byte)grayValue;
                    pixelData[pixelIndex + 1] = (byte)grayValue;
                    pixelData[pixelIndex + 2] = (byte)grayValue;
                }
            }

            Marshal.Copy(pixelData, 0, ptrFirstPixel, byteCount);
            processed.UnlockBits(bmpData);

            return processed;
        }

        public Bitmap colorInverse(Bitmap loaded)
        {
            if (loaded == null)
            {
                throw new ArgumentException("Picture Box required is null");
            }

            Bitmap processed = new Bitmap(loaded);

            BitmapData bmpData = processed.LockBits(new Rectangle(0, 0, processed.Width, processed.Height),
                                                    ImageLockMode.ReadWrite, processed.PixelFormat);
            int bytesPerPixel = Image.GetPixelFormatSize(processed.PixelFormat) / 8;
            int byteCount = bmpData.Stride * processed.Height;
            byte[] pixelData = new byte[byteCount];
            IntPtr ptrFirstPixel = bmpData.Scan0;
            Marshal.Copy(ptrFirstPixel, pixelData, 0, byteCount);

            for (int y = 0; y < processed.Height; y++)
            {
                int rowStartIndex = y * bmpData.Stride;
                for (int x = 0; x < processed.Width; x++)
                {
                    int pixelIndex = rowStartIndex + x * bytesPerPixel;

                    pixelData[pixelIndex] = (byte)(255 - pixelData[pixelIndex]);
                    pixelData[pixelIndex + 1] = (byte)(255 - pixelData[pixelIndex + 1]);
                    pixelData[pixelIndex + 2] = (byte)(255 - pixelData[pixelIndex + 2]);
                }
            }

            Marshal.Copy(pixelData, 0, ptrFirstPixel, byteCount);
            processed.UnlockBits(bmpData);

            return processed;
        }

        public Bitmap sepiaScale(Bitmap loaded)
        {
            if (loaded == null)
            {
                throw new ArgumentException("Picture Box required is null");
            }

            Bitmap processed = new Bitmap(loaded);

            BitmapData bmpData = processed.LockBits(new Rectangle(0, 0, processed.Width, processed.Height),
                                                    ImageLockMode.ReadWrite, processed.PixelFormat);
            int bytesPerPixel = Image.GetPixelFormatSize(processed.PixelFormat) / 8;
            int byteCount = bmpData.Stride * processed.Height;
            byte[] pixelData = new byte[byteCount];
            IntPtr ptrFirstPixel = bmpData.Scan0;
            Marshal.Copy(ptrFirstPixel, pixelData, 0, byteCount);

            for (int y = 0; y < processed.Height; y++)
            {
                int rowStartIndex = y * bmpData.Stride;
                for (int x = 0; x < processed.Width; x++)
                {
                    int pixelIndex = rowStartIndex + x * bytesPerPixel;
                    int redValue = (int)(0.393 * pixelData[pixelIndex] + 0.769 * pixelData[pixelIndex + 1] + 0.189 * pixelData[pixelIndex + 2]);
                    int greenValue = (int)(0.349 * pixelData[pixelIndex] + 0.686 * pixelData[pixelIndex + 1] + 0.168 * pixelData[pixelIndex + 2]);
                    int blueValue = (int)(0.272 * pixelData[pixelIndex] + 0.534 * pixelData[pixelIndex + 1] + 0.131 * pixelData[pixelIndex + 2]);

                    if (redValue > 255) redValue = 255;
                    if (greenValue > 255) greenValue = 255;
                    if (blueValue > 255) blueValue = 255;

                    pixelData[pixelIndex] = (byte)blueValue;
                    pixelData[pixelIndex + 1] = (byte)greenValue;
                    pixelData[pixelIndex + 2] = (byte)redValue;
                }
            }

            Marshal.Copy(pixelData, 0, ptrFirstPixel, byteCount);
            processed.UnlockBits(bmpData);

            return processed;
        }

        public Bitmap histogram(Bitmap loaded)
        {
            if (loaded == null )
            {
                throw new ArgumentException("Picture Box required is null");
            }

            Bitmap processed = new Bitmap(loaded);

            BitmapData bmpData = processed.LockBits(new Rectangle(0, 0, processed.Width, processed.Height),
                                                    ImageLockMode.ReadWrite, processed.PixelFormat);
            int bytesPerPixel = Image.GetPixelFormatSize(processed.PixelFormat) / 8;
            int byteCount = bmpData.Stride * processed.Height;
            byte[] pixelData = new byte[byteCount];
            IntPtr ptrFirstPixel = bmpData.Scan0;
            Marshal.Copy(ptrFirstPixel, pixelData, 0, byteCount);
            int[] histData = new int[256];

            for (int y = 0; y < processed.Height; y++)
            {
                int rowStartIndex = y * bmpData.Stride;
                for (int x = 0; x < processed.Width; x++)
                {
                    int pixelIndex = rowStartIndex + x * bytesPerPixel;

                    int grayValue = (pixelData[pixelIndex] + pixelData[pixelIndex + 1] + pixelData[pixelIndex + 2]) / 3;

                    histData[grayValue]++;
                }
            }

            Marshal.Copy(pixelData, 0, ptrFirstPixel, byteCount);
            processed.UnlockBits(bmpData);



            Bitmap histProcessed = new Bitmap(256, 400);
            for (int x = 0; x < 256; x++)
                for (int y = 0; y < 400; y++)
                    histProcessed.SetPixel(x, y, Color.White);

            for (int x = 0; x < 256; x++)
                for (int y = 0; y < Math.Min(histData[x] / 5, histProcessed.Height - 1); y++)
                    histProcessed.SetPixel(x, (histProcessed.Height - 1) - y, Color.Black);

            return histProcessed;
        }

        public Bitmap subtract(Bitmap loaded, Bitmap backgroundLoaded)
        {

            if(loaded == null || backgroundLoaded == null)
            {
                throw new ArgumentException("One of the Picture Boxes required is null");
            }

            if (loaded.Width != backgroundLoaded.Width || loaded.Height != backgroundLoaded.Height)
            {
                throw new ArgumentException("Both bitmaps must have the same dimensions.");
            }

            Bitmap processed = new Bitmap(loaded.Width, loaded.Height, loaded.PixelFormat);

            Color green = Color.FromArgb(0, 0, 255);
            int grayGreenValue = (green.R + green.G + green.B) / 3;
            int threshold = 5;

            BitmapData loadedData = loaded.LockBits(new Rectangle(0, 0, loaded.Width, loaded.Height),
                                                    ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData backgroundData = backgroundLoaded.LockBits(new Rectangle(0, 0, backgroundLoaded.Width, backgroundLoaded.Height),
                                                                  ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData processedData = processed.LockBits(new Rectangle(0, 0, processed.Width, processed.Height),
                                                          ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            int stride = loadedData.Stride;
            int byteCount = stride * loaded.Height;

            byte[] loadedPixels = new byte[byteCount];
            byte[] backgroundPixels = new byte[byteCount];
            byte[] processedPixels = new byte[byteCount];

            Marshal.Copy(loadedData.Scan0, loadedPixels, 0, byteCount);
            Marshal.Copy(backgroundData.Scan0, backgroundPixels, 0, byteCount);

            for (int y = 0; y < loaded.Height; y++)
            {
                for (int x = 0; x < loaded.Width; x++)
                {
                    int pixelIndex = y * stride + x * 3;

                    int grayValue = (loadedPixels[pixelIndex] + loadedPixels[pixelIndex + 1] + loadedPixels[pixelIndex + 2]) / 3;
                    int subtractValue = Math.Abs(grayValue - grayGreenValue);

                    if (subtractValue < threshold)
                    {
                        processedPixels[pixelIndex] = backgroundPixels[pixelIndex];
                        processedPixels[pixelIndex + 1] = backgroundPixels[pixelIndex + 1];
                        processedPixels[pixelIndex + 2] = backgroundPixels[pixelIndex + 2];
                    }
                    else
                    {
                        processedPixels[pixelIndex] = loadedPixels[pixelIndex];
                        processedPixels[pixelIndex + 1] = loadedPixels[pixelIndex + 1];
                        processedPixels[pixelIndex + 2] = loadedPixels[pixelIndex + 2];
                    }
                }
            }

            Marshal.Copy(processedPixels, 0, processedData.Scan0, byteCount);

            loaded.UnlockBits(loadedData);
            backgroundLoaded.UnlockBits(backgroundData);
            processed.UnlockBits(processedData);

            return processed;
        }   
    }
}
