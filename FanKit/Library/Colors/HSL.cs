using System;
using Windows.UI;

namespace FanKit.Library.Colors
{
    /// <summary> Color form HSL </summary>
    public class HSL
    {
        /// <summary> Alpha </summary>
        public byte A;

        /// <summary> Hue </summary>
        public double H
        {
            get => h;
            set
            {
                if (value < 0) h = 0;
                else if (value > 360) h = 360;
                else h = value;
            }
        }
        private double h;

        /// <summary> Saturation </summary>
        public double S
        {
            get => s;
            set
            {
                if (value < 0) s = 0;
                else if (value > 100) s = 100;
                else s = value;
            }
        }
        private double s;

        /// <summary> Lightness </summary>
        public double L
        {
            get => l;
            set
            {
                if (value < 0) l = 0;
                else if (value > 100) l = 100;
                else l = value;
            }
        }
        private double l;


        public HSL(byte A, double H, double S, double L) { this.A = A; this.H = H; this.S = S; this.L = L; }


        /// <summary> RGB to HSL </summary>
        /// <param name="H"> Hue </param>
        /// <returns> Color </returns>
        public static Color HSLtoColor(double H)
        {
            double hh = H / 60;
            byte xhh = (byte)((1 - Math.Abs(hh % 2 - 1)) * 255);

            if (hh < 1) return Color.FromArgb(255, 255, xhh, 0);
            else if (hh < 2) return Color.FromArgb(255, xhh, 255, 0);
            else if (hh < 3) return Color.FromArgb(255, 0, 255, xhh);
            else if (hh < 4) return Color.FromArgb(255, 0, xhh, 255);
            else if (hh < 5) return Color.FromArgb(255, xhh, 0, 255);
            else return Color.FromArgb(255, 255, 0, xhh);
        }
        /// <summary> RGB to HSL </summary>
        /// <param name="hsl"> HSL </param>
        /// <returns> Color </returns>
        public static Color HSLtoRGB(HSL hsl)
        {
            if (hsl.S == 0)
            {
                byte l = (byte)(hsl.L * 255.0);
                return Color.FromArgb(hsl.A, l, l, l);
            }

            double var_2;
            if (hsl.L < 0.5) var_2 = hsl.L * (1 + hsl.S);
            else var_2 = (hsl.L + hsl.S) - (hsl.S * hsl.L);

            double var_1 = 2.0 * hsl.L - var_2;

            return Color.FromArgb
            (
               a: hsl.A,
               r: (byte)(255.0 * HSL.HtoRGB(var_1, var_2, hsl.H + (1.0 / 3.0))),
               g: (byte)(255.0 * HSL.HtoRGB(var_1, var_2, hsl.H)),
               b: (byte)(255.0 * HSL.HtoRGB(var_1, var_2, hsl.H - (1.0 / 3.0)))
            );
        }
        private static double HtoRGB(double v1, double v2, double vH)
        {
            if (vH < 0) vH += 1;
            if (vH > 1) vH -= 1;
            if (6.0 * vH < 1) return v1 + (v2 - v1) * 6.0 * vH;
            if (2.0 * vH < 1) return v2;
            if (3.0 * vH < 2) return v1 + (v2 - v1) * ((2.0 / 3.0) - vH) * 6.0;

            return v1;
        }


        /// <summary> RGB to HSL </summary>
        /// <param name="color"> Color </param>
        /// <returns> HSL </returns>
        public static HSL RGBtoHSL(Color color)
        {
            double del_R, del_G, del_B;

            double R = color.R / 255.0;
            double G = color.G / 255.0;
            double B = color.B / 255.0;

            double Min = Math.Min(R, Math.Min(G, B));//Min. value of RGB
            double Max = Math.Max(R, Math.Max(G, B));//Max. value of RGB
            double del_Max = Max - Min;//Delta RGB value

            double L = (Max + Min) / 2.0;
            double H = 0;
            double S;

            if (del_Max == 0)//This is a gray, no chroma...
            {
                //H = 2.0/3.0;  
                H = 0;
                S = 0;
            }
            else//Chromatic data...
            {
                if (L < 0.5) S = del_Max / (Max + Min);
                else S = del_Max / (2 - Max - Min);

                del_R = (((Max - R) / 6.0) + (del_Max / 2.0)) / del_Max;
                del_G = (((Max - G) / 6.0) + (del_Max / 2.0)) / del_Max;
                del_B = (((Max - B) / 6.0) + (del_Max / 2.0)) / del_Max;

                if (R == Max) H = del_B - del_G;
                else if (G == Max) H = (1.0 / 3.0) + del_R - del_B;
                else if (B == Max) H = (2.0 / 3.0) + del_G - del_R;

                if (H < 0) H += 1;
                if (H > 1) H -= 1;
            }

            return new HSL(color.A, H, S, L);
        }

    }
}
