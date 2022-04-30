

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXPublish.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
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
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WiXPublish.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WiXAddIn
{
    [WiXDisplayNameAttribte("Publish")]
    public class WiXPublish : WiXEntry 
    {
        private string m_Control;
        private string m_Event;
        private string m_Dialog;
        private string m_Value;
        private WiXStringElement  m_Condition;
        [WiXElement ()]
        public WiXStringElement  Condition
        {
            get { return m_Condition; }
            set
            {
                if (m_Condition != value)
                {
                    m_Condition = value;
                }
            }
        }
        [WiXAttribute ()]
        public string Value
        {
            get { return m_Value; }
            set
            {
                if (m_Value != value)
                {
                    m_Value = value;
                }
            }
        }
        [WiXAttribute()]
        public string Dialog
        {
            get { return m_Dialog; }
            set
            {
                if (m_Dialog != value)
                {
                    m_Dialog = value;
                }
            }
        }
        [WiXAttribute()]
        public string Event
        {
            get { return m_Event; }
            set
            {
                if (m_Event != value)
                {
                    m_Event = value;
                }
            }
        }
        [WiXAttribute()]
        public string Control
        {
            get { return m_Control; }
            set
            {
                if (m_Control != value)
                {
                    m_Control = value;
                }
            }
        }
        public WiXPublish()
        {
            this.Id = null;
            Dialog = "ExitDialog";
            Control="Finish" ;
            Event="DoAction" ;
            Value="LaunchApplication";
            m_Condition = new WiXStringElement();
            m_Condition.Value = "WIXUI_EXITDIALOGOPTIONALCHECKBOX = 1 and NOT Installed";
        }
    }
}

