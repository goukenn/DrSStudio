

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFPropertyChanged.cs
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
file:WPFPropertyChanged.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn
{
    public enum enuWPFPropertyChangedType
    {
        ID = enuPropertyChanged.Id ,
        Definition = enuPropertyChanged.Definition ,
        MatrixChanged = Definition + 1,
        ColorChanged = Definition +2
    }
    public class WPFPropertyChanged : CoreWorkingObjectPropertyChangedEventArgs 
    {
        public static readonly WPFPropertyChanged MatrixChanged;
        public static readonly WPFPropertyChanged ColorChanged;
        static WPFPropertyChanged()
        {
            MatrixChanged = new WPFPropertyChanged(enuWPFPropertyChangedType.MatrixChanged);
            ColorChanged = new WPFPropertyChanged(enuWPFPropertyChangedType.ColorChanged);
        }
        public WPFPropertyChanged(enuWPFPropertyChangedType type):base((
            enuPropertyChanged ) type, null)
        {
        }
    }
}

