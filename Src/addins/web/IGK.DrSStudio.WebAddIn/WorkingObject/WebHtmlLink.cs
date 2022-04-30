

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebHtmlLink.cs
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
file:WebHtmlLink.cs
*/
using IGK.ICore.Actions;
 using IGK.ICore.Drawing2D.Mecanism;
 using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.MecanismActions;
namespace IGK.DrSStudio.WebAddIn.WorkingObject
{
    [WebElementAttribute("WebHtmlLink", typeof(Mecanism))]
    class WebHtmlLink : WebHtmlElementBase
    {
        Core2DDrawingLayeredElement m_Target;
        private HtmlStringElement m_text;
     
        private string m_Href;
        private string m_HrefTarget;

        /// <summary>
        /// get or set the text of the element
        /// </summary>
        public string Text
        {
            get { return m_text.Text; }
            set
            {
                if (m_text.Text != value)
                {
                    m_text.Text = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        public override void Dispose()
        {
            if (this.m_text != null)
            {
                this.m_text.Dispose();
                this.m_text = null;
            }
            base.Dispose();
        }
        public WebHtmlLink()
        {
            this.m_HrefTarget = "_blank";
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue("_blank")]    
        [Category(WebConstant.HTML_PROPERTY)]
        public string HrefTarget
        {
            get { return m_HrefTarget; }
            set
            {
                if (m_HrefTarget != value)
                {
                    m_HrefTarget = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(null)]
        [Category(WebConstant.HTML_PROPERTY)]
        /// <summary>
        /// get or set the href string
        /// </summary>
        public string Href
        {
            get { return m_Href; }
            set
            {
                if (m_Href != value)
                {
                    m_Href = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var group = parameters.AddGroup("Info");
            group.AddItem(GetType().GetProperty("Href"));
            group.AddItem(GetType().GetProperty("HrefTarget"));
            return parameters;
        }
        /// <summary>
        /// get or set the target
        /// </summary>
        public Core2DDrawingLayeredElement Target {
            get {
                return this.m_Target;
            }
        }
        public override bool CanRenderShadow
        {
            get
            {
                return false;
            }
        }
        protected override void Translate(float dx, float dy, enuMatrixOrder order, bool temp)
        {
            if (!temp )
                this.m_Target.Translate (dx, dy, order);
        }
        public override bool CanRotate
        {
            get
            {
                return false;
            }
        }
        public override bool CanScale
        {
            get
            {
                return false;
            }
        }

        public override void Visit(ICore2DDrawingVisitor visitor)
        {
            this.m_text.Visit(visitor);
        }
   
        public new  class Mecanism : Core2DDrawingSurfaceMecanismBase<WebHtmlLink>
        {
            private WebHtmlLink  m_CurrentLink;
            public WebHtmlLink  CurrentLink
            {
                get { return m_CurrentLink; }
            }
            class UndoAction : CoreMecanismActionBase
            {
                private Mecanism mecanism;
                public UndoAction(Mecanism mecanism)
                {
                    this.mecanism = mecanism;
                }
                protected override bool PerformAction()
                {
                    this.mecanism.CurrentLayer.Elements.Remove (this.mecanism.CurrentLink);
                    this.mecanism.CurrentLayer.Elements.Add (this.mecanism.CurrentLink.Target);
                    this.mecanism.m_CurrentLink = null;
                    return true;
                }
            }
            protected override void GenerateActions()
            {
                base.GenerateActions();
                this.AddAction(enuKeys.Escape, new UndoAction(this));
            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                if (e.Button == enuMouseButtons.Left)
                {
                    this.SelectOne (e.FactorPoint );
                    if ((this.CurrentLayer.SelectedElements.Count == 1) && !(this.CurrentLayer.SelectedElements[0] is WebHtmlLink ))
                    {
                        WebHtmlLink link = new WebHtmlLink();
                        link.m_Target = this.CurrentLayer.SelectedElements[0] as Core2DDrawingLayeredElement ;
                        link.InitElement();
                        this.CurrentLayer.Elements.Remove(link.Target);
                        this.CurrentLayer.Elements.Add(link);
                        this.CurrentLayer.Select(link);
                        this.m_CurrentLink = link;
                        this.Element = link;
                    }
                }
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {                
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
            }
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
            if (this.m_Target != null)
                path.AddRectangle(this.m_Target.GetDisplayBounds());
           
        }
      
     
    }
}

