

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidTargetManagerResourceItem.cs
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

namespace IGK.DrSStudio.Android.Tools
{
    /// <summary>
    /// represent a android resources item
    /// </summary>
    public class AndroidTargetManagerResourceItem
    {
        private string m_FileName;
        /// <summary>
        /// get the filename of this resoruces
        /// </summary>
        public string FileName
        {
            get { return m_FileName; }
        }

        private enuAndroidManagerResourceType m_ResourceType;

        public enuAndroidManagerResourceType ResourceType
        {
            get { return m_ResourceType; }
         
        }
        public AndroidTargetManagerResourceItem()
        {

        }
        public  static AndroidTargetManagerResourceItem LoadXMLResources(string f)
        {
            if (System.IO.File.Exists(f) == false)
                return null;
            //System.Xml.XmlReader r = System.Xml.XmlReader.Create(f);
            //System.Xml.XmlDocument d = new System.Xml.XmlDocument();
            //d.Load(r);
            //r.Close();

            AndroidTargetManagerResourceItem i = new AndroidTargetManagerResourceItem();
            i.m_FileName = f;
            i.m_ResourceType = enuAndroidManagerResourceType.XmlResource;
            return i;
        }
        public static AndroidTargetManagerResourceItem LoadImageFile(string filename)
        {
            if (System.IO.File.Exists(filename) == false)
                return null;
            
            AndroidTargetManagerResourceItem i = new AndroidTargetManagerResourceItem();
            i.m_FileName = filename;
            i.m_ResourceType = enuAndroidManagerResourceType.PictureResource;
            return i;
        }
    }
}
