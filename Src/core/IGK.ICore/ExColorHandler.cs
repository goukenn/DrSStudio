﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    public class ExColorHandler
    {
        /// <summary>
        /// represnet RGB structure
        /// </summary>
        public struct RGB
        {
            // All values are between 0 and 255.
            public int Red;
            public int Green;
            public int Blue;
            public RGB(int R, int G, int B)
            {
                Red = R;
                Green = G;
                Blue = B;
            }
            public override string ToString()
            {
                return String.Format("({0}, {1}, {2})", Red, Green, Blue);
            }
        }
        /// <summary>
        /// Represent value corresponding to 
        /// </summary>
        public struct HSV
        {
            //value ar between 0 and 360
            public int Hue;
            // All values are between 0 and 255.
            public int Saturation;
            public int value;
            public HSV(int H, int S, int V)
            {
                Hue = H;
                Saturation = S;
                value = V;
            }
            public override string ToString()
            {
                return String.Format("({0}, {1}, {2})", Hue, Saturation, value);
            }
        }

        public static RGB HSVtoRGB(HSV HSV)
        {//0-360 - rest 0-255
            double h;
            double s;
            double v;
            double r = 0;
            double g = 0;
            double b = 0;
            // Scale Hue to be between 0 and 360. Saturation
            // and value scale to be between 0 and 1.
            h = ((double)HSV.Hue / 360.0f);
            s = (double)HSV.Saturation / 255;
            v = (double)HSV.value / 255;
            if (s == 0)
            {
                // If s is 0, all colors are the same.
                // This is some flavor of gray.
                r = v;
                g = v;
                b = v;
            }
            else
            {
                double p;
                double q;
                double t;
                double fractionalSector;
                int sectorNumber;
                double sectorPos;
                // The color wheel consists of 6 sectors.
                // Figure out which sector you//re in.
                sectorPos = h / 60;
                sectorNumber = (int)(Math.Floor(sectorPos));
                // get the fractional part of the sector.
                // That is, how many degrees into the sector
                // are you?
                fractionalSector = sectorPos - sectorNumber;
                // Calculate values for the three axes
                // of the color. 
                p = v * (1 - s);
                q = v * (1 - (s * fractionalSector));
                t = v * (1 - (s * (1 - fractionalSector)));
                // Assign the fractional colors to r, g, and b
                // based on the sector the angle is in.
                switch (sectorNumber)
                {
                    case 0:
                        r = v;
                        g = t;
                        b = p;
                        break;
                    case 1:
                        r = q;
                        g = v;
                        b = p;
                        break;
                    case 2:
                        r = p;
                        g = v;
                        b = t;
                        break;
                    case 3:
                        r = p;
                        g = q;
                        b = v;
                        break;
                    case 4:
                        r = t;
                        g = p;
                        b = v;
                        break;
                    case 5:
                        r = v;
                        g = p;
                        b = q;
                        break;
                }
            }
            // return an RGB structure, with values scaled
            // to be between 0 and 255.
            return new RGB((int)(r * 255), (int)(g * 255), (int)(b * 255));
        }

        /// <summary>
        /// convert RGB color to HSV
        /// </summary>
        /// <param name="RGB"></param>
        /// <returns></returns>
        public static HSV RGBtoHSV(RGB RGB)
        {
            // In this function, R, G, and B values must be scaled 
            // to be between 0 and 1.
            // HSV.Hue will be a value between 0 and 360, and 
            // HSV.Saturation and value are between 0 and 1.
            // The code must scale these to be between 0 and 255 for
            // the purposes of this application.
            double min;
            double max;
            double delta;
            double r = (double)RGB.Red / 255;
            double g = (double)RGB.Green / 255;
            double b = (double)RGB.Blue / 255;
            double h;
            double s;
            double v;
            min = Math.Min(Math.Min(r, g), b);
            max = Math.Max(Math.Max(r, g), b);
            v = max;
            delta = max - min;
            if (max == 0 || delta == 0)
            {
                // R, G, and B must be 0, or all the same.
                // In this case, S is 0, and H is undefined.
                // Using H = 0 is as good as any...
                s = 0;
                h = 0;
            }
            else
            {
                s = delta / max;
                if (r == max)
                {
                    // Between Yellow and Magenta
                    h = (g - b) / delta;
                }
                else if (g == max)
                {
                    // Between Cyan and Yellow
                    h = 2 + (b - r) / delta;
                }
                else
                {
                    // Between Magenta and Cyan
                    h = 4 + (r - g) / delta;
                }
            }
            // Scale h to be between 0 and 360. 
            // This may require adding 360, if the value
            // is negative.
            h *= 60;
            if (h < 0)
            {
                h += 360;
            }
            // Scale to the requirements of this 
            // application. All values are between 0 and 255.
            return new HSV((int)h, (int)(s * 255), (int)(v * 255));
        }
    }

}