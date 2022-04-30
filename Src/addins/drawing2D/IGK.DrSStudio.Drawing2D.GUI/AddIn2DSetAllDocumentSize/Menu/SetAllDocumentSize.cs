

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SetAllDocumentSize.cs
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
file:SetAllDocumentSize.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Menu
{
    using IGK.ICore.WinCore;
using IGK.ICore.Menu;
    using IGK.ICore.Drawing2D.Menu;
    using IGK.ICore.Drawing2D.Tools;
    using IGK.ICore.Tools;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.WinUI;
    using IGK.DrSStudio.Drawing2D.Menu;
    using IGK.ICore;
    [IGKD2DDocumentMenuAttribute("SetAllDocumentProperty", 400)]
    sealed class SetAllDocumentSizeMenu : Core2DDrawingMenuBase
    {
        protected override bool IsEnabled()
        {
            return base.IsEnabled() && (this.CurrentSurface.Documents.Count > 1);
        }
        protected override bool PerformAction()
        {
            if (this.CurrentSurface.Documents.Count > 1)
            {
                DocumentSizeConfig config = new DocumentSizeConfig();
                //config.
                config.Size = new Size2i(this.CurrentSurface.CurrentDocument.Width,
                    this.CurrentSurface.CurrentDocument.Height);
                if (Workbench.ConfigureWorkingObject(config, "title.configurealldocumentsize".R(), false, Size2i.Empty) == enuDialogResult.OK)
                {
                    for (int i = 0; i < this.CurrentSurface.Documents.Count; i++)
                    {
                        this.CurrentSurface.Documents[i].SetSize(config.Size.Width, config.Size.Height);
                    }
                }
            }
            return false;
        }
        class DocumentSizeConfig : ICoreWorkingConfigurableObject
        {
            private Size2i m_Size;
            public Size2i Size
            {
                get { return m_Size; }
                set
                {
                    if (!m_Size.Equals(value))
                    {
                        m_Size = value;
                    }
                }
            }
            public enuParamConfigType GetConfigType()
            {
                return enuParamConfigType.ParameterConfig;
            }
            public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
            {
                
                var group = parameters.AddGroup("DocumentSize");
                Type t = this.GetType();
                group.AddItem("Width", "lb.Width.caption", this.m_Size.Width, enuParameterType.IntNumber, (object sender,
                    CoreParameterChangedEventArgs e) =>
                    {
                        this.m_Size.Width = global::System.Convert.ToInt32(e.Value);
                    });
                group.AddItem("Height", "lb.Height.caption", this.m_Size.Height, enuParameterType.IntNumber, (object sender,
                   CoreParameterChangedEventArgs e) =>
                   {
                       this.m_Size.Height = global::System.Convert.ToInt32(e.Value);
                   });
                return parameters;
            }
            public ICoreControl GetConfigControl()
            {
                throw new NotImplementedException();
            }
            public string Id
            {
                get { return "SetAllDocumentSize"; }
            }
        }
    }

}

