using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS
{
    /// <summary>
    /// dummy column info used internally
    /// </summary>
    public sealed class GSDummyColumnInfo : IGSDbColumnInfo 
    {
        public string clName { get; set; }
        public bool clNotNull { get; set; }
        public string clType { get; set; }
        public int clTypeLength { get; set; }
        public string clDefault { get; set; }
        public bool clIsUniqueColumnMember { get; set; }
        public int clColumnMemberIndex { get; set; }
        public bool clAutoIncrement { get; set; }
        public bool clIsPrimary { get; set; }
        public bool clIsUnique { get; set; }
        public bool clIsIndex { get; set; }
        public string clDescription { get; set; }
        public string clInsertFunction { get; set; }
        public string clUpdateFunction { get; set; }
        public string clInputType { get; set; }
        public GSDummyColumnInfo()
        {
        }


        public string clLinkType
        {
            get {
                return string.Empty;
            }
        }
    }
}
