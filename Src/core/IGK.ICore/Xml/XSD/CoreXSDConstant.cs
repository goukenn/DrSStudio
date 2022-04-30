using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Xml.XSD
{
    public class CoreXSDConstant
    {
        public const string DATETIME = "yyyy-mm-ddThh:mm:ss";
        public const string NAMESPACEURI = "http://www.w3.org/2001/XMLSchema";
        public const string XML_XSD = "http://www.w3.org/XML/1998/namespace";
        public const int UNBOUNDED = -1;

        public const string ENUM_XS_MINMAXLENGTH = "xs:minmaxlength";
        public const string ENUM_XS_ENUMERATION =  "xs:enumeration";
        public const string ENUM_XS_PATTERN =  "xs:pattern";
        public const string ENUM_XS_MINMAXINCLUSIVE = "xs:minmaxinclusive";
        public const string ENUM_XS_MINMAXEXCLUSIVE = "xs:minmaxexclusive";

        public const string ENUM_XS_MININCLUSIVE="xs:minInclusive";
        public const string ENUM_XS_MAXINCLUSIVE="xs:maxInclusive";
        public const string ENUM_XS_MAXEXCLUSIVE = "xs:maxExclusive";

    }
}
