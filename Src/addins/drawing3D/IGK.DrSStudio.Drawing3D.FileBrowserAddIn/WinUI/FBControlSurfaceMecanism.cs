

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FBControlSurfaceMecanism.cs
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
file:FBControlSurfaceMecanism.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IGK.DrSStudio.Drawing3D.FileBrowser.WinUI
{
    
using IGK.DrSStudio.Actions;
    using IGK.DrSStudio.WinUI;
    using IGK.ICore.Mecanism;
    using IGK.ICore;
    using IGK.ICore.WinUI;
    
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Actions;
    
    using IGK.DrSStudio.Drawing3D.FileBrowser.Actions;
    using IGK.DrSStudio.Drawing3D.FileBrowser.Tools;
    /// <summary>
    /// represent file browser mecanism
    /// </summary>
    public class FBControlSurfaceMecanism : 
        CoreMecanismBase,
        ICoreWorkingMecanism
    {
        private FBControlSurface m_surface;
        public FBControlSurfaceMecanism(FBControlSurface surface)
            : base()
        {
             this.m_surface = surface;
             this.Register(m_surface);
             this.CurrentSurface = surface;
             this.m_isFreezed = false;
        }
        protected bool Register(FBControlSurface t)
        {
            Surface.MouseClick += Surface_MouseClick;
            return base.Register(t);
        }
        public override bool UnRegister()
        {
            this.CurrentSurface.MouseClick -= Surface_MouseClick;
            return base.UnRegister();
        }

        void Surface_MouseClick(object sender, CoreMouseEventArgs e)
        {

            var v_s = this.CurrentSurface as FBControlSurface;
            switch (e.Button)
            {
                case enuMouseButtons.Left:
            if (v_s != null)
                v_s.Scene.GoLeft();
                    break;
                case enuMouseButtons.Right:
                    if (v_s != null)
                        v_s.Scene.GoRight();
                    break;
            }
        }

     
    
        
        protected override void GenerateActions()
        {
            this.Actions.Add(enuKeys.Control | enuKeys.I, new InitizaleAppModel());
            this.Actions.Add(enuKeys.Control | enuKeys.O, new _OpenFileAction());
            this.Actions.Add(enuKeys.Control | enuKeys.D, new _SelectDirectory ());
            this.Actions.Add(enuKeys.Control | enuKeys.Enter , new _SetFullScreen());
            this.Actions.Add(enuKeys.Escape ,this.Actions[enuKeys.Control | enuKeys.Enter ]);
            this.Actions.Add(enuKeys.Control | enuKeys.Shift | enuKeys.P, new FileBrowser_SettingAction  ());
            this.Actions.Add(enuKeys.Left, new FBNavigation(enuKeys.Left));
            this.Actions.Add(enuKeys.Right, new FBNavigation(enuKeys.Right));
            this.Actions.Add(enuKeys.Up, new FBNavigation(enuKeys.Up));
            this.Actions.Add(enuKeys.Down , new FBNavigation(enuKeys.Down));
        }
        class FBNavigation : FBAction 
        {            
            private enuKeys enuKeys;
            
            public FBNavigation(enuKeys enuKeys)
            {
                this.enuKeys = enuKeys;
            }

            protected override bool PerformAction()
            {
                var v_s = this.Mecanism.CurrentSurface as FBControlSurface ;
                switch (this.enuKeys)
                {
                    case enuKeys.Left:
                        v_s.Scene.GoLeft();
                        return true;
                    case enuKeys.Right:
                        v_s.Scene.GoRight();
                        return true;
                    case enuKeys.Up:
                        v_s.Scene.GoUp();
                        return true;
                    case enuKeys.Down :
                        v_s.Scene.GoDown();
                        return true;
                    default:
                        break;
                }
                return false;
            }
        }

        class InitizaleAppModel : FBAction
        {
            protected override bool PerformAction()
            {
                var v_s = this.Mecanism.CurrentSurface as FBControlSurface;
                v_s.Scene.Initialize();
                return false;
            }
        }
        public override void Freeze()
        {
        }
        public override void UnFreeze()
        {
        }

        private bool m_isFreezed;

        public override bool IsFreezed
        {
            get { return m_isFreezed; }
        }
        

        /// <summary>
        /// register action to be performed by key dow 
        /// </summary>
        public class MecanismActionCollections :
            CoreMecanismActionBaseCollections,
            ICoreMecanismActionCollections
            
        {           
       
            /// <summary>
            /// return true if not available
            /// </summary>
            /// <param name="m"></param>
            /// <returns></returns>
            public override bool IsNotAvailable(ICoreMessage m)
            {
                var s =  FBViewControlTool.Instance.Surface ;
                if ((s!=null) && (FBViewControlTool.Instance.CurrentSurface ==
                    s) )
                {
                    if ((m.HWnd == s.Handle )|| (s.Scene .Handle == m.HWnd ))
                        return false;
                }
                return true;

            }
            //public override bool IsNotAvailable(Message m)
            //{
            //    //if (FBViewControlTool.Instance.CurrentSurface == 
            //    //    FileBrowserTools.Instance.Surface )
            //    //    return  ((this.Mecanism.CurrentSurface.IsDisposed) || !this.Mecanism .AllowActions);
            //    return true;
            //}
            public MecanismActionCollections(FBControlSurfaceMecanism mecanism)
                : base(mecanism)
            {
            }
            public new IFBAction  this[enuKeys key]
            {
                get
                {
                    return base[key] as IFBAction;
                }
                set
                {
                    base[key] = value as ICoreMecanismAction ;
                }
            }
        }



        public override void Edit(ICoreWorkingObject element)
        {

        }
       

        public override bool CanProcessActionMessage(ICoreMessage message)
        {
            return false;
        }

        protected override ICoreMecanismActionCollections CreateActionMecanismCollections()
        {
            return new MecanismActionCollections(this);
        }

        protected override ICoreSnippet CreateSnippet(int demand, int index)
        {
            //no snippet
            return null;
        }

        protected override ICoreSnippetCollections CreateSnippetCollections()
        {
            //no script
            return null;
        }

        protected override ICoreWorkingConfigurableObject GetEditElement()
        {
            return null;
        }

        public override void Invalidate()
        {
            
        }

        public override ICoreWorkingSurface Surface
        {
            get { return this.m_surface; }
        }
    }
}

