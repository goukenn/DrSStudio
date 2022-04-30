using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Editor.FontEditor
{



    class GenerateBalafonFontDefinition
    {
        public static void Generate(string filename, IFontEditorDefinition def, params ICore2DDrawingDocument[] documents) {
            StringBuilder b = new StringBuilder();
            int i = 0;
            int v_code = 0;
            bool v_no = true;
            b.AppendLine("<?php");
            b.AppendLine(string.Format("//Font definition for : {0}", def.Name));
            
            foreach (ICore2DDrawingDocument item in documents)
            {

                //item.GetParam("FontDefefiion");
                Core2DDrawingLayerDocument g = item as Core2DDrawingLayerDocument;
                var cb = g.GetParam<FontEditorDocument>();
                if (v_no)
                {
                    v_code = 0;
                }
                else
                {
                    if (cb != null)
                    {
                        v_code = cb.Code;
                    }
                    else
                    {
                        v_code = 0xF000 + i;
                    }
                }
                b.AppendLine(string.Format("$def[\"{0}\"]=0x{1};", item.Id, v_code.ToString("X4")));
                if (!v_no)
                {
                    i++;
                }
                else
                    v_no = false;
            }
            b.Append("?>");
            File.WriteAllText(filename, b.ToString());

        }
    }
}
