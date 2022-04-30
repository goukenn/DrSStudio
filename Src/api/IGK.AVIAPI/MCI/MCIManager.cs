

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MCIManager.cs
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
file:MCIManager.cs
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
using System.Runtime.InteropServices;
namespace IGK.AVIApi.MCI
{
    /*
     * 
     * 
     * configure playse to MCI device
     * 
     * */
    /// <summary>
    /// Represent the multimedia controller interface manager
    /// </summary>
    public static class MCIManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="identifier"></param>
        /// <param name="type">waveaudio, mpegaudio, audio, video</param>
        /// <param name="wait"></param>
        /// <param name="notify"></param>
        /// <returns></returns>
        public static bool Open(string filename,
            string identifier,
            string type,
            bool wait,
            bool notify)
        {
            string cmp = string.Format("open \"{0}\" type {1} alias {2}{3}",
                filename,
                type,
                identifier,
                string.Format("{0}{1}",
                wait ? " wait" : string.Empty,
                notify ? " notify" : string.Empty));
            try
            {
                //send a status to get if the device aloreade op
                string mode = GetStatus(identifier, "mode", 155);                
                return false ;
            }
            catch {                 
            }
            try
            {
                SendString(cmp);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
                return false;
        }
        public static bool Open(AVI.AVIFile avifile,
            string identifier,
            string type,
            bool wait,
            bool notify)
        {
            string s = 
                string.Format("{0}{1}",
                wait ? " wait" : string.Empty,
                notify ? " notify" : string.Empty);
            string cmd =
                string.Format(@"open @{0} type {1} alias {2}{3}",
                avifile.Handle,
                type,
                identifier,
                string.IsNullOrEmpty (s)?string.Empty : " "+s
                );
            try
            {
                SendString(cmd);
                return true;
            }
            catch(Exception Exception)
            {
                Console.WriteLine(Exception.Message);
            }
            return false;
        }
        public static void OpenWaveAudio(string name, string filename)
        {
            SendString(string.Format ("open \"{1}\" type waveaudio alias {0}",
            name, filename));
        }
        public static void Save(string name,string filename)
        {
            int i = MCIApi.mciSendString(string.Format("save {0} {1}",
                name, filename), IntPtr.Zero, 0, IntPtr .Zero);
            if (i != 0)
            {
                throw ThrowError(i);
            }
        }
        public static void Play(string id)
        {
            int i = MCIApi.mciSendString(string.Format("play {0} ",
                id), IntPtr.Zero, 0, IntPtr.Zero);
            if (i != 0)
            {
                throw ThrowError(i);
            }
        }
        /// <summary>
        /// throw exception
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private static Exception ThrowError(int i)
        {
            string t = string.Empty;
            IntPtr v_alloc = Marshal.AllocCoTaskMem(255);
            bool v = MCIApi.mciGetErrorString(i, v_alloc, 255);
            t = Marshal.PtrToStringAnsi(v_alloc);
            Marshal.FreeCoTaskMem(v_alloc);
            return new Exception(t);
        }
        /// <summary>
        /// send a string command to mci
        /// </summary>
        /// <param name="stringCommand"></param>
        public static void SendString(string stringCommand)
        {
            int i = MCIApi.mciSendString(stringCommand,IntPtr.Zero ,0,IntPtr.Zero );
            if (i != 0)
                throw ThrowError(i);
        }
        public static string SendString(string stringCommand, int bufferSize)
        {
            IntPtr v_ptr =  Marshal.AllocCoTaskMem(bufferSize);
            int i = MCIApi.mciSendString(stringCommand, v_ptr, bufferSize , IntPtr.Zero);
            if (i != 0)
                throw ThrowError(i);
            string s = Marshal.PtrToStringAnsi(v_ptr);
            Marshal.FreeCoTaskMem(v_ptr);
            return s;
        }
        public static string GetStatus(string identifier, string command, int leg)
        {
            string t = string.Empty;
            IntPtr v_alloc = Marshal.AllocCoTaskMem(leg);
            int i = MCIApi.mciSendString(string.Format ("status {0} {1}",identifier , command), v_alloc , leg , IntPtr.Zero);            
            if (i==0)
            t = Marshal.PtrToStringAnsi(v_alloc);
            Marshal.FreeCoTaskMem(v_alloc);
            if (i != 0)
                throw ThrowError(i);
            return t;
        }
        internal static void SetStatus(string identifier, string command, int value)
        {            
            int i = MCIApi.mciSendString(string.Format("set {0} status {1} {2}", identifier,command , value), IntPtr.Zero , 0, IntPtr.Zero);
            if (i != 0)
                throw ThrowError(i);
        }
        /// <summary>
        /// get the number  of device open
        /// </summary>
        /// <param name="device">name of the device, specify "all" to get all open device </param>
        /// <param name="leg"></param>
        /// <returns></returns>
        public static  int GetNumberOfDeviceOpen(string device, int leg)
        {
            string t =string.Empty ;
            IntPtr v_alloc = Marshal.AllocCoTaskMem(leg);
            int i = MCIApi.mciSendString(string.Format("sysinfo {0} quantity open",device ), v_alloc , leg , IntPtr.Zero);            
            if (i==0)
                t = Marshal.PtrToStringAnsi(v_alloc);
            Marshal.FreeCoTaskMem(v_alloc);
            if (i != 0)
                throw ThrowError(i);
            return int.Parse (t);
        }
        public static uint MMSystemVersion {
            get {
                return MCIApi.mmsystemGetVersion();
            }
        }
        internal static string Capabilitys(string command, string identifier)
        {
            string cmd = string.Format("capability {0} {1}",
                identifier,
                command);
            return GetString(cmd, 255);
        }
        internal static bool Capabilityb(string command, string identifier)
        {
            string cmd = string.Format("capability {0} {1}",
                identifier,
                command);
            return GetBoolean(cmd);            
        }
        internal static int Capabilityi(string command, string identifier)
        {
            string cmd = string.Format("capability {0} {1}",
              identifier,
              command);
            return int.Parse (GetString(cmd, 255));          
        }
        internal static bool GetBoolean(string command)
        {            
            return Convert.ToBoolean (GetString (command ,32));   
        }
        /// <summary>
        /// get the mci string command value
        /// </summary>
        /// <param name="command"></param>
        /// <param name="leg"></param>
        /// <returns></returns>
        internal static string GetString(string command, int leg)
        {
            string t = string.Empty;
            IntPtr v_alloc = Marshal.AllocCoTaskMem(leg);
            int i = MCIApi.mciSendString(command, v_alloc, leg, IntPtr.Zero);
            if (i == 0)
                t = Marshal.PtrToStringAnsi(v_alloc);
            Marshal.FreeCoTaskMem(v_alloc);
            if (i != 0)
                throw ThrowError(i);
            return t;
        }
        /// <summary>
        /// capture the file
        /// </summary>
        /// <param name="name"></param>
        /// <param name="wait"></param>
        /// <param name="notify"></param>
        public static void Capture(string name, bool wait, bool notify)
        {
            MCIManager.SendString(string.Format("capture {0} ",
              name,
              string.Format("{0}{1}",
              notify ? " notify" : string.Empty,
              wait ? " wait" : string.Empty
              ))
              );
        }
        public static void Record(string name,bool overwrite, bool wait, bool notify)
        {
            MCIManager.SendString(string.Format("record {0} ",
             name,
             string.Format("{0}{1}{2}",
             overwrite ?" overwrite": string.Empty ,
             notify ? " notify" : string.Empty,
             wait ? " wait" : string.Empty
             ))
             );
        }
        public static void Pause(string name, bool wait, bool notify)
        {
            MCIManager.SendString(string.Format("pause {0} ",
           name,
           string.Format("{0}{1}",
           notify ? " notify" : string.Empty,
           wait ? " wait" : string.Empty
           ))
           );
        }
        public static void Stop(string name, bool wait, bool notify)
        {
            MCIManager.SendString(string.Format("stop {0}",
              name,
              string.Format("{0}{1}",
              notify ? " notify" : string.Empty,
              wait ? " wait" : string.Empty
              ))
              );
        }
        /// <summary>
        /// close
        /// </summary>
        /// <param name="name"></param>
        public static void Close(string name)
        {
            MCIManager.SendString(string.Format("close {0}",
             name
             )
             );
        }
    }
}

