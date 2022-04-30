

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLImportImage.cs
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
file:GLImportImage.cs
*/
using System;
    using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace IGK.DrSStudio.GLPictureEditorAddIn.Menu.Action
{
    
using IGK.ICore;using IGK.DrSStudio.Codec;
    using IGK.DrSStudio.Drawing2D;
    [GLEditorMenu(GLEditorConstant.MENU_GL_ACTION+".ImportImage",0)]
    sealed class GLImportImage : GLEditorMenuBase 
    {
        protected override bool PerformAction()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                //ICoreCodec[] d = CoreSystem.Instance.Codecs.GetDecoders(CoreConstant.CAT_PICTURE);
                //ofd.Filter = CoreCodecBase. GetFilter(d);
                //if (ofd.ShowDialog() == DialogResult.OK)
                //{
                //    d = IGK.DrSStudio.Codec.CoreDecoderBase.GetDecodersByExtension(System.IO.Path.GetExtension(ofd.FileName));
                //    if (d.Length > 0)
                //    {
                //        ICoreBitmap bmp = (d[0] as ICoreBitmapDecoder).GetBitmap(ofd.FileName);
                //        this.CurrentSurface.Import(bmp);
                //        bmp.Dispose();
                //        return true;
                //    }
                //}
            }
            return false;
        }
    }
}

