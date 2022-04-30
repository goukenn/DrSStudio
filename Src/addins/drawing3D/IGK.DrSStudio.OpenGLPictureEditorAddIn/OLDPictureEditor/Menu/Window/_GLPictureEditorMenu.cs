

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _GLPictureEditorMenu.cs
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
file:_GLPictureEditorMenu.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.GLPictureEditorAddIn.Menu.Window
{
    
using IGK.ICore;using IGK.DrSStudio.GLPictureEditorAddIn.WinUI;
    [IGK.DrSStudio.Menu.CoreMenu ("Window.OpenGLPictureEditor", 504)]
    public sealed class NewGLFileEditorMenu : IGK.DrSStudio.Menu.CoreApplicationMenu 
    {
        protected override bool PerformAction()
        {
            using (System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog())
            {
                ofd.Filter = fileFilter();// IGK.DrSStudio.Codec.CoreEncoder.GetFilter(CoreConstant.CAT_Picture);
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    GLEditorSurface surface = new GLEditorSurface();
                    this.Workbench.Surfaces.Add(surface);
                    this.Workbench.CurrentSurface = surface;
                    surface.LoadFile(ofd.FileName);
                }
            }
            return base.PerformAction();
        }
        string fileFilter()
        {
            StringBuilder sub = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            IGK.DrSStudio.Codec.ICoreCodec[] d = IGK.DrSStudio.Codec.CoreDecoderBase.GetDecoders();
            sb.Append(CoreSystem.GetString("MSG.AllSupportedFiles") + "|");
            bool flag = false;
            string filter = string.Empty;
            for (int i = 0; i < d.Length; i++)
            {
                if (d[i] is IGK.DrSStudio.Codec.ICoreBitmapDecoder)
                {
                    if (flag)
                    {
                        sub.Append("|");
                    }
                    filter = d[i].GetFilter();
                    sub.Append(filter);
                    sb.Append(filter.Split('|')[1]);
                    flag = true;
                }
            }
            sb.Append("|"+sub.ToString());
            return sb.ToString();
        }
    }
}

