﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Drawing2D
{
    public enum enuCopyMode
    {             
          SRCCOPY = 0x00CC0020,
          SRCPAINT = 0x00EE0086,
          SRCAND = 0x008800C6,
          SRCINVERT = 0x00660046, 
          SRCERASE = 0x00440328, 
          NOTSRCCOPY = 0x00330008,
          NOTSRCERASE = 0x001100A6,
          MERGECOPY = 0x00C000CA, 
          MERGEPAINT = 0x00BB0226,
          PATCOPY = 0x00F00021, 
          PATPAINT = 0x00FB0A09, 
          PATINVERT = 0x005A0049, 
          DSTINVERT = 0x00550009, 
          BLACKNESS = 0x00000042,
          WHITENESS = 0x00FF0062,             
        SrcCopy  = 0x00CC0020 ,
        SrcPaint = SRCPAINT ,
        SrcAnd = SRCAND ,
        SrcInvert = SRCINVERT ,
        SrcErase = SRCERASE  ,
        SrcNotCopy = NOTSRCCOPY ,
        SrcNotErrase= NOTSRCERASE,
        MergeCopy = MERGECOPY ,
        MergePaint = MERGEPAINT ,
        PatCopy = PATCOPY ,
        PatPaint = PATPAINT  ,
        PatInvert = PATINVERT ,
        DstInvert = DSTINVERT ,
        Blackness = BLACKNESS ,
        Whiteness = WHITENESS 
    }
}
