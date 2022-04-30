

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreSufaceActionBase.cs
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
file:CoreSufaceActionBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Actions
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Actions;
    using IGK.ICore.Drawing2D.WinUI;
    public abstract class CoreSufaceActionBase : CoreActionBase, ICoreSurfaceAction
    {
        #region ICoreSurfaceAction Members
        private ICore2DDrawingSurface m_CurrentSurface;


        public ICore2DDrawingSurface CurrentSurface
        {
            get { return this.m_CurrentSurface; }
            internal protected set{ this.m_CurrentSurface = value;}
        }
        #endregion
        public override string Id
        {
            get { return string.Format ("Drawing2DSurface.Action.{0}", this.GetType ().Name ); }
        }
    }
}

