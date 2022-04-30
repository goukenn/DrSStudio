using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection;
using System.Text;

namespace IGK.ICore.DB
{
    /// <summary>
    /// get or set the table info
    /// </summary>
    public class CoreDataColumnInfo : ICoreDataColumnInfo
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
        /// <summary>
        /// table where column is declared
        /// </summary>
        public string TableName { get; private set; }
        public Type InterfaceType { get; private set; }
        public Type ColumnType { get; private set; }
        public string clUpdateFunction { get; set; }
        public string clInsertFunction { get; set; }
        public string clInputType { get; set; }
        /// <summary>
        /// get property info
        /// </summary>
        public ICoreDataPropertyInfo PropertyInfo { get; private set; }

        public CoreDataColumnInfo()
        {
        }
        private CoreDataColumnInfo(Type interfaceType, string Name, PropertyInfo prInfo, Type declaringType)
        {
            this.TableName = Name;
            this.InterfaceType = interfaceType;
            this.ColumnType = declaringType;
            this.PropertyInfo = CreatePropetyInfo(prInfo);

        }

        private ICoreDataPropertyInfo CreatePropetyInfo(PropertyInfo prInfo)
        {
            return new CoreDummyPropertyInfo(prInfo.PropertyType,
                Attribute.GetCustomAttribute(prInfo, typeof(CoreDataGuiAttribute)) as CoreDataGuiAttribute,
                Attribute.GetCustomAttribute(prInfo, typeof(CoreDataTableDisplayInfoAttribute)) as CoreDataTableDisplayInfoAttribute
                );
        }
        public override string ToString()
        {
            return "DBColumnInfo[clName:" + this.clName + "]";
        }

        internal static CoreDataColumnInfo CreateTableColumnInfo(Type t, PropertyInfo prop)
        {
            return new CoreDataColumnInfo(t, CoreDataTableAttribute.GetTableName(t), prop, prop.PropertyType);
        }
        /// <summary>
        /// get column info from interface type
        /// </summary>
        /// <param name="name"></param>
        /// <param name="InterfaceType"></param>
        /// <returns></returns>
        public static CoreDataColumnInfo CreateTableInfo(string name, Type InterfaceType)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name");
            if (InterfaceType == null)
                throw new ArgumentNullException("interfaceType");
            CoreDataColumnInfo c = new CoreDataColumnInfo();
            c.clName = name;
            c.clType = "Int";
            c.PropertyInfo = CreatePropetyInfo(InterfaceType);
            return c;
        }
        /// <summary>
        /// create a property info
        /// </summary>
        /// <param name="InterfaceType"></param>
        /// <returns></returns>
        private static ICoreDataPropertyInfo CreatePropetyInfo(Type InterfaceType)
        {
            if (InterfaceType == null)
                return null;
            return new CoreDummyPropertyInfo(InterfaceType,
                             Attribute.GetCustomAttribute(InterfaceType, typeof(CoreDataGuiAttribute)) as CoreDataGuiAttribute,
                Attribute.GetCustomAttribute(InterfaceType, typeof(CoreDataTableDisplayInfoAttribute)) as CoreDataTableDisplayInfoAttribute);
        }
        public string clLinkType
        {
            get
            {
                return !CoreDataContext.Contains(this.PropertyInfo.PropertyType) ? null : CoreDataContext.GetTableName(this.PropertyInfo.PropertyType);
            }
        }


    }
}
