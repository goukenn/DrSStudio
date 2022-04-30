using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly:CoreAddIn()]

namespace test_color_point
{
    class Program
    {
        static void Main(string[] args)
        {
            CoreSystem.InitWithEntryAssembly(typeof(Program).Assembly);


            var d = CoreColorHandle.RGBtoHSV(Colorf.Red);
            Colorf cc = CoreColorHandle.XYtoColorf(1.0f, 0.0f);
            d = CoreColorHandle.RGBtoHSV(Colorf.Cyan);
            cc = CoreColorHandle.XYtoColorf(-1.0f, 0.0f);


            Bitmap bmp = new Bitmap(256, 256);
            Graphics g = Graphics.FromImage(bmp);
            
            
            Colorf c =CoreColorHandle.XYtoColorf(0.5f, .5f);

            SolidBrush br = new SolidBrush(c.ToGdiColor());
            g.FillRectangle(br, new RectangleF(0, 0, 256, 256));

            g.Flush();

            g.Dispose();
            bmp.Save("d:\\temp\\out.png");

            Console.WriteLine(c);
            c = CoreColorHandle.XYtoColorf(0.2f, -0.4f);
            Console.WriteLine(c);
            c = CoreColorHandle.XYtoColorf(-0.2f, -0.4f);
            Console.WriteLine(c);

            c = CoreColorHandle.XYtoColorf(-0.2f, 0.4f);
            Console.WriteLine(c);
            Console.ReadLine();
        }
    }

    [CoreApplication()]
    class App : WinCoreApplication
    {
    }
}
