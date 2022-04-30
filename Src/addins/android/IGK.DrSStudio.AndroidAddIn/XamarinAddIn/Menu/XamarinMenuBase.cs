
using IGK.DrSStudio.Android.Xamarin.WinUI;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Xamarin.Menu
{
    /// <summary>
    /// represent a xamarin menu base
    /// </summary>
    public abstract class XamarinMenuBase : CoreApplicationMenu
    {
        private object m_Params;

        /// <summary>
        /// get or set the param that will be used for DoAction method
        /// </summary>
        public object Params
        {
            get { return m_Params; }
            set
            {
                if (m_Params != value)
                {
                    m_Params = value;
                }
            }
        }
        public new XamarinEditorSurface CurrentSurface {
            get {
                return base.CurrentSurface as XamarinEditorSurface;
            }
        }
        protected override bool IsVisible()
        {
            return this.CurrentSurface != null;
        }
        protected override bool IsEnabled()
        {
            return this.CurrentSurface != null;
        }
    }
}
