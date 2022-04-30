

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CrossRectangleElement.cs
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
file:CrossRectangleElement.cs
*/
using System; 
using IGK.ICore; using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace IGK.DrSStudio.Drawing2D.BoxElement
{
    using IGK.ICore.Codec;
    using IGK.ICore.WinUI.Configuration;
    [IGKD2DDrawingAdvancedElement("CrossRectangle", typeof(Mecanism), ImageKey=CoreImageKeys.DE_CROSSREC_GKDS)]
    public sealed class CrossRectangleElement : RectangleElement  
    {
        private enuCrossType m_CrossType;
        [CoreXMLAttribute  ()]
        [CoreXMLDefaultAttributeValue (enuCrossType.Cross )]
        public enuCrossType CrossType
        {
            get { return m_CrossType; }
            set
            {
                if (m_CrossType != value)
                {
                    m_CrossType = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public CrossRectangleElement()
        {
            this.m_CrossType = enuCrossType.Cross;
        }
        
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {

            path.Reset();
            path.AddRectangle(this.Bounds);

            switch (this.CrossType)
            {
                case enuCrossType.SideWays:
                    path.AddLine(this.Bounds.Location, this.Bounds.BottomRight);
                    path.CloseFigure();
                    path.AddLine(this.Bounds.BottomLeft, this.Bounds.TopRight);
                    path.CloseFigure();
                    break;
                case enuCrossType.Cross :
                    path.AddLine(this.Bounds.MiddleTop , this.Bounds.MiddleBottom);
                    path.CloseFigure();
                    path.AddLine(this.Bounds.MiddleLeft , this.Bounds.MiddleRight );
                    path.CloseFigure();
                    break;
                case enuCrossType.Envelope :                    
                    path.AddLines(new Vector2f[] { this.Bounds.Location,this.Bounds.Center, this.Bounds.TopRight });
                    path.CloseFigure();
                    break;
            }
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            parameters.AddGroup(CoreConstant.PARAM_DEFINITION).AddItem(GetType().GetProperty("CrossType"));
            return parameters;
        }
        public new class Mecanism : RectangleElement.Mecanism
        { 
        }
    }
}

