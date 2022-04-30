using System;
using System.Collections.Generic;

namespace IGK.ICore.IO
{
    /// <summary>
    /// represent a glyfs data structure
    /// </summary>
    internal struct WOFFFileGlyfData
    {
        public int Unicode { get; set; }
        public Vector2f[] Points { get; set; }
        public byte[] Types { get; set; }
        public Vector2i Size { get; set; }


        /// <summary>
        /// convert a bezier curve to quadric
        /// </summary>
        /// <param name="pts"></param>
        /// <param name="t"></param>
        public static void CurveToQuadrics(ref Vector2f[] pts, ref Byte[] t) {


            Vector2f c0, c1, c2 , c3;
            Vector2f q0, q1, q2;
            List<Vector2f> v_optst = new List<Vector2f>();
            List<byte> v_ot = new List<byte>();
          
           int ln = pts.Length;
            bool quadric = false;
            int jackv = 0;
            for (int i = 0; i < ln; i++)
            {
                if (t[i] == 0) {
                    
                    if (quadric) {
                        v_optst.Add(pts[i-1]);
                        v_ot.Add(0x01);
                    }

                    v_optst.Add(pts[i]);
                    v_ot.Add(0);
                    quadric = false;
                    continue;
                }
               
                if (((t[i] & 0x3) == 0x3) && ((i + 2) < pts.Length))
                {
                    if (quadric)
                    {
                        //    v_ot[v_ot.Count - 1] = 0x03;
                        jackv--;
                        //remove quadric object
                        v_optst.RemoveAt(v_optst.Count - 1);
                        v_ot.RemoveAt(v_ot.Count - 1);

                        //v_ot[v_optst.Count - 1] = 0x3;

                    }
                    c0 = pts[i-1];
                    c1 = pts[i];
                    c2 = pts[i + 1];
                    c3 = pts[i + 2];
                    q0 = c0;
                    //q1 = (3 / 4.0f) * ((c1 + c2) - (c0 / 3.0f) - (c3 / 3.0f));
                    q1 = (3 / 2.0f) * (c1 + ((-1 / 3.0f) * c0));
                    //q1 = c0;
                    q2 = c3;

                   
                    v_optst.Add(q1);
                    v_ot.Add(0x3);



                    if (((t[i + 2] & 0x80) == 0x80) || (i+2>= ln))
                    {
                        Console.WriteLine("last point is on line");
                        //v_optst.Add(q2);
                        //v_ot.Add(0x3);// | 0x80);
                        jackv++;
                    }
                    else
                    {
                        v_optst.Add(q2);
                        v_ot.Add((byte)0x01);

                        jackv += 2;
                        //v_optst.Add(q2);
                        //v_ot.Add((byte)(0x03));// | (((t[i + 2] & 0x80) == 0x80) ? 0x80 : 0)));

                    }


                    //q2 = c3;
                    //q1 = (3 / 2.0f) * (c2 - ((1 / 3.0f) * c3));
                    //q0 = 3 * (c1 - (c2 - ((1 / 3.0f) * c3)));


                    //pts[i] = q0;
                    //pts[i + 1] = q1;
                    //pts[i + 2] = q2;

                    //t[i] = 1;// (byte)(t[i + 2] & ~2);
                    //t[i + 1] = 1;// (byte)(t[i + 2] & ~2);
                    //t[i + 2] = 1;// (byte)(t[i + 2] & ~2);
                    i += 2;
                    quadric = true;
                }
                else {
                    v_optst.Add(pts[i]);
                    v_ot.Add(t[i]);
                    quadric = false;
                    jackv++;
                }
            }

            pts = v_optst.ToArray();
            t = v_ot.ToArray();

        }
    }
}