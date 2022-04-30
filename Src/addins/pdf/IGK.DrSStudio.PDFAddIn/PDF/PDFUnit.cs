

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFUnit.cs
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
file:PDFUnit.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using IGK.ICore.Data;

namespace IGK.PDF
{
    using IGK.ICore.WinUI;
    using IGK.ICore.ComponentModel;
    using IGK.ICore;
    using System.Diagnostics;
    //[TypeConverter (typeof(PDFUnitTypeConverter))]
    public class  PDFUnit :
        IPDFUnitCm ,
        IPDFUnitInch ,
        IPDFUnitMm ,
        IPDFUnitPica ,
        IPDFUnitPixel ,
        IPDFUnitPoint,
        IPDFUnitPercent,
        IPDFUnitEmSize
    {
        private float m_value;//unit in pixel
        private enuUnitType m_unitype; //unit type
        private float m_dpi; //unit dpi
        static readonly float sm_defaultDPI = 96.0f;
        public static readonly string EmptyPixel = "0px";


        static PDFUnit() {
            
          
        }
        /// <summary>
        /// get the unit type
        /// </summary>
        public enuUnitType UnitType {
            get { return this.m_unitype; }
        }
        /// <summary>
        /// get value int parent dim
        /// </summary>
        /// <param name="parentDim">in pixel</param>
        /// <param name="unit">init required</param>
        /// <returns></returns>
        public float GetValue(float parentDim, enuUnitType unit)
        {
            if (unit == enuUnitType.percent)
            {
                //parentDim -> 100%
                //x         -> this.value 
                return (parentDim * ((IPDFUnitPercent)this).Value) / 100.0f;
            }
            else
                return GetValue(unit);
        }
        /// <summary>
        /// get the value in the unit type
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public float GetValue(enuUnitType unit)
        {
            switch (unit)
            {
                case enuUnitType.px:
                    return ((IPDFUnitPixel)this).Value;
                case enuUnitType.pic:
                    return ((IPDFUnitPica)this).Value;
                case enuUnitType.cm :
                    return ((IPDFUnitCm)this).Value;
                case enuUnitType.mm:
                    return ((IPDFUnitMm)this).Value;
                case enuUnitType.inch:
                    return ((IPDFUnitInch)this).Value;
                case enuUnitType.pt:
                    return ((IPDFUnitPoint)this).Value;
                case enuUnitType.percent:
                    return ((IPDFUnitPercent)this).Value;
                case enuUnitType.em :
                    return ((IPDFUnitEmSize)this).Value;
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
        public PDFUnit()
        {
            this.m_dpi = sm_defaultDPI;
            this.m_unitype = enuUnitType.px;
            this.m_value = 0.0f;
        }
        public PDFUnit(float value, float dpi)
        {
            this.m_value = value;
            this.m_dpi = dpi;
            this.m_unitype = enuUnitType.px;
        }
        public PDFUnit(PDFUnit value, float dpi)
        {
            //change the dpi
            this.m_dpi = dpi;
            this.m_unitype = value.UnitType;
            this.Value = value.Value;
        }
        public float Value {
            get {
                switch (this.m_unitype)
                {
                    //case enuUnitType.px:
                    //case enuUnitType .percent :
                    //    return this.m_value;                        
                    case enuUnitType.pic:
                        return (this as IPDFUnitPica).Value;                        
                    case enuUnitType.cm:
                        return (this as IPDFUnitCm).Value;                        
                    case enuUnitType.mm:
                        return (this as IPDFUnitMm).Value;                        
                    case enuUnitType.inch:
                        return (this as IPDFUnitInch).Value;                        
                    case enuUnitType .pt :
                        return (this as IPDFUnitPoint).Value;
                    default:
                        break;
                }
                return ((IPDFUnitPixel)this).Value;
            }
            set {
                switch (this.m_unitype)
                {
                    case enuUnitType.px:
                    case enuUnitType.percent :
                        ((IPDFUnitPixel)this).Value = value;
                        break;
                    case enuUnitType .pt :
                        (this as IPDFUnitPoint).Value = value;
                        break;
                    case enuUnitType.pic:
                        (this as IPDFUnitPica).Value = value ;
                        break;
                    case enuUnitType.cm:
                        (this as IPDFUnitCm).Value =value ;
                        break;
                    case enuUnitType.mm:
                        (this as IPDFUnitMm).Value =value ;
                        break;
                    case enuUnitType.inch:
                        (this as IPDFUnitInch).Value =value ;
                        break;
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
                ((this.UnitType != enuUnitType.percent )  ? this.UnitType.ToString (): "%"));
        }
        /// <summary>
        /// return to string value
        /// </summary>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public string ToString(enuUnitType unitType)
        {
            switch (unitType)
            {
                case enuUnitType.px:
                    return string.Format("{0}{1}", ((IPDFUnitPixel)this).Value, unitType.ToString());                    
                case enuUnitType.pic:
                    return string.Format("{0}{1}", ((IPDFUnitPica)this).Value, unitType.ToString());                    
                case enuUnitType.cm:
                    return string.Format("{0}{1}", ((IPDFUnitCm)this).Value, unitType.ToString());
                case enuUnitType.mm:
                    return string.Format("{0}{1}", ((IPDFUnitMm)this).Value, unitType.ToString());
                case enuUnitType.inch:
                    return string.Format("{0}{1}", ((IPDFUnitInch)this).Value, unitType.ToString());
                case enuUnitType.pt:
                    return string.Format("{0}{1}", ((IPDFUnitPoint)this).Value, unitType.ToString());
                case enuUnitType.percent:
                    return string.Format("{0}{1}", this.Value, unitType.ToString());
                case enuUnitType.em:
                    return string.Format("{0}{1}", ((IPDFUnitEmSize)this).Value, unitType.ToString());
                default:
                    break;
            }
            return this.ToString();
        }
        #region IPDFUnitPica Members
        float IPDFUnitPica.Value
        {
            get
            {
                return (this as IPDFUnitInch).Value / 6.0f;
            }
            set
            {
                (this as IPDFUnitInch).Value = value * 6.0f;
            }
        }
        #endregion
        #region IPDFUnitMm Members
        float IPDFUnitMm.Value
        {
            get
            {
                return ((IPDFUnitCm)this).Value * 10f;
            }
            set
            {
                ((IPDFUnitCm)this).Value  =value / 10.0f;
            }
        }
        #endregion
        #region IPDFUnitInch Members
        float IPDFUnitInch.Value
        {
            get
            {
                return ((IPDFUnitPixel ) this).Value  / m_dpi;
            }
            set
            {
                ((IPDFUnitPixel)this).Value = value * m_dpi;
            }
        }
        #endregion
        #region IPDFUnitCm Members
        float IPDFUnitCm.Value
        {
            get
            {
                return ((IPDFUnitInch)this).Value * 2.54f;
            }
            set
            {
                ((IPDFUnitInch)this).Value = value / 2.54f;
            }
        }
        #endregion
        public static implicit operator PDFUnit(string value)
        {
            PDFUnit v_unit = new PDFUnit();
            v_unit.m_dpi = sm_defaultDPI;
            v_unit.m_value = 0;
            v_unit.m_unitype = enuUnitType.px;
            float m = 0.0f;
            if (!string.IsNullOrEmpty (value))
            {
                value = value.Trim().Replace(" ","");
                bool v = false;
                if (value.EndsWith("%"))
                {
                    v_unit.m_unitype = enuUnitType.percent;
                    value = value.Replace("%", "");
                    if (float.TryParse(value, out m))
                    {
                        v_unit.Value = m;
                    }
                    else
                        v_unit.Value = 0.0f;
                }
                else
                {
                    foreach (enuUnitType t in Enum.GetValues(typeof(enuUnitType)))
                    {
                        if (t == enuUnitType.percent) continue;
                        if (value.ToLower().EndsWith(t.ToString().ToLower ()))
                        {
                            v = true;
                            v_unit.m_unitype = t;
                            string v_sv = value.ToLower ().Replace(t.ToString().ToLower(), "").Trim();
                            if (float.TryParse(v_sv, out m))
                            {
                                v_unit.Value = m;
                               // bool vd = ((IPDFUnitPixel)v_unit) == ((IPDFUnitVector2i ) v_unit);
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
        public static implicit operator string(PDFUnit value)
        { return value.ToString(); }
        /// <summary>
        /// implicit operator to convert int to core unit
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit   operator PDFUnit(int value)
        {
            PDFUnit d = new PDFUnit();
            d.m_dpi = sm_defaultDPI ;
            d.m_value = value ;
            d.m_unitype = enuUnitType.px ;
            return d;
        }
        public static implicit operator PDFUnit(float value)
        {
            PDFUnit d = new PDFUnit();
            d.m_dpi = sm_defaultDPI;
            d.m_value = value;
            d.m_unitype = enuUnitType.px;
            return d;
        }
        public static implicit operator PDFUnit(double  value)
        {
            PDFUnit d = new PDFUnit();
            d.m_dpi = sm_defaultDPI;
            d.m_value =(float) Math.Round(value);
            d.m_unitype = enuUnitType.px;
            return d;
        }
        #region IPDFUnitVector2i Members
        float IPDFUnitPoint.Value
        {
            get
            {
                return (this as IPDFUnitPixel).Value * (72.0f / this.m_dpi); //(this.m_dpi / 72.0f);
            }
            set
            {
                (this as IPDFUnitPixel ).Value = value * ( this.m_dpi /72.0f) ;
            }
        }
        float IPDFUnitEmSize.Value {
            get { 
                return (this as IPDFUnitPoint).Value  * CoreScreen.DpiY / 72.0f;
            }
            set{
                float dpiy = CoreScreen.DpiY;
                if (dpiy> 0)
                (this as IPDFUnitPoint).Value  = value * 72.0f / dpiy  ;
            }
        }
        #endregion
        public static PDFUnit FromHundredOfInch(int hofInch, float dpi)
        {
            float i = hofInch / 100.0f;
            PDFUnit d = new PDFUnit();
            d.m_dpi = dpi;
            ((IPDFUnitInch)d).Value = i;
            return d;
        }
        public int GetUndrethOfInch()
        {
            return (int)System.Math.Floor(((IPDFUnitInch)this).Value * 100.0f);
        }
        float IPDFUnitPixel.Value
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

        public float GetPixel() { return ((IPDFUnitPixel)this).Value; }
        public float GetPoint() { return ((IPDFUnitPoint)this).Value; }
        public float GetCm() { return ((IPDFUnitCm)this).Value; }
        public float GetMm() { return ((IPDFUnitMm)this).Value; }
        public float GetPica() { return ((IPDFUnitPica)this).Value; }
        public float GetInch() { return ((IPDFUnitInch)this).Value; }
        public float GetEm() { return ((IPDFUnitEmSize )this).Value;}


    }
}

