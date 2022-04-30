

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PresentationMecanism.cs
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
file:PresentationMecanism.cs
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
namespace IGK.DrSStudio.Presentation.WinUI
{
    using IGK.ICore.WinCore;
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Actions;
    using IGK.ICore.WinUI;
    using IGK.ICore.Actions;
    using IGK.ICore;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Mecanism;
    using IGK.ICore.Tools;
    using IGK.DrSStudio.Presentation.Actions;

    public class PresentationMecanism :
        CoreMecanismBase,
        ICoreWorkingMecanism
    {       
        public PresentationForm PresentationForm {
            get {
                return this.CurrentSurface?.FindForm() as PresentationForm;//?.PresentationForm;
            }
        }
        public new PresentationSurface CurrentSurface {
            get {
                return base.CurrentSurface as PresentationSurface;
            }
        }

        public PresentationMecanism(PresentationSurface surface):base()
        {
            base.CurrentSurface = surface;
            this.AllowActions = true;
            this.Register(surface);
        }
        protected override ICoreMecanismActionCollections CreateActionMecanismCollections()
        {
            return new MecanismActionCollections(this);
        }
        protected override void GenerateActions()
        {
 	        this.Actions.Add (enuKeys.Control | enuKeys.Enter, new _MakeFullScreenAction());
            this.Actions.Add(enuKeys.Control | enuKeys.E, new _EditDocument());
            this.Actions.Add(enuKeys.Escape, new _EscapeFullScreen());
            this.Actions.Add(enuKeys.Next , new _GoToNext());
            this.Actions.Add(enuKeys.Back , new _GoToPrevious());
            this.Actions.Add(enuKeys.Left, this.Actions[enuKeys.Back]);
            this.Actions.Add(enuKeys.Right , this.Actions[enuKeys.Next ]);
            this.Actions.Add(enuKeys.Up , this.Actions[enuKeys.Back ]);
            this.Actions.Add(enuKeys.Down , this.Actions[enuKeys.Next]);
            this.Actions.Add(enuKeys.Home,  new _GoToStart());
            this.Actions.Add(enuKeys.End, new _GoToEnd());
            this.Actions.Add(enuKeys.Control | enuKeys.O, new _OpenFile( ));
            this.Actions.Add(enuKeys.Control | enuKeys.P, new _PrintDocumentAction());
        }
        #region ICoreWorkingMecanism Members
        public override void Edit(ICoreWorkingObject element)
        {
        }
       
        public override bool Register(ICoreWorkingSurface surface)
        {
            CoreActionRegisterTool.Instance.AddFilterMessage(this.Actions );

            
            surface.MouseClick += Surface_MouseClick;
            return true;
        }

        private void Surface_MouseClick(object sender, CoreMouseEventArgs e)
        {
            var s = this.CurrentSurface;

            if (e.Button == enuMouseButtons.Left) {
                if (this.IsShiftKey)
                    s.PresentationDocument.MoveBack();
                else
                    s.PresentationDocument.MoveNext();
                s.Invalidate();
            }
        }
        
        public override bool UnRegister()
        {
            CoreActionRegisterTool.Instance.RemoveFilterMessage(this.Actions);
            return true;
        }
      
        public override void Freeze()
        {
        }
        public override void UnFreeze()
        {
        }
        #endregion

        

        
        ///// <summary>
        ///// register action to be performed by key dow 
        ///// </summary>


        /// <summary>
        /// register action to be performed by key dow 
        /// </summary>
        public class MecanismActionCollections :
            CoreMecanismActionBaseCollections,
            ICoreMecanismActionCollections

        {
            public MecanismActionCollections(ICoreWorkingMecanism mecanism) : base(mecanism)
            {
            }
         
            /// <summary>
            /// return true if not available
            /// </summary>
            /// <param name="m"></param>
            /// <returns></returns>
            public override bool IsNotAvailable(ICoreMessage m)
            {
                //var s = FBViewControlTool.Instance.Surface;
                //if ((s != null) && (FBViewControlTool.Instance.CurrentSurface ==
                //    s))
                //{
                //    if ((m.HWnd == s.Handle) || (s.Scene.Handle == m.HWnd))
                //        return false;
                //}

                //Console.WriteLine("d : " + m.HWnd + " x "+ Mecanism.CurrentSurface.Handle);
                if (m.HWnd == Mecanism.CurrentSurface.Handle)
                {
                    //Mecanism?.CurrentSurface?.Handle) {
                    return false;
                }
                return true;

            }

            public override bool PreFilterMessage(ref ICoreMessage m)
            {
                return base.PreFilterMessage(ref m);
            }
            //public override bool IsNotAvailable(Message m)
            //{
            //    //if (FBViewControlTool.Instance.CurrentSurface == 
            //    //    FileBrowserTools.Instance.Surface )
            //    //    return  ((this.Mecanism.CurrentSurface.IsDisposed) || !this.Mecanism .AllowActions);
            //    return true;
            //}
            //public MecanismActionCollections(FBControlSurfaceMecanism mecanism)
            //    : base(mecanism)
            //{
            //}
            //public new IFBAction this[enuKeys key]
            //{
            //    get
            //    {
            //        return base[key] as IFBAction;
            //    }
            //    set
            //    {
            //        base[key] = value as ICoreMecanismAction;
            //    }
            //}
        }



        //public class  MecanismActionCollection<T> :
        //    CoreMecanismActionBaseCollections<T>,
        //    ICoreMecanismActionCollections,
        //    IMessageFilter
        //{
        //    public MecanismActionCollection(PresentationMecanism mecanism)
        //        : base(mecanism)
        //    {
        //    }
        //    //public override bool IsNotAvailable(Message m)
        //    //{
        //    //    return false;// base.IsNotAvailable(m);
        //    //}
        //    public new IPresentationActions  this[enuKeys key]
        //    {
        //        get
        //        {
        //            return base[key] as IPresentationActions;
        //        }
        //        set
        //        {
        //            base[key] = value;
        //        }
        //    }


        //    public override int Priority
        //    {
        //        get { throw new NotImplementedException(); }
        //    }

        //    public bool PreFilterMessage(ref Message m)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}


        public override bool CanProcessActionMessage(ICoreMessage message)
        {
            return false;
        }
       
        protected override ICoreSnippet CreateSnippet(int demand, int index)
        {
            return null;
        }
        protected override ICoreSnippetCollections CreateSnippetCollections()
        {
            return null;
        }
        protected override ICoreWorkingConfigurableObject GetEditElement()
        {
            return null;
        }
        public override void Invalidate()
        {
            this.CurrentSurface.Invalidate();
        }

        public override ICoreWorkingSurface Surface
        {
            get { return base.CurrentSurface; }
        }

        public override bool IsFreezed
        {
            get { return false; }
        }
    }
}

