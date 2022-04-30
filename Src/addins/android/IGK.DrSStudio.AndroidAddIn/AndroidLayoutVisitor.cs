

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidLayoutVisitor.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore.Codec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android
{
    using IGK.ICore;
    using System.IO;
    using drsXML = IGK.ICore.Xml;
    

    [CoreVisitor("AndroidAxml")]
    /// <summary>
    /// 
    /// </summary>
    class AndroidLayoutVisitor : CoreEncoderVisitor
    {
        
        private drsXML.CoreXmlWriter cWriter;

        public override void Flush()
        {
            cWriter.Flush();
        }
        class AndroidLayoutVisitorWriter : drsXML.CoreXmlWriter 
        {
            public AndroidLayoutVisitorWriter(StringBuilder sb, 
                 System.Xml.XmlWriterSettings setting):base(sb, setting)
            {

            }
            public override bool MustCloseTag(string tagName)
            {
                return false;
            }
            public static new AndroidLayoutVisitorWriter Create(StringBuilder sb, System.Xml.XmlWriterSettings setting)
            {
                return new AndroidLayoutVisitorWriter(sb, setting);
            }
        }
        public AndroidLayoutVisitor()
        {
            cWriter = AndroidLayoutVisitorWriter.Create(new StringBuilder(), new System.Xml.XmlWriterSettings()
            { 
                Indent = true ,
                OmitXmlDeclaration= false 
            });
            
        }
        public void Visit(AndroidLinearLayout linearLayout)
        {
            cWriter.WriteStartElement("LinearLayout");
            cWriter.AddNameSpace("android", AndroidConstant.ANDROID_NAMESPACE);
            cWriter.WriteAttributeString("android:layout_width", "fill_parent");
            cWriter.WriteAttributeString("android:layout_height", "fill_parent");
            cWriter.WriteAttributeString("android:orientation", linearLayout.Orientation.ToString().ToLower ());
            cWriter.WriteAttributeString("android:background", Colorf.CornflowerBlue.ToString(true));

            foreach (var item in linearLayout.Elements)
            {
                this.Visit(item);
            }
            cWriter.WriteEndElement();
        }
        public void Visit(AndroidLayoutItem item)
        {
            if (item == null)
                return;
            cWriter.WriteStartElement(CoreWorkingObjectAttribute.GetObjectName (item));

            cWriter.WriteAttributeString("android:id", "@+id/" + item.Id);

            cWriter.WriteEndElement();
        }

        internal void SaveTo(string filename)
        {
            cWriter.Flush();
            File.WriteAllText(filename, cWriter.StringBuilder.ToString());
        }
    }
}
