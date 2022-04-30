using System;

namespace IGK.ICore.IO
{
    /// <summary>
    /// unicode range form bits 96 to 127
    /// </summary>
    [Flags()]
    public enum enuWOFFFileUnicodeRange4 : uint
    {
        Buginese =0x1,
        Glagolitic=0x2,
        Tifinagh=0x4,
        YijingHexagramSymbols = 0x8,
        SylotiNagri=0x10,
        Lydian = 0x1<<125,
        DominoTiles=0x1<<126
        //123-127 reserved for process usage
    }
}