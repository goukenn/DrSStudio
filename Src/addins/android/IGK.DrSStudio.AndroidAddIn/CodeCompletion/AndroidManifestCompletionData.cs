

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidManifestCompletionData.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿

using IGK.ICore;using IGK.DrSStudio.Android.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.CodeCompletion
{
    class AndroidManifestCompletionData : AndroidCodeCompletionBase
    {
        private AndroidAttributeDefinition m_item;
        public AndroidManifestCompletionData(string data):base(data)
        {
            this.Data = getDisplayName(data);
        }
        private string getDisplayName(string name)
        {
            //check for data primiary
            string[] data = { 
                                "AndroidManifestSupportsInput",
                                "AndroidManifestCompatibleScreens"
                            };
            for (int i = 0; i < data.Length; i++)
            {
                if (name.StartsWith(data[i]))
                {
                    var d = name.Substring(data[i].Length);
                    if (string.IsNullOrEmpty (d)==false )
                        return string.Join("-", SplitUpperChar(d));
                    break;
                }

            }       
            if (name.StartsWith("AndroidManifest.AndroidManifest"))
            {
                return this.getDisplayName(name.Substring(name.Split('.')[0].Length + 1));
            }
            string s = "AndroidManifest";
            if (name.StartsWith(s))
            {
                string m = name.Substring(s.Length);
                if (!string.IsNullOrEmpty(m))
                {
                    name = string.Join("-", SplitUpperChar(m));
                }
                else
                {
                    //set data to manifest
                    name = "manifest";
                }
            }
            return name;
        }
        

        private string[] SplitUpperChar(string m)
        {
            List<string> m_s = new List<string>();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < m.Length; i++)
            {
                if ((i > 0)
                    && (char.IsUpper(m[i])))
                    { 
                        //start a new word
                        m_s.Add(sb.ToString().ToLower());
                        sb.Clear();
                    }                
                sb.Append(m[i]);

            }
            if (sb.Length > 0)
                m_s.Add(sb.ToString().ToLower());
            return m_s.ToArray();
        }

        public override object Description
        {
            get
            {
                string decs = this.m_item.GetDescription();
                if (string.IsNullOrEmpty (decs))
                {
                    decs += this.Data+ 
                        (string.IsNullOrEmpty (this.m_item.Parent)?string.Empty : " : "+ 
                        this.getParentDisplayName(this.m_item.Parent))+ "\n";
                    if (string.IsNullOrEmpty(this.m_item.Format))
                    {

                        decs += "Values: \n";
                        decs += this.m_item.GetValues();
                    }
                    else {
                        decs += "Format : "+this.m_item.Format+"\n";
                    }
                    var c = this.m_item.Childs;
                    if (c.Length > 0)
                    {
                        decs += "Attributes: \n";
                        foreach (var e in c)
                        {
                            decs +="- "+e+"\n";
                        }
                        
                    }
                    
                }
                return decs;
            }
        }

        private string getParentDisplayName(string p)
        {
            string[] t = p.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < t.Length; i++)
            {
                t[i] = getDisplayName(t[i]);
            }
            return string.Join("|", t);
        }

      
        public override System.Windows.Media.ImageSource Image
        {
            get
            {
                switch (this.m_item.Type)
                { 
                    case "attr":
                        return AndroidImageResources.GetImage("android_manifest_attr");
                    case "declare-styleable":
                        return AndroidImageResources.GetImage("android_manifest_properties");
                        
                        
                    default:
                        break;

                }
                return base.Image;
            }
           
        }
        public AndroidManifestCompletionData(string data, AndroidAttributeDefinition item):this(data)
        {
            this.m_item = item;
        }
        public override void Complete(ICSharpCode.AvalonEdit.Editing.TextArea textArea, ICSharpCode.AvalonEdit.Document.ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            base.Complete(textArea, completionSegment, insertionRequestEventArgs);
        }
        
    }
}
