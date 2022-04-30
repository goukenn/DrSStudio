

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreSurfaceValidator.cs
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
file:CoreSurfaceValidator.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using IGK.ICore;using IGK.ICore.Codec;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// used to validate a surface
    /// </summary>
    public static class CoreSurfaceValidator
    {
        static List<string> sm_errorList;
        /// <summary>
        /// get error message after call the IsValidSurface method and return value of this method is false.
        /// </summary>
        public static string[] Error {
            get {
                return sm_errorList.ToArray();
            }
        }
        static CoreSurfaceValidator() {
            sm_errorList = new List<string>();
        }
        public static bool IsValidSurface(Type surfaceType)
        {
            sm_errorList.Clear();
            MethodInfo m = surfaceType.GetMethod(
                                    CoreConstant.METHOD_CREATESURFACE,
                                     System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic,
                                     null,
                                     new Type[]{
                                         typeof(ICoreProject),
                                         typeof (ICoreWorkingDocument[])
                                     },
                                     null);
            if (m == null)
                sm_errorList.Add("Error: Method CreateSurface(ICoreProject, IcoreWorkingDocument[]) not found");
            return (sm_errorList.Count == 0) ;            
        }
    }
}

