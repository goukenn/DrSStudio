namespace IGK.ICore.IO
{
    public enum enuWOFFFileNameID:uint
    {
        Copyright =0,
        FontFamily=1,
        /// <summary>
        /// you must set this to be a subfont family. can be one of the value Regular
        /// </summary>
        FontSubFamily=2,
        FontIdentifierName = 3,
        FullFontName=4,
        Version = 5,
        Trademark = 7,
        Manufacturer =8,
        Designer = 9,
        FileDescription =10,
        UrlDesigner = 12,
        UrlVendor = 13,
        LicenseURi =14,
        SampleText = 19,
        LicenceDescription = 13
        
    }
}