namespace IGK.ICore.Xml.XSD
{
    public class CoreXSDItemAttribute: CoreXSDTypeBase, ICoreXSDAttribute
    {
        public string Default { get; internal set; }
        public string Fixed { get; internal set; }
        public string Form { get; internal set; }
        public string Id { get; internal set; }
        public string Ref { get; internal set; }
        public string Type { get; internal set; }
        public string Use { get; internal set; }

        public override bool IsRequired => this.Use == "required";
    }
}