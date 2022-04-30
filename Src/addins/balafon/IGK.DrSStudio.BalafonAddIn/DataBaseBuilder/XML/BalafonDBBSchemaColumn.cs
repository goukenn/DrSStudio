using IGK.ICore;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon.DataBaseBuilder.XML
{
    public class BalafonDBBSchemaColumn : BalafonDBBSchemaElement
    {
        public BalafonDBBSchemaColumn()
            : base(BalafonDBBConstant.COLUMNDEF_TAG)
        {

        }
        public override CoreXmlAttributeValue this[string attribute] { get => base[attribute]; set
            {
                if (attribute == "clType") {
                    value = ResolvType(value);
                }
                base[attribute] = value;
            }
        }
        static string ResolvType(string type) {
            switch ( type?.ToLower()) {
                case "int":
                case "float":
                case "double":
                case "date":
                    return type.UCFirst();
                case "bigint":
                    return "BigInt";
                case "ubigint":
                    return "UBigInt";
                case "ufloat":
                    return "UFloat";
                case "udouble":
                    return "UDouble";
                case "datetime":
                    return "DateTime";
                case "timestamp":
                    return "TimeStamp";
            }
            return type;
        }
    }
}
