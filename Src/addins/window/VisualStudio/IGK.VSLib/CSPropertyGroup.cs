using IGK.ICore.Codec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.VSLib
{
    public class CSPropertyGroup : CSItemBase
    {
        [CoreXMLAttribute]
        public string Condition
        {
            get
            {
                return (string)this[nameof(Condition)];
            }
            set
            {
                this[nameof(Condition)] = value;
            }
        }
        [CoreXMLElement]
        public bool Optimize
        {
            get
            {
                return Convert.ToBoolean(this[nameof(Optimize)].Value);
            }
            set
            {
                this[nameof(Optimize)].Value = value;
            }
        }
        [CoreXMLElement]
        public bool DebugSymbols
        {
            get
            {
                return Convert.ToBoolean(this[nameof(DebugSymbols)].Value);
            }
            set
            {
                this[nameof(DebugSymbols)].Value  = value;
            }
        }
        [CoreXMLElement]
        public string OutputPath
        {
            get
            {
                return (string)this[nameof(OutputPath)];
            }
            set
            {
                this[nameof(OutputPath)] = value;
            }
        }
        [CoreXMLElement]
        public int WarningLevel
        {
            get
            {
                return Convert.ToInt32(this[nameof(WarningLevel)].Value);
            }
            set
            {
                this[nameof(WarningLevel)] = value;
            }
        }
        [CoreXMLElement]
        public enuCSDebugType DebugType
        {
            get
            {
                return (enuCSDebugType)Enum.Parse(typeof(enuCSDebugType), this[nameof(DebugType)]?.Value.ToString());
            }
            set
            {
                this[nameof(DebugType)].Value = value;
            }
        }

        [CoreXMLElement]
        public string PlatformTarget
        {
            get
            {
                return (string)this[nameof(PlatformTarget)];
            }
            set
            {
                this[nameof(PlatformTarget)] = value;
            }
        }


        ///<summary>
        ///public .ctr
        ///</summary>
        public CSPropertyGroup() : base(CSConstants.PROPERTYGROUP_TAG)
        {
            this.SetAttribute(nameof(DebugSymbols), true);
            this.SetAttribute(nameof(Optimize), false);
            this.SetAttribute(nameof(OutputPath), "Bin");
            this.SetAttribute(nameof(WarningLevel), 4);
            this.SetAttribute(nameof(DebugType), enuCSDebugType.full);
            
    }
    }
}
