

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ItemPositionChanged.cs
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
file:_ItemPositionChanged.cs
*/
using IGK.ICore;using IGK.DrSStudio.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.HistoryActions
{
    public class _ItemPositionChanged : HistoryActionBase
    {
        public override void Undo()
        {
            throw new NotImplementedException();
        }
        public override void Redo()
        {
            throw new NotImplementedException();
        }
        public override string Info
        {
            get { throw new NotImplementedException(); }
        }
        public override string ImgKey
        {
            get { throw new NotImplementedException(); }
        }
    }
}
