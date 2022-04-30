using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android.Entities
{
    public sealed class AndroidAttrsFormat 
    {
        public static readonly AndroidAttrsFormat String;

        private string m_Name;

        public string Name
        {
            get { return m_Name; }            
        }
        static AndroidAttrsFormat() {
            String = new AndroidAttrsFormat("string");
        }
        private AndroidAttrsFormat()
        {

        }
        private AndroidAttrsFormat(string name):this()
        {
            this.m_Name = name;
        }
    }
}
