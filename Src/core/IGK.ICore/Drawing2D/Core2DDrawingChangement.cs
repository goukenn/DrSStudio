

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingChangement.cs
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
file:Core2DDrawingChangement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// used to infor changement possibility
    /// </summary>
    public sealed class Core2DDrawingChangement
    {
        public static readonly CoreWorkingObjectPropertyChangedEventArgs Empty  = CoreWorkingObjectPropertyChangedEventArgs.Empty ;
        public static readonly CoreWorkingObjectPropertyChangedEventArgs Id = CoreWorkingObjectPropertyChangedEventArgs.Id;
        public static readonly CoreWorkingObjectPropertyChangedEventArgs Definition = CoreWorkingObjectPropertyChangedEventArgs.Definition;
        public static readonly CoreWorkingObjectPropertyChangedEventArgs Matrix = new CoreWorkingObjectPropertyChangedEventArgs((enuPropertyChanged)enu2DPropertyChangedType.MatrixChanged);
        public static readonly CoreWorkingObjectPropertyChangedEventArgs Brush = new CoreWorkingObjectPropertyChangedEventArgs((enuPropertyChanged)enu2DPropertyChangedType.BrushChanged );
        public static readonly CoreWorkingObjectPropertyChangedEventArgs ViewChanged = new CoreWorkingObjectPropertyChangedEventArgs((enuPropertyChanged)enu2DPropertyChangedType.ViewChanged);
        public static readonly CoreWorkingObjectPropertyChangedEventArgs FontChanged = new CoreWorkingObjectPropertyChangedEventArgs((enuPropertyChanged)enu2DPropertyChangedType.FontChanged);
    }
}

