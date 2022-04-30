

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ContourElementMecanismActionBase.cs
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
file:ContourElementMecanismActionBase.cs
*/

using IGK.ICore.Actions;
using System; using IGK.ICore; using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore.MecanismActions;
namespace IGK.DrSStudio.Drawing2D.Contour.MecanismActions
{
    public abstract class ContourElementMecanismActionBase : CoreMecanismActionBase
    {
        internal new ContourElement.Mecanism Mecanism
        {
            get
            {
                return base.Mecanism as ContourElement.Mecanism;
            }
        }
        public ContourElementMecanismActionBase()
            : base()
        {
        }
    }
}

