

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreScreen.cs
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
file:CoreScreen.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
CoreApplicationManager.Instance : ICore
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
namespace IGK.ICore.WinUI
{
    using IGK.ICore;using IGK.ICore.WinUI;
    public class CoreScreen
    {
        private static CoreScreen sm_instance;
        private int m_w;
        private int m_h;
        private float m_dpix;
        private float m_dpiy;
        private int m_bitcount;
        private int m_numberOfScreen;
        private CoreScreen()
        {
        }
        public static CoreScreen Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static CoreScreen()
        {
            sm_instance = new CoreScreen();
            InitDisplayInfo();
        }
        private static void InitDisplayInfo()
        {
           ICoreScreenInfo v_screenInfo = CoreApplicationManager.Application.GetScreenInfo();
           if (v_screenInfo != null)
           {
               Instance.m_dpix = v_screenInfo.DpiX;
               Instance.m_dpiy = v_screenInfo.DpiY;
               Instance.m_bitcount = v_screenInfo.BitCount;
               Instance.m_numberOfScreen = v_screenInfo.NumberOrScreen;
               Instance.m_w = v_screenInfo.Width;
               Instance.m_h = v_screenInfo.Height;
           }
        }
        /// <summary>
        /// get the max screen dpi
        /// </summary>
        public static float Dpi {
            get {
                return Math.Max(Instance.m_dpix, Instance.m_dpiy);
            }
        }
        public static float  DpiX { get { return Instance.m_dpix; } }
        public static float  DpiY { get{return Instance.m_dpiy ;}}
        #region ICoreScreen Members
        public int Width
        {
            get { return this.m_w; }
        }
        public int Height
        {
            get { return this.m_h; }
        }
        public int BitCount
        {
            get { return this.m_bitcount; }
        }
        public int NumberOfScreen {
            get {
                return this.m_numberOfScreen;
            }
        }
        public bool ChangeDisplay(int Width, int Height, int Bitcount)
        {
            //CoreScreenSetting setting = 
            //    GetCurrentDisplaySetting(System.Windows.Forms.Screen.PrimaryScreen.DeviceName );
            return false;
        }
        #endregion
        /// <summary>
        /// restore the reg setting
        /// </summary>
        public static void RestoreRegSetting()
        {
            ChangeDisplaySettings (null, 0);
        }
        public static DisplayDevice[] GetDisplayDevices()
        {
            List<DisplayDevice> v_dev = new List<DisplayDevice>();
            DISPLAYDEV_STRUCT m_out = new DISPLAYDEV_STRUCT ();
            m_out.cb = Marshal.SizeOf(m_out);
            int idDevNum = 0;
            while (EnumDisplayDevices(null, idDevNum, ref m_out, 0))
            {
                v_dev.Add( new DisplayDevice(idDevNum, m_out));
                idDevNum++;
            }
            return v_dev.ToArray();
        }
        public static CoreScreenSetting GetCurrentDisplaySetting(string devicename)
        {
            LPDEVMODE v_mode = new LPDEVMODE();
            v_mode.dmSize = (byte)Marshal.SizeOf(v_mode);
              bool v = EnumDisplaySettings(devicename,
               ENUM_CURRENT_SETTINGS,
               ref v_mode);
              if (v)
              {
                  return new CoreScreenSetting(v_mode);
              }
              return null;
        }
        public static CoreScreenSetting[] EnumDisplaySetting(string devicename)
        {
            LPDEVMODE v_mode = new LPDEVMODE();
            v_mode.dmSize =(byte) Marshal.SizeOf(v_mode);
            List<CoreScreenSetting> m_models = new List<CoreScreenSetting>();
            int cmode = 0;//initialize
            while (EnumDisplaySettings(devicename,
                  cmode ,
                  ref v_mode))
            {
                if (cmode != 0)
                    m_models.Add( new CoreScreenSetting( v_mode));
                cmode++;
            }
            return m_models.ToArray();
        
        }
        [DllImport("user32.dll")]
        static extern bool EnumDisplayDevices(string lpDevice, int idDevNum,
           ref DISPLAYDEV_STRUCT device, int flag);
        [DllImport("user32.dll")]
        static extern bool EnumDisplaySettings(string lpDevice, 
            int mode,
            ref LPDEVMODE device
            );
        [DllImport("user32.dll")]
        static extern bool EnumDisplaySettings(string lpDevice,
            int mode,
            IntPtr  device
            );
        [DllImport("user32.dll")]
        static extern int ChangeDisplaySettings(LPDEVMODECLASS devMode,
            int flag
            );
        [StructLayout  (LayoutKind.Sequential )]
        public struct DISPLAYDEV_STRUCT
        {
            internal int cb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            internal char[] DeviceName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
            internal char[] DeviceString;
            internal int StateFlags;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
            internal char[] DeviceID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
            internal char[] DeviceKey;
        }
        internal const int CCHDEVICENAME = 32;
        internal const int CCHFORMNAME =32;
        [StructLayout  (LayoutKind.Sequential )]
        public class LPDEVMODECLASS
        {
            internal LPDEVMODE m_info;
            public LPDEVMODECLASS(LPDEVMODE info)
            {
                this.m_info = info;
            }
        }
        [StructLayout  (LayoutKind.Sequential )]
        public struct LPDEVMODE {
          
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CCHDEVICENAME)]
            internal char[] dmDeviceName;
            internal byte dmSpecVersion;
            internal byte dmDriverVersion;
            internal byte dmSize;
            internal byte dmDriverExtra;
            internal int dmFields;
            //16
            /* union {
               // printer only fields 
               struct {*/
            short dmOrientation;
            short dmPaperSize;
            short dmPaperLength;
            short dmPaperWidth;
            short dmScale;
            short dmCopies;
            short dmDefaultSource;
            short dmPrintQuality;
         
            short dmColor;
            short dmDuplex;
            short dmYResolution;
            short dmTTOption;
            short dmCollate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CCHFORMNAME)]
            internal char[] dmFormName;
            internal int dmLogPixels;
            //for display property 
            internal int dmBitsPerPel;
            internal int dmPelsWidth;
            internal int dmPelsHeight;
            internal int dmDisplayFlags;
          
            internal int dmDisplayFrequency;
            //#if(WINVER >= 0x0400)
            internal int dmICMMethod;
            internal int dmICMIntent;
            internal int dmMediaType;
            internal int dmDitherType;
            internal int dmReserved1;
            internal int dmReserved2;
            //#if (WINVER >= 0x0500) || (_WIN32_WINNT >= 0x0400)
            internal int dmPanningWidth;
            internal int dmPanningHeight;
}

        internal const int DISPLAY_DEVICE_ATTACHED_TO_DESKTOP = 0x00000001;
        internal const int DISPLAY_DEVICE_MULTI_DRIVER      =  0x00000002;
        internal const int DISPLAY_DEVICE_PRIMARY_DEVICE    =  0x00000004;
        internal const int DISPLAY_DEVICE_MIRRORING_DRIVER  =  0x00000008;
        internal const int DISPLAY_DEVICE_VGA_COMPATIBLE    =  0x00000010;
        internal const int DISPLAY_DEVICE_REMOVABLE         =  0x00000020;
        internal const int DISPLAY_DEVICE_MODESPRUNED       =  0x08000000;
        internal const int DISPLAY_DEVICE_REMOTE            =  0x04000000;
        internal const int DISPLAY_DEVICE_DISCONNECT        =  0x02000000;
        internal const int DISPLAY_DEVICE_TS_COMPATIBLE     =  0x00200000;
        internal const int DISPLAY_DEVICE_UNSAFE_MODES_ON   =  0x00080000;
        internal const int DISPLAY_DEVICE_ACTIVE            =  0x00000001;
        internal const int DISPLAY_DEVICE_ATTACHED          =  0x00000002;
        internal const int CDS_DYNAMIC = 0x0;
        internal const int CDS_UPDATEREGISTRY = 0x00000001;
        internal const int CDS_TEST = 0x00000002;
        internal const int CDS_FULLSCREEN = 0x00000004;
        internal const int CDS_GLOBAL = 0x00000008;
        internal const int CDS_SET_PRIMARY = 0x00000010;
        internal const int CDS_VIDEOPARAMETERS = 0x00000020;
        internal const int CDS_RESET = 0x40000000;
        internal const int CDS_NORESET = 0x10000000;
        //reponse on changed display
        internal const int DISP_CHANGE_SUCCESSFUL = 0;
        internal const int DISP_CHANGE_RESTART = 1;
        internal const int DISP_CHANGE_FAILED = -1;
        internal const int DISP_CHANGE_BADMODE = -2;
        internal const int DISP_CHANGE_NOTUPDATED = -3;
        internal const int DISP_CHANGE_BADFLAGS = -4;
        internal const int DISP_CHANGE_BADPARAM = -5;
        [Flags ()]
        public enum DisplayFlag{
            AttachedToDesktop = DISPLAY_DEVICE_ATTACHED_TO_DESKTOP,
            MultiDriver = DISPLAY_DEVICE_MULTI_DRIVER ,
            PrimaryDevice = DISPLAY_DEVICE_PRIMARY_DEVICE ,
            MirroringDriver = DISPLAY_DEVICE_MIRRORING_DRIVER ,
            VGACompatible = DISPLAY_DEVICE_VGA_COMPATIBLE ,
            DeviceRemote = DISPLAY_DEVICE_REMOTE,
            DeviceDisconnect = DISPLAY_DEVICE_DISCONNECT ,
            TS_Compatible = DISPLAY_DEVICE_TS_COMPATIBLE ,
            ModeSpruned = DISPLAY_DEVICE_MODESPRUNED 
        }
        internal const int ENUM_CURRENT_SETTINGS = -1;
        internal const int ENUM_REGISTRY_SETTINGS = -2;
        public class DisplayDevice
        {
            private DISPLAYDEV_STRUCT m_deviceInfo;
            private int m_id;
            internal DisplayDevice(int id , DISPLAYDEV_STRUCT deviceInfo){
                this.m_deviceInfo = deviceInfo;
                this.m_id = id;
            }           
            public string Name
            {
                get { return GetString(m_deviceInfo.DeviceName); }
            }
            public string DeviceString {
                get { return GetString(m_deviceInfo.DeviceString); }
            }
            public string DeviceKey {
                get { return GetString(m_deviceInfo.DeviceKey); }
            }
            public string DeviceId {
                get { return GetString(m_deviceInfo.DeviceID); }
            }
            public DisplayFlag Flag {
                get { return (DisplayFlag)this.m_deviceInfo.StateFlags; }
            }
            private string GetString(char[] p)
            {
                StringBuilder sb = new StringBuilder();
                int i = 0;
                while (p[i]!='\0')
                {
                    sb.Append (p[i]);
                    i++;
                }                
                return sb.ToString();
            }
            public override string ToString()
            {
                return this.Name;
            }
        }
        internal static enuCoreScreenResponse ChangeDisplay(CoreScreenSetting setting, enuCoreScreenChangeRequest request)
        {
            enuCoreScreenResponse test =(enuCoreScreenResponse) ChangeDisplaySettings(new LPDEVMODECLASS(setting.m_devmode),
                (int)request );
            return test;
        }
    }
}

