

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CircleElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:CircleElement.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D ;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK ;
    using IGK.DrSStudio.Drawing2D ;
    using IGK.DrSStudio.Drawing2D.WinUI ;
    using IGK.DrSStudio.Codec;
    /// <summary>
    /// rendering circle element structure
    /// </summary>
    [Core2DDrawingStandardItem("Circle",
       typeof(Mecanism),
       Keys = Keys.C)]
    public class CircleElement : 
        CoreCircleElementBase ,
        ICore2DFillModeElement ,
        ICore2DCircleModelElement
    {
        private enuCircleModel m_Model;
        protected override void BuildBeforeResetTransform()
        {
            Matrix m = this.GetMatrix();
            if (m.IsIdentity) 
                return;
            this.Center = CoreMathOperation.TransformVector2fPoint(m, new Vector2f[] { this.Center })[0];
        }
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            IGK.DrSStudio.WinUI.ICoreParameterConfigCollections p = base.GetParameters(parameters);
            var g = p.AddGroup(CoreConstant.PARAM_DEFINITION);
            g.AddItem(GetType().GetProperty("enuFillMode"));
            g.AddItem (GetType ().GetProperty ("Model"));
            return p;
        }
        [CoreXMLAttribute()]  
        [CoreXMLDefaultAttributeValue (enuCircleModel .Ellipse )]
        /// <summary>
        /// get or set the circle model
        /// </summary>
        public enuCircleModel Model
        {
            get { return m_Model; }
            set
            {
                if (m_Model != value)
                {
                    m_Model = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);             
                }
            }
        }
        protected override void  GeneratePath()
        {
            if (Radius == null)
            {
                this.SetPath(null);
                return;
            }
            CoreGraphicsPath p = new CoreGraphicsPath();
            for (int i = 0; i < Radius .Length; i++)
            {
                if (this.m_Model == enuCircleModel.Ellipse)
                {
                    p.AddEllipse(Center,
                        new Vector2f (Radius[i], Radius[i]) );
                }
                else 
                p.AddRectangle(
                    CoreMathOperation.GetBounds(Center,
                    Radius[i]));
            }
            p.enuFillMode = this.enuFillMode;
            this.SetPath(p);
        }
        /// <summary>
        /// circle mecanism
        /// </summary>
        protected new class Mecanism : CoreCircleElementBase.Mecanism 
        { 
        }
    }
}

