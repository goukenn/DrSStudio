

using IGK.ICore.Resources;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: QRCodeResources.cs
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
file:QRCodeResources.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.QRCodeLib
{
    /// <summary>
    /// represent a QR code Resources
    /// </summary>
    internal class QRCodeResources
    {
        internal static Byte[] GetResource(string fileName)
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {

                var g = CoreResources.GetResource(fileName);
                return g;

            }
            object o = Properties.Resources.ResourceManager.GetObject(fileName);
            return o as Byte[];            
        }
    }
}

