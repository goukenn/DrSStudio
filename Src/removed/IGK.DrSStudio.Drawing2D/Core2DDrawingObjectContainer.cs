

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingObjectContainer.cs
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
file:Core2DDrawingObjectContainer.cs
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D
{
    /// <summary>
    /// represent id element container a base class
    /// </summary>
    public class Core2DDrawingObjectContainer : Core2DDrawingObjectBase, ICoreWorkingObjectIdElementContainer 
    {
        private ICoreWorkingObjectIdManager m_idManager;
        public Core2DDrawingObjectContainer():base()
        {
            this.m_idManager = CreateIdManager();
        }
        protected virtual ICoreWorkingObjectIdManager CreateIdManager()
        {
            return new CoreWorkingObjectIdManagerBase ();
        }
        [Browsable(false)]
        public virtual ICoreWorkingObjectIdManager IdManager
        {
            get
            {
                //if (this.Parent is ICoreWorkingObjectIdElementContainer)
                //    return (this.Parent as ICoreWorkingObjectIdElementContainer).IdManager;
                return m_idManager;
            }
            set
            {
                if ((this.m_idManager != value) && (value!=null))
                {
                    this.m_idManager.Unregister(this);
                    this.m_idManager = value;
                    value.Register(this);
                    OnIdManagerChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler IdManagerChanged;
        ///<summary>
        ///raise the EventName 
        ///</summary>
        protected virtual void OnIdManagerChanged(EventArgs e)
        {
            if (IdManagerChanged != null)
                IdManagerChanged(this, e);
        }
    }
}

