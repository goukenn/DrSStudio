

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _Paste.cs
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
file:_Paste.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
using IGK.ICore.Actions;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using IGK.ICore.Drawing2D;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D.WinUI;

namespace IGK.DrSStudio.Menu.Edit
{
    [DrSStudioMenu("File.Edit.CreateNewDocumentFromClipBoard", 4, 
        Shortcut = enuKeys.Control | enuKeys.Shift | enuKeys .V,
        ImageKey=CoreImageKeys.MENU_PASTE_GKDS)]
    class CreateNewDocumentFromClipBoardMenu : CoreApplicationMenu 
    {
        public override enuActionType ActionType
        {
            get
            {
                return enuActionType.SystemAction;
            }
        }
        protected override bool PerformAction()
        {
            string[] formats = new string[] {
                "Drawing2DElement",
            "Image",
            System.Windows.Forms.DataFormats.Bitmap,
            System.Windows.Forms.DataFormats.EnhancedMetafile ,
            System.Windows.Forms.DataFormats.MetafilePict ,
            System.Windows.Forms.DataFormats.FileDrop  
          
            };

            if (WinCoreClipBoard.Contains("igk://drawing2D/docToAppend")) {

                string o = (string)WinCoreClipBoard.GetData("igk://drawing2D/docToAppend");
                var tab = CoreXMLSerializerUtility.GetAllObjects(o);
                if (tab?.Length > 0) {

                    var d = (this.CurrentSurface as ICore2DDrawingSurface).Documents;
                    for (int i = 0; i < tab.Length; i++)
                    {
                        d.Add(tab[i] as ICore2DDrawingDocument);
                    }                   
                    return true;
                }
                //don't save the action
                return false;
            }



            if (WinCoreClipBoard.Contains(formats))
            {
                var o = WinCoreClipBoard.GetData(formats);
                var bmp = o as Bitmap;
                if (bmp != null)
                {
                    Core2DDrawingLayerDocument c = new Core2DDrawingLayerDocument(bmp.Width, bmp.Height);
                     CoreDecoder.Instance.OpenDocument(this.Workbench, c);
                    var cd = CoreApplicationManager.Application.ResourcesManager.CreateBitmap(bmp);
                    if (cd !=null )
                    {
                        ImageElement imgElement = ImageElement.CreateFromBitmap(cd);
                        c.CurrentLayer.Elements.Add(imgElement );
                    }
                     return true;
                }
                else
                {
                    var tab = WinCoreClipBoard.GetPastableItems(formats);
                    if ((tab != null) && (tab.Length > 0))
                    {
                        ImageElement imgElement = tab[0] as ImageElement;
                        if (imgElement != null)
                        {
                            Core2DDrawingLayerDocument c = new Core2DDrawingLayerDocument(imgElement.Width, imgElement.Height);
                            c.CurrentLayer.Elements.Add(imgElement);
                            CoreDecoder.Instance.OpenDocument(this.Workbench, c);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        protected override bool IsEnabled()
        {
            return true;// WinCoreClipBoard.Contains(DataFormats.Bitmap);
        }

        protected override bool IsVisible()
        {
            return true;
        }
        
    }
}

