

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _GetBase64PictureImage.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
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
﻿using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore;
namespace IGK.DrSStudio.WebAddIn.Menu.Tools.Web
{
    [DrSStudioMenu("Tools.Web.GetPictureFromBase64String", 0x300)]
    class WebGetBase64PictureImage :
        WebDrawing2DMenuBase ,
        ICoreWorkingConfigurableObject
    {
        private string m_Text;

        public string Text
        {
            get { return m_Text; }
            set
            {
                if (m_Text != value)
                {
                    m_Text = value;
                }
            }
        }
        protected override bool PerformAction()
        {
            if (Workbench.ConfigureWorkingObject(this,
                "title.getPictureFromBase64String".R(),
                false, new Size2i (300, 400)) == enuDialogResult.OK)
            {
              var g =   WinCoreBitmapOperation.StringBase64ToBitmap(this.Text);
              WinCoreClipBoard.CopyToClipBoard(g);
              CoreMessageBox.NotifyMessage("title.notify".R(),
                    "msg.webtools.imagehavebeencopied_1".R("clipboard"));
            }
            return false;
        }

        public ICoreControl GetConfigControl()
        {
            return null;
        }

        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }

        public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            var group = parameters.AddGroup("Base64String");
            group.AddItem(GetType().GetProperty("Text"));
            return parameters;
        }
    }
}
