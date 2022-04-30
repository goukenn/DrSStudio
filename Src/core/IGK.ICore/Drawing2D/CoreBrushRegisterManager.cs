

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreBrushRegister.cs
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
file:CoreBrushRegister.cs
*/

﻿using IGK.ICore;using IGK.ICore.Drawing2D;
using IGK.ICore.Tools;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    /// <summary>
    /// Represent the brush register
    /// </summary>
    public static class CoreBrushRegisterManager
    {        
        private static Dictionary<Colorf, ICorePen> sm_systemPen;
        private static CoreBrushSystemPenBrushOwner sm_sys_PenBrushOwner;

        static CoreBrushRegisterManager()
        {
            sm_systemPen = new Dictionary<Colorf, ICorePen>();
            sm_sys_PenBrushOwner = new CoreBrushSystemPenBrushOwner();
            if (CoreApplicationManager.Application !=null)
                CoreApplicationManager.ApplicationExit += new EventHandler(_ApplicationExit);
        }
        static void _ApplicationExit(object sender, EventArgs e)
        {
            ///free All Used Resources            
        }
        public struct HatchBrushStruct
        {
            private enuHatchStyle style;
            private Colorf cl1;
            private Colorf cl2;
            public HatchBrushStruct(enuHatchStyle style, Colorf cl1, Colorf cl2)
            {
                this.style = style;
                this.cl1 = cl1;
                this.cl2 = cl2;
            }
        }
        public struct DualLinearBrushInfoStruct { 
        }
        public static T GetPen<T>(Colorf cl) where T: class , IDisposable 
        {
            if (CoreApplicationManager.Application!=null)
                return CoreApplicationManager.Application.BrushRegister.GetPen<T>(cl) as T;
            return null;
        }
        public static T GetBrush<T>(Colorf cl) where T : class , IDisposable
        {
            if (CoreApplicationManager.Application != null)
                return CoreApplicationManager.Application.BrushRegister.GetBrush<T>(cl) as T;
            return null;
        }
        public static T GetBrush<T>(ICoreBitmap bitmap) where T : class , IDisposable 
        {
            if (CoreApplicationManager.Application != null)
            return CoreApplicationManager.Application.BrushRegister.GetBrush<T>(bitmap ) as T;
            return default (T);
        }
        public static T GetBrush<T>(ICoreBrush coreBrush) where T : class , IDisposable 
        {
            if ((CoreApplicationManager.Application != null)&& (coreBrush != null))
                return CoreApplicationManager.Application.BrushRegister.GetBrush<T>(coreBrush) as T;
            return default(T);
        }

        public static ICorePen GetPen(Colorf colorf)
        {
            if (sm_systemPen.ContainsKey(colorf))
            {
                return sm_systemPen[colorf];
            }
            CorePen cp = new CorePen(sm_sys_PenBrushOwner);
            sm_systemPen.Add(colorf, cp);
            return cp;

        }
        /// <summary>
        /// used to provide data for system pen
        /// </summary>
        sealed  class CoreBrushSystemPenBrushOwner : ICoreBrushOwner {
            public enuBrushSupport BrushSupport
            {
                get { return enuBrushSupport.All; }
            }

            public CoreGraphicsPath GetPath()
            {
                return null;
            }

            public Matrix GetMatrix()
            {
                return Matrix.Identity;
            }

            public ICoreBrush GetBrush(enuBrushMode enuBrushMode)
            {
                return null;
            }

            public event CoreWorkingObjectPropertyChangedEventHandler PropertyChanged;

            public string Id
            {
                get { return GetType().FullName; }
            }

            public CoreBrushSystemPenBrushOwner()
            {
                this.OnProperyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }

            private void OnProperyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
            {
                if (PropertyChanged != null) {
                    PropertyChanged(this, e);
                }
            }
        }
    }
}

