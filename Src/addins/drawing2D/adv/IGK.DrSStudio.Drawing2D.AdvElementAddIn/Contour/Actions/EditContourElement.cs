

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: EditContourElement.cs
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
file:EditContourElement.cs
*/

using IGK.ICore.Actions;
using System; using IGK.ICore; using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Contour
{
    class EditContourElement : CoreActionBase
    {
        private ContourElement contourElement;
        public EditContourElement(ContourElement contourElement)
        {
            this.contourElement = contourElement;
        }
        public override string Id
        {
            get { return "{7452EFEE-FC29-4CA9-81DE-BBBC95BA34B5}"; }
        }
        protected override bool PerformAction()
        {
            if (CoreSystem.Instance.Workbench!=null)
                CoreSystem.Instance.Workbench.ConfigureWorkingObject(
                    this.contourElement.Element,
                    "title.editContourElement".R(),false, Size2i .Empty  );
            return true;
        }
    }
}

