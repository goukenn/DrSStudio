

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TypeExtensions.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
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
﻿//////////////////////////////////////////////////////////////////////////////////
//	GDI+ Extensions
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://csharpgdiplus11.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.Reflection;

namespace IGK.DrSStudio.Imaging
{
    /// <summary>
    /// Provides internal extensions for the Type class.
    /// </summary>
    internal static class TypeExtensions
    {
        /// <summary>
        /// Invokes a non-public static method for a Type.
        /// </summary>
        /// <typeparam name="TResult">The return type of the static method.</typeparam>
        /// <param name="type">The Type to invoke the static method for.</param>
        /// <param name="methodName">The name of the static method.</param>
        /// <param name="args">The arguments for the static method.</param>
        /// <returns>The return value of the static method.</returns>
        /// <exception cref="System.InvalidOperationException">Static method could not be located.</exception>
        public static TResult InvokeStaticPrivateMethod<TResult>(this Type type, string methodName, params object[] args)
        {
            MethodInfo lmiInfo = type.GetMethod(methodName,
                BindingFlags.Static | BindingFlags.NonPublic);

            if (lmiInfo != null)
                return (TResult)(lmiInfo.Invoke(null, args));
            else
                throw new InvalidOperationException(
                    string.Format(
                        "Static method '{0}' could not be located in object type '{1}'.",
                        methodName, type.FullName));
        }
    }
}
