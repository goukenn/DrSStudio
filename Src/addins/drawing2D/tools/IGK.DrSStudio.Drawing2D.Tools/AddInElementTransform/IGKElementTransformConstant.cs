

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKElementTransformConstant.cs
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
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D
{
    class IGKElementTransformConstant
    {
        public const string MENU_INVERT_X = "Menu.Tools.Invert.X";
        public const string MENU_INVERT_Y = "Menu.Tools.Invert.Y";
        //GetMenu
        public const string MENU_LAYER_MOVE_ELEMENT_UP = IGKD2DrawingConstant.MENU_LAYER +".MoveElementUp";
        public const string MENU_LAYER_MOVE_ELEMENT_DOWN = IGKD2DrawingConstant.MENU_LAYER + ".MoveElementDown";
        public const string MENU_LAYER_MOVE_ELEMENT_TOSTART = IGKD2DrawingConstant.MENU_LAYER + ".MoveElementBack";
        public const string MENU_LAYER_MOVE_ELEMENT_TOEND = IGKD2DrawingConstant.MENU_LAYER + ".MoveElementTop";
    }
}