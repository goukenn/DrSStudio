

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WsdlUtility.cs
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
using System.Text;

namespace IGK.DrSStudio.Wsdl
{
    public static class WsdlUtility
    {
        internal static string GetNamespace(enuWsdlOperationType enuWsdlOperationType)
        {
            switch (enuWsdlOperationType)
            {               
                case enuWsdlOperationType.Soap_1_2:
                    return WsdlConstant.SOAP_1_2;
                case enuWsdlOperationType.HttpGet:
                case enuWsdlOperationType.HttpPost:
                    return WsdlConstant.HTTP;         
            }
            return WsdlConstant.SOAP;
        }

        internal static string GetPrimitiveType(Type type)
        {
            if (type == typeof(string))
                return "string";
            if (type == typeof(int))
                return "int";
            if (type == typeof(float))
                return "float";
            if (type == typeof(double))
                return "double";

            return null;
        }
    }
}
