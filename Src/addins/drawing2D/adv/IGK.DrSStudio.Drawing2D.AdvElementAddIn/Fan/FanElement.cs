

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FanElement.cs
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
file:FanElement.cs
*/
using System; 
using IGK.ICore; using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore.Codec;
using IGK.ICore.WinUI;
namespace IGK.DrSStudio.Drawing2D.Fan
{
    [FanElementAttribute("Fan", typeof (Mecanism))]
    public class FanElement : CustomPolygonElement 
    {
        private enuFanType m_FanType;
        private int m_PointSize;
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(enuFanType.Triangles)]
        public int PointSize
        {
            get { return m_PointSize; }
            set
            {
                if (m_PointSize != value)
                {
                    m_PointSize = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
      
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue (enuFanType.Triangles )]
        public enuFanType FanType
        {
            get { return m_FanType; }
            set
            {
                if (m_FanType != value)
                {
                    m_FanType = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public FanElement(){ 
          
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_FanType = enuFanType.Triangles;
            this.m_PointSize = 1;
        }
      protected override void InitGraphicPath(CoreGraphicsPath v_path)
{
    v_path.Reset();
    if (this.Points == null)
        return;
            int c = 0;
            switch (this.FanType )
            {
                case  enuFanType.Triangles:
                    for (c = 0; c < this.Points.Length - 2; c += 2)
                    {
                        v_path.AddPolygon(new Vector2f[]{
                    this.Points[0],
                    this.Points[c+1],
                    this.Points[c+2]
                });
                    }
                    if (c < this.Points.Length - 1)
                    {
                        v_path.AddLine(this.Points[c], this.Points[c + 1]);
                    }
                    break;
                case  enuFanType.TrianglesFan :
                //mbuil
                    for (c = 0; c < this.Points.Length - 2; ++c)
                    {
                        v_path.AddPolygon(new Vector2f[]{
                    this.Points[0],
                    this.Points[c+1],
                    this.Points[c+2]
                });
                    }
                    if (this.Closed && (this.Points.Length > 2))
                    {
                        v_path.AddPolygon(new Vector2f[]{
                    this.Points[0],
                    this.Points[1],
                    this.Points[this.Points.Length -1]});
                    }
                    else
                    {
                        if (c < this.Points.Length - 1)
                        {
                            v_path.AddLine(this.Points[c], this.Points[c + 1]);
                        }
                    }
                    break;
                case  enuFanType.TriangleStrip :
                    for (c = 0; c < this.Points.Length - 2; ++c)
                    {
                        v_path.AddPolygon(new Vector2f[]{
                    this.Points[c],
                    this.Points[c+1],
                    this.Points[c+2]
                });
                    }
                    if (this.Closed && (this.Points.Length > 2))
                    {
                        v_path.AddPolygon(new Vector2f[]{
                        this.Points[this.Points.Length -2],                    
                    this.Points[this.Points.Length -1],
                    this.Points[0],
                    });
                        v_path.AddPolygon(new Vector2f[]{
                        this.Points[this.Points.Length -1],
                    this.Points[0],                    
                    this.Points[1]});
                    }
                    else
                    {
                        if (c < this.Points.Length - 1)
                        {
                            v_path.AddLine(this.Points[c], this.Points[c + 1]);
                        }
                    }
                    break;
                case enuFanType.Points:
                    Rectanglef rc = Rectanglef.Empty;
                    for (int i = 0; i < this.Points.Length; i++)
                    {
                        rc = new Rectanglef(this.Points[i], Size2f.Empty );
                        rc.Inflate (m_PointSize,m_PointSize);
                        v_path.AddRectangle(rc);
                    }
                    break;
                case enuFanType.Quads :
                    if (this.Points.Length < 4)
                    {
                        Vector2f[] t = Array.ConvertAll<Vector2f, Vector2f>(this.Points,
                        new Converter<Vector2f, Vector2f>(delegate(Vector2f i)
                        {
                            return (Vector2f)i;
                        }));
                        v_path.AddLines(t);
                    }
                    else
                    {
                        for (int i = 0; i < (this.Points.Length - 3); i += 4)
                        {
                            v_path.AddPolygon(new Vector2f[] { 
                            this.Points[i],
                            this.Points[i+1],
                            this.Points[i+2],
                            this.Points[i+3],
                        });
                        }
                    }
                    break;
                case enuFanType.QuadsStrip :
                    if (this.Points.Length < 4)
                    {
                        Vector2f[] t = Array.ConvertAll <Vector2f , Vector2f>(this.Points,
                            new Converter<Vector2f,Vector2f> ( delegate (Vector2f i){
                                return (Vector2f )i; 
                            }));
                        v_path.AddLines(t);
                    }
                    else
                    {
                        for (int i = 0; i < (this.Points.Length - 3); i +=2)
                        {
                            v_path.AddPolygon(new Vector2f[] { 
                            this.Points[i],
                             this.Points[i+1],
                             this.Points[i+2],
                            this.Points[i+3]                           
                        });
                        }
                    }
                    break;
            }
            v_path.FillMode = this.FillMode;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            ICoreParameterGroup group =  parameters.AddGroup("FanProperty");
            group.AddItem(GetType().GetProperty("FanType"));
            return parameters;
        }
        new sealed class Mecanism : CustomPolygonElement.Mecanism
        { 
        }
    }
}

