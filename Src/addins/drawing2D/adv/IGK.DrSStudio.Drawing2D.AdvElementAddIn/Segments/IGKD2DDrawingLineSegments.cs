using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Drawing2D.Segments
{
    [AttributeUsage (AttributeTargets.Class , AllowMultiple =false , Inherited = false )]
    public class IGKD2DDrawingLineSegmentsAttribute : Core2DDrawingStandardElementAttribute
    {
        public IGKD2DDrawingLineSegmentsAttribute(string n, Type mecanism):base(n, mecanism ){

        }
        public override string GroupName{
            get
            {
                return "LineMecanism";
            }
        }
    }
}
