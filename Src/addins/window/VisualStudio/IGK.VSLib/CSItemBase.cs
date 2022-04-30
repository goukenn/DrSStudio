using IGK.ICore.Codec;
using IGK.ICore.Xml;
using System;

namespace IGK.VSLib
{
    public abstract class CSItemBase : CoreXmlElement
    {
        [CoreXMLAttribute(false)]
        public override string Id { get => base.Id; set => base.Id = value; }

        public CSItemBase(string tagName):base(tagName)
        {
            
        }

        /// <summary>
        /// factor create
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T Create<T>(string name) {
            var ht = typeof(CSItemBase);
            var v_asm = ht.Namespace;
            Type t = 
            ht.Assembly.GetType($"{v_asm}.CS{name}", false, true);
            if ((t != null) && t.IsSubclassOf(ht)){
                return (T)t.Assembly.CreateInstance(t.FullName);
            }
            return default(T);
        }

        public override CoreXmlElement CreateChildNode(string tagName)
        {
            return Create< CSItemBase>(tagName) ?? new DummyNode(tagName);
        }

        class DummyNode : CSItemBase {
            ///<summary>
            ///public .ctr
            ///</summary>
            public DummyNode(string tagName):base(tagName )
            {

            }
        }
    }
}