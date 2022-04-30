

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PickSCGlobalTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:PickSCGlobalTool.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Drawing2D;
using IGK.DrSStudio.PickSystemColorAddin.WinUI;
using IGK.ICore.Tools;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.PickSystemColorAddin.Tools
{
    [CoreTools("Tool.PickGlobalColorTool")]
    class PickSCGlobalTool : CoreToolBase
    {
        private static PickSCGlobalTool sm_instance;
        private Form[] pickeck;
        private Vector2i m_Position;
        private enuPickMode m_PickMode;
        private Rectanglei m_SelectedRectangle;
        /// <summary>
        /// get or set the selected rectangle
        /// </summary>
        public Rectanglei SelectedRectangle
        {
            get { return m_SelectedRectangle; }
            set
            {
                if (m_SelectedRectangle != value)
                {
                    m_SelectedRectangle = value;
                }
            }
        }
        /// <summary>
        /// get the picking mode
        /// </summary>
        public enuPickMode PickMode
        {
            get { return m_PickMode; }
        }
        public Vector2i Position
        {
            get { return m_Position; }
            set
            {
                if (m_Position != value)
                {
                    m_Position = value;
                }
            }
        }
        private PickSCGlobalTool()
        {
        }
        public static PickSCGlobalTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static PickSCGlobalTool()
        {
            sm_instance = new PickSCGlobalTool();
        }
        internal void ClosePick(Form picker)
        {
            foreach (Form item in this.pickeck)
            {
                item.Close();
            }
            Application.DoEvents();
            switch (this.PickMode)
            {
                case enuPickMode.Image:
                    //pick Color from screen
                    if (this.CurrentSurface is ICoreWorkingInsertItemSurface)
                    {
                        if (!this.m_SelectedRectangle.IsEmpty)
                        {
                            int w = this.m_SelectedRectangle.Width;
                            int h = this.m_SelectedRectangle.Height;
                            using (Bitmap bmp = new Bitmap(w, h))
                            {
                                using (Graphics g = Graphics.FromImage(bmp))
                                {
                                    g.CopyFromScreen(
                                        this.m_SelectedRectangle.X,
                                        this.m_SelectedRectangle.Y,
                                        0,0, 
                                        bmp.Size);
                                }
                                ImageElement v_img = ImageElement.CreateFromBitmap(bmp.ToCoreBitmap());
                                if (v_img != null)
                                    (this.CurrentSurface as ICoreWorkingInsertItemSurface).Insert(v_img);
                            }
                        }
                    }
                    break;
                case enuPickMode.Color:                    
                default:
                    //pick Color from screen
                    if (this.CurrentSurface is ICoreWorkingColorSurface)
                    {
                        using (Bitmap bmp = new Bitmap(1, 1))
                        {
                            using (Graphics g = Graphics.FromImage(bmp))
                            {
                                g.CopyFromScreen(Position.X , Position.Y,
                                    0,0, new Size(1, 1));
                            }
                            Colorf cl = bmp.GetPixel(0, 0).CoreConvertFrom<Colorf>();
                            (this.CurrentSurface as ICoreWorkingColorSurface).CurrentColor = cl;
                        }
                    }
                    break;
            }
        }
        internal  void PickColor()
        {
            this.m_PickMode = enuPickMode.Color;
            List<Form> v_forms = new List<Form> ();
            Form v_form = null;
            for (int i = 0; i < Screen.AllScreens.Length; i++)
			{
                v_form = new FormPicker(Screen.AllScreens [i]);
                v_form.Owner = this.MainForm as Form;
                v_forms.Add(v_form);
                v_form.Show();
			}
            this.pickeck = v_forms.ToArray();
        }
        internal void CaptureImage()
        {
            this.m_PickMode = enuPickMode.Image;
            List<Form> v_forms = new List<Form>();
            Form v_form = null;
            for (int i = 0; i < Screen.AllScreens.Length; i++)
            {
                v_form = new ImagePickerForm(Screen.AllScreens[i]);
                v_form.Owner = this.MainForm as Form;
                v_forms.Add(v_form);
                v_form.Show();
            }
            this.pickeck = v_forms.ToArray();
        }
        internal class FormPicker : Form
        {
            private Screen screen;
            public Screen Screen { get { return this.screen; } }
            public FormPicker()
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
              //  this.TopMost = true;
                this.BackColor = Color.Black;
                this.Opacity = 0.1f;
                this.StartPosition = FormStartPosition.Manual;
                this.Click += FormPicker_Click;
            }
            void FormPicker_Click(object sender, EventArgs e)
            {
                Instance.Position = new Vector2i (Control.MousePosition.X, Control.MousePosition.Y);
                Instance.ClosePick(this);
            }
            public FormPicker(Screen screen):this()
            {                
                this.screen = screen;
                this.Bounds = screen.Bounds;
            }
        }
    }
}

