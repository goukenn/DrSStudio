

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreConstant.cs
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
file:WinCoreConstant.cs
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore
{
    public class WinCoreConstant
    {
        public const string CTRL_DESIGNER = "IGK.DrSStudio.WinCore.Design.ControlDesigner, IGK.DrSStudio.WinCoreDesign";
        
        public const string CTRL_PANEL_DESIGNER = "IGK.DrSStudio.WinCore.Design.PanelDesigner, IGK.DrSStudio.WinCoreDesign";
        
        public static readonly System.Drawing.Size DUMMY_MENU_PICTURE_SIZE_16x16 = new System.Drawing.Size(16, 16);
        public static readonly System.Drawing.Image DUMMY_MENU_PICTURE_32x32 = new Bitmap(32,32);
        public static readonly System.Drawing.Image DUMMY_MENU_PICTURE_16x16 = new Bitmap(16, 16);
        public static int DEFAULT_DIALOG_HEIGHT = 150;
        public static int DEFAULT_DIALOG_WIDTH = 420;
        public  const string WIN_CORE_LIB_NAME = "IGKDEV WinCore";
    }
}

