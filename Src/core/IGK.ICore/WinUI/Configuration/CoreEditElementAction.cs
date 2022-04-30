

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreEditElementAction.cs
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
file:CoreEditElementAction.cs
*/
using IGK.ICore;using IGK.ICore.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.WinUI.Configuration
{
    public class CoreParameterEditElementAction : CoreParameterActionBase 
    {
        public CoreParameterEditElementAction(string name, string captionKey, ICoreWorkingConfigurableObject obj) :
            base(name, captionKey, new CoreEditElementAction(obj))
        {
        }
        class CoreEditElementAction : CoreActionBase
        {
            private ICoreWorkingConfigurableObject obj;
            public CoreEditElementAction(ICoreWorkingConfigurableObject obj):base()
            {
                this.obj = obj;
            }
            protected override bool PerformAction()
            {
                CoreSystem.GetWorkbench().ConfigureWorkingObject(
                    this.obj, "title.editelement".R(this.obj.Id), false,
                    Size2i.Empty );
                return true;
            }
            //public override string Id
            //{
            //    get { return "{0136AB8B-3C85-4539-835A-92FA9ECCD17C}"; }
            //}
        }
    }
}

