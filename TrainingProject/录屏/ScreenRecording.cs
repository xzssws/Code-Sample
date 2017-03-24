using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TrainingProject
{
    //�ڲ���
    public class AVIWriter
    {
        #region ���߰���

        //֡
        public uint FrameCount = 12;

        private const string AVIFILE32 = "AVIFIL32";
        private int _pfile = 0;
        private IntPtr _ps = new IntPtr(0);
        private IntPtr _psCompressed = new IntPtr(0);

        //֡
        public UInt32 _frameRate = 12;

        private int _count = 0;
        private UInt32 _width = 0;
        private UInt32 _stride = 0;
        private UInt32 _height = 0;

        //avi��ʶ
        private UInt32 _fccType = 1935960438; // vids

        private UInt32 _fccHandler = 808810089;// IV50
        private Bitmap _bmp;

        [DllImport(AVIFILE32)]
        private static extern void AVIFileInit();

        [DllImport(AVIFILE32)]
        private static extern int AVIFileOpenW(ref int ptr_pfile, [MarshalAs(UnmanagedType.LPWStr)]string fileName, int flags, int dummy);

        [DllImport(AVIFILE32)]
        private static extern int AVIFileCreateStream(int ptr_pfile, out IntPtr ptr_ptr_avi, ref AVISTREAMINFOW ptr_streaminfo);

        [DllImport(AVIFILE32)]
        private static extern int AVIMakeCompressedStream(out IntPtr ppsCompressed, IntPtr aviStream, ref AVICOMPRESSOPTIONS ao, int dummy);

        [DllImport(AVIFILE32)]
        private static extern int AVIStreamSetFormat(IntPtr aviStream, Int32 lPos, ref BITMAPINFOHEADER lpFormat, Int32 cbFormat);

        [DllImport(AVIFILE32)]
        unsafe private static extern int AVISaveOptions(int hwnd, UInt32 flags, int nStreams, IntPtr* ptr_ptr_avi, AVICOMPRESSOPTIONS** ao);

        [DllImport(AVIFILE32)]
        private static extern int AVIStreamWrite(IntPtr aviStream, Int32 lStart, Int32 lSamples, IntPtr lpBuffer, Int32 cbBuffer, Int32 dwFlags, Int32 dummy1, Int32 dummy2);

        [DllImport(AVIFILE32)]
        private static extern int AVIStreamRelease(IntPtr aviStream);

        [DllImport(AVIFILE32)]
        private static extern int AVIFileRelease(int pfile);

        [DllImport(AVIFILE32)]
        private static extern void AVIFileExit();

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct AVISTREAMINFOW
        {
            public UInt32 fccType;
            public UInt32 fccHandler;
            public UInt32 dwFlags;
            public UInt32 dwCaps;
            public UInt16 wPriority;
            public UInt16 wLanguage;
            public UInt32 dwScale;
            public UInt32 dwRate;
            public UInt32 dwStart;
            public UInt32 dwLength;
            public UInt32 dwInitialFrames;
            public UInt32 dwSuggestedBufferSize;
            public UInt32 dwQuality;
            public UInt32 dwSampleSize;
            public UInt32 rect_left;
            public UInt32 rect_top;
            public UInt32 rect_right;
            public UInt32 rect_bottom;
            public UInt32 dwEditCount;
            public UInt32 dwFormatChangeCount;
            public UInt16 szName0;
            public UInt16 szName1;
            public UInt16 szName2;
            public UInt16 szName3;
            public UInt16 szName4;
            public UInt16 szName5;
            public UInt16 szName6;
            public UInt16 szName7;
            public UInt16 szName8;
            public UInt16 szName9;
            public UInt16 szName10;
            public UInt16 szName11;
            public UInt16 szName12;
            public UInt16 szName13;
            public UInt16 szName14;
            public UInt16 szName15;
            public UInt16 szName16;
            public UInt16 szName17;
            public UInt16 szName18;
            public UInt16 szName19;
            public UInt16 szName20;
            public UInt16 szName21;
            public UInt16 szName22;
            public UInt16 szName23;
            public UInt16 szName24;
            public UInt16 szName25;
            public UInt16 szName26;
            public UInt16 szName27;
            public UInt16 szName28;
            public UInt16 szName29;
            public UInt16 szName30;
            public UInt16 szName31;
            public UInt16 szName32;
            public UInt16 szName33;
            public UInt16 szName34;
            public UInt16 szName35;
            public UInt16 szName36;
            public UInt16 szName37;
            public UInt16 szName38;
            public UInt16 szName39;
            public UInt16 szName40;
            public UInt16 szName41;
            public UInt16 szName42;
            public UInt16 szName43;
            public UInt16 szName44;
            public UInt16 szName45;
            public UInt16 szName46;
            public UInt16 szName47;
            public UInt16 szName48;
            public UInt16 szName49;
            public UInt16 szName50;
            public UInt16 szName51;
            public UInt16 szName52;
            public UInt16 szName53;
            public UInt16 szName54;
            public UInt16 szName55;
            public UInt16 szName56;
            public UInt16 szName57;
            public UInt16 szName58;
            public UInt16 szName59;
            public UInt16 szName60;
            public UInt16 szName61;
            public UInt16 szName62;
            public UInt16 szName63;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct AVICOMPRESSOPTIONS
        {
            public UInt32 fccType;
            public UInt32 fccHandler;
            public UInt32 dwKeyFrameEvery;

            public UInt32 dwQuality;
            public UInt32 dwBytesPerSecond;

            public UInt32 dwFlags;
            public IntPtr lpFormat;
            public UInt32 cbFormat;
            public IntPtr lpParms;
            public UInt32 cbParms;
            public UInt32 dwInterleaveEvery;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BITMAPINFOHEADER
        {
            public UInt32 biSize;
            public Int32 biWidth;
            public Int32 biHeight;
            public Int16 biPlanes;
            public Int16 biBitCount;
            public UInt32 biCompression;
            public UInt32 biSizeImage;
            public Int32 biXPelsPerMeter;
            public Int32 biYPelsPerMeter;
            public UInt32 biClrUsed;
            public UInt32 biClrImportant;
        }

        #endregion ���߰���

        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="fileName">�ļ�����</param>
        /// <param name="frameRate">֡����</param>
        /// <param name="width">���</param>
        /// <param name="height">�߶�</param>
        /// <returns>����ͼƬ</returns>
        public Bitmap Create(string fileName, UInt32 frameRate, int width, int height)
        {
            _frameRate = frameRate;
            _width = (UInt32)width;
            _height = (UInt32)height;
            _bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            //����Ϊ24λλͼ
            BitmapData bmpDat = _bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            _stride = (UInt32)bmpDat.Stride;
            _bmp.UnlockBits(bmpDat);
            AVIFileInit();
            int hr = AVIFileOpenW(ref _pfile, fileName, 4097, 0);
            if (hr != 0)
            {
                throw new AVIException("��������!");
            }

            CreateStream();
            SetOptions();
            return _bmp;
        }

        /// <summary>
        /// ���֡
        /// </summary>
        public void AddFrame()
        {
            BitmapData bmpDat = _bmp.LockBits(new Rectangle(0, 0, (int)_width, (int)_height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb); int hr = AVIStreamWrite(_psCompressed, _count, 1, bmpDat.Scan0, (Int32)(_stride * _height), 0, 0, 0);
            if (hr != 0) throw new AVIException("AVIStreamWrite");
            _bmp.UnlockBits(bmpDat);
            _bmp.Dispose();
            GC.SuppressFinalize(bmpDat);
            GC.Collect();
            _count++;
        }

        /// <summary>
        /// ����֡
        /// </summary>
        /// <param name="nextframe">ͼƬ</param>
        public void LoadFrame(Bitmap nextframe)
        {
            _bmp = nextframe;
        }

        /// <summary>
        /// ���ز����֡
        /// </summary>
        /// <param name="nextframe">ͼƬ</param>
        public void AddFrame(Bitmap nextframe)
        {
            _bmp = nextframe;
            AddFrame();
        }

        /// <summary>
        /// �ر�
        /// </summary>
        public void Close()
        {
            AVIStreamRelease(_ps);
            AVIStreamRelease(_psCompressed);
            AVIFileRelease(_pfile);
            AVIFileExit();
        }

        /// <summary>
        /// �������ļ�
        /// </summary>
        private void CreateStream()
        {
            AVISTREAMINFOW strhdr = new AVISTREAMINFOW();
            strhdr.fccType = _fccType;
            strhdr.fccHandler = _fccHandler;
            strhdr.dwFlags = 0;
            strhdr.dwCaps = 0;
            strhdr.wPriority = 0;
            strhdr.wLanguage = 0;
            strhdr.dwScale = 1;
            strhdr.dwRate = _frameRate;//֡
            strhdr.dwStart = 0;
            strhdr.dwLength = 0;
            strhdr.dwInitialFrames = 0;
            strhdr.dwSuggestedBufferSize = _height * _stride;
            strhdr.dwQuality = 0xffffffff;
            strhdr.dwSampleSize = 0;
            strhdr.rect_top = 0;
            strhdr.rect_left = 0;
            strhdr.rect_bottom = _height;
            strhdr.rect_right = _width;
            strhdr.dwEditCount = 0;
            strhdr.dwFormatChangeCount = 0;
            strhdr.szName0 = 0;
            strhdr.szName1 = 0;

            int hr = AVIFileCreateStream(_pfile, out _ps, ref strhdr);

            if (hr != 0)
            {
                throw new AVIException("AVIFileCreateStream");
            }
        }

        /// <summary>
        /// ѡ������
        /// </summary>
        unsafe private void SetOptions()
        {
            AVICOMPRESSOPTIONS opts = new AVICOMPRESSOPTIONS();
            opts.fccType = _fccType;
            opts.fccHandler = 1129730893;
            opts.dwQuality = 7500;
            opts.dwBytesPerSecond = 0;
            opts.dwFlags = FrameCount;//֡��
            opts.lpFormat = new IntPtr(0);
            opts.cbFormat = 0;
            opts.dwInterleaveEvery = 0;
            AVICOMPRESSOPTIONS* p = &opts;
            AVICOMPRESSOPTIONS** pp = &p;
            IntPtr x = _ps;
            IntPtr* ptr_ps = &x;
            //AVISaveOptions(0, 0, 1, ptr_ps, pp);
            int hr = AVIMakeCompressedStream(out _psCompressed, _ps, ref opts, 0);
            if (hr != 0)
            {
                throw new AVIException("AVIMakeCompressedStream");
            }
            BITMAPINFOHEADER bi = new BITMAPINFOHEADER();
            bi.biSize = 40;
            bi.biWidth = (Int32)_width;
            bi.biHeight = (Int32)_height;
            bi.biPlanes = 1;
            bi.biBitCount = 24;
            bi.biCompression = 0;
            bi.biSizeImage = _stride * _height;
            bi.biXPelsPerMeter = 0;
            bi.biYPelsPerMeter = 0;
            bi.biClrUsed = 0;
            bi.biClrImportant = 0;
            hr = AVIStreamSetFormat(_psCompressed, 0, ref bi, 40);
            if (hr != 0)
            {
                throw new AVIException("AVIStreamSetFormat", hr);
            }
        }
    }

    //�ڲ��첽��������
    public class ParamInfo
    {
        public string saveFile { get; set; }

        public string[] imageFiles { get; set; }

        public Image[] images { get; set; }

        public List<Image> imglist { get; set; }

        public uint frame { get; set; }

        public int width { get; set; }

        public int height { get; set; }
    }

    //�ڲ��쳣��
    public class AVIException : ApplicationException
    {
        public AVIException(string s)
            : base(s)
        {
        }

        public AVIException(string s, Int32 hr)
            : base(s)
        {
            if (hr == AVIERR_BADPARAM)
            {
                err_msg = "AVIERR_BADPARAM";
            }
            else
            {
                err_msg = "unknown";
            }
        }

        public string ErrMsg()
        {
            return err_msg;
        }

        private const Int32 AVIERR_BADPARAM = -2147205018;
        private string err_msg;
    }

    public class ScreenRecording
    {
        public ScreenRecording()
        {
            Load();
        }

        public ScreenRecording(PictureBox p)
            : this()
        {
            this.pb = p;
        }

        #region ����API

        private int _X, _Y;

        [StructLayout(LayoutKind.Sequential)]
        private struct ICONINFO
        {
            public bool fIcon;
            public Int32 xHotspot;
            public Int32 yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CURSORINFO
        {
            public Int32 cbSize;
            public Int32 flags;
            public IntPtr hCursor;
            public Point ptScreenPos;
        }

        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        private static extern int GetSystemMetrics(int mVal);

        [DllImport("user32.dll", EntryPoint = "GetCursorInfo")]
        private static extern bool GetCursorInfo(ref CURSORINFO cInfo);

        [DllImport("user32.dll", EntryPoint = "CopyIcon")]
        private static extern IntPtr CopyIcon(IntPtr hIcon);

        [DllImport("user32.dll", EntryPoint = "GetIconInfo")]
        private static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO iInfo);

        #endregion ����API

        #region ȫ���ֶ�

        //�첽�������
        private BackgroundWorker _savePathWorker;

        public BackgroundWorker SavePathWorker
        {
            get
            {
                if (_savePathWorker == null) _savePathWorker = new System.ComponentModel.BackgroundWorker();
                //��ʼ������¼�
                _savePathWorker.RunWorkerCompleted += (sender, e) =>
                {
                    if (SaveFileAsyncMethod != null) SaveFileAsyncMethod();
                    SavePathWorker.Dispose();
                };

                return _savePathWorker;
            }
            set { _savePathWorker = value; }
        }

        //���ӵ���ͼƬ
        public event Action<Image> GetImage;

        //������
        private Timer timer;

        //AVI��д��
        private AVIWriter VideoWriter;

        //AVI֡
        private Bitmap CurFrame;

        //AVI�߶�
        private int VideoHeight;

        //AVI���
        private int VideoWidth;

        //֡��
        private List<string> Frames;

        //ͼƬ����·��
        private string RootPath;

        //ȫ���Ƿ��¼���
        private bool CanCursor;

        //��������
        private int NameFlag;

        //����
        private int Tick = 20;

        //��ʾ��
        private PictureBox pb;

        //�첽����¼��ĵ���
        public event Action SaveFileAsyncMethod;

        #endregion ȫ���ֶ�

        #region �ڲ�����

        //����
        private void Load()
        {
            timer = new Timer();
            Frames = new List<string>();
            CanCursor = true;
            NameFlag = 0;
            RootPath = Application.StartupPath.ToString();
            Tick = 80;

            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = Tick;
        }

        //��ȡ��Ļ��ͼ�й��
        private Bitmap CaptureDesktop()
        {
            try
            {
                int _CX = 0, _CY = 0;
                Bitmap _Source = new Bitmap(GetSystemMetrics(0), GetSystemMetrics(1));
                using (Graphics g = Graphics.FromImage(_Source))
                {
                    g.CopyFromScreen(0, 0, 0, 0, _Source.Size);
                    g.DrawImage(CaptureCursor(ref _CX, ref _CY), _CX, _CY);
                    g.Dispose();
                }
                _X = (800 - _Source.Width) / 2;
                _Y = (600 - _Source.Height) / 2;
                return _Source;
            }
            catch
            {
                return null;
            }
        }

        //��ȡû�й�����Ļ��ͼ
        private Bitmap CaptureNoCursor()
        {
            Bitmap _Source = new Bitmap(GetSystemMetrics(0), GetSystemMetrics(1));
            using (Graphics g = Graphics.FromImage(_Source))
            {
                g.CopyFromScreen(0, 0, 0, 0, _Source.Size);
                g.Dispose();
            }
            return _Source;
        }

        //��ȡ���
        private Bitmap CaptureCursor(ref int _CX, ref int _CY)
        {
            IntPtr _Icon;
            CURSORINFO _CursorInfo = new CURSORINFO();
            ICONINFO _IconInfo;
            _CursorInfo.cbSize = Marshal.SizeOf(_CursorInfo);
            if (GetCursorInfo(ref _CursorInfo))
            {
                if (_CursorInfo.flags == 0x00000001)
                {
                    _Icon = CopyIcon(_CursorInfo.hCursor);

                    if (GetIconInfo(_Icon, out _IconInfo))
                    {
                        _CX = _CursorInfo.ptScreenPos.X - _IconInfo.xHotspot;
                        _CY = _CursorInfo.ptScreenPos.Y - _IconInfo.yHotspot;
                        return Icon.FromHandle(_Icon).ToBitmap();
                    }
                }
            }
            return null;
        }

        //��ʱ���  ����ת��Ϊ�߳�
        private void TimerTick()
        {
            GC.Collect();
            //����ͼ��
            Bitmap tmp;
            //�����Ƿ���ʾͼ���ͼ
            tmp = CanCursor ? CaptureDesktop() : CaptureNoCursor();
            if (NameFlag == 0)
            {
                VideoWidth = tmp.Width;
                VideoHeight = tmp.Height;
            }
            //ִ��ʱ��
            if (GetImage != null) GetImage(tmp);

            #region TODO ���Լ������¼���

            //��ʾ¼��
            if (pb != null) pb.Image = tmp;
            //���屣��·��
            string picpath = RootPath + "\\" + NameFlag + ".bmp";

            //����ͼƬ�����ش���
            tmp.Save(picpath);
            //��֡�������֡��·��
            Frames.Add(picpath);
            //֡��־����
            NameFlag++;

            #endregion TODO ���Լ������¼���
        }

        //��ش����¼�
        private void timer_Tick(object sender, EventArgs e)
        {
            TimerTick();
        }

        #endregion �ڲ�����

        #region ��������

        #region ��ͼ

        /// <summary>
        /// �����Ƿ���ͼƬ
        /// </summary>
        /// <param name="ShowCur">�Ƿ���ͼ��</param>
        /// <returns>��Ļ��ͼ</returns>
        public Image CreateImage(bool ShowCur)
        {
            if (ShowCur)
            {
                return CaptureDesktop();
            }
            else
            {
                return CaptureNoCursor();
            }
        }

        /// <summary>
        /// ��ȡ���ͼ��
        /// </summary>
        /// <returns></returns>
        public Image CreateCur()
        {
            int x = 0;
            int y = 0;
            Bitmap resultBit = CaptureCursor(ref x, ref y);
            Console.WriteLine("X:{0}  Y:{1}", x, y);
            return resultBit;
        }

        #endregion ��ͼ

        #region ���

        /// <summary>
        /// ��ʼ���
        /// </summary>
        public void BeginVideo()
        {
            Frames.Clear();//���֡��
            timer.Start();//��ʼ����
        }

        /// <summary>
        /// ֹͣ���
        /// </summary>
        public void EndVideo()
        {
            timer.Stop();
            NameFlag = 0;
        }

        #endregion ���

        #region ͼƬʽ����

        /// <summary>
        /// ����¼���ļ�
        /// </summary>
        /// <param name="images">ͼƬ��</param>
        /// <param name="frame">֡��</param>
        /// <param name="SavePath">�ļ�·��</param>
        /// <param name="Width">¼����</param>
        /// <param name="Height">¼��߶�</param>
        public void ExpVideo(uint frame, string SavePath, int Width, int Height, params Image[] images)
        {
            try
            {
                GC.Collect();
                VideoWriter = new AVIWriter();
                VideoWriter._frameRate = frame;
                //AVI������ͼ��Բ���С��width��height
                CurFrame = VideoWriter.Create(SavePath, 1, Width, Height);

                for (int i = 0; i < images.Length; i++)
                {
                    Bitmap cache = new Bitmap(images[i]);
                    cache.RotateFlip(RotateFlipType.Rotate180FlipX);
                    VideoWriter.LoadFrame(cache);
                    VideoWriter.AddFrame();
                    cache.Dispose();
                    images[i].Dispose();
                }

                //�رն���
                VideoWriter.Close();
                //�ͷŵ�ǰ֡
                CurFrame.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ����¼���ļ�
        /// </summary>
        /// <param name="frame">֡��</param>
        /// <param name="SavePath">�ļ�·��</param>
        /// <param name="images">ͼƬ��</param>
        /// <returns></returns>
        public bool ExpVideo(string SavePath, uint frame = 12, params Image[] images)
        {
            if (images != null && images.Length > 0)
            {
                int Width = images[0].Width;
                int Height = images[1].Height;
                ExpVideo(frame, SavePath, Width, Height, images);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ������Ƶ�ļ�Ĭ��12֡
        /// </summary>
        /// <param name="SavePath">�ļ�·��</param>
        /// <param name="images">ͼƬ����</param>
        /// <returns>�Ƿ�ɹ�</returns>
        public bool ExpVideo(string SavePath, List<Image> images)
        {
            Image[] imageList = images.ToArray();
            return ExpVideo(SavePath, 12, imageList);
            //��������
        }

        #endregion ͼƬʽ����

        #region �ļ�ʽ����

        /// <summary>
        /// ����¼��
        /// </summary>
        /// <param name="savePath">�����ַ</param>
        public void ExpVideo(string savePath)
        {
            string[] files = Frames.ToArray();
            ExpVideo(files, savePath);
        }

        public void ExpVideo(string[] ImageFiles, string SavePath, uint frame = 12)
        {
            try
            {
                GC.Collect();

                VideoWriter = new AVIWriter();
                VideoWriter._frameRate = frame;
                //avi������ͼ��Բ���С��width��height
                CurFrame = VideoWriter.Create(SavePath, 1, VideoWidth, VideoHeight);
                //����֡��
                int count = ImageFiles.Length;

                for (int i = 0; i < count; i++)
                {
                    string FileName = ImageFiles[i].ToString();
                    //���ͼ��
                    Image image = Image.FromFile(FileName);
                    Bitmap cache = new Bitmap(image);
                    //����ת��Ϊavi������෴�����Է�ת
                    cache.RotateFlip(RotateFlipType.Rotate180FlipX);
                    //����ͼ��
                    VideoWriter.LoadFrame(cache);
                    //���֡
                    VideoWriter.AddFrame();
                    cache.Dispose();
                    image.Dispose();
                    GC.Collect();
                }
                //�رն���
                VideoWriter.Close();
                //�ͷŵ�ǰ֡
                CurFrame.Dispose();
                GC.Collect();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion �ļ�ʽ����

        #region �첽����


        /// <summary>
        /// �첽���淽��
        /// </summary>
        /// <param name="SaveFile">����·��</param>
        public void BeginSaveFile(string SaveFile)
        {
            SavePathWorker.DoWork += (sender, e) =>
                ExpVideo(e.Argument.ToString());

            SavePathWorker.RunWorkerAsync(SaveFile);
        }

        /// <summary>
        /// �첽�����ļ���
        /// </summary>
        /// <param name="ImageFiles">�ļ�·������</param>
        /// <param name="SavePath">����·��</param>
        public void BeginSaveFile(string[] ImageFiles, string SavePath)
        {
            SavePathWorker.DoWork += (sender, e) =>
            {
                ParamInfo param = e.Argument as ParamInfo;
                ExpVideo(param.imageFiles, param.saveFile);
            };

            SavePathWorker.RunWorkerAsync(new ParamInfo
            {
                imageFiles = ImageFiles,
                saveFile = SavePath
            });
        }

        /// <summary>
        /// �첽������Ƶ�ļ�
        /// </summary>
        /// <param name="frame">֡��</param>
        /// <param name="SavePath">����·��</param>
        /// <param name="Width">��Ƶ���</param>
        /// <param name="Height">��Ƶ�߶�</param>
        /// <param name="images">ͼƬ����</param>
        public void BeginExpVideo(uint frame, string SavePath, int Width, int Height, params Image[] images)
        {
            SavePathWorker.DoWork += (sender, e) =>
            {
                ParamInfo param = e.Argument as ParamInfo;
                ExpVideo(param.frame, param.saveFile, param.width, param.height, param.images);
            };

            SavePathWorker.RunWorkerAsync(new ParamInfo
            {
                frame = frame,
                saveFile = SavePath,
                width = Width,
                height = Height,
                images = images
            });
        }

        /// <summary>
        /// �첽������Ƶ�ļ�
        /// </summary>
        /// <param name="SavePath">����·��</param>
        /// <param name="frame">֡��</param>
        /// <param name="images">ͼƬ����</param>
        public void BeginExpVideo(string SavePath, int frame = 12, params Image[] images)
        {
            SavePathWorker.DoWork += (sender, e) =>
            {
                ParamInfo param = e.Argument as ParamInfo;
                ExpVideo(param.saveFile, param.frame, param.images);
            };

            SavePathWorker.RunWorkerAsync(new ParamInfo { saveFile = SavePath, frame = 12, images = images });
        }

        /// <summary>
        /// �첽������Ƶ�ļ�
        /// </summary>
        /// <param name="SavePath">����·��</param>
        /// <param name="images">ͼƬ����</param>
        public void BeginExpVideo(string SavePath, List<Image> images)
        {
            SavePathWorker.DoWork += (sender, e) =>
            {
                ParamInfo param = e.Argument as ParamInfo;
                ExpVideo(param.saveFile, param.imglist);
            };

            SavePathWorker.RunWorkerAsync(new ParamInfo
            {
                saveFile = SavePath,
                imglist = images
            });
        }

        #endregion �첽����

        #endregion ��������
    }
}