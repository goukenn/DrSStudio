

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreMecanismEditElement.cs
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
file:CoreMecanismEditElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Actions.Mecanism
{
    using IGK.ICore.WinCore;
using IGK.ICore;
    using IGK.ICore.Actions;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.WinUI;
    [CoreAction("Drawing2D.CoreMecanismEditElement")]
    public class CoreMecanismEditElement : CoreActionBase 
    {
        public override string Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                base.Id = value;
            }
        }
        public CoreMecanismEditElement():base()
        {
            //this.Id = "name";
        }
        protected override bool PerformAction()
        {
            ICore2DDrawingSurface vs =  CoreSystem.Instance.Workbench.CurrentSurface as ICore2DDrawingSurface;
            if (vs != null)
            { 
                ICoreWorkingToolManagerSurface c = vs as ICoreWorkingToolManagerSurface ;
                ICore2DDrawingLayeredElement e  = vs.CurrentLayer.SelectedElements[0];

                var services = CoreSystemServices.GetServiceByName("DesignerService") as ICoreDesignerService;
                Type t = services?.GetEditionTool(e.GetType()) ?? e.GetType();


                //CoreServices.GetEditionTools(e.GetType())
                if (t != null)
                {
                    c.CurrentTool = t;// e.GetType();
                    c.Mecanism.Edit(e);
                }
            }
            return false;
        }
    }
}

