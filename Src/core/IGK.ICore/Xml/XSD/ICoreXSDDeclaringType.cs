﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Xml.XSD
{
    public interface  ICoreXSDDeclaringType: ICoreXSDType
    {
        enuXSDType XSDType { get; }
    }
}
