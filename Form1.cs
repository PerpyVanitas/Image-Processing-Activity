using System.Diagnostics;
using System.Windows.Forms;

namespace ImageProcessing
{
    public partial class Form1 : Form
    {
        Bitmap loaded, processed, backgroundLoaded;
        Camera camera;
        Filters filter;
        ConvFilters convFilters;
        State state;
        enum State
        {
            None,
            Image,
            Video
        }

        public Form1()
        {
            state = State.None;
            InitializeComponent();
            this.FormClosing += MainForm_FormClosing;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            camera.stopVideoCapture();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            camera = new Camera(pbLoadedBox, pbProcessedBox, pbBackgroundBox);
            filter = new Filters();
            convFilters = new ConvFilters();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            processed.Save(saveFileDialog1.FileName);
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            camera.stopVideoCapture();

            state = State.Image;
            loaded = new Bitmap(openFileDialog1.FileName);
            pbLoadedBox.Image = loaded;
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void loadBackgroundforSubtractionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void openFileDialog2_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            backgroundLoaded = new Bitmap(openFileDialog2.FileName);
            pbBackgroundBox.Image = backgroundLoaded;
        }


        private void turnOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            state = State.Video;
            camera.startVideoCapture();
        }

        private void turnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            camera.stopVideoCapture();
        }

        private void basicCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (state == State.Image)
            {
                processed = filter.basicCopy(loaded);
                pbProcessedBox.Image = processed;
            }
            else if (state == State.Video)
            {
                camera.stopThread();
                Thread.Sleep(500);
                camera.startBasicCopyVideoCapture();
            }
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (state == State.Image)
            {
                processed = filter.grayScale(loaded);
                pbProcessedBox.Image = processed;
            }
            else if (state == State.Video)
            {
                camera.stopThread();
                Thread.Sleep(500);
                camera.startGrayScaleVideoCapture();
            }
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (state == State.Image)
            {
                processed = filter.sepiaScale(loaded);
                pbProcessedBox.Image = processed;
            }
            else if (state == State.Video)
            {
                camera.stopThread();
                Thread.Sleep(500);
                camera.startSepiaScaleVideoCapture();
            }
        }

        private void colorInversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (state == State.Image)
            {
                processed = filter.colorInverse(loaded);
                pbProcessedBox.Image = processed;
            }
            else if (state == State.Video)
            {
                camera.stopThread();
                Thread.Sleep(500);
                camera.startColorInverseVideoCapture();
            }
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (state == State.Image)
            {
                processed = filter.histogram(loaded);
                pbProcessedBox.Image = processed;
            }
            else if (state == State.Video)
            {
                camera.stopThread();
                Thread.Sleep(500);
                camera.startHistogramVideoCapture();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (state == State.Image)
            {
                processed = filter.subtract(loaded, backgroundLoaded);
                pbProcessedBox.Image = processed;
            }
            else if (state == State.Video)
            {
                camera.stopThread();
                Thread.Sleep(500);
                camera.startSubtractionVideoCapture();
            }

        }

        private void subtractionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (state == State.Image)
            {
                processed = filter.subtract(loaded, backgroundLoaded);
                pbProcessedBox.Image = processed;
            }
            else if (state == State.Video)
            {
                camera.stopThread();
                Thread.Sleep(500);
                camera.startSubtractionVideoCapture();
            }
        }

        private void tbFilterBar_Scroll(object sender, EventArgs e)
        {

        }

        private void smoothingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (state == State.Image)
            {
                processed = new Bitmap(pbLoadedBox.Image);
                convFilters.Smooth(processed, 1);
                pbProcessedBox.Image = processed;
            }
            else if (state == State.Video)
            {
                camera.stopThread();
                Thread.Sleep(500);
                camera.startSmoothVideoCapture();
            }
        }

        private void gaussianBlurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (state == State.Image)
            {
                processed = new Bitmap(pbLoadedBox.Image);
                convFilters.GaussianBlur(processed, 4);
                pbProcessedBox.Image = processed;
            }
            else if (state == State.Video)
            {
                camera.stopThread();
                Thread.Sleep(500);
                camera.startGaussianVideoCapture();
            }
        }

        private void meanRemovalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (state == State.Image)
            {
                processed = new Bitmap(pbLoadedBox.Image);
                convFilters.MeanRemoval(processed, 9);
                pbProcessedBox.Image = processed;
            }
            else if (state == State.Video)
            {
                camera.stopThread();
                Thread.Sleep(500);
                camera.startMeanRemovalVideoCapture();
            }
        }

        private void sharpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (state == State.Image)
            {
                processed = new Bitmap(pbLoadedBox.Image);
                convFilters.Sharpen(processed, 11);
                pbProcessedBox.Image = processed;
            }
            else if (state == State.Video)
            {
                camera.stopThread();
                Thread.Sleep(500);
                camera.startSharpenVideoCapture();
            }
        }

        private void embossToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (state == State.Image)
            {
                processed = new Bitmap(pbLoadedBox.Image);
                convFilters.EmbossLaplacian(processed);
                pbProcessedBox.Image = processed;
            }
            else if (state == State.Video)
            {
                camera.stopThread();
                Thread.Sleep(500);
                camera.startEmbossVideoCapture();
            }
        }

        private void edgeDetectQuickToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (state == State.Image)
            {
                processed = new Bitmap(pbLoadedBox.Image);
                convFilters.EdgeDetectQuick(processed);
                pbProcessedBox.Image = processed;
            }
            else if (state == State.Video)
            {
                camera.stopThread();
                Thread.Sleep(500);
                camera.startEdgeDetectQuickVideoCapture();
            }
        }

        private void kirshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (state == State.Image)
            {
                processed = new Bitmap(pbLoadedBox.Image);
                convFilters.EdgeDetectConvolution(processed, 1, (Byte)tbFilterBar.Value);
                pbProcessedBox.Image = processed;
            }
            else if (state == State.Video)
            {
                camera.stopThread();
                Thread.Sleep(500);
                camera.startKirshVideoCapture((Byte)tbFilterBar.Value);
            }
        }

        private void prewittToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (state == State.Image)
            {
                processed = new Bitmap(pbLoadedBox.Image);
                convFilters.EdgeDetectConvolution(processed, 2, (Byte)tbFilterBar.Value);
                pbProcessedBox.Image = processed;
            }
            else if (state == State.Video)
            {
                camera.stopThread();
                Thread.Sleep(500);
                camera.startPrewittVideoCapture((Byte)tbFilterBar.Value);
            }
        }

        private void sobelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (state == State.Image)
            {
                processed = new Bitmap(pbLoadedBox.Image);
                convFilters.EdgeDetectConvolution(processed, 3, (Byte)tbFilterBar.Value);
                pbProcessedBox.Image = processed;
            }
            else if (state == State.Video)
            {
                camera.stopThread();
                Thread.Sleep(500);
                camera.startSobelVideoCapture((Byte)tbFilterBar.Value);
            }
        }

        private void edgeDetectHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (state == State.Image)
            {
                processed = new Bitmap(pbLoadedBox.Image);
                convFilters.EdgeDetectHorizontal(processed);
                pbProcessedBox.Image = processed;
            }
            else if (state == State.Video)
            {
                camera.stopThread();
                Thread.Sleep(500);
                camera.startEdgeDetectHorizontalVideoCapture();
            }
        }

        private void edgeDetectVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (state == State.Image)
            {
                processed = new Bitmap(pbLoadedBox.Image);
                convFilters.EdgeDetectVertical(processed);
                pbProcessedBox.Image = processed;
            }
            else if (state == State.Video)
            {
                camera.stopThread();
                Thread.Sleep(500);
                camera.startEdgeDetectVerticalVideoCapture();
            }
        }

        private void edgeDetectHomogenityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (state == State.Image)
            {
                processed = new Bitmap(pbLoadedBox.Image);
                convFilters.EdgeDetectHomogenity(processed, (Byte)tbFilterBar.Value);
                pbProcessedBox.Image = processed;
            }
            else if (state == State.Video)
            {
                camera.stopThread();
                Thread.Sleep(500);
                camera.startEdgeDetectHomogenityVideoCapture((Byte)tbFilterBar.Value);
            }
        }

        private void edgeDetectDifferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (state == State.Image)
            {
                processed = new Bitmap(pbLoadedBox.Image);
                convFilters.EdgeDetectDifference(processed, (Byte)tbFilterBar.Value);
                pbProcessedBox.Image = processed;
            }
            else if (state == State.Video)
            {
                camera.stopThread();
                Thread.Sleep(500);
                camera.startEdgeDetectDifferenceVideoCapture((Byte)tbFilterBar.Value);
            }
        }

        private void edgeEnhanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (state == State.Image)
            {
                processed = new Bitmap(pbLoadedBox.Image);
                convFilters.EdgeEnhance(processed, (Byte)tbFilterBar.Value);
                pbProcessedBox.Image = processed;
            }
            else if (state == State.Video)
            {
                camera.stopThread();
                Thread.Sleep(500);
                camera.startEdgeEnhanceVideoCapture((Byte)tbFilterBar.Value);
            }
        }
    }
}
