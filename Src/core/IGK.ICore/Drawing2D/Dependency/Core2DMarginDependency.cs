using IGK.ICore.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D.Dependency
{
    [CoreDependencyName("Margin")]
    public class Core2DMarginDependency : CoreDependencyObject
    {
        public static readonly CoreDependencyProperty LeftProperty;
        public static readonly CoreDependencyProperty TopProperty;
        public static readonly CoreDependencyProperty BottomProperty;
        public static readonly CoreDependencyProperty RightProperty;

        static Core2DMarginDependency(){         
            Type t = typeof (Core2DMarginDependency);
            LeftProperty = CoreDependencyProperty.Register("Left", typeof(CoreUnit), t, null);
            RightProperty = CoreDependencyProperty.Register("Right", typeof(CoreUnit), t, null);
            TopProperty = CoreDependencyProperty.Register("Top", typeof(CoreUnit), t, null);
            BottomProperty = CoreDependencyProperty.Register("Bottom", typeof(CoreUnit), t, null);
        }
    }
}
