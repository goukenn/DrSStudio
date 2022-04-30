

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreReflectionExtensions.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    public static class CoreReflectionExtensions
    {

        public static Type FindType(this AppDomain domain, string dllFile, string name)
        {
            dllFile  = dllFile.ToLower();
            foreach (Assembly item in domain.GetAssemblies())
            {
                try
                {
                    string l = item.Location.ToLower();
                    if ((l == dllFile) || System.IO.Path.GetFileName (l) == dllFile )
                    {
                        Type t = item.GetType(name, false, true);
                        if (t != null)
                            return t;
                    }
                }
                catch
                {
                }
            }
            return null;
        }
        
        static CoreReflectionExtensions() {

        }
        /// <summary>
        /// Used to visit method.
        /// </summary>
        /// <param name="methodBase">method base to visit</param>
        /// <param name="obj">instance or null object to visit. if null visited method must be static.</param>
        /// <param name="parameters">parameters of the current object</param>
        /// <returns>return value of the current method invocation. </returns>
        public static object Visit(this MethodBase methodBase, object obj, params object[] parameters)
        {
            if (methodBase == null)
                return null;
            
            MethodBase v_b = methodBase;
            RuntimeMethodHandle v_mh = methodBase.MethodHandle;
            List<Type> r = new List<Type>();
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i] == null)
                    r.Add(typeof(Nullable));
                else
                    r.Add(parameters[i].GetType());
            }
            Type v_t = obj != null ? obj.GetType() : methodBase.DeclaringType;
            try
            {
                MethodInfo v_m = v_t.GetMethod(v_b.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static,
                    null,
                    r.ToArray(), null);
                if ((v_m != null) && (v_m.MethodHandle != v_mh))
                {
                    return v_m.Invoke(obj, parameters);
                } 
            }
            catch (AmbiguousMatchException ex)
            {

                CoreLog.WriteDebug("Ambiguous found When Visiting the methods" + v_b.Name + " type :" + v_t + " : " + ex.Message);

            }
            catch (Exception ex)
            {
                CoreLog.WriteLine("Exception : " + ex.Message);
            }
            return null;
        }

        public static object Visit(this MethodBase methodBase, object obj, ParameterInfo[] info, params object[] parameters)
        {
            MethodBase v_b = methodBase;
            RuntimeMethodHandle v_mh = methodBase.MethodHandle;
            List<Type> r = new List<Type>();
            if ((info != null) && (parameters!=null) && (info.Length > 0) && (info.Length == parameters.Length))
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i] == null)
                    {
                        r.Add(info[i].ParameterType);
                    }
                    else
                        r.Add(parameters[i].GetType());
                }
            }
            Type v_t = obj != null ? obj.GetType() : methodBase.DeclaringType;
            try
            {
                MethodInfo v_m = v_t.GetMethod(v_b.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static,
                    null,
                    r.ToArray(), null);
                if ((v_m != null) && (v_m.MethodHandle != v_mh))
                {
                    return v_m.Invoke(obj, parameters);
                }
            }
            catch (AmbiguousMatchException ex)
            {

                CoreLog.WriteLine("Ambiguous found When Visiting the methods" + v_b.Name + " type :" + v_t + " : " + ex.Message);

            }
            catch (Exception ex)
            {
                CoreLog.WriteLine("Exception Ambiguous found : " + ex.Message);
            }
            return null;
        }

        public static object Visit(this MethodBase methodBase, object obj, Type[] info, params object[] parameters)
        {

           
            MethodBase v_b = methodBase;
            RuntimeMethodHandle v_mh = methodBase.MethodHandle;
            List<Type> r = new List<Type>();
            if ((info != null) && (parameters != null) && (info.Length > 0) && (info.Length == parameters.Length))
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (info[i] != null)
                    {
                        r.Add(info[i]);
                    }
                    else
                        r.Add(parameters[i].GetType());
                }
            }
            Type v_t = obj != null ? obj.GetType() : methodBase.DeclaringType;
            try
            {
                MethodInfo v_m = v_t.GetMethod(v_b.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static,
                    null,
                    r.ToArray(), null);
                if ((v_m != null) && (v_m.MethodHandle != v_mh))
                {         
                   
                    return v_m.Invoke(obj, parameters);
                }
            }
            catch (AmbiguousMatchException ex)
            {

                CoreLog.WriteLine("Ambiguous found When Visiting the methods" + v_b.Name + " type :" + v_t + " : " + ex.Message);

            }
            catch (Exception ex)
            {
                CoreLog.WriteLine("Exception Ambiguous found : " + ex.Message);
            }
            return null;
        }
    }
}
