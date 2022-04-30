using IGK.ICore.Codec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.VSLib
{
    public sealed class CSGlobalPropertyGroup : CSItemBase
    {
      

        [CoreXMLElement]
        public string OutputType
        {
            get
            {
                return this[nameof(OutputType)];
            }
            set
            {
                this[nameof(OutputType)] = value;
            }
        }
        [CoreXMLElement]
        public string RootNamespace
        {
            get
            {
                return this[nameof(RootNamespace)];
            }
            set
            {
                this[nameof(RootNamespace)] = value;
            }
        }
        [CoreXMLElement]
        public string AssemblyName
        {
            get
            {
                return this[nameof(AssemblyName)];
            }
            set
            {
                this[nameof(AssemblyName)] = value;
            }
        }
        [CoreXMLElement]
        public string TargetFrameworkVersion
        {
            get
            {
                return this[nameof(TargetFrameworkVersion)];
            }
            set
            {
                this[nameof(TargetFrameworkVersion)] = value;
            }
        }


        [CoreXMLElement]
        public string ProjectTypeGuids
        {
            get
            {
                return (string)this[nameof(ProjectTypeGuids)];
            }
            set
            {
                this[nameof(ProjectTypeGuids)] = value;
            }
        }
        [CoreXMLElement]
        public string ProjectGuid
        {
            get
            {
                return (string)this[nameof(ProjectGuid)];
            }
            set
            {
                this[nameof(ProjectGuid)] = value;
            }
        }
        
        [CoreXMLElement]
        public string DefineConstants
        {
            get
            {
                return (string)this[nameof(DefineConstants)];
            }
            set
            {
                this[nameof(DefineConstants)] = value;
            }
        }
        /// <summary>
        /// semi column warning disable
        /// </summary>
        [CoreXMLElement]
        public string NoWarn
        {
            get
            {
                return (string)this[nameof(NoWarn)];
            }
            set
            {
                this[nameof(NoWarn)] = value;
            }
        }

        public CSGlobalPropertyGroup() : base(CSConstants.PROPERTYGROUP_TAG){
            this.SetAttribute(nameof(OutputType), "Library");
            this.SetAttribute(nameof(RootNamespace), "IGK");
            this.SetAttribute(nameof(AssemblyName), "IGK");
            this.SetAttribute(nameof(TargetFrameworkVersion), "v7.1");
        }
    }
}
