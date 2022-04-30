

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebLengthUnit.cs
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
file:WebLengthUnit.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WebProjectAddIn
{
    interface IWebEmUnit
    {
        float Value { get; set; }
    }
    /// <summary>
    /// represent a web length
    /// </summary>
    public class  WebLengthUnit:
        ICoreUnitPixel,
        ICoreUnitPoint,
        ICoreUnitPercent,
        IWebEmUnit,
        ICoreUnitInch
    {
        private float m_value;//unit in pixel
        private enuWebUnitType m_unitype; //unit type
        private float m_dpi; //unit dpi
        static readonly float sm_defaultDPI = 96.0f;
        /// <summary>
        /// get the unit type
        /// </summary>
        public enuWebUnitType UnitType {
            get { return this.m_unitype; }
        }
        /// <summary>
        /// get value int parent dim
        /// </summary>
        /// <param name="parentDim">in pixel</param>
        /// <param name="unit">init required</param>
        /// <returns></returns>
        public float GetValue(float parentDim, enuWebUnitType unit)
        {
            if (unit == enuWebUnitType.percent)
            {
                //parentDim -> 100%
                //x         -> this.value 
                return (parentDim * ((ICoreUnitPercent)this).Value) / 100.0f;
            }
            else
                return GetValue(unit);
        }
        public WebLengthUnit()
        {
            this.m_unitype = enuWebUnitType.px;
            this.m_value = 0.0f;
            this.m_dpi = sm_defaultDPI;
        }
        /// <summary>
        /// get the value in the unit type
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public float GetValue(enuWebUnitType unit)
        {
            switch (unit)
            {
                case enuWebUnitType.px:
                    return ((ICoreUnitPixel)this).Value;
                //case enuWebUnitType.pic:
                //    return ((ICoreUnitPica)this).Value;
                //case enuWebUnitType.cm:
                //    return ((IWebLengthUnitCm)this).Value;
                //case enuWebUnitType.mm:
                //    return ((IWebLengthUnitMm)this).Value;
                case enuWebUnitType.inch:
                    return ((ICoreUnitInch)this).Value;                    
                case enuWebUnitType.pt:
                    return ((ICoreUnitPoint)this).Value;   
                case enuWebUnitType.em :
                    return ((IWebEmUnit)this).Value;
                case enuWebUnitType.percent:
                    return ((ICoreUnitPercent)this).Value;
                default:
                    break;
            }
            return 0.0f;
        }
        /// <summary>
        /// get the unit dpi
        /// </summary>
        public float Dpi {
            get { return this.m_dpi; }
        }
        public WebLengthUnit(float value, float dpi)
        {
            this.m_value = value;
            this.m_dpi = dpi;
            this.m_unitype = enuWebUnitType.px;
        }
        public WebLengthUnit(WebLengthUnit value, float dpi)
        {
            //change the dpi
            this.m_dpi = dpi;
            this.m_unitype = value.UnitType;
            this.m_value = value.m_value ;
        }
        public float Value {
            get {
                switch (this.m_unitype)
                {
                    case enuWebUnitType.px:
                    case enuWebUnitType.percent:
                        return this.m_value;                        
                    //case enuWebUnitType.pic:
                    //    return (this as ICoreUnitPica).Value;                        
                    //case enuWebUnitType.cm:
                    //    return (this as ICoreUnitCm).Value;                        
                    //case enuWebUnitType.mm:
                    //    return (this as ICoreUnitMm).Value;                        
                    //case enuWebUnitType.inch:
                    //    return (this as ICoreUnitInch).Value;                        
                    case enuWebUnitType .pt :
                        return (this as ICoreUnitPoint).Value;
                    case enuWebUnitType.em:
                        return ((IWebEmUnit)this).Value;
                    default:
                        break;
                }
                return ((ICoreUnitPixel)this).Value;
            }
            set {
                switch (this.m_unitype)
                {
                    case enuWebUnitType.px:
                        ((ICoreUnitPixel)this).Value = value;
                        break;
                    case enuWebUnitType.percent:
                        ((ICoreUnitPercent)this).Value = value;
                        break;
                    case enuWebUnitType .pt :
                        (this as ICoreUnitPoint).Value = value;
                        break;
                    case enuWebUnitType.em :
                        ((IWebEmUnit)this).Value = value ;
                        break ;
                    //case enuWebUnitType.pic:
                    //    (this as ICoreUnitPica).Value = value ;
                    //    break;
                    //case enuWebUnitType.cm:
                    //    (this as ICoreUnitCm).Value =value ;
                    //    break;
                    //case enuWebUnitType.mm:
                    //    (this as ICoreUnitMm).Value =value ;
                    //    break;
                    case enuWebUnitType.inch:
                        (this as ICoreUnitInch).Value =value ;
                        break;
                    //case enuWebUnitType.
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// override to string meethod
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}{1}",
                this.Value,
                ((this.UnitType != enuWebUnitType.percent )  ? this.UnitType.ToString (): "%"));
        }
        /// <summary>
        /// return to string value
        /// </summary>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public string ToString(enuWebUnitType unitType)
        {
            switch (unitType)
            {
                case enuWebUnitType.px:
                    return string.Format("{0}{1}", ((ICoreUnitPixel)this).Value, unitType.ToString());                    
                //case enuWebUnitType.pic:
                //    return string.Format("{0}{1}", ((IWebLengthUnitPica)this).Value, unitType.ToString());                    
                //case enuWebUnitType.cm:
                //    return string.Format("{0}{1}", ((IWebLengthUnitCm)this).Value, unitType.ToString());
                //case enuWebUnitType.mm:
                //    return string.Format("{0}{1}", ((IWebLengthUnitMm)this).Value, unitType.ToString());
                //case enuWebUnitType.inch:
                //    return string.Format("{0}{1}", ((IWebLengthUnitInch)this).Value, unitType.ToString());
                case enuWebUnitType.pt:
                    return string.Format("{0}{1}", ((ICoreUnitPoint)this).Value, unitType.ToString());
                case enuWebUnitType.percent:
                    return string.Format("{0}{1}", this.Value, unitType.ToString());
                case enuWebUnitType.em :
                    return string.Format("{0}{1}", ((IWebEmUnit )this).Value, unitType.ToString());
                default:
                    break;
            }
            return this.ToString();
        }
        #region IWebLengthUnitPica Members
        //float ICoreUnitPica.Value
        //{
        //    get
        //    {
        //        return (this as IWebLengthUnitInch).Value / 6.0f;
        //    }
        //    set
        //    {
        //        (this as IWebLengthUnitInch).Value = value * 6.0f;
        //    }
        //}
        #endregion
        //#region IWebLengthUnitMm Members
        //float IWebLengthUnitMm.Value
        //{
        //    get
        //    {
        //        return ((IWebLengthUnitCm)this).Value * 10f;
        //    }
        //    set
        //    {
        //        ((IWebLengthUnitCm)this).Value  =value / 10.0f;
        //    }
        //}
        //#endregion
        //#region IWebLengthUnitInch Members
        //float IWebLengthUnitInch.Value
        //{
        //    get
        //    {
        //        return ((IWebLengthUnitPixel ) this).Value  / m_dpi;
        //    }
        //    set
        //    {
        //        ((IWebLengthUnitPixel)this).Value = value * m_dpi;
        //    }
        //}
        //#endregion
        //#region IWebLengthUnitCm Members
        //float IWebLengthUnitCm.Value
        //{
        //    get
        //    {
        //        return ((IWebLengthUnitInch)this).Value * 2.54f;
        //    }
        //    set
        //    {
        //        ((IWebLengthUnitInch)this).Value = value / 2.54f;
        //    }
        //}
        //#endregion
        public static implicit operator WebLengthUnit(int pixvalue)
        {
            WebLengthUnit d = new WebLengthUnit();
            d.m_dpi = sm_defaultDPI;
            d.m_value = pixvalue;
            d.m_unitype = enuWebUnitType.px;
            return d;
        }
        public static implicit operator WebLengthUnit(string value)
        {
            WebLengthUnit   v_unit = new WebLengthUnit();
            v_unit.m_dpi = sm_defaultDPI;
            v_unit.m_value = 0;
            v_unit.m_unitype = enuWebUnitType.px;
            float m = 0.0f;
            if (!string.IsNullOrEmpty (value))
            {
                value = value.Trim().Replace(" ","");
                bool v = false;
                if (value.EndsWith("%"))
                {
                    v_unit.m_unitype = enuWebUnitType.percent;
                    value = value.Replace("%", "");
                    if (float.TryParse(value, out m))
                    {
                        v_unit.m_value = m;
                    }
                    else
                        v_unit.Value = 0.0f;
                }
                else
                {
                    foreach (enuWebUnitType t in Enum.GetValues(typeof(enuWebUnitType)))
                    {
                        if (t == enuWebUnitType.percent) continue;
                        if (value.ToLower().EndsWith(t.ToString().ToLower ()))
                        {
                            v = true;
                            v_unit.m_unitype = t;
                            string v_sv = value.ToLower ().Replace(t.ToString().ToLower(), "").Trim();
                            if (float.TryParse(v_sv, out m))
                            {
                                v_unit.Value = m;
                               // bool vd = ((IWebLengthUnitPixel)v_unit) == ((IWebLengthUnitPoint ) v_unit);
                            }
                            else
                                v_unit.Value = 0.0f;
                            break;
                        }
                    }
                }
                if (v == false)
                {
                    //consider as pixel
                    if (float.TryParse(value, out m))
                    {
                        v_unit.Value = m;
                    }
                    else
                        v_unit.Value = 0.0f;
                } 
            }
           // float vc = v_unit.Value;
            return v_unit;
        }
        public static implicit operator string(WebLengthUnit value)
        { return value.ToString(); }
        #region IWebLengthUnitPoint Members
        float ICoreUnitPoint.Value
        {
            get
            {
                return (this as ICoreUnitPixel).Value * this.m_dpi / 72.0f;
            }
            set
            {
                (this as ICoreUnitPixel).Value = value * 72 / this.m_dpi;
            }
        }
        #endregion
        public static WebLengthUnit FromHundredOfInch(int hofInch, float dpi)
        {
            float i = hofInch / 100.0f;
            WebLengthUnit d = new WebLengthUnit();
            d.m_dpi = dpi;
            //((CoreUniUnitInch)d).Value = i;
            return d;
        }
        public int GetUndrethOfInch()
        {
            return (int)System.Math.Floor(((ICoreUnitInch)this).Value * 100.0f);
        }
        #region IWebLengthUnitPixel Members
        float ICoreUnitPixel.Value
        {
            get
            {
                return this.m_value;
            }
            set
            {
                this.m_value = value;
            }
        }
        #endregion
        float IWebEmUnit.Value
        {
            get
            {
                return ((ICoreUnitPixel)this).Value * 1/16.0f;
            }
            set
            {
                ((ICoreUnitPixel)this).Value = value * 16;
            }
        }
        float ICoreUnitPercent.Value
        {
            get
            {
                return this.m_value;
            }
            set
            {
                this.m_value = value;
            }
        }
    }
}

