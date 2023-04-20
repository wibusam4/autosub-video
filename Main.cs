using AForge.Video.FFMPEG;
using System;
using System.Windows.Forms;
namespace AutoSub
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnMiniSize_Click(object sender, EventArgs e)
        {
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnGetPathVideo_Click(object sender, EventArgs e)
        {
            GetPathVideo();
        }

        private void GetPathVideo()
        {
            using (OpenFileDialog file = new OpenFileDialog())
            {
                file.InitialDirectory = "c:\\";
                file.Filter = "mp4 files (*.mp4)|*.mp4|All files (*.*)|*.*";
                file.FilterIndex = 2;
                file.RestoreDirectory = true;

                if (file.ShowDialog() == DialogResult.OK)
                {
                    GetSub.PathVideo = file.FileName;
                    txtPathVideo.Text = file.FileName;
                }
            }
            if(!string.IsNullOrEmpty(GetSub.PathVideo))
            {
                using (var video = new VideoFileReader())
                {
                    video.Open(GetSub.PathVideo);

                    // Lấy số khung hình trên mỗi giây (frame rate) của video
                    double frameRate = video.FrameRate;

                    MessageBox.Show(frameRate.ToString());

                    video.Close();
                }
            }

            

        }
    }
}
