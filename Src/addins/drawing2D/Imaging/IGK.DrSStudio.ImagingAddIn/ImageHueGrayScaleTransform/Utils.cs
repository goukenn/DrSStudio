using IGK.DrSStudio.Imaging.ImageHueGrayScaleTransform.WinUI;
using IGK.ICore;

namespace IGK.DrSStudio.Imaging.ImageHueGrayScaleTransform
{
    class Utils
    {
        public static void HueGrayTranform(ref byte[] vdata, int width, 
            int height, int minHue, int maxHue,
            enuHueGrayMode mode,
            IGK.ICore.Matrix matrix=null
            )
        {
            //byte r, g, b, a;
            int offset = 0;
            CoreColorHandle.RGB db = new CoreColorHandle.RGB();
            CoreColorHandle.HSV hb = new CoreColorHandle.HSV();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    offset = (y * (width * 4)) + (4 * x);
                    db.Red = vdata[offset];
                    db.Green = vdata[offset + 1];
                    db.Blue = vdata[offset + 2];
                    hb = CoreColorHandle.RGBtoHSV(db);

                    if (!((hb.Hue >= minHue) && (hb.Hue <= maxHue)))
                    {
                        //gray scale color
                        //Operation
                        //grayscale
                        switch (mode)
                        {
                            case enuHueGrayMode.GrayScale:
                                vdata[offset] =
                                    vdata[offset + 1] =
                                    vdata[offset + 2] =
                                    (byte)(db.Red * 0.298 + db.Green * 0.587 + db.Blue * 0.114);
                                break;
                            case enuHueGrayMode.Transparent:
                                vdata[offset] =
                                vdata[offset + 1] =
                                vdata[offset + 2] =
                                vdata[offset + 3] = 0;
                                break;
                            case enuHueGrayMode.RedOnly:
                                //red only
                                vdata[offset] = 0; //b
                                vdata[offset + 1] = 0;//g
                                //vdata[offset + 2] = 0;//r
                                //vdata[offset + 3] = 0;//a
                                break;
                            case enuHueGrayMode.GreenOnly:
                                vdata[offset] = 0;
                                vdata[offset + 2] = 0;
                                break;
                            case enuHueGrayMode.BlueOnly:
                                vdata[offset+1] = 0;
                                vdata[offset+2] = 0;
                                break;

                            case enuHueGrayMode.ColorMatrix:
                                Vector4f c = new Vector4f(vdata[offset],
                                    vdata[offset + 1],
                                    vdata[offset + 2],
                                    vdata[offset + 3]) * matrix;


                                vdata[offset] = WinCoreBitmapOperation.TrimByte((int)c.X) ;
                                vdata[offset + 1] = WinCoreBitmapOperation.TrimByte((int)c.Y);
                                vdata[offset + 2] = WinCoreBitmapOperation.TrimByte((int)c.Z);
                                vdata[offset + 3] = WinCoreBitmapOperation.TrimByte((int)c.W);
                                break;
                        }
                    }

                }
            }



        }

    }
}
