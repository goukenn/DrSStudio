

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GdiSharpenEffect.cs
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
    [GdiEffectMenu("Sharpen", 2)]
    class GdiSharpenEffectMenu : GdiBitmapEffectMenu
    {
        private SharpenEffect m_beffect;

        public GdiSharpenEffectMenu()
        {
            this.Amount = 100;
            this.Radius = 50;
        }
        protected override bool PerformAction()
        {
            this.OldBitmap = this.ImageElement.Bitmap.Clone() as ICoreBitmap;
            m_beffect = new SharpenEffect(this.Radius, this.Amount);
            if (m_beffect.CanApply)
            {
                this.ApplyEffect(true);
                if (Workbench.ConfigureWorkingObject(this, "title.gdishapenEffect".R(), true, Size2i.Empty ) == enuDialogResult.OK)
                {
                    this.ApplyEffect(false);
                }
                else { 
                    //restore temp 
                    this.ImageElement.SetBitmap(this.OldBitmap, true);
                    this.CurrentSurface.RefreshScene();
                }
            }
            return false;
        }


        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }

        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var g = parameters.AddGroup("Default");
            g.AddTrackbar(GetType().GetProperty("Amount"), 0, 100, 0,
              (o, e) =>
              {
                  this.Amount = Convert.ToSingle(e.Value);
                  this.ApplyEffect(true);
              }
              );
            g.AddTrackbar(GetType().GetProperty("Radius"), 0, 255, 0,
                (o, e) =>
                {
                    try
                    {
                        this.Radius = Convert.ToSingle (e.Value);
                        this.ApplyEffect(true);
                    }
                    catch { 
                    }
                }
                );
            return parameters;
        }
        protected override void ApplyEffect(bool temp)
        {
            this.m_beffect.Amount = this.Amount;
            this.m_beffect.Radius = this.Radius;
            MainForm.SetCursor(Cursors.WaitCursor);
            Bitmap bmp = this.OldBitmap.ToGdiBitmap();
            m_beffect.ApplyToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
            
            this.ImageElement.SetBitmap(bmp, temp);
            this.CurrentSurface.RefreshScene();
            MainForm.SetCursor(Cursors.Default);
        }
     

        public float Amount { get; set; }
        public float Radius { get; set; }
    }
}
