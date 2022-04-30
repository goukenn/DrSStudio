using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.WinUI;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using IGK.ICore.WinCore;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace IGK.DrSStudio.Imaging.ImageHueGrayScaleTransform.WinUI
{
    class XHueConfigControl : UIXConfigControlBase
    {
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private Drawing2D.WinUI.IGKXBulletIntervalTrackBar c_bulletTrack;
        private Thread m_th;
        private ICoreBitmap m_source_bmp;
        private byte[] m_srcData;
        private enuHueGrayMode vm_mode;

        public float StartValue { get; set; }
        public float EndValue { get; set; }
        ///<summary>
        ///public .ctr
        ///</summary>
        public XHueConfigControl()
        {
            this.InitializeComponent();
            this.StartValue = 0;
            this.EndValue = 0;
        }

        public ImageElement ImageElement { get; internal set; }
        public ICore2DDrawingSurface Surface { get; internal set; }

        private void InitializeComponent()
        {
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.c_bulletTrack = new IGK.DrSStudio.Drawing2D.WinUI.IGKXBulletIntervalTrackBar();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(30, 285);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(63, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "preview";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "GrayScale",
            "Mask",
            "ColorMatrix"});
            this.comboBox1.Location = new System.Drawing.Point(30, 70);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(252, 21);
            this.comboBox1.TabIndex = 2;
            // 
            // igkxBulletIntervalTrackBar1
            // 
            this.c_bulletTrack.CaptionKey = null;
            this.c_bulletTrack.Location = new System.Drawing.Point(30, 30);
            this.c_bulletTrack.MaxValue = 360;
            this.c_bulletTrack.MinValue = 0;
            this.c_bulletTrack.Name = "igkxBulletIntervalTrackBar1";
            this.c_bulletTrack.Size = new System.Drawing.Size(252, 24);
            this.c_bulletTrack.TabIndex = 3;
            this.c_bulletTrack.IntervalChanged += new System.EventHandler(this.igkxBulletIntervalTrackBar1_IntervalChanged);

            // 
            // XHueConfigControl
            // 
            this.Controls.Add(this.c_bulletTrack);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.checkBox1);
            this.Name = "XHueConfigControl";
            this.Size = new System.Drawing.Size(310, 336);
            this.Load += new System.EventHandler(this.XHueConfigControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void XHueConfigControl_Load(object sender, EventArgs e)
        {
            this.comboBox1.DataSource =Enum.GetValues(typeof(enuHueGrayMode));
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            this.comboBox1.SelectedItem = enuHueGrayMode.GrayScale;
            this.comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            this.c_bulletTrack.PaintCursor += _PainCursor;
            this.Start();
        }
        private void _PainCursor(object sender, PaintEventArgs e) {
            float angle = 0;
            Color[] v_cls = new Color[] {
                    Color.Red,
                    Color.Yellow,
                    Color.Lime,
                    Color.Aqua,
                    Color.Blue,
                    Color.Magenta,
                    Color.Red
                };
            float[] v_pos = new float[7];
            for (int i = 0; i < v_pos.Length; i++)
            {
                v_pos[i] = i / 6.0f;
            }

            try
            {
                using (LinearGradientBrush br = new LinearGradientBrush(this.c_bulletTrack.ClientRectangle, Color.Empty, Color.Empty, angle))
                {
                    ColorBlend v_blend = new ColorBlend(6)
                    {
                        Colors = v_cls,
                        Positions = v_pos
                    };
                    br.InterpolationColors = v_blend;
                    br.LinearColors = v_cls;
                    br.GammaCorrection = false;
                    e.Graphics.FillRectangle(br, e.ClipRectangle);
                }
            }
            catch {
            }

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Start();
        }

        void Start() {

            if (m_th != null)
            {
                m_th.Abort();
                m_th.Join();               
            }
            m_th = new Thread(this.Apply);
            m_th.SetApartmentState(ApartmentState.STA);
            m_th.IsBackground = true;
            this.vm_mode = (enuHueGrayMode)this.comboBox1.SelectedItem;
            m_th.Start();

            while (!m_th.IsAlive) ; //wait to start

        }
        public 



        void Apply() {

            int min = this.c_bulletTrack.Interval.Min;
            int max = this.c_bulletTrack.Interval.Max;
            enuHueGrayMode v_mode = this.vm_mode;
            if (min >= max)
                return;
           // BitmapData v_lsrc = null; //bitmap lock source
            BitmapData v_ldest= null; //bitmap lock destination
            bool aborted = false;
           // Bitmap ib = null;
            Rectangle v_rc = Rectangle.Empty;
            //ICoreBitmap v_bmp = null;
            int mode = 0;


            try
            {
                if (m_source_bmp == null)
                {//get source of bitmap data
                    this.m_source_bmp = this.ImageElement.Bitmap.Clone() as ICoreBitmap;
                }
                this.m_srcData = m_source_bmp.ToData();
                mode = 1;
                v_rc = new Rectangle(0, 0, m_source_bmp.Width, m_source_bmp.Height);
                Utils.HueGrayTranform(ref m_srcData,
                    m_source_bmp.Width, m_source_bmp.Height, min, max,
                    v_mode
                    );
                mode = 2;

                var sb = this.ImageElement.Bitmap.GetGdiBitmap();
                v_ldest = sb.LockBits(v_rc, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                mode = 3;
                Marshal.Copy(m_srcData, 0, v_ldest.Scan0, m_srcData.Length);
                mode = 4;
                sb.UnlockBits(v_ldest);
                mode = 5;
                v_ldest = null;


                this.Surface.BeginInvoke(new MethodInvoker(() =>
                    {
                        this.ImageElement.SetBitmap(null, true);
                        this.Surface.Invalidate();
                    }));

            }
            catch (ThreadAbortException a)
            {
                Debug.WriteLine(a.Message);
                Debug.WriteLine("Mode " + mode);
                if (aborted)
                {
                }
                aborted = true;
            }
            catch (InvalidOperationException ex) {
                Debug.WriteLine("ex.M : " + ex.Message);
            }
            finally
            {
                if (aborted)
                {
                    if (v_ldest != null)
                    {

                    }
                    if (mode < 5)
                    {

                    }
                }
            }

        }


        //void Apply() {

        //    //HueGrayTranform(ref vdata,v_bmp.Width, v_bmp.Height, 200, 210);

        //    int min = this.igkxBulletIntervalTrackBar1.Interval.Min;
        //    int max = this.igkxBulletIntervalTrackBar1.Interval.Max;

        //    if (min >= max)
        //        return;
        //    BitmapData id = null;
        //    bool aborted = false;
        //    Bitmap ib = null;
        //    Rectangle v_rc = Rectangle.Empty;
        //    ICoreBitmap v_bmp = null;


        //    lock (m_lockedflag)
        //    {
                
        //        try
        //        {
        //            if (m_source_bmp == null)
        //            {
        //                this.m_source_bmp = this.ImageElement.Bitmap.Clone() as ICoreBitmap;
        //            }

        //            v_bmp = this.m_source_bmp.Clone() as ICoreBitmap;                    
        //            v_rc = new Rectangle(0, 0, v_bmp.Width, v_bmp.Height);
        //            if (v_data == null)
        //            {
        //                v_data = v_bmp.ToData();
        //            }
        //            else
        //            {
                        
        //                ib = this.m_source_bmp.GetGdiBitmap();
        //                id = ib.LockBits(
        //                    v_rc,
        //                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                        
        //                Marshal.Copy(id.Scan0, v_data, 0, v_data.Length);// 0

        //                ib.UnlockBits(id);
        //                id = null;
                        
        //            }

        //            Utils.HueGrayTranform(ref v_data, v_bmp.Width, v_bmp.Height, min, max);
        //        }
        //        catch (ThreadAbortException ex)
        //        {
        //            aborted = true;
        //        }
        //        finally {

        //            if (aborted && (id != null) && (ib.PixelFormat != PixelFormat.Undefined)){

        //                ib.UnlockBits(id);
        //            }
        //        }
        //        if (aborted)
        //            return;

        //        aborted = false;
        //        //copy to output
        //        var sb = this.ImageElement.Bitmap.GetGdiBitmap();
        //        BitmapData data = null;
              
        //        lock (sb) {
        //            try
        //            {
        //                data = sb.LockBits(v_rc, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                        
        //                Marshal.Copy(v_data, 0, data.Scan0, v_data.Length);

        //                sb.UnlockBits(data);
        //                data = null;
                        
        //                v_bmp.Dispose();
                        
        //            }
        //            catch (ThreadAbortException ex)
        //            {
        //                aborted = true;
        //            }
        //            catch (Exception gex) {
        //            }
        //            finally{
        //                if (aborted && (data != null)) {
        //                    sb.UnlockBits(data);
        //                }
        //            }
        //        }
        //        //var v_bmp = this.m_source_bmp.Clone() as ICoreBitmap;

        //        //var vdata = v_bmp.ToData();


        //        //Utils.HueGrayTranform(ref vdata, v_bmp.Width, v_bmp.Height, min, max);
        //        //Bitmap bmp = new Bitmap(v_bmp.Width, v_bmp.Height, PixelFormat.Format32bppArgb);
        //        //var data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

        //        //Marshal.Copy(vdata, 0, data.Scan0, vdata.Length);

        //        //bmp.UnlockBits(data);

        //        //v_bmp.Dispose();

        //        //var obmp = WinCoreBitmap.Create(bmp);
        //        if (this.Surface.InvokeRequired)
        //        {
        //            this.Surface.BeginInvoke(new MethodInvoker(() =>
        //            {
        //                this.ImageElement.SetBitmap(null, true);
        //            this.Surface.Invalidate();
        //            }));
        //        }
        //        else
        //        {
        //            //this.ImageElement.SetBitmap(obmp, false);
        //            this.Surface.Invalidate();
        //        }
        //    }
        //}

     

        private void igkxBulletIntervalTrackBar1_IntervalChanged(object sender, EventArgs e)
        {
            this.Start();
        }
    }
}
