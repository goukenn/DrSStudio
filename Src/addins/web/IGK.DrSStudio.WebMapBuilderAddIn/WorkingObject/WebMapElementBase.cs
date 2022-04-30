

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebMapElementBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WebMapElementBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using IGK.ICore.Drawing2D.Mecanism;
namespace IGK.DrSStudio.Web.WorkingObject
{
    public abstract  class WebMapElementBase : Core2DDrawingLayeredElement, IWebMapElement
    {
        private Rectanglef m_Bounds;
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue("0;0;0;0")]
        /// <summary>
        /// get or set bound of the web map element
        /// </summary>
        public Rectanglef Bounds
        {
            get { return m_Bounds; }
            set
            {
                if (m_Bounds != value)
                {
                    m_Bounds = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public class Mecanism<T> : Core2DDrawingSurfaceMecanismBase<T>
            where T : class , ICore2DDrawingLayeredElement 
        { 
        }
        public abstract string Render(IWebMapRendererOption option);
        /// <summary>
        /// get xml writer setting
        /// </summary>
        /// <returns></returns>
        protected virtual XmlWriterSettings GetXmlWriterSetting()
        {
            XmlWriterSettings v_setting = new XmlWriterSettings();
            v_setting.OmitXmlDeclaration = true;
            v_setting.Indent = true;
            return v_setting;
        }
        public override enuBrushSupport BrushSupport
        {
            get { return enuBrushSupport.None; }
        }
        public override ICoreBrush GetBrush(enuBrushMode mode)
        {
            return null;
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            path.AddRectangle(this.Bounds);
        }
    }
}

