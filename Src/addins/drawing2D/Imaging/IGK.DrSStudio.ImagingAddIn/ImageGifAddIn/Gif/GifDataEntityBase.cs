

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GifDataEntityBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
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
file:GifDataEntityBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.GifAddIn.Gif
{
    public abstract  class GifDataEntityBase : ICoreDataEntity 
    {
        private ICoreDataChain  m_Chain;
        public ICoreDataChain  Chain
        {
            get { return m_Chain; }
            set
            {
                if (m_Chain != value)
                {
                    m_Chain = value;
                }
            }
        }
        public abstract string Name
        {
            get;
        }
        public virtual string Render()
        {
                return this.Name ;
        }
        public virtual  void Read(System.IO.Stream stream)
        {
        }
        public virtual void Write(System.IO.Stream stream)
        {
        }
        public abstract void Copy(ICoreDataEntity dataEntity);
    }
}

