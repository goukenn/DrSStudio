using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class Core2DDrawingGroupElementAttribute : Core2DDrawingStandardElementAttribute 
    {
        private string m_Group;

        public override string GroupName
        {
            get
            {
                return this.m_Group;
            }
        }
        public Core2DDrawingGroupElementAttribute(string name, string group, Type mecanism):base(name, mecanism )
        {
            this.m_Group = group;
        }
    }
}
