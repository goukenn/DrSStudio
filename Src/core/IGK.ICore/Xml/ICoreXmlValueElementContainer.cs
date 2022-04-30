using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Xml
{
    public interface ICoreXmlValueElementContainer<T>
    {
        bool IsChildElementProperty(string name);
        T GetElementPropertyChild(string name);
        string GetElementProperty(string name);
        void SetElementProperty(string name, string value);
    }
}
