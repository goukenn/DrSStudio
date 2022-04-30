

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: NoiseEffect.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:NoiseEffect.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
namespace IGK.DrSStudio.Drawing2D.NoiseEffect
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D.Effects.NoiseEffect;
    using IGK.DrSStudio.WinUI;
    /// <summary>
    /// represent a noize effect base
    /// </summary>
    public class NoiseEffect : ImageEffectBase, IDisposable 
    {
        private bool m_Preview;
        /// <summary>
        /// get or set the preview action
        /// </summary>
        public bool Preview
        {
            get { return m_Preview; }
            set
            {
                if (m_Preview != value)
                {
                    m_Preview = value;
                    this.OnPropertyChanged (CoreWorkingObjectPropertyChangedEventArgs.Definition );
                }
            }
        }
        public override string EffectName
        {
            get { return "NoizeEffect"; }
        }
        private enuNoiseType m_NoiseType;
        private byte m_RandomMax;
        private byte m_Offset;
        public byte Offset
        {
            get { return m_Offset; }
            set
            {
                if (m_Offset != value)
                {
                    m_Offset = value;
                    this.OnPropertyChanged (CoreWorkingObjectPropertyChangedEventArgs.Definition );
                }
            }
        }
        public byte RandomMax
        {
            get { return m_RandomMax; }
            set
            {
                if (m_RandomMax != value)
                {
                    m_RandomMax = value;
                    this.OnPropertyChanged (CoreWorkingObjectPropertyChangedEventArgs.Definition );
                }
            }
        }
        public enuNoiseType NoiseType
        {
            get { return m_NoiseType; }
            set
            {
                if (m_NoiseType != value)
                {
                    m_NoiseType = value;
                    this.OnPropertyChanged (CoreWorkingObjectPropertyChangedEventArgs.Definition );
                }
            }
        }
        public NoiseEffect()
        {
            this.m_RandomMax = 200;
            this.m_Preview = true;
            this.m_Offset = 0;
            this.m_NoiseType = enuNoiseType.AddColor ;
            this.PropertyChanged += new CoreWorkingObjectPropertyChangedEventHandler(NoiseEffect_PropertyChanged);
        }
        public override void Dispose()
        {
            if (_newBitmap != null)
            {
                _newBitmap.Dispose();
            }
            base.Dispose();
        }
        void NoiseEffect_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            this._genTab();
            this.ApplyEffect(true);
        }
        Bitmap _newBitmap;
        bool _genB;
        void _genTab()
        {
            if (this.OldBitmap == null)
                return;
            Bitmap bmp = this.OldBitmap.Clone() as Bitmap;
            Rectangle v_rc = new System.Drawing.Rectangle(System.Drawing.Point.Empty, this.OldBitmap.Size);
            BitmapData v_data = bmp.LockBits(v_rc, System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            byte[] v_tab = new byte[v_data.Stride * v_data.Height];
            Marshal.Copy(v_data.Scan0, v_tab, 0, v_tab.Length);
            switch (this.m_NoiseType)
            {
                case enuNoiseType.Gray:
                    BuildGray(v_tab, v_data.Stride, bmp.Width, bmp.Height);
                    break;
                case enuNoiseType.AddGray:
                    BuildAddGray(v_tab, v_data.Stride, bmp.Width, bmp.Height);
                    break;
                case enuNoiseType.AddColor:
                    BuildAddColor(v_tab, v_data.Stride, bmp.Width, bmp.Height);
                    break;
            }
            Marshal.Copy(v_tab, 0, v_data.Scan0, v_tab.Length);
            bmp.UnlockBits(v_data);
            _newBitmap = bmp;
            _genB = true;
        }
        protected override void ApplyEffect(bool temp)
        {
            if (((_newBitmap != null) && (temp && Preview)) || !temp)
            {
                if (_genB && !temp)
                {
                    _newBitmap = this.ImageElement.Bitmap.Clone() as Bitmap;
                }
                this.ImageElement.SetBitmap(_newBitmap, temp);
                this.ImageElement.InvalidateDesignSurface(true);
                this._newBitmap = null;
            }
        }
        private void BuildAddColor(Byte[] v_tab, int stride, int width, int height)
        {
            Random rdm = new Random();
            int offset = 0;
            byte c = 0;
            int h = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    offset = (j * stride) + (4 * i);
                    c = (byte)rdm.Next(this.m_RandomMax) ;
                    h = v_tab[offset] + c - Offset ;
                    v_tab[offset] = (byte)((h > 255) ? 255 : ((h < 0) ? 0 : h));
                    c = (byte)rdm.Next(this.m_RandomMax);
                    h = v_tab[offset + 1] + c - Offset ;
                    v_tab[offset + 1] = (byte)((h > 255) ? 255 : ((h < 0) ? 0 : h)); ;
                    c = (byte)rdm.Next(this.m_RandomMax);
                    h = v_tab[offset + 2] + c - Offset ;
                    v_tab[offset + 2] = (byte)((h > 255) ? 255 : ((h < 0) ? 0 : h)); ;
                }
            }
        }
        private void BuildAddGray(Byte[] v_tab, int stride, int width, int height)
        {
            Random rdm = new Random();
            int offset = 0;
            byte c = 0;
            int h = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    offset = (j * stride) + (4 * i);
                    c = (byte)rdm.Next(this.m_RandomMax);
                    h = v_tab[offset] + c - Offset ;
                    v_tab[offset] =(byte) ((h > 255) ? 255 : ((h < 0)? 0 : h ) );
                    h = v_tab[offset+1] + c - Offset ;
                    v_tab[offset + 1] = (byte)((h > 255) ? 255 : ((h < 0) ? 0 : h)); 
                    h = v_tab[offset+2] + c - Offset ;
                    v_tab[offset + 2] = (byte)((h > 255) ? 255 : ((h < 0) ? 0 : h)); 
                }
            }
        }
        private void BuildGray(Byte[] v_tab,int stride, int width,int height)
        {
            Random rdm = new Random ();
 	        int offset = 0;
            byte c  =0;
            int h = 0;
            for (int i = 0; i < width ; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    offset = (j * stride )+ (4 * i);
                    c = (byte)rdm.Next(this.m_RandomMax);
                    h = c - Offset;
                    c = (byte)((h > 255) ? 255 : ((h < 0) ? 0 : h));
                    v_tab[offset ] = c;
                    v_tab[offset  + 1] = c;
                    v_tab[offset  + 2] = c;
                    v_tab[offset  + 3] = 255;
                }
            }
}
        public override DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            ICoreParameterConfigCollections  cp  = base.GetParameters(parameters);
            var g = cp.AddGroup("Definition");
            Type t  = GetType ();
            g.AddItem(t.GetProperty("NoiseType"));
            g.AddTrackbar(t.GetProperty("RandomMax"), 0, 255, this.m_RandomMax, (object o, IGK.DrSStudio.WinUI.Configuration.CoreParameterChangedEventArgs e ) => { 
                int i =Convert.ToInt32 ( e.Value);
                if ((i >= 0) && (i <= 255)) {
                    this.RandomMax =(byte) i;
                }
            });
            g.AddTrackbar(t.GetProperty("Offset"), 0, 255, this.m_RandomMax, (object o, IGK.DrSStudio.WinUI.Configuration.CoreParameterChangedEventArgs e) =>
            {
                int i = Convert.ToInt32(e.Value);
                if ((i >= 0) && (i <= 255))
                {
                    this.Offset  = (byte)i;
                }
            });
            g = cp.AddGroup("Preview");
            g.AddItem(t.GetProperty("Preview"));
            return cp;
        }
    }
}

