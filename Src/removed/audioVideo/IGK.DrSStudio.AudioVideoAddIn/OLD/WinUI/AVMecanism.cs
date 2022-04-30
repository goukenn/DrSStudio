

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AVMecanism.cs
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
file:AVMecanism.cs
*/
using IGK.ICore;using IGK.DrSStudio.AudioVideo.MecanismActions;
using IGK.DrSStudio.Mecanism;
using IGK.DrSStudio.Tools.ActionRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.AudioVideo.WinUI
{
    class AVMecanism : CoreMecanismBase
    {
        public AVMecanism(XVideoSurfaceBase surface)
        {
            this.Register(surface);
            this.CurrentSurface = surface;
        }
        public override Actions.ICoreMecanismActionCollections CreateActionCollections()
        {
            return new AVMecanismActionCollections (this);
        }
        protected override void GenerateActions()
        {
            this.AddAction(Keys.P, new AVPlayAction());
            this.AddAction(Keys.S, new AVStopAction()); 
        }
        public override void Edit(ICoreWorkingObject element)
        {
            throw new NotImplementedException();
        }
        public override void EndEdition()
        {
            throw new NotImplementedException();
        }
        public override bool Register(DrSStudio.WinUI.ICoreWorkingSurface surface)
        {
            CoreActionRegisterTool.Instance.AddFilterMessage(this.Actions);
            return true;
        }
        public override bool UnRegister()
        {
            CoreActionRegisterTool.Instance.RemoveFilterMessage (this.Actions);
            return true;
        }
        class AVMecanismActionCollections : CoreMecanismBase.CoreMecanismActionCollection
        {
            public AVMecanismActionCollections(AVMecanism mecanism):base(mecanism )
            {
            }
            public override bool IsNotAvailable(System.Windows.Forms.Message m)
            {
                if (this.Workbench !=null)
                return (this.Mecanism.CurrentSurface != this.Workbench.CurrentSurface ) ;//mecanism. false;
                return false;
            }
        }
    }
}

