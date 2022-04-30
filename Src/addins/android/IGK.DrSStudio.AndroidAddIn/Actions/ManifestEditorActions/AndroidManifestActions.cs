

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidManifestActions.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using ICSharpCode.AvalonEdit.Editing;

using IGK.ICore;using IGK.DrSStudio.Android.WinUI;
using IGK.ICore.MecanismActions;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Actions
{
    public abstract class AndroidManifestActions : CoreMecanismActionBase
    {
        static List<Type> sm_actions;
        private AndroidManifestEditorSurface m_Surface;
        /// <summary>
        /// get the adnroid manifest editor surface
        /// </summary>
        public AndroidManifestEditorSurface Surface { get { return this.m_Surface; } }

        public TextArea TextArea { get { return this.m_Surface.TextArea;  } }
        public abstract  enuKeys Key{
            get;
        }
        static AndroidManifestActions() {
            sm_actions = new List<Type>();
            LoadActions();
        }

        private static void LoadActions()
        {
            foreach (Type item in System.Reflection.Assembly.GetExecutingAssembly ().GetTypes())
            {
                if (item.IsSubclassOf(typeof(AndroidManifestActions)))
                { 
                   //AndroidManifestActions c =  item.Assembly.CreateInstance (item.FullName) as AndroidManifestActions;
                    //if (c!=null)
                    sm_actions.Add (item);
                }
            }
        }

        public static AndroidManifestActions[] GetActions(AndroidManifestEditorSurface surface )
        {
            List<AndroidManifestActions> actions = new List<AndroidManifestActions>();
            if (surface != null)
            { 
                //create taction instance
                foreach (Type item in sm_actions)
                {
                    var action = item.Assembly.CreateInstance(item.FullName ) as AndroidManifestActions;
                    if (action != null)
                    {
                        action.m_Surface = surface;
                        actions.Add(action);
                    }
                }
            }
            return actions.ToArray();
        }
    }
}
