

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GdiBlurEffectMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using IGK.DrSStudio.Imaging.Effects;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Imaging.Menu
{
    [GdiEffectMenu("Blur", 0)]
    class GdiBlurEffectMenu : 
        GdiBitmapEffectMenu        
    {
        private BlurEffect m_beffect;
        private bool m_Edged;
        
        protected override bool PerformAction()
        {
            m_beffect = new BlurEffect(this.Radius, this.Edged);
            this.OldBitmap = this.ImageElement.Bitmap.Clone () as ICoreBitmap;
            if (m_beffect.CanApply)
            {
                if (Workbench.ConfigureWorkingObject(this, "title.gdiblurEffect".R (), true, Size2i.Empty ) == enuDialogResult.OK)
                {
                    ApplyEffect(false);
                }
                else { 
                    //restore old bitmap
                    this.Restore();
                }
            }
            return false;
        }

        

        protected override void ApplyEffect(bool temp)
        {
            this.CurrentSurface.SetCursor(Cursors.WaitCursor);
            m_beffect.ExpandEdges = this.Edged;
            this.m_beffect.Radius = this.Radius;
            Bitmap bmp = this.OldBitmap.ToGdiBitmap();
            m_beffect.ApplyToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
            this.ImageElement.SetBitmap(bmp, temp);
            this.CurrentSurface.RefreshScene();
            this.CurrentSurface.SetCursor(Cursors.Default);
        }


        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }

        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters  = base.GetParameters(parameters);
            var g = parameters.AddGroup("Default");
            g.AddItem(GetType().GetProperty("Edged"));
            g.AddTrackbar (GetType().GetProperty("Radius"), 0, 255, 0,
                (o,e)=>{
                    try
                    {
                        this.Radius = Convert.ToSingle ( e.Value);
                        this.ApplyEffect(true);
                    }
                    catch { 
                    }
                }
                );
            return parameters;
        }

        //private void ApplyTemp()
        //{
        //    Workbench.MainForm.SetCursor(Cursors.WaitCursor);
        //    Bitmap bmp = this.ImageElement.Bitmap.ToGdiBitmap();
        //    m_beffect.ApplyToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
        //    //  beffect.Validate(bmp);
        //    this.ImageElement.SetBitmap(bmp, true);
        //    this.CurrentSurface.RefreshScene();
        //    Workbench.MainForm.SetCursor(Cursors.Default);
        //}

        public bool Edged
        {
            get { return this.m_Edged; }
            set
            {
                if (this.m_Edged != value)
            {
                this.m_Edged = value;
                ApplyEffect(false);
            }
        } }
        public float Radius { get; set; }
    }
}
