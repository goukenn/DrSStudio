

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DockingRenderer.cs
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
file:DockingRenderer.cs
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
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK ;
    using IGK.ICore.WinUI;

    public static class DockingRenderer
    {
        static DockingRenderer() {
            //register all values
            foreach (var item in typeof(DockingRenderer).GetProperties( System.Reflection.BindingFlags.Static ))
            {                
                item.GetValue(null);
            }
        }

        public static Colorf DockingTabBarStartColor { get { return CoreRenderer.GetColor("DockingTabBarStartColor", Colorf.FromFloat(0.25f)); } }
        public static Colorf DockingTabBarEndColor { get { return CoreRenderer.GetColor("DockingTabBarEndColor", Colorf.FromFloat(0.25f)); } }
    
        public static Colorf DockingTabBarOverForeColor { get { return CoreRenderer.GetColor("DockingTabBarOverForeColor", Colorf.Black); } }
        public static Colorf DockingTabBarOngletForeColor { get { return CoreRenderer.GetColor("DockingTabBarOngletForeColor", Colorf.Black ); } }
        public static Colorf DockingTabBarDisableColor { get { return CoreRenderer.GetColor("DockingTabBarDisableColor", Colorf.FromFloat (0.5f)); } }

       public static Colorf DockingTabBarOngletColor { get { return CoreRenderer.GetColor("DockingTabBarOngletColor", Colorf.FromFloat(1.0f, 0.2f, 0.8f)); } }
       public static Colorf DockingTabBarOngletBorderColor { get { return CoreRenderer.GetColor("DockingTabBarOngletBorderColor", Colorf.Transparent ); } }
       public static Colorf DockingTabBarOngletUnselectedColor { get { return CoreRenderer.GetColor("DockingTabBarOngletUnselectedColor", Colorf.FromFloat(0.6f)); } }
        //redering property
        public static Colorf DockingTabBarSelectedStartColor { get  { return CoreRenderer.GetColor("DockingTabBarSelectedStartColor", Colorf.FromFloat (0.25f));            }        }
        public static Colorf DockingTabBarSelectedEndColor   { get  { return CoreRenderer.GetColor("DockingTabBarSelectedEndColor", Colorf.FromFloat (0.30f));            }        }
        public static Colorf DockingTabBarSelectedForeColor { get { return CoreRenderer.GetColor("DockingTabBarSelectedForeColor", Colorf.White ); } }
    }
}

