

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WorkingItem.cs
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
file:WorkingItem.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Resources;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Tools
{
    /// <summary>
    /// represent some working item
    /// </summary>
    public class WorkingItem : ICoreGTWorkingItem
    {
        private string m_Title;
        private enuKeys m_Keys;
        private Type m_ToolType;
        private string m_CaptionKey;
        private IWorkingItemGroup m_group;
        private string m_ImageKey;
        public string ImageKey
        {
            get { return m_ImageKey; }
        }
        public WorkingItem(IWorkingItemGroup group, Type type, string captionKey, string v_imgk, enuKeys key, string defaultTitle)
        {
            this.m_group = group;
            this.m_ToolType = type;
            this.m_CaptionKey = captionKey;
            this.m_ImageKey = v_imgk;
            this.m_Keys = key ;
            this.m_Title = defaultTitle;         
        }
        public override string ToString()
        {
            return this.Title + "[" + CoreResources.GetString(this.m_Keys) + "]";
        }
        public IWorkingItemGroup Group
        {
            get { return m_group; }
        }
        public string CaptionKey
        {
            get { return m_CaptionKey; }
            set
            {
                if (m_CaptionKey != value)
                {
                    m_CaptionKey = value;
                }
            }
        }
        public Type ToolType
        {
            get { return m_ToolType; }
        }
        public enuKeys Keys
        {
            get { return m_Keys; }
            set
            {
                if (m_Keys != value)
                {
                    m_Keys = value;
                }
            }
        }
        public string Title
        {
            get { return m_Title; }
        }
        IWorkingItemGroup ICoreGTWorkingItem.Group
        {
            get { return this.m_group; }
        }
    }
}

