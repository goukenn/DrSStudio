using System;

namespace IGK.ICore.IO
{

    /// <summary>
    /// unicode range form bits 64 to 95
    /// </summary>
    [Flags()]
    public enum enuWOFFFileUnicodeRange3 : uint
    {
        CombiningHalfMark=0x1, //64
        NewTaiLue=(uint) 0x1<<31
    }
}