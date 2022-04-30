

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CorePen.cs
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
file:CorePen.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
namespace IGK.DrSStudio.Drawing2D
{
    public class CorePen : CoreBrush, ICorePen, ICorePenStruct
    {
        public override string ToString()
        {
            return "Pen";
        }
        CorePenStruct m_penStruct;
        internal static readonly CorePen Black;
        static CorePen()
        {
            Black = new CorePen(null);
            Black.SetSolidColor(IGK.Colorf.Black);
        }
        public enuPenAlignment Alignment
        {
            get { return m_penStruct.Alignment; }
            set { m_penStruct.Alignment = value; OnBrushDefinitionChanged(EventArgs.Empty); }
        }
        public enuDashCap DashCap
        {
            get { return m_penStruct.DashCap; }
            set { m_penStruct.DashCap = value; OnBrushDefinitionChanged(EventArgs.Empty); }
        }
        public ICoreLineStyle DashStyle
        {
            get { return m_penStruct.DashStyle; }
            set { m_penStruct.DashStyle = value; OnBrushDefinitionChanged(EventArgs.Empty); }
        }
        public ICoreLineCap EndCap
        {
            get { return m_penStruct.EndCap; }
            set { m_penStruct.EndCap = value; }
        }
        public enuLineJoin LineJoin
        {
            get { return m_penStruct.LineJoin; }
            set { m_penStruct.LineJoin = value; OnBrushDefinitionChanged(EventArgs.Empty); }
        }
        public float MiterLimit
        {
            get { return m_penStruct.MiterLimit; }
            set { m_penStruct.MiterLimit = value; OnBrushDefinitionChanged(EventArgs.Empty); }
        }
        public ICoreLineCap StartCap
        {
            get { return m_penStruct.StartCap; }
            set { m_penStruct.StartCap = value; OnBrushDefinitionChanged(EventArgs.Empty); }
        }
        public System.Single Width
        {
            get { return m_penStruct.Width; }
            set { m_penStruct.Width = value; OnBrushDefinitionChanged(EventArgs.Empty); }
        }
        #region ICorePen Members
        public CorePen(ICore2DBrushOwner owner)
            : base(owner)
        {
            this.m_penStruct = CorePenStruct.Empty;
            this.m_penStruct.Width = 1.0f;
        }
        public void SetStrokeProperty(float width,
       enuPenAlignment enuPenAlignment,
       enuDashStyle dashStyle,
       enuLineCap startCap,
       enuLineCap endCap,
       enuDashCap dashCap,
       enuLineJoin lineJoin,
       float mitterLimit)
        {
            if (width > 0) this.Width = width;
            this.Alignment = enuPenAlignment;
            this.DashStyle = CorePenDashStyle.GetLineStyle(dashStyle);
            this.StartCap = CorePenLineCap.GetLineCap(startCap);
            this.EndCap = CorePenLineCap.GetLineCap(endCap);
            this.DashCap = dashCap;
            this.LineJoin = lineJoin;
            this.MiterLimit = mitterLimit;
            OnBrushDefinitionChanged(EventArgs.Empty);
        }
        public void SetStrokeProperty(float width,
           enuPenAlignment enuPenAlignment,
           ICoreLineStyle dashStyle,
           ICoreLineCap startCap,
           ICoreLineCap endCap,
           enuDashCap dashCap,
           enuLineJoin lineJoin,
           float mitterLimit)
        {
            if (width > 0) this.Width = width;
            this.Alignment = enuPenAlignment;
            this.DashStyle = dashStyle;
            this.StartCap = startCap;
            this.EndCap = endCap;
            this.DashCap = dashCap;
            this.LineJoin = lineJoin;
            this.MiterLimit = mitterLimit;
            OnBrushDefinitionChanged(EventArgs.Empty);
        }
        #endregion
        public override string GetDefinition()
        {
            string b = base.GetDefinition();
            if (!this.m_penStruct.Equals(CorePenStruct.Empty))
            {
                //save only different property
                object v_o1 = null;
                object v_o2 = null;
                bool i = false;
                StringBuilder sb = new StringBuilder();
                sb.Append(b);
                foreach (PropertyInfo item in typeof(CorePenStruct).GetProperties(
                     BindingFlags.Instance | BindingFlags.Public
                    ))
                {
                    v_o1 = item.GetValue(this.m_penStruct, null);
                    v_o2 = item.GetValue(CorePenStruct.Empty, null);
                    if (!v_o1.Equals(v_o2))
                    {
                        if (i == true)
                            sb.Append(";");
                        else
                            i = true;
                        if (v_o1 is ICoreWorkingDefinitionObject)
                        {
                            sb.Append(string.Format("{0}:{1}", item.Name, (v_o1 as ICoreWorkingDefinitionObject).GetDefinition()));
                        }
                        else
                            sb.Append(string.Format("{0}:{1}", item.Name, v_o1));
                    }
                }
                return sb.ToString();
            }
            return b;
        }
        public override void CopyDefinition(string value)
        {
            string[] t = value.Split(';', ':');
            PropertyInfo item = null;
            CorePenStruct p = CorePenStruct.Empty;
            System.ComponentModel.TypeConverter v_conv = null;
            for (int i = 0; i < t.Length; i += 2)
            {
                item = typeof(CorePenStruct).GetProperty(t[i], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if ((item != null) && item.CanWrite)
                {
                    v_conv = System.ComponentModel.TypeDescriptor.GetConverter(item.PropertyType);
                    if (v_conv != null)
                    {
                        object v_v = v_conv.ConvertFromString(t[i + 1]);
                        switch (item.Name)
                        {
                            case "Width":
                                p.Width = float.Parse(t[i + 1]);
                                break;
                            case "Alignment":
                                p.Alignment = (enuPenAlignment)Enum.Parse(typeof(enuPenAlignment), t[i + 1]);
                                break;
                            case "StartCap":
                                p.StartCap = GetLineCap(t[i + 1]);
                                break;
                            case "EndCap":
                                p.EndCap = GetLineCap(t[i + 1]);
                                break;
                            case "LineJoin":
                                p.LineJoin = (enuLineJoin)Enum.Parse(typeof(enuLineJoin), t[i + 1]);
                                break;
                            case "MiterLimit":
                                p.MiterLimit = float.Parse(t[i + 1]);
                                break;
                            case "DashStyle":
                                p.DashStyle = GetDashStyle(t[i + 1]);
                                break;
                            case "DashCap":
                                p.DashCap = (enuDashCap)Enum.Parse(typeof(enuDashCap), t[i + 1]);
                                break;
                        }
                    }
                }
            }
            this.m_penStruct = p;
            base.CopyDefinition(value);
        }
        private ICoreLineStyle GetDashStyle(string p)
        {
            return CorePenDashStyle.GetLineStyle(p);
        }
        private ICoreLineCap GetLineCap(string p)
        {
            if (Enum.IsDefined(typeof(enuLineCap),
                p))
            {
                return CorePenLineCap.GetLineCap((enuLineCap)
                    Enum.Parse(typeof(enuLineCap),
                    p));
            }
            return CorePenLineCap.GetLineCap(p);
        }
        public override void Copy(ICoreBrush iCoreBrush)
        {
            Type t = typeof(CorePen);
            object v_obj = null;
            System.Reflection.BindingFlags flag =
                System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance;
            foreach (System.Reflection.FieldInfo f in
                t.GetFields(flag))
            {
                if (iCoreBrush.GetType().GetField(f.Name, flag) == null)
                    continue;
                switch (f.Name)
                {
                    case "m_owner":
                    case "m_Brush":
                    case "m_pen":
                        continue;
                }
                v_obj = f.GetValue(iCoreBrush);
                if (v_obj == null) continue;
                if (v_obj is ICloneable)
                {
                    v_obj = (v_obj as ICloneable).Clone();
                }
                //if (this.GetType ().GetProperty (f.Name )!=null)
                f.SetValue(this, v_obj);
            }
            base.Copy(iCoreBrush);
        }
        #region ICorePenStruct Members
        enuLineCap ICorePenStruct.EndCap 
        {
            get
            {
                return this.EndCap.LineCap;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        enuLineCap ICorePenStruct.StartCap
        {
            get
            {
                return this.StartCap.LineCap;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        enuDashStyle ICorePenStruct.DashStyle
        {
            get
            {
                return this.DashStyle.Style;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}

