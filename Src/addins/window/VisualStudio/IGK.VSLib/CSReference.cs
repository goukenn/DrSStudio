using IGK.ICore.Codec;

namespace IGK.VSLib
{
    public class CSReference : CSItemBase
    {
        [CoreXMLAttribute]
        public string Include
        {
            get
            {
                return (string)this[nameof(Include)];
            }
            set
            {
                this[nameof(Include)] = value;
            }
        }



        ///<summary>
        ///public .ctr
        ///</summary>
        public CSReference() : base(CSConstants.REFERENCE_TAG)
        {

        }
        ///<summary>
        ///public .ctr
        ///</summary>
        protected CSReference(string tagName):base(tagName )
        {

        }
    }
}
