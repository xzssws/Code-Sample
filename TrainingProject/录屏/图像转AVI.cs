using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace TrainingProject
{
    public partial class 图像转AVI : BaseForm
    {
        public 图像转AVI()
        {
            InitializeComponent();
            fs = new ScreenRecording(PicShow);
            AddCommand("截屏(有光标)", ScreenImageYo);
            AddCommand("截屏(无光标)", ScreenImageWU);
            AddCommand("截取光标", CurImage);
            AddCommand("开始录像", Begin);
            AddCommand("停止录像", Stop);
            AddCommand("保存录像", Save);
            AddCommand("测试", delegate()
            {
                List<Image> img = new List<Image>();
                for (int i = 180; i < 189; i++)
                {
                    img.Add(Image.FromFile(i + ".bmp"));
                }
                fs.ExpVideo(12, @"d:\\dd.avi", 1366, 768, img.ToArray());
            });
        }

        private ScreenRecording fs;

        private void ScreenImageYo()
        {
            PicShow.Image = fs.CreateImage(true);
        }

        private void ScreenImageWU()
        {
            PicShow.Image = fs.CreateImage(false);
        }

        private void Begin()
        {
            fs.BeginVideo();
        }

        private void Stop()
        {
            fs.EndVideo();
        }

        private void CurImage()
        {
            PicShow.Image = fs.CreateCur();
        }
        ToolStripItem tbb;
        private void Save()
        {
            SaveFileDialog ofd = new SaveFileDialog();
            fs.SaveFileAsyncMethod += new System.Action(fs_SaveFileAsyncMethod);
            if (DialogResult.OK == ofd.ShowDialog())
                fs.BeginSaveFile(ofd.FileName);
            tbb = (CommandSource as ToolStripItem);
            tbb.Enabled = false;
        }

        void fs_SaveFileAsyncMethod()
        {
            tbb.Enabled = true;
            MessageBox.Show("保存成功");
        }
    }
}