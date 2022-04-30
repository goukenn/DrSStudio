

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DInsertAppendDocument.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.DrSStudio.Menu;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:IGKD2DInsertAppendDocument.cs
*/
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D.Menu.Insert
{
    [DrSStudioMenu(CoreConstant.MENU_INSERT+".AppendImageAsDocumentBlock", 0x20)]
    sealed class IGKD2DInsertAppendDocument : Core2DDrawingMenuBase
    {
        protected override bool PerformAction()
        {
            if (this.CurrentSurface.CanAddDocument)
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    string filterExtensions = CoreDecoderBase.GetFilter(CoreConstant.CAT_PICTURE);
                    ofd.Filter =  string.Format ("filter.pictures".R() +"| {0}", filterExtensions);//+ PICTURES FILES | *.bmp;*.png; *.tiff; *.gif; *.gkds; *.jpeg; *.jpg";
                    ofd.Multiselect = true ;
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        for (int i = 0; i < ofd.FileNames.Length; i++)
			            {
                            ImageElement img = ImageElement.CreateFromFile(ofd.FileNames[i]);
                            if (img == null)continue;
                            ICore2DDrawingDocument v_doc = this.CurrentSurface.CreateNewDocument();
                            v_doc.CurrentLayer.Elements.Add (img);
                            v_doc.SetSize(img.Width, img.Height);
                            this.CurrentSurface.Documents.Add (v_doc);
			            }
                    }
                }
            }
            return false;
        }
    }
}

