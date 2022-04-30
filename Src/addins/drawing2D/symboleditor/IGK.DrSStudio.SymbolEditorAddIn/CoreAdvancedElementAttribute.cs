using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.SymbolEditorAddIn
{
     [AttributeUsage (AttributeTargets.Class , Inherited=false, AllowMultiple=false )]
    class CoreAdvancedElementAttribute : Core2DDrawingStandardElementAttribute  
    {
        public override string GroupName
        {
            get
            {
                return "advanced";
            }
        }
        public CoreAdvancedElementAttribute(string name, Type mecanism)
            : base(name, mecanism)
        {
        }
    }
}
