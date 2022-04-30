

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DLayerKeyManagerTool.cs
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
file:IGKD2DLayerKeyManagerTool.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Tools
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Drawing2D.Menu;
    using IGK.ICore.Menu;
    using IGK.ICore.Resources;
    using IGK.ICore.Tools;
    using IGK.ICore.WinUI;
     [CoreTools("Tool.IGKD2DLayerKeyManagerTool")]
    class IGKD2DLayerKeyManagerTool :
        CoreShortCutMenuContainerToolBase,
        ICoreMenuMessageShortcutContainer
    {
        private static IGKD2DLayerKeyManagerTool sm_instance;
        private IGKD2DLayerKeyManagerTool():base()
        {
            this.Key = enuKeys.L;
        }
        public static IGKD2DLayerKeyManagerTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static IGKD2DLayerKeyManagerTool()
        {
            sm_instance = new IGKD2DLayerKeyManagerTool();
        }
        protected override void GenerateHostedControl()
        {
            base.GenerateHostedControl();
        }
    }
}

