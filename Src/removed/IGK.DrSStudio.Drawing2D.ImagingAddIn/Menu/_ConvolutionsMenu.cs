

/*
IGKDEV @ 2008 - 2014
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
/*
IGKDEV - [2008 - 2014]
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
    using IGK.ICore;using IGK.DrSStudio.Menu;
    [CoreMenu("Image.Convolutions",31)]
    class _Convolutions: ImageMenuBase
    {
        public _Convolutions()
        {
            CoreMenuAttribute v_attrib = null;
            string[] cNode = new string[] { "PH", "PB", "SM","LP","NORM","SOBEL","CHENFREI","OPKIRSH","BSDIR" };
            ChildMenu v_child = null;
            int v_count = 0;
            for (int i = 0; i < cNode.Length; i++)
			{
            v_attrib = new CoreMenuAttribute(string.Format("{0}.{1}", this.Id, cNode[i]), i);
                if ((i + 1 ) >=cNode.Length )
                v_attrib.SeparatorAfter = true ;
            ChildDummyMenu m = new ChildDummyMenu();
            m.SetAttribute(v_attrib);
            Register(v_attrib, m);
			}
            v_count += cNode.Length;  
            foreach (FieldInfo ft in typeof(CoreConvolutionParam).GetFields(BindingFlags.NonPublic | BindingFlags.Public |
                 BindingFlags.Static))
            {
                if (ft.FieldType == typeof(CoreConvolutionParam))
                {
                    v_attrib = new CoreMenuAttribute(string.Format("{0}.{1}", this.Id, ft.Name.Replace ("_",".")), v_count);
                    v_child = new ChildMenu(ft.GetValue(null) as CoreConvolutionParam );
                    v_child.SetAttribute(v_attrib);
                    Register(v_attrib, v_child);
                    v_count++;
                }
            }
        }
        /// <summary>
        /// used to create root element
        /// </summary>
        internal class ChildDummyMenu : ImageMenuBase
        { }
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
                    this.ImageElement.SetBitmap (m_param.ApplyConvolution (
                        this.ImageElement.Bitmap, null),
                        false );
                }
                return false;
            }
        }
    }
}

