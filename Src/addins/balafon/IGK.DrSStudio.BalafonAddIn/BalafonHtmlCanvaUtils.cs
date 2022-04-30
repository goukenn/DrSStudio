using IGK.ICore;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon
{
    class BalafonHtmlCanvaUtils
    {
        /// <summary>
        /// get the path canvas script definition string
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetPathDefinition(ICore2DDrawingLayeredElement item)
        {
            var p = item.GetPath();
            Vector2f[] v_pts = null;
            byte[] v_types = null;

            p.GetAllDefinition(out v_pts, out v_types);

            if ((v_pts == null) || (v_pts.Length == 0))
                return string.Empty;
            StringBuilder v_sb = new StringBuilder();
            
            //string v_mode = "M";
            //StringBuilder sb = m_sb;
           // v_sb.Append("/*path items #" + item.Id + "*/");
            v_sb.Append(string.Format("v_ctx.beginPath();"));
            List<Vector2f> v_bezierPoint = new List<Vector2f>();
            //bool v_newfig = false;
            for (int i = 0; i < v_pts.Length; i++)
            {
                switch ((enuGdiGraphicPathType)v_types[i])
                {
                    case enuGdiGraphicPathType.StartFigure:
                        v_sb.Append(string.Format("v_ctx.moveTo({0},{1});",
               v_pts[i].X,
               v_pts[i].Y));
                        break;
                    case enuGdiGraphicPathType.ControlPoint:
                        if (v_bezierPoint.Count == 3)
                        {
                            v_sb.Append(string.Format("v_ctx.bezierCurveTo({0});",
                                string.Join(",", v_bezierPoint.ToArray()).Replace(";", ",")));
                            v_bezierPoint.Clear();
                        }
                        v_bezierPoint.Add(v_pts[i]);
                        //         v_sb.Append(string.Format("v_ctx.bezierCurveTo({0},{1});",
                        //v_pts[i].X,
                        //v_pts[i].Y));
                        break;
                    case enuGdiGraphicPathType.LinePoint:
                        if (v_bezierPoint.Count > 0)
                        {
                            switch (v_bezierPoint.Count)
                            {
                                case 3:
                                    v_sb.Append(string.Format("v_ctx.bezierCurveTo({0});",
                                    string.Join(",", v_bezierPoint.ToArray()).Replace(";", ",")));
                                    v_bezierPoint.Clear();
                                    break;
                                default:
                                    break;
                            }
                        }
                        v_sb.Append(string.Format("v_ctx.lineTo({0},{1});",
                 v_pts[i].X,
                 v_pts[i].Y));
                        break;
                    case enuGdiGraphicPathType.Marker | enuGdiGraphicPathType.ControlPoint:
                        //sb.Append(string.Join(",", v_pts[i].X, v_pts[i].Y) + " ");
                        //  sb.Append(" z ");
                        break;
                    case enuGdiGraphicPathType.Marker:
                        break;
                    case enuGdiGraphicPathType.Mask:
                        break;
                    case enuGdiGraphicPathType.EndPoint:
                        break;
                    default:
                        if (((enuGdiGraphicPathType)v_types[i] & enuGdiGraphicPathType.EndPoint)
                            == enuGdiGraphicPathType.EndPoint)
                        {
                            int b = v_types[i];
                            int sdb = 0x80;
                            var s = b - sdb;
                            switch (v_bezierPoint.Count)
                            {
                                case 2:
                                    v_bezierPoint.Add(v_pts[i]);
                                    v_sb.Append(string.Format("v_ctx.bezierCurveTo({0});",
                                        string.Join(",", v_bezierPoint.ToArray()).Replace(";", ",")));
                                    v_bezierPoint.Clear();
                                    break;
                                case 0:
                                    v_sb.Append(string.Format("v_ctx.lineTo({0},{1});",
                                      v_pts[i].X,
                                      v_pts[i].Y));
                                    break;
                                case 3:
                                    v_sb.Append(string.Format("v_ctx.bezierCurveTo({0});",
                               string.Join(",", v_bezierPoint.ToArray()).Replace(";", ",")));
                                    v_bezierPoint.Clear();
                                    if ((s & 1) == 1)
                                    {
                                        v_sb.Append(string.Format("v_ctx.lineTo({0},{1});",
                                          v_pts[i].X,
                                          v_pts[i].Y));
                                    }
                                    break;
                                default:
                                    break;
                            }
                            //}
                            //if (i < (v_pts.Length - 1))
                            //{
                            //start new figure
                            v_sb.Append(string.Format("v_ctx.closePath();"));
                        }
                        break;
                }

            }

            if (v_bezierPoint.Count == 3)
            {//render bezier point count
                v_sb.Append(string.Format("v_ctx.bezierCurveTo({0});",
                    string.Join(",", v_bezierPoint.ToArray()).Replace(";", ",")));
                v_bezierPoint.Clear();
            }
            return v_sb.ToString();
        }
    }
}
