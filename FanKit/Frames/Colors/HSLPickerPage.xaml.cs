using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace FanKit.Frames.Colors
{
    public sealed partial class HSLPickerPage : Page
    {
        public HSLPickerPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
          this.HSLPicker.Color = Color.FromArgb(255, 0, 187, 255);

            this.MarkdownText1.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Colors/HSLPickerXaml.txt");
            this.MarkdownText2.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Colors/HSLPickerUserXaml.txt");
            this.MarkdownText3.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Colors/HSLPickerUserCs.txt");
        }


        private void HSLPicker_ColorChange(object sender, Color Value)=>  this.PaletteSolidBrush.Color = Value;


        /// <summary>
        /// HSL to RGB 
        /// </summary>
        /// <param name="A">A(W):0~100</param>
        /// <param name="H">H(X):0~360</param>
        /// <param name="S">S(Y):0~100</param>
        /// <param name="L">L(Z):0~100</param>
        /// <returns>Color form RGB</returns>
        public Color HSLtoRGB(byte A, double H, double S, double L)
        {
            double hh = H % 360.0;
            double ss = S / 100.0;
            double ll = L / 100.0;

            if (ss == 0.0)
            {
                byte lllllllll = (byte)(ll * 255.0);
                return Color.FromArgb((byte)A, lllllllll, lllllllll, lllllllll);
            }
            else
            {
                double dhh = hh / 60.0;
                int nhh = (int)Math.Floor(dhh);
                double rhh = dhh - nhh;

                double rr = ll * (1.0 - ss);
                double gg = ll * (1.0 - (ss * rhh));
                double bb = ll * (1.0 - (ss * (1.0 - rhh)));

                switch (nhh)
                {
                    case 0: return Color.FromArgb((byte)A, (byte)(ll * 255.0), (byte)(bb * 255.0), (byte)(rr * 255.0));
                    case 1: return Color.FromArgb((byte)A, (byte)(gg * 255.0), (byte)(ll * 255.0), (byte)(rr * 255.0));
                    case 2: return Color.FromArgb((byte)A, (byte)(rr * 255.0), (byte)(ll * 255.0), (byte)(bb * 255.0));
                    case 3: return Color.FromArgb((byte)A, (byte)(rr * 255.0), (byte)(gg * 255.0), (byte)(ll * 255.0));
                    case 4: return Color.FromArgb((byte)A, (byte)(bb * 255.0), (byte)(rr * 255.0), (byte)(ll * 255.0));
                    default: return Color.FromArgb((byte)A, (byte)(ll * 255.0), (byte)(rr * 255.0), (byte)(gg * 255.0));
                }
            }
        }

        /// <summary>
        /// RGB to HSL
        /// </summary>
        /// <param name="color">Color form RGB</param>
        /// <returns>A(W):0~100, H(X):0~360, S(Y):0~100, L(Z):0~100</returns>
        public HSL RGBtoHSL(Color color)
        {
            float R = color.R / 255.0f;
            float G = color.G / 255.0f;
            float B = color.B / 255.0f;

            float max = Math.Max(Math.Max(R, G), B);
            float min = Math.Min(Math.Min(R, G), B);
            float L = (min + max) / 2.0f;

            if (L <= 0.0) return new HSL(color.A, 0, 0, 0);

            float dist = max - min;
            float S = dist;

            if (S > 0.0)
            {
                if (L <= 0.5f) S /= (max + min);
                else S /= (2.0f - max - min);
            }
            else return new HSL(color.A, 0, 0, 0);

            float rr = (max - R) / dist;
            float gg = (max - G) / dist;
            float bb = (max - B) / dist;

            float H;
            if (R == max)
            {
                if (G == min) H = 5.0f + bb;
                else H = 1.0f - gg;
            }
            else if (G == max)
            {
                if (B == min) H = 3.0f - bb;
                else H = 3.0f - bb;
            }
            else// if (B == max)
            {
                if (R == min) H = 3.0f + gg;
                else H = 5.0f - rr;
            }

            return new HSL(color.A, (float)(H * 60d), (float)(S * 100.0d), (float)(L * 200.0d));
        }


    } 
}
