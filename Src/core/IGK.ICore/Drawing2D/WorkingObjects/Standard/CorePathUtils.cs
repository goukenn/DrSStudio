using System;
using System.Collections.Generic;

namespace IGK.ICore.Drawing2D
{
    public class CorePathUtils
    {
        public static void ReversePath(CoreGraphicsPath path, out Vector2f[] pts, out byte[] types)
        {


            var p = path;
     
            p.GetAllDefinition(out pts, out types);


            List<Vector2f> tr = new List<Vector2f>();
            List<Byte> tb = new List<byte>();
            bool first = true;
            byte bpoint = 0;
            for (int i = pts.Length - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                }
                if (first)
                {
                    tb.Add(0);
                    first = false;
                }
                else
                {
                    switch (types[i])
                    {
                        case 0:
                            tb.Add((byte)bpoint);
                            break;
                        default:

                            if ((types[i] & (byte)enuGdiGraphicPathType.EndPoint) == (byte)enuGdiGraphicPathType.EndPoint)
                            {
                                tb.Add((byte)(types[i] - (byte)enuGdiGraphicPathType.EndPoint));
                            }
                            else
                            {
                                tb.Add(types[i]);
                            }
                            break;
                    }
                }
                tr.Add(pts[i]);
                if ((types[i] & 3) == 3)
                {
                    bpoint = 3;
                }
                else if ((types[i] & 1) == 1)
                    bpoint = 1;
                else
                    bpoint = 0;
            }


            pts = tr.ToArray();
            types = tb.ToArray();
        }
    }
}