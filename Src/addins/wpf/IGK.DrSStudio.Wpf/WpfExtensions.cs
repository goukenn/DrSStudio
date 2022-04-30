

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WpfExtensions.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using ICSharpCode.AvalonEdit;
using IGK.ICore;
using IGK.ICore.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace IGK.ICore.Wpf
{
    public static class WpfExtensions
    {
        /// <summary>
        /// get the wpf font familly from string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static FontFamily WpfFontFamily(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return null;
            return (System.Windows.Media.FontFamily)
            new System.Windows.Media.FontFamilyConverter().ConvertFromString(text);
        }

        public static void InitTextEditor(this ICSharpCode.AvalonEdit.TextEditor editor)
        {
            editor.FontFamily = ((string)CoreSettings.GetSettingValue("CodeEditorEnvironment.FontName", "consolas")).WpfFontFamily();
            editor.FontSize = (float)Convert.ToSingle(CoreSettings.GetSettingValue("CodeEditorEnvironment.FontSize", 12.0f));

        }
    }
}
