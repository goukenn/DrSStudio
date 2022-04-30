using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.MecanismActions;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D.MecanismActions
{
    static class ReflectionExtension
    {
        public static object GetPropertyValue(this object target, string propertyName, object @default = null) {
            if (!(target is null)) {
                var type = target.GetType();
                var prop = type.GetProperty(propertyName);
                if (prop != null){
                    return prop.GetValue(target);
                }

            }

            return @default;
        }
    }
    class Core2DDrawingZoom0: CoreMecanismActionBase
    {
        protected override bool PerformAction()
        {

            
            if ((this.Mecanism?.Surface.GetPropertyValue("Scene")) is IIGKSceneTransform v_s)
            { 
                v_s.Zoom();
                return true; 
            }
            return false;

        }
    }
}
