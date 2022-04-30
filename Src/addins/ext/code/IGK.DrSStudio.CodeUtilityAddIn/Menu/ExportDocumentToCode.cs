

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ExportDocumentToCode.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:ExportDocumentToCode.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
namespace IGK.DrSStudio.CodeUtilityAddIn.Menu
{
using IGK.ICore.Drawing2D;
    using IGK.ICore.Drawing2D.Menu;
    using IGK.ICore.Menu;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore;
    using IGK.DrSStudio.Menu;
    [DrSStudioMenu("Tools.ExporttoCode", 100)]
    class ExportDocumentToCode : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            ICore2DDrawingSurface s = this.CurrentSurface as ICore2DDrawingSurface;
            if (s == null)
                return false;
            ICore2DDrawingLayeredElement v_element = s.CurrentLayer.SelectedElements[0];
            if (v_element == null)
                return false ;
            StringBuilder sb = new StringBuilder();
            ICoreGraphicsPath v_path = v_element.GetPath();
            if (v_path == null)
                return false;
            string v_name = typeof(GraphicsPath).FullName;
            sb.AppendLine(string.Format("{0} v_g = new {1}({2},{3}); ",v_name,v_name,
                getPoint(v_path ), getDefinition(v_path)));
            sb.AppendLine(string.Format ("v_g.FillMode = {0}.{1};", typeof(FillMode).FullName, v_path.FillMode));
            if (v_element is ICore2DDrawingDualBrushElement)
            {
                ICore2DDrawingDualBrushElement br = v_element as ICore2DDrawingDualBrushElement;
                sb.AppendLine("//Brush");
                sb.AppendLine (getBrushDefinition (
                    CoreBrushRegisterManager.GetBrush<Brush>(br.GetBrush (enuBrushMode.Fill ))));
            }
            string fname = IGK.ICore.IO.PathUtils.GetTempFileWithExtension("txt"); //Path.GetTempFileName() + ".txt";
            File.WriteAllText(fname, sb.ToString());
            System.Diagnostics.Process.Start(fname);
            return false;
        }
        private string getPoint(ICoreGraphicsPath v_path)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("new {0}[]{{", typeof(Vector2f).FullName));
            bool v = false;
            foreach (Vector2f item in v_path.PathPoints)
            {
                if (v) sb.Append(",");
                sb.Append(string.Format("new {0}({1}f,{2}f)", typeof(Vector2f).FullName,
                    item.X,
                    item.Y));
                v = true;
            }
            sb.Append("}");
            return sb.ToString();
        }
        private string getDefinition(ICoreGraphicsPath v_path)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("new {0}[]{{", typeof(Byte).FullName));
            bool v = false;
            foreach (Byte item in v_path.PathTypes)
            {
                if (v) sb.Append(",");
                sb.Append(string.Format("{0}",item) );
                v = true;
            }
            sb.Append("}");
            return sb.ToString();
        }
        private string getBrushDefinition(Brush br)
        {
            StringBuilder sb = new StringBuilder();
            if (br is SolidBrush)
            {
                SolidBrush v_sb = br as SolidBrush;
                sb.Append(string.Format("{0} brush = new {1}({2});", v_sb.GetType().FullName,v_sb.GetType().FullName, Convert.ToUInt32 (v_sb.Color.ToArgb())));
            }
            else if (br is HatchBrush )
            {
                HatchBrush v_sb = br as HatchBrush ;
                sb.Append(string.Format("{0} brush = new {1}({2});", v_sb.GetType().FullName,v_sb.GetType().FullName, string.Format ("{0},{1},{2}",
                    v_sb.HatchStyle , v_sb.ForegroundColor , v_sb.HatchStyle )));
            }
            else if (br is LinearGradientBrush)
            {}
            else if (br is TextureBrush )
            {}
            else if (br is PathGradientBrush )
            {
            }
            return sb.ToString();
        }
    }
}

