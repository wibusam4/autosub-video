using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using FFMediaToolkit;
using FFMediaToolkit.Decoding;
using FFMediaToolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Tesseract;

namespace AutoSub
{
    public partial class Main : Form
    {

        private string PathVideo = "";

        private MediaFile MediaFileVideo;

        private Random MyRandom;

        private int NumRandom = 0;

        private Bitmap MyBitMap;

        private Rectangle SelectedArea = Rectangle.Empty;

        private Rectangle EndArea = Rectangle.Empty;

        private bool Selecting, MyMouseDown = false;

        private Point StartPoint;

        private Point EndPoint;

        private Point StartImagePoint;

        private Point EndImagePoint;

        private bool IsGray;

        private bool IsNoise;

        private bool IsThreshold;

        private bool IsDilation;
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            panelVideo.Enabled = false;
            CheckBoxGray.Enabled = false;
            CheckBoxNoise.Enabled = false;
            CheckBoxThreshold.Enabled = false;
            btnGetRectangle.Enabled = false;
            try
            {
                FFmpegLoader.FFmpegPath = Environment.CurrentDirectory + "\\lib";
            }
            catch (Exception)
            {
                MessageBox.Show("Hệ thống bị thiếu file cài đặt, vui lòng thử lại sau!", "Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Environment.Exit(0);
            }
        }

        private void btnMiniSize_Click(object sender, EventArgs e)
        {
            base.WindowState = FormWindowState.Minimized;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnGetPathVideo_Click(object sender, EventArgs e)
        {
            GetVideo();
        }

        private void btnGetRectangle_Click(object sender, EventArgs e)
        {
            Selecting = true;
        }

        private void TrackBarVideo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds((double)TrackBarVideo.Value);
                lblTimeSpan.Text = string.Concat(new string[]
                {
                timeSpan.Hours.ToString(),
                ":",
                timeSpan.Minutes.ToString(),
                ":",
                timeSpan.Seconds.ToString()
                });
                ImageData frame = MediaFileVideo.Video.GetFrame(TimeSpan.FromSeconds((double)TrackBarVideo.Value));
                MyBitMap = new Bitmap(frame.ToBitmap());
                PictureBoxMain.Image = MyBitMap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnGetSub_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(DoGetSub))
            {
                IsBackground = true,
                Priority = ThreadPriority.Highest
            };
            thread.Start();
        }

        private void btnDemoSub_Click(object sender, EventArgs e)
        {
            Bitmap original = new Bitmap(PictureBoxDemo.Image);
            Thread getSub = new Thread(()=> DemoSub(original));
            getSub.IsBackground = true;
            getSub.Start();
        }

        private void btnReloadSub_Click(object sender, EventArgs e)
        {
            subVideoBindingSource.DataSource = null;
            subVideoBindingSource.DataSource = SubVideo.ListSub;
            //GridSub.Refresh();
        }

        private void GridSub_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SubVideo getSub = (SubVideo)subVideoBindingSource.Current;
            //PictureBoxSub.Image = getSub.ImageSub;
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (Selecting)
            {
                StartPoint = PictureBoxMain.PointToClient(Cursor.Position);
                StartImagePoint = new Point((int)(StartPoint.X * 1.0 / PictureBoxMain.Width * PictureBoxMain.Image.Width),
                                            (int)(StartPoint.Y * 1.0 / PictureBoxMain.Height * PictureBoxMain.Image.Height));

                MyMouseDown = true;
            }
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (Selecting)
            {
                int num = Math.Max(StartPoint.X, e.X) - Math.Min(StartPoint.X, e.X);
                int num2 = Math.Max(StartPoint.Y, e.Y) - Math.Min(StartPoint.Y, e.Y);
                SelectedArea = new Rectangle(StartPoint.X, StartPoint.Y, num, num2);
                Refresh();
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (Selecting)
            {
                Selecting = false;
                MyMouseDown = false;
                CaculateEndPoint();
                GetImageFromRectangle();
            }
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (MyMouseDown)
            {
                using (Pen pen = new Pen(Color.FromArgb(255, 71, 87), 2f))
                {
                    e.Graphics.DrawRectangle(pen, SelectedArea);
                }
            }
        }

        private void GetVideo()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "mp4|*.mp4";
                openFileDialog.ShowDialog();
                if (openFileDialog.FileName == null || openFileDialog.FileName == "" || !File.Exists(openFileDialog.FileName))
                {
                    MessageBox.Show(this, "Vui lòng chọn đúng file video.");
                }
                else
                {
                    PathVideo = openFileDialog.FileName;
                    txtPathVideo.Text = PathVideo;
                    MediaFileVideo = MediaFile.Open(PathVideo);
                    MyRandom = new Random((int)DateTimeOffset.Now.ToUnixTimeMilliseconds());
                    NumRandom = MyRandom.Next(0, (int)MediaFileVideo.Info.Duration.TotalSeconds);
                    ImageData frame = MediaFileVideo.Video.GetFrame(TimeSpan.FromSeconds((double)NumRandom));
                    MyBitMap = new Bitmap(frame.ToBitmap());
                    PictureBoxMain.Image = MyBitMap;
                    panelVideo.Enabled = true;
                    CheckBoxGray.Enabled = true;
                    CheckBoxNoise.Enabled = true;
                    CheckBoxThreshold.Enabled = true;
                    btnGetRectangle.Enabled = true;
                    TrackBarVideo.Maximum = (int)MediaFileVideo.Info.Duration.TotalSeconds - 1;
                    //ProgessGetSub.Maximum = TrackBarVideo.Maximum;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void CaculateEndPoint()
        {
            EndPoint = PictureBoxMain.PointToClient(Cursor.Position);
            int x = (int)(EndPoint.X * 1.0 / PictureBoxMain.Width * PictureBoxMain.Image.Width);
            int y = (int)(EndPoint.Y * 1.0 / PictureBoxMain.Height * PictureBoxMain.Image.Height);
            EndImagePoint = new Point(x, y);
            int xArea = Math.Min(StartImagePoint.X, EndImagePoint.X);
            int yArea = Math.Min(StartImagePoint.Y, EndImagePoint.Y);
            int width = Math.Abs(StartImagePoint.X - EndImagePoint.X);
            int height = Math.Abs(StartImagePoint.Y - EndImagePoint.Y);
            EndArea = new Rectangle(xArea, yArea, width, height);
        }

        private void DoGetSub()
        {
            int num = 0;
            while ((double)num < (this.MediaFileVideo.Video.Info.Duration.TotalSeconds - 1.0) * 1000.0)
            {
                TimeSpan timeSpan = TimeSpan.FromMilliseconds((double)num);
                ImageData frame = this.MediaFileVideo.Video.GetFrame(timeSpan);
                Bitmap original = new Bitmap(frame.ToBitmap());
                Image<Bgr, byte> image = new Bitmap(original).ToImage<Bgr, byte>();
                image.ROI = this.EndArea;
                Image<Bgr, byte> image2 = image.Copy();
                image.ROI = Rectangle.Empty;
                original = image2.ToBitmap<Bgr, byte>();
                GetSub(original, timeSpan);
                //ProgessGetSub.Value = num / 1000;
                num += 1000;
            }
            MessageBox.Show("Đã lấy xong");
            StreamWriter streamWriter = new StreamWriter(Application.StartupPath + "//subvideo.in");
            foreach(SubVideo sub in SubVideo.ListSub)
            {
                streamWriter.WriteLine(sub.Sub + " : " + sub.Time.ToString());
            }
            streamWriter.Close();
        }

        private void GetSub(Bitmap bitmap, TimeSpan timeSpan)
        {
            try
            {
                this.PictureBoxDemo.Image = bitmap;
                TesseractEngine engine = new TesseractEngine(Environment.CurrentDirectory + "/tessdata", "chi_sim", EngineMode.Default);
                Page page = engine.Process(bitmap, new PageSegMode?(PageSegMode.Auto));
                string text = page.GetText();
                if (string.IsNullOrEmpty(text))
                {
                    return;
                }
                if (SubVideo.ListSub.Count == 0)
                {
                    SubVideo.ListSub.Add(new SubVideo(0,text, timeSpan, "loading", bitmap));
                    return;
                }
                SubVideo.ListSub.Add(new SubVideo(SubVideo.ListSub.Count-1, text, timeSpan, "loading", bitmap));
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckBoxGray_CheckedChanged(object sender, EventArgs e)
        {
            IsGray = CheckBoxGray.Checked;
            GetImageFromRectangle();
        }

        private void CheckBoxNoise_CheckedChanged(object sender, EventArgs e)
        {
            IsNoise = CheckBoxNoise.Checked;
            GetImageFromRectangle();
        }

        private void CheckBoxThreshold_CheckedChanged(object sender, EventArgs e)
        {
            IsThreshold = CheckBoxThreshold.Checked;
            GetImageFromRectangle();
        }

        private void GetImageFromRectangle()
        {
            Bitmap original = new Bitmap(PictureBoxMain.Image);
            Image<Bgr, byte> image = new Bitmap(original).ToImage<Bgr, byte>();
            if (IsGray)
            {
                Image<Gray, byte> grayImage = image.Convert<Gray, byte>();
                grayImage.ROI = this.EndArea;
                Image<Gray, byte> grayImage2 = grayImage.Copy();
                grayImage.ROI = Rectangle.Empty;
                if (IsDilation)
                {
                    Mat element = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
                    CvInvoke.Dilate(grayImage2, grayImage2, element, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
                }
                
                if (IsNoise) grayImage2 = grayImage2.SmoothGaussian(3);
                if (IsThreshold) grayImage2 = grayImage2.ThresholdBinaryInv(new Gray(200), new Gray(255));
                original = grayImage2.ToBitmap<Gray, byte>();
                PictureBoxDemo.Image = original;
                return;
            }
            image.ROI = this.EndArea;
            Image<Bgr, byte> imageNew = image.Copy();
            imageNew.ROI = Rectangle.Empty;
            PictureBoxDemo.Image = imageNew.ToBitmap();
        }

        private void btnGetSub_Click_1(object sender, EventArgs e)
        {
            Thread thread = new Thread(DoGetSub);
            thread.IsBackground = true;
            thread.Start();
        }

        private void DemoSub(Bitmap bitmap)
        {
            try
            {
                this.PictureBoxDemo.Image = bitmap;
                
                TesseractEngine engine = new TesseractEngine(Environment.CurrentDirectory + "/tessdata", "chi_sim", EngineMode.Default);
                Page page = engine.Process(bitmap, new PageSegMode?(PageSegMode.Auto));
                string text = page.GetText();
                MessageBox.Show(text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
