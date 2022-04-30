using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android
{
    class AndroidSourceProperties
    {
        Dictionary<string, string> m_dics;
        ///<summary>
        ///public .ctr
        ///</summary>
        public AndroidSourceProperties()
        {
            this.m_dics = new Dictionary<string, string>();
        }
        public string Desc {
            get {
                return this.m_dics["Pkg.Desc"];
            }
        }
        public string Version {
            get {
                return this.m_dics["Platform.Version"];
            }
        }
        public string ApiLevel {
            get
            {
                return this.m_dics["AndroidVersion.ApiLevel"];
            }
        }
        internal void Add(string v1, string v2)
        {
            this.m_dics.Add(v1, v2);
        }
    }
}
