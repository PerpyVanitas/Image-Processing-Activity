using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Video;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ImageProcessing
{
    
    partial class Camera
    {
        public PictureBox pbLoadedBox;
        public PictureBox pbProcessedBox;
        public PictureBox pbBackgroundLoadedBox;

        private Filters filter = new Filters();
        private ConvFilters convFilter = new ConvFilters();

        public FilterInfoCollection filterInfo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        public VideoCaptureDevice videoCapture;
        public Bitmap videoCaptureRaw;
        public Thread processingThread;
        private Boolean shouldStop = false;

        public Camera(PictureBox pbLoadedBox, PictureBox pbProcessedBox, PictureBox pbBackgroundLoadedBox)
        {
            this.pbLoadedBox = pbLoadedBox;
            this.pbProcessedBox = pbProcessedBox;
            this.pbBackgroundLoadedBox = pbBackgroundLoadedBox;
        }

        public void startVideoCapture()
        {
            videoCapture = new VideoCaptureDevice(filterInfo[0].MonikerString);
            videoCapture.NewFrame += VideoCapture_NewFrame;
            videoCapture.Start();
        }

        private void VideoCapture_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pbLoadedBox.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        public void startBasicCopyVideoCapture()
        {
            shouldStop = false;
            processingThread = new Thread(BasicCopyVideo);
            processingThread.IsBackground = true;
            processingThread.Start();
        }

        private void BasicCopyVideo()
        {
            Bitmap loaded, processed;
            int tick = 0;
            do
            {
                if (shouldStop) break;
                loaded = new Bitmap((Bitmap)pbLoadedBox.Image.Clone());
                
                processed = filter.basicCopy(loaded);

                tick++;

                if (!shouldStop)
                {
                    pbProcessedBox.Invoke((MethodInvoker)(() => pbProcessedBox.Image = processed));
                }

            } while (tick <= 100);

        }

        public void startGrayScaleVideoCapture()
        {
            shouldStop = false;
            processingThread = new Thread(GrayScaleVideo);
            processingThread.IsBackground = true;
            processingThread.Start();
        }

        private void GrayScaleVideo()
        {
            Bitmap loaded, processed;
            int tick = 0;
            do
            {
                if (shouldStop) break;
                loaded = new Bitmap((Bitmap)pbLoadedBox.Image.Clone());

                processed = filter.grayScale(loaded);

                tick++;

                if (!shouldStop)
                {
                    pbProcessedBox.Invoke((MethodInvoker)(() => pbProcessedBox.Image = processed));
                }

            } while (tick <= 100);

        }

        public void startSepiaScaleVideoCapture()
        {
            shouldStop = false;
            processingThread = new Thread(SepiaScaleVideo);
            processingThread.IsBackground = true;
            processingThread.Start();
        }

        private void SepiaScaleVideo()
        {
            Bitmap loaded, processed;
            int tick = 0;
            do
            {
                if (shouldStop) break;
                loaded = new Bitmap((Bitmap)pbLoadedBox.Image.Clone());

                processed = filter.sepiaScale(loaded);

                tick++;

                if (!shouldStop)
                {
                    pbProcessedBox.Invoke((MethodInvoker)(() => pbProcessedBox.Image = processed));
                }

            } while (tick <= 100);

        }

        public void startColorInverseVideoCapture()
        {
            shouldStop = false;
            processingThread = new Thread(ColorInverseVideo);
            processingThread.IsBackground = true;
            processingThread.Start();
        }

        private void ColorInverseVideo()
        {
            Bitmap loaded, processed;
            int tick = 0;
            do
            {
                if (shouldStop) break;
                loaded = new Bitmap((Bitmap)pbLoadedBox.Image.Clone());

                processed = filter.colorInverse(loaded);

                tick++;

                if (!shouldStop)
                {
                    pbProcessedBox.Invoke((MethodInvoker)(() => pbProcessedBox.Image = processed));
                }

            } while (tick <= 100);

        }

        public void startHistogramVideoCapture()
        {
            shouldStop = false;
            processingThread = new Thread(HistogramVideo);
            processingThread.IsBackground = true;
            processingThread.Start();
        }

        private void HistogramVideo()
        {
            Bitmap loaded, processed;
            int tick = 0;
            do
            {
                if (shouldStop) break;
                loaded = new Bitmap((Bitmap)pbLoadedBox.Image.Clone());

                processed = filter.histogram(loaded);

                tick++;

                if (!shouldStop)
                {
                    pbProcessedBox.Invoke((MethodInvoker)(() => pbProcessedBox.Image = processed));
                }

            } while (tick <= 100);

        }

        public void startSubtractionVideoCapture()
        {
            shouldStop = false;
            processingThread = new Thread(SubtractionVideo);
            processingThread.IsBackground = true;
            processingThread.Start();
        }

        private void SubtractionVideo()
        {
            Bitmap loaded, processed, backgroundLoaded;
            int tick = 0;
            do
            {
                if (shouldStop) break;
                loaded = new Bitmap((Bitmap)pbLoadedBox.Image.Clone());
                backgroundLoaded = new Bitmap((Bitmap)pbBackgroundLoadedBox.Image.Clone());

                processed = filter.subtract(loaded, backgroundLoaded);

                tick++;

                if (!shouldStop)
                {
                    pbProcessedBox.Invoke((MethodInvoker)(() => pbProcessedBox.Image = processed));
                }

            } while (tick <= 100);

        }

        public void startSmoothVideoCapture()
        {
            shouldStop = false;
            processingThread = new Thread(SmoothVideo);
            processingThread.IsBackground = true;
            processingThread.Start();
        }

        private void SmoothVideo()
        {
            Bitmap loaded, processed;
            int tick = 0;
            do
            {
                if (shouldStop) break;
                processed = new Bitmap((Bitmap)pbLoadedBox.Image.Clone());

                convFilter.Smooth(processed, 1);

                tick++;

                if (!shouldStop)
                {
                    pbProcessedBox.Invoke((MethodInvoker)(() => pbProcessedBox.Image = processed));
                }

            } while (tick <= 100);

        }

        public void startGaussianVideoCapture()
        {
            shouldStop = false;
            processingThread = new Thread(GaussianVideo);
            processingThread.IsBackground = true;
            processingThread.Start();
        }

        private void GaussianVideo()
        {
            Bitmap loaded, processed;
            int tick = 0;
            do
            {
                if (shouldStop) break;
                processed = new Bitmap((Bitmap)pbLoadedBox.Image.Clone());

                convFilter.GaussianBlur(processed, 4);

                tick++;

                if (!shouldStop)
                {
                    pbProcessedBox.Invoke((MethodInvoker)(() => pbProcessedBox.Image = processed));
                }

            } while (tick <= 100);

        }

        public void startMeanRemovalVideoCapture()
        {
            shouldStop = false;
            processingThread = new Thread(MeanRemovalVideo);
            processingThread.IsBackground = true;
            processingThread.Start();
        }

        private void MeanRemovalVideo()
        {
            Bitmap loaded, processed;
            int tick = 0;
            do
            {
                if (shouldStop) break;
                processed = new Bitmap((Bitmap)pbLoadedBox.Image.Clone());

                convFilter.MeanRemoval(processed, 9);

                tick++;

                if (!shouldStop)
                {
                    pbProcessedBox.Invoke((MethodInvoker)(() => pbProcessedBox.Image = processed));
                }

            } while (tick <= 100);

        }

        public void startSharpenVideoCapture()
        {
            shouldStop = false;
            processingThread = new Thread(SharpenVideo);
            processingThread.IsBackground = true;
            processingThread.Start();
        }

        private void SharpenVideo()
        {
            Bitmap loaded, processed;
            int tick = 0;
            do
            {
                if (shouldStop) break;
                processed = new Bitmap((Bitmap)pbLoadedBox.Image.Clone());

                convFilter.Sharpen(processed, 11);

                tick++;

                if (!shouldStop)
                {
                    pbProcessedBox.Invoke((MethodInvoker)(() => pbProcessedBox.Image = processed));
                }

            } while (tick <= 100);

        }

        public void startEmbossVideoCapture()
        {
            shouldStop = false;
            processingThread = new Thread(EmbossVideo);
            processingThread.IsBackground = true;
            processingThread.Start();
        }

        private void EmbossVideo()
        {
            Bitmap loaded, processed;
            int tick = 0;
            do
            {
                if (shouldStop) break;
                processed = new Bitmap((Bitmap)pbLoadedBox.Image.Clone());

                convFilter.EmbossLaplacian(processed);

                tick++;

                if (!shouldStop)
                {
                    pbProcessedBox.Invoke((MethodInvoker)(() => pbProcessedBox.Image = processed));
                }

            } while (tick <= 100);

        }

        public void startEdgeDetectQuickVideoCapture()
        {
            shouldStop = false;
            processingThread = new Thread(EdgeDetectQuickVideo);
            processingThread.IsBackground = true;
            processingThread.Start();
        }

        private void EdgeDetectQuickVideo()
        {
            Bitmap loaded, processed;
            int tick = 0;
            do
            {
                if (shouldStop) break;
                processed = new Bitmap((Bitmap)pbLoadedBox.Image.Clone());

                convFilter.EdgeDetectQuick(processed);

                tick++;

                if (!shouldStop)
                {
                    pbProcessedBox.Invoke((MethodInvoker)(() => pbProcessedBox.Image = processed));
                }

            } while (tick <= 100);

        }

        public void startKirshVideoCapture(Byte value)
        {
            shouldStop = false;
            processingThread = new Thread(() => KirshVideo(value));
            processingThread.IsBackground = true;
            processingThread.Start();
        }

        private void KirshVideo(Byte value)
        {
            Bitmap loaded, processed;
            int tick = 0;
            do
            {
                if (shouldStop) break;
                processed = new Bitmap((Bitmap)pbLoadedBox.Image.Clone());

                convFilter.EdgeDetectConvolution(processed, 1, value);

                tick++;

                if (!shouldStop)
                {
                    pbProcessedBox.Invoke((MethodInvoker)(() => pbProcessedBox.Image = processed));
                }

            } while (tick <= 100);

        }

        public void startPrewittVideoCapture(Byte value)
        {
            shouldStop = false;
            processingThread = new Thread(() => PrewittVideo(value));
            processingThread.IsBackground = true;
            processingThread.Start();
        }

        private void PrewittVideo(Byte value)
        {
            Bitmap loaded, processed;
            int tick = 0;
            do
            {
                if (shouldStop) break;
                processed = new Bitmap((Bitmap)pbLoadedBox.Image.Clone());

                convFilter.EdgeDetectConvolution(processed, 2, value);

                tick++;

                if (!shouldStop)
                {
                    pbProcessedBox.Invoke((MethodInvoker)(() => pbProcessedBox.Image = processed));
                }

            } while (tick <= 100);

        }

        public void startSobelVideoCapture(Byte value)
        {
            shouldStop = false;
            processingThread = new Thread(() => SobelVideo(value));
            processingThread.IsBackground = true;
            processingThread.Start();
        }

        private void SobelVideo(Byte value)
        {
            Bitmap loaded, processed;
            int tick = 0;
            do
            {
                if (shouldStop) break;
                processed = new Bitmap((Bitmap)pbLoadedBox.Image.Clone());

                convFilter.EdgeDetectConvolution(processed, 3, value);

                tick++;

                if (!shouldStop)
                {
                    pbProcessedBox.Invoke((MethodInvoker)(() => pbProcessedBox.Image = processed));
                }

            } while (tick <= 100);

        }

        public void startEdgeDetectHorizontalVideoCapture()
        {
            shouldStop = false;
            processingThread = new Thread(EdgeDetectHorizontalVideo);
            processingThread.IsBackground = true;
            processingThread.Start();
        }

        private void EdgeDetectHorizontalVideo()
        {
            Bitmap loaded, processed;
            int tick = 0;
            do
            {
                if (shouldStop) break;
                processed = new Bitmap((Bitmap)pbLoadedBox.Image.Clone());

                convFilter.EdgeDetectHorizontal(processed);

                tick++;

                if (!shouldStop)
                {
                    pbProcessedBox.Invoke((MethodInvoker)(() => pbProcessedBox.Image = processed));
                }

            } while (tick <= 100);

        }

        public void startEdgeDetectVerticalVideoCapture()
        {
            shouldStop = false;
            processingThread = new Thread(EdgeDetectVerticalVideo);
            processingThread.IsBackground = true;
            processingThread.Start();
        }

        private void EdgeDetectVerticalVideo()
        {
            Bitmap loaded, processed;
            int tick = 0;
            do
            {
                if (shouldStop) break;
                processed = new Bitmap((Bitmap)pbLoadedBox.Image.Clone());

                convFilter.EdgeDetectVertical(processed);

                tick++;

                if (!shouldStop)
                {
                    pbProcessedBox.Invoke((MethodInvoker)(() => pbProcessedBox.Image = processed));
                }

            } while (tick <= 100);

        }

        public void startEdgeDetectHomogenityVideoCapture(Byte value)
        {
            shouldStop = false;
            processingThread = new Thread(() => EdgeDetectHomogenityVideo(value));
            processingThread.IsBackground = true;
            processingThread.Start();
        }

        private void EdgeDetectHomogenityVideo(Byte value)
        {
            Bitmap loaded, processed;
            int tick = 0;
            do
            {
                if (shouldStop) break;
                processed = new Bitmap((Bitmap)pbLoadedBox.Image.Clone());

                convFilter.EdgeDetectHomogenity(processed, value);

                tick++;

                if (!shouldStop)
                {
                    pbProcessedBox.Invoke((MethodInvoker)(() => pbProcessedBox.Image = processed));
                }

            } while (tick <= 100);

        }

        public void startEdgeDetectDifferenceVideoCapture(Byte value)
        {
            shouldStop = false;
            processingThread = new Thread(() => EdgeDetectDifferenceVideo(value));
            processingThread.IsBackground = true;
            processingThread.Start();
        }

        private void EdgeDetectDifferenceVideo(Byte value)
        {
            Bitmap loaded, processed;
            int tick = 0;
            do
            {
                if (shouldStop) break;
                processed = new Bitmap((Bitmap)pbLoadedBox.Image.Clone());

                convFilter.EdgeDetectDifference(processed, value);

                tick++;

                if (!shouldStop)
                {
                    pbProcessedBox.Invoke((MethodInvoker)(() => pbProcessedBox.Image = processed));
                }

            } while (tick <= 100);

        }

        public void startEdgeEnhanceVideoCapture(Byte value)
        {
            shouldStop = false;
            processingThread = new Thread(() => EdgeEnhanceVideo(value));
            processingThread.IsBackground = true;
            processingThread.Start();
        }

        private void EdgeEnhanceVideo(Byte value)
        {
            Bitmap loaded, processed;
            int tick = 0;
            do
            {
                if (shouldStop) break;
                processed = new Bitmap((Bitmap)pbLoadedBox.Image.Clone());

                convFilter.EdgeEnhance(processed, value);

                tick++;

                if (!shouldStop)
                {
                    pbProcessedBox.Invoke((MethodInvoker)(() => pbProcessedBox.Image = processed));
                }

            } while (tick <= 100);

        }

        public void stopThread()
        {

            if (processingThread != null && processingThread.IsAlive)
            {
                shouldStop = true;
                pbProcessedBox.Image = null;
            }
        }

        public void stopVideoCapture()
        {
            //devices[0].Stop();
            if (videoCapture != null)
            {
                if (videoCapture.IsRunning)
                {
                    videoCapture.SignalToStop();
                    pbLoadedBox.Image = null;
                }
            }


            if (processingThread != null && processingThread.IsAlive)
            {
                shouldStop = true;
                pbProcessedBox.Image = null;
            }
        }
    }
}
