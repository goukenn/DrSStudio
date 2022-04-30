

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreUnit.cs
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
file:CoreUnit.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using IGK.ICore.Data;

namespace IGK.ICore
{
    using IGK.ICore.WinUI;
    using IGK.ICore.ComponentModel;
    using IGK.ICore;
    using System.Diagnostics;
    [TypeConverter (typeof(CoreUnitTypeConverter))]
    public class  CoreUnit :
        ICoreUnitCm ,
        ICoreUnitInch ,
        ICoreUnitMm ,
        ICoreUnitPica ,
        ICoreUnitPixel ,
        ICoreUnitPoint,
        ICoreUnitPercent,
        ICoreUnitEmSize
    {
        private float m_value;//unit in pixel
        private enuUnitType m_unitype; //unit type
        private float m_dpi; //unit dpi
        static readonly float sm_defaultDPI = 96.0f;
        public static readonly string EmptyPixel = "0px";


        static CoreUnit() {
            
            if (CoreSystemEnvironment.DesignMode)
            {
                
            }
            else
            {
                Debug.Assert(CoreApplicationManager.Application != null, "CoreUnit::CoreApplication is Null.");
                var i = CoreApplicationManager.Application.GetScreenInfo();
                if (i != null)
                {
                    sm_defaultDPI = Math.Min(i.DpiX, i.DpiY);
                }
            }
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
                return (parentDim * ((ICoreUnitPercent)this).Value) / 100.0f;
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
                    return ((ICoreUnitPixel)this).Value;
                case enuUnitType.pic:
                    return ((ICoreUnitPica)this).Value;
                case enuUnitType.cm:
                    return ((ICoreUnitCm)this).Value;
                case enuUnitType.mm:
                    return ((ICoreUnitMm)this).Value;
                case enuUnitType.inch:
                    return ((ICoreUnitInch)this).Value;
                case enuUnitType.pt:
                    return ((ICoreUnitPoint)this).Value;
                case enuUnitType.percent:
                    return ((ICoreUnitPercent)this).Value;
                case enuUnitType.em :
                    return ((ICoreUnitEmSize)this).Value;
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
        public CoreUnit()
        {
            this.m_dpi = sm_defaultDPI;
            this.m_unitype = enuUnitType.px;
            this.m_value = 0.0f;
        }
        public CoreUnit(float value, float dpi)
        {
            this.m_value = value;
            this.m_dpi = dpi;
            this.m_unitype = enuUnitType.px;
        }
        public CoreUnit(CoreUnit value, float dpi)
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
                        return (this as ICoreUnitPica).Value;                        
                    case enuUnitType.cm:
                        return (this as ICoreUnitCm).Value;                        
                    case enuUnitType.mm:
                        return (this as ICoreUnitMm).Value;                        
                    case enuUnitType.inch:
                        return (this as ICoreUnitInch).Value;                        
                    case enuUnitType .pt :
                        return (this as ICoreUnitPoint).Value;
                    default:
                        break;
                }
                return ((ICoreUnitPixel)this).Value;
            }
            set {
                switch (this.m_unitype)
                {
                    case enuUnitType.px:
                    case enuUnitType.percent :
                        ((ICoreUnitPixel)this).Value = value;
                        break;
                    case enuUnitType .pt :
                        (this as ICoreUnitPoint).Value = value;
                        break;
                    case enuUnitType.pic:
                        (this as ICoreUnitPica).Value = value ;
                        break;
                    case enuUnitType.cm:
                        (this as ICoreUnitCm).Value =value ;
                        break;
                    case enuUnitType.mm:
                        (this as ICoreUnitMm).Value =value ;
                        break;
                    case enuUnitType.inch:
                        (this as ICoreUnitInch).Value =value ;
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
                    return string.Format("{0}{1}", ((ICoreUnitPixel)this).Value, unitType.ToString());                    
                case enuUnitType.pic:
                    return string.Format("{0}{1}", ((ICoreUnitPica)this).Value, unitType.ToString());                    
                case enuUnitType.cm:
                    return string.Format("{0}{1}", ((ICoreUnitCm)this).Value, unitType.ToString());
                case enuUnitType.mm:
                    return string.Format("{0}{1}", ((ICoreUnitMm)this).Value, unitType.ToString());
                case enuUnitType.inch:
                    return string.Format("{0}{1}", ((ICoreUnitInch)this).Value, unitType.ToString());
                case enuUnitType.pt:
                    return string.Format("{0}{1}", ((ICoreUnitPoint)this).Value, unitType.ToString());
                case enuUnitType.percent:
                    return string.Format("{0}{1}", this.Value, unitType.ToString());
                case enuUnitType.em:
                    return string.Format("{0}{1}", ((ICoreUnitEmSize)this).Value, unitType.ToString());
                default:
                    break;
            }
            return this.ToString();
        }
        #region ICoreUnitPica Members
        float ICoreUnitPica.Value
        {
            get
            {
                return (this as ICoreUnitInch).Value / 6.0f;
            }
            set
            {
                (this as ICoreUnitInch).Value = value * 6.0f;
            }
        }
        #endregion
        #region ICoreUnitMm Members
        float ICoreUnitMm.Value
        {
            get
            {
                return ((ICoreUnitCm)this).Value * 10f;
            }
            set
            {
                ((ICoreUnitCm)this).Value  =value / 10.0f;
            }
        }
        #endregion
        #region ICoreUnitInch Members
        float ICoreUnitInch.Value
        {
            get
            {
                return ((ICoreUnitPixel ) this).Value  / m_dpi;
            }
            set
            {
                ((ICoreUnitPixel)this).Value = value * m_dpi;
            }
        }
        #endregion
        #region ICoreUnitCm Members
        float ICoreUnitCm.Value
        {
            get
            {
                return ((ICoreUnitInch)this).Value * 2.54f;
            }
            set
            {
                ((ICoreUnitInch)this).Value = value / 2.54f;
            }
        }
        #endregion
        public static implicit operator CoreUnit(string value)
        {
            CoreUnit v_unit = new CoreUnit();
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
                               // bool vd = ((ICoreUnitPixel)v_unit) == ((ICoreUnitVector2i ) v_unit);
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
        public static implicit operator string(CoreUnit value)
        { return value.ToString(); }
        /// <summary>
        /// implicit operator to convert int to core unit
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit   operator CoreUnit(int value)
        {
            CoreUnit d = new CoreUnit();
            d.m_dpi = sm_defaultDPI ;
            d.m_value = value ;
            d.m_unitype = enuUnitType.px ;
            return d;
        }
        public static implicit operator CoreUnit(float value)
        {
            CoreUnit d = new CoreUnit();
            d.m_dpi = sm_defaultDPI;
            d.m_value = value;
            d.m_unitype = enuUnitType.px;
            return d;
        }
        public static implicit operator CoreUnit(double  value)
        {
            CoreUnit d = new CoreUnit();
            d.m_dpi = sm_defaultDPI;
            d.m_value =(float) Math.Round(value);
            d.m_unitype = enuUnitType.px;
            return d;
        }
        #region ICoreUnitVector2i Members
        float ICoreUnitPoint.Value
        {
            get
            {
                return (this as ICoreUnitPixel).Value * (72.0f / this.m_dpi); //(this.m_dpi / 72.0f);
            }
            set
            {
                (this as ICoreUnitPixel ).Value = value * ( this.m_dpi /72.0f) ;
            }
        }
        float ICoreUnitEmSize.Value {
            get { 
                return (this as ICoreUnitPoint).Value  * CoreScreen.DpiY / 72.0f;
            }
            set{
                float dpiy = CoreScreen.DpiY;
                if (dpiy> 0)
                (this as ICoreUnitPoint).Value  = value * 72.0f / dpiy  ;
            }
        }
        #endregion
        public static CoreUnit FromHundredOfInch(int hofInch, float dpi)
        {
            float i = hofInch / 100.0f;
            CoreUnit d = new CoreUnit();
            d.m_dpi = dpi;
            ((ICoreUnitInch)d).Value = i;
            return d;
        }
        public int GetUndrethOfInch()
        {
            return (int)System.Math.Floor(((ICoreUnitInch)this).Value * 100.0f);
        }
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

        public float GetPixel() { return ((ICoreUnitPixel)this).Value; }
        public float GetPoint() { return ((ICoreUnitPoint)this).Value; }
        public float GetCm() { return ((ICoreUnitCm)this).Value; }
        public float GetMm() { return ((ICoreUnitMm)this).Value; }
        public float GetPica() { return ((ICoreUnitPica)this).Value; }
        public float GetInch() { return ((ICoreUnitInch)this).Value; }
        public float GetEm() { return ((ICoreUnitEmSize )this).Value;}


    }
}

