

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebCssEditorActions.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
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
﻿using ICSharpCode.AvalonEdit.Editing;
using IGK.DrSStudio.WebCssEditorAddIn.WinUI;
using IGK.ICore.MecanismActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.WebCssEditorAddIn.Actions
{
    /// <summary>
    /// represent a base mecanism css editor mecanism 
    /// </summary>
    abstract class WebCssEditorActions : CoreMecanismActionBase
    {
        static List<Type> sm_actions;
        private WebCssEditorSurface.WebCssEditorMecanism m_mecanism;
        public abstract  enuKeys Key { get; }
        public WebCssEditorSurface CurrentSurface {
            get {
                return this.m_mecanism.CurrentSurface as WebCssEditorSurface;
            }
        }
        public new WebCssEditorSurface.WebCssEditorMecanism Mecanism {
            get {
                return base.Mecanism as WebCssEditorSurface.WebCssEditorMecanism;
            }
        }
        public TextArea TextArea {
            get {
                return this.CurrentSurface.TextArea;
            }
        }
        static WebCssEditorActions() { 
            //load primary type
            sm_actions 
                 = new List<Type>();
            Type v_types = MethodInfo.GetCurrentMethod().DeclaringType;
            foreach(Type t in  v_types.Assembly.GetTypes ())
            {
                if (t.IsSubclassOf(v_types))
                {
                    sm_actions.Add(t);
                }
            }

        }
        protected WebCssEditorActions()
        {
        }

        internal static WebCssEditorActions[] GetActions(WebCssEditorSurface.WebCssEditorMecanism webCssEditorMecanism)
        {
            if (webCssEditorMecanism == null)
                return null;
            List<WebCssEditorActions> v_lactions = new List<WebCssEditorActions>();
            foreach (Type t in sm_actions)
            {
                var g = t.Assembly.CreateInstance(t.FullName) as WebCssEditorActions;
                if (g != null)
                {
                    g.m_mecanism = webCssEditorMecanism;
                    v_lactions.Add(g);
                }
            }
            return v_lactions.ToArray();
        }
    }
}
