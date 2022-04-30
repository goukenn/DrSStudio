

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebCssAttributeManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
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
﻿using IGK.ICore.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.WebCssEditorAddIn
{
    public class WebCssAttributeManager
    {
        static Dictionary<string, WebCssAttributeDefinition> sm_attribs;
        static WebCssAttributeManager() {
            sm_attribs = new Dictionary<string, WebCssAttributeDefinition>();
            InitCssDefinition();
        }

        private static void InitCssDefinition()
        {
            IGK.ICore.Xml.CoreXmlElement e = IGK.ICore.Xml.CoreXmlElement.CreateXmlNode(string.Empty);
            e.LoadString(CoreResources.GetResourceString("css/cssdefinition"));

            var v_tdef = e.getElementsByTagName("cssDefinition");
            if ((v_tdef != null) && (v_tdef.Length > 0))
            {
                var v_def = v_tdef[0] as IGK.ICore.Xml.CoreXmlElement;
                string v_n = string.Empty ;
                foreach (IGK.ICore.Xml.CoreXmlElement item in v_def.Childs)
                {
                    if (item == null) continue;

                    switch (item.TagName)
                    {
                        case "cssProperty":
                            {
                                v_n = item["name"];
                                if (!string.IsNullOrEmpty(v_n) && !sm_attribs.ContainsKey(v_n))
                                {
                                    sm_attribs.Add(v_n, new WebCssAttributeDefinition()
                                    {
                                        Name = v_n,
                                        Description = item["description"],
                                        Category = item["category"],
                                        PropertyType = item["type"],
                                        Version = item["version"]
                                    });
                                }
                            }
                            break;
                        case "cssEnum":
                            foreach (IGK.ICore.Xml.CoreXmlElement ritem in item.Childs)
                            {
                                v_n = ritem.TagName;
                                if (!string.IsNullOrEmpty(v_n) && !sm_attribs.ContainsKey(v_n))
                                {
WebCssAttributeDefinition.WebCssEnumValueAttributeDefinition            kt=                         new WebCssAttributeDefinition.WebCssEnumValueAttributeDefinition(v_n);
                                    sm_attribs.Add(v_n, kt);
                                    kt.Load(ritem);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    
                  
                }
            }
            
            foreach (var item in sm_attribs)
            {
                item.Value.UpdateValue(sm_attribs);
            }

            
        }
        public static Dictionary<string, WebCssAttributeDefinition>.Enumerator GetDefinitions()
        {
            return sm_attribs.GetEnumerator ();
        }
    }
}
