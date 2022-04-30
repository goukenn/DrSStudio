

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _SizeTransform.cs
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
using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:_SizeTransform.cs
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.ElementTransform.Menu.Tools
{
    using IGK.DrSStudio.Drawing2D;
    [DrSStudioMenu("Tools.SizeTransform",
     23)]
    class _SizeTransform : Core2DDrawingMenuBase 
    {
        public _SizeTransform()
        {
            int v_count = 0;
            foreach (enuSize2DTransform item in Enum.GetValues(typeof(enuSize2DTransform)))
            {
                CoreMenuAttribute v_attr = new CoreMenuAttribute(string.Format(this.Id + ".{0}", item.ToString()), v_count);
                v_attr.ImageKey = string.Format("btn_2DSizeTransform_{0}", item.ToString());
                SizeMenu ch = new SizeMenu(item);
                ch.SetAttribute(v_attr);
                if (this.Register(v_attr, ch) == false)
                {
#if DEBUG
                    CoreMessageBox.Show(string.Format("Element not  : [{0}]", v_attr.Name));
#endif
                    continue;

                }
                this.Childs.Add(ch);
                v_count++;
            }
        }
        class SizeMenu : Core2DDrawingMenuBase
        {
            enuSize2DTransform m_transform;
            public SizeMenu(enuSize2DTransform transform)
            {
                this.m_transform = transform;
            }
            protected override bool PerformAction()
            {
                ICore2DDrawingLayeredElement[] elements = CurrentSurface.CurrentLayer.SelectedElements.ToArray();
                if (elements.Length < 2) return false;
                Vector2f v_ref = Vector2f.Zero;
                ICore2DDrawingLayeredElement v_c = elements[0];
                float REF = 0.0f;
                float ex = 0.0f;
                float ey = 0.0f;
                switch (this.m_transform)
                {
                    case  enuSize2DTransform.SameWidth :
                        REF = v_c.GetBound().Width;
                        if (REF == 0) return false;
                        ey = 1.0f;
                        for (int i = 1; i < elements.Length; i++)
                        {
                            v_c = elements[i];
                            try
                            {
                                ex = 1 / (v_c.GetBound().Width / REF);
                                if (ex != 0.0f)
                                    v_c.Scale(ex, ey, enuMatrixOrder.Append);
                            }
                            catch { 
                            }
                        }
                        break;
                    case enuSize2DTransform.SameHeight :
                        REF = v_c.GetBound().Height;
                        if (REF == 0) return false;
                        ex = 1.0f;
                        for (int i = 1; i < elements.Length; i++)
                        {
                            v_c = elements[i];
                            try{
                            ey = 1 / (v_c.GetBound().Height / REF);                            
                            if (ey != 0.0f)
                                v_c.Scale(ex, ey, enuMatrixOrder.Append );
                            }
                            catch {
                            }
                        }
                        break;
                }
                this.CurrentSurface.RefreshScene();
                return false;
            }
        }
    }
}

