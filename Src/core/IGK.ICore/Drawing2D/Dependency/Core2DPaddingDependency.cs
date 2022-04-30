using IGK.ICore.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D.Dependency
{
    [CoreDependencyName("Padding")]
    public class Core2DPaddingDependency : CoreDependencyObject
    {
        public static readonly CoreDependencyProperty LeftProperty;
        public static readonly CoreDependencyProperty TopProperty;
        public static readonly CoreDependencyProperty BottomProperty;
        public static readonly CoreDependencyProperty RightProperty;

        static Core2DPaddingDependency()
        {
            Type t = MethodInfo.GetCurrentMethod().DeclaringType;
            LeftProperty = CoreDependencyProperty.Register("Left", typeof(float), t, null);
            RightProperty = CoreDependencyProperty.Register("Right", typeof(float), t, null);
            TopProperty = CoreDependencyProperty.Register("Top", typeof(float), t, null);
            BottomProperty = CoreDependencyProperty.Register("Bottom", typeof(float), t, null);
        }
    }
}
