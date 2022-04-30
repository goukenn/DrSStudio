

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GCSingleColorPickerBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:GCSingleColorPickerBase.cs
*/
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio
{
    /// <summary>
    /// represent a base color picker Control
    /// </summary>
    class GCSingleColorPickerBase : IGKXUserControl ,IGCSSingleColorPicker 
    {
        private Colorf m_Color;

        protected static int GetLuminosityValue(Colorf cl, int minvalue, int maxvalue)
        {
            return (int)(minvalue+ (Colorf.GetLuminosity(cl) * (maxvalue  - minvalue) / 100.0f));
        }
        public GCSingleColorPickerBase()
        {
   
        }
        protected virtual void EditFromProperty()
        { 
        }
        /// <summary>
        /// event raised when color changed
        /// </summary>
        public event EventHandler ColorChanged;
        /// <summary>
        /// get or set the color of this element
        /// </summary>
        public Colorf Color
        {
            get { return m_Color; }
            set
            {
                if (!m_Color.Equals(value))
                {
                    m_Color = value;
                    this.EditFromProperty();
                    OnColorChanged(EventArgs.Empty);
                }
            }
        }
       
        protected virtual void OnColorChanged(EventArgs eventArgs)
        {
            if (this.ColorChanged != null)
            {
               this.ColorChanged(this, eventArgs);
            }
        }
    }
}

