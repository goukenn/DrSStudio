using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Xml.XSD
{
    /// <summary>
    /// reprenset a public CoreXSDItemInfo
    /// </summary>
    public class CoreXSDItemInfo : ICoreXSDItemInfo

    {
        private string[] m_attributes;
        private string[] m_elements;

        public string[] Attributes => m_attributes;

        public string[] Elements => m_elements;

        public ICoreXSDType Source { get; private set; }

        private CoreXSDItemInfo(){

        }
        public static ICoreXSDItemInfo Create(CoreXSDFileBase _file, string v, ICoreXSDType s)
        {
            CoreXSDItemInfo v_o = new  CoreXSDItemInfo();
            
            v_o.Source = s;

            List<string> v_attr = new List<string> ();
            List<string> v_elements = new List<string> ();
            foreach (var item in s.GetAttributes())
            {
                v_attr.Add (item.Name);
            }
            v_o.m_attributes = v_attr.ToArray();


            //load element in propagation algoritm

            CoreXSDUtils.FetchElement(_file, s, (item)=> {
                v_elements.Add ((item as CoreXSDNode).Name);
                return null;
            });
            v_o.m_elements = v_elements.ToArray();



            return v_o;
        }
    }
}
