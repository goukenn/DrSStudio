namespace IGK.ICore.IO
{
    public class WOFFFileGasp
    {
        public short Value { get; set; } //value must ve range from 0x0000 to 0xFFFF
        public enuWOFFGaspFlag Flags { get; set; }
    }
}