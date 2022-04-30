

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ConvolutionsMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
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
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:_ConvolutionsMenu.cs
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
using System.Reflection;
namespace IGK.DrSStudio.Drawing2D.Menu
{
    using IGK.DrSStudio.Imaging;
    using System.Drawing;
    [DrSStudioMenu("Image.Convolutions",0x20, SeparatorBefore=true)]
    class _Convolutions: ImageMenuBase
    {
        public _Convolutions()
        {
            this.IsRootMenu = false;
        }
        protected override void InitMenu()
        {
            base.InitMenu();
            CoreMenuAttribute v_attrib = null;
            //passe haut : PH
            //passe bas : PB
            //SM
            string[] cNode = new string[] { "PH", "PB", "SM", "LP", "NORM", "SOBEL", "CHENFREI", "OPKIRSH", "BSDIR" };
            ChildMenu v_child = null;
            int v_count = 0;
            string v_key = string.Empty;
            
            for (int i = 0; i < cNode.Length; i++)
            {
                v_key = string.Format("{0}.{1}", this.Id, cNode[i]);
                v_attrib = new CoreMenuAttribute(v_key, i);
                if ((i + 1) >= cNode.Length)
                    v_attrib.SeparatorAfter = true;
                ChildDummyMenu m = new ChildDummyMenu();
                m.SetAttribute(v_attrib);
                if (!Register(v_attrib, m))
                {
                    CoreLog.WriteLine("msg.menuNotRegistrated".R(v_attrib.Name));
                }
                else 
                    this.Childs.Add(m);
            }
            v_count = 0;
            foreach (FieldInfo ft in typeof(CoreConvolutionParam).GetFields(BindingFlags.NonPublic | BindingFlags.Public |
                 BindingFlags.Static))
            {
                if (ft.FieldType == typeof(CoreConvolutionParam))
                {
                    v_key = string.Format("{0}.{1}", this.Id, ft.Name.Replace("_", "."));
                    v_attrib = new CoreMenuAttribute(v_key, v_count);
                    v_child = new ChildMenu(ft.GetValue(null) as CoreConvolutionParam);
                    v_child.SetAttribute(v_attrib);
                    if (!Register(v_attrib, v_child))
                    {
                        CoreLog.WriteLine("msg.menuNotRegistrated".R(v_attrib.Name));
                    }
                    else {
                        string k = System.IO.Path.GetFileNameWithoutExtension(v_key);
                        CoreMenuActionBase m = CoreSystem.GetAction(k) as CoreMenuActionBase ;
                        if (m!=null)
                        m.Childs.Add(v_child);

                    }                        
                    v_count++;
                }
            }
            this.Childs.Sort();
        }
        /// <summary>
        /// used to create root element
        /// </summary>
        internal class ChildDummyMenu : ImageMenuBase
        {          
        }
        class ChildMenu : ImageMenuBase
        {
            CoreConvolutionParam m_param;
            public ChildMenu(CoreConvolutionParam param)
            {
                if (param == null)
                    throw new CoreException(enuExceptionType.ArgumentIsNull, "param");
                this.m_param = param;
            }
            protected override bool PerformAction()
            {
                if (this.ImageElement != null)
                {
                    Bitmap c = ImagingUtils.GetClonedBitmap ( this.ImageElement.Bitmap)
                    as Bitmap;
                    this.ImageElement.SetBitmap (m_param.ApplyConvolution (
                        c , null).ToCoreBitmap (),
                        false );
                    this.CurrentSurface.RefreshScene();
                }
                return false;
            }
        }
    }
}

