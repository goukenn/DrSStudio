using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Drawing2D;
using IGK.ICore;

namespace IGK.DrSStudio.Balafon.Tools.BrushManagement
{
    class BalafonManageUtils
    {
        static string GetColor(Colorf cl) {
            if (cl.A == 1.0f)
                return cl.ToString(true);
            return string.Format("rgba({0},{1},{2},{3})",
                cl.R.TrimByte(),
                cl.G.TrimByte(),
                cl.B.TrimByte(),
                cl.A);
        }
        internal static string ConvertToCss(ICoreBrush c)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("background:");
            sb.AppendLine(GetColor(c.Colors[0])+";");
            switch (c.BrushType) {
                case enuBrushType.Solid:
                    
                    break;
                case enuBrushType.LinearGradient:
                    string value = string.Empty;
                    for (int i = 0; i < c.Colors.Length; i++)
                    {
                        if (i > 0)
                            value += ",";
                        value += GetColor(c.Colors[i]);//.ToString(true);
                    }

                    value = c.Angle+"deg, " +  value;
     //               if (preg_match("/^(left|top|right|bottom)/i", trim($v_stand)))
     //               {
					//$v_stand = "to ".$v_stand;
     //               }
				    sb.AppendLine($"background: -webkit-linear-gradient({value}); background: -ms-linear-gradient({value}); background: -moz-linear-gradient({value});background: -o-linear-gradient({value}); background: linear-gradient({value});");

                    break;
            }


            return sb.ToString();
        }
    }
}
