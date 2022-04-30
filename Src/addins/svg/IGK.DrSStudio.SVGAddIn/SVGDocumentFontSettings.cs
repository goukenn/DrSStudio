using System.Text;

namespace IGK.DrSStudio.SVGAddIn
{
    /// <summary>
    /// represent setting for every document for svg font
    /// </summary>
    internal class SVGDocumentFontSettings
    {
        public string Unicode { get; set; }
        public int HorizAdvX { get; set; }

        ///<summary>
        ///public .ctr
        ///</summary>
        public SVGDocumentFontSettings()
        {
            this.Unicode = Encoding.Unicode.GetString(new byte[]{ 13});
            this.HorizAdvX = 1050; // horizontal adv-x
        }
    }
}