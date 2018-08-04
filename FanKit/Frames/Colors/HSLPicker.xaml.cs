using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace FanKit.Frames.Colors
{
    public sealed partial class HSLPicker : UserControl
    {

        //Delegate
        public delegate void ColorChangeHandler(object sender, Color Value);
        public event ColorChangeHandler ColorChange = null;


        #region DependencyProperty


        private Color color = Color.FromArgb(255,255,255,255);
        public Color Color
        {
            get => color;
            set
            {
                color = value;
                this.HSL = RGBtoHSL(value);
            }
        }


        public HSL HSL
        {
            get { return (HSL)GetValue(HSLProperty); }
            set { SetValue(HSLProperty, value); }
        }
        public static readonly DependencyProperty HSLProperty = DependencyProperty.Register(nameof(HSL), typeof(HSL), typeof(HSLPicker), new PropertyMetadata(new HSL(255,360,100, 100), new PropertyChangedCallback(HSLOnChanged)));
        private static void HSLOnChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            HSLPicker con = (HSLPicker)sender;

            if (e.NewValue is HSL NewValue) con.HSLChanged(NewValue);
        }
        private void HSLChanged(HSL value)
        {
            byte A = value.A;
            double H = value.H;
            double S = value.S;
            double L = value.L;

            //H          
           this. HSlider.Value = this.HPicker.Value = (int)H;
            this.HG.Color = this.HA.Color = HSLtoRGB(A, 0, S, L);
            this.HB.Color = HSLtoRGB(A, 60, S, L);
            this.HC.Color = HSLtoRGB(A, 120, S, L);
            this.HD.Color = HSLtoRGB(A, 180, S, L);
            this.HE.Color = HSLtoRGB(A, 240, S, L);
            this.HF.Color = HSLtoRGB(A, 300, S, L);
            //S
            this.SSlider.Value = SPicker.Value = (int)S;
            this.SLeft.Color = HSLtoRGB(A, H, 0.0d, L);
            this.SRight.Color = HSLtoRGB(A, H, 100.0d, L);
            //L
            this.LSlider.Value = LPicker.Value = (int)L;
            this.LLeft.Color = HSLtoRGB(A, H, S, 0.0d);
            this.LRight.Color = HSLtoRGB(A, H, S, 100.0d);

            this.color = HSLtoRGB(A, H, S, L);
            this.ColorChange?.Invoke(this, this.color);
        }


        #endregion


        public HSLPicker()
        {
            this.InitializeComponent();
        }


        private void HSlider_ValueChangeDelta(object sender, RangeBaseValueChangedEventArgs e) => this.HSL = new HSL(this.HSL.A, e.NewValue, this.HSL.S, this.HSL.L);
        private void SSlider_ValueChangeDelta(object sender, RangeBaseValueChangedEventArgs e) => this.HSL = new HSL(this.HSL.A, this.HSL.H, (int)e.NewValue, this.HSL.L);
        private void LSlider_ValueChangeDelta(object sender, RangeBaseValueChangedEventArgs e) => this.HSL = new HSL(this.HSL.A, this.HSL.H, this.HSL.S, (int)e.NewValue);

        private void HPicker_ValueChange(object sender, int Value) => this.HSL = new HSL(this.HSL.A, Value, this.HSL.S, this.HSL.L);
        private void SPicker_ValueChange(object sender, int Value) => this.HSL = new HSL(this.HSL.A, this.HSL.H, (int)Value, this.HSL.L);
        private void LPicker_ValueChange(object sender, int Value) => this.HSL = new HSL(this.HSL.A, this.HSL.H, this.HSL.S, (int)Value);


        #region RGB HSL


        /// <summary>
        /// HSL to RGB 
        /// </summary>
        /// <param name="A">A(W):0~255</param>
        /// <param name="H">H(X):0~360</param>
        /// <param name="S">S(Y):0~100</param>
        /// <param name="L">L(Z):0~100</param>
        /// <returns>Color form RGB</returns>
        public static Color HSLtoRGB(byte A, double H, double S, double L)
        {
            double s = S / 100.0;
            double l = L / 100.0;
            byte ll = (byte)(l * 255.0);

            if (s == 0.0) return Color.FromArgb(A, ll, ll, ll);

            double hh = H % 360.0;
            double dhh = hh / 60.0;
            int nhh = (int)Math.Floor(dhh);
            double rhh = dhh - nhh;

            byte rr = (byte)(l * (1.0 - s) * 255.0);
            byte gg = (byte)(l * (1.0 - (s * rhh)) * 255.0);
            byte bb = (byte)(l * (1.0 - (s * (1.0 - rhh))) * 255.0);

            switch (nhh)
            {
                case 0: return Color.FromArgb(A, ll, bb, rr);
                case 1: return Color.FromArgb(A, gg, ll, rr);
                case 2: return Color.FromArgb(A, rr, ll, bb);
                case 3: return Color.FromArgb(A, rr, gg, ll);
                case 4: return Color.FromArgb(A, bb, rr, ll);
                default: return Color.FromArgb(A, ll, rr, gg);
            }
        }
        public static Color HSLtoRGB(double H)
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


        /// <summary>
        /// RGB to HSL
        /// </summary>
        /// <param name="color">Color form RGB</param>
        /// <returns>A(W):0~255, H(X):0~360, S(Y):0~100, L(Z):0~100</returns>
        public static HSL RGBtoHSL(Color color)
        {
            double R = color.R / 255.0;
            double G = color.G / 255.0;
            double B = color.B / 255.0;

            double max = Math.Max(Math.Max(R, G), B);
            double min = Math.Min(Math.Min(R, G), B);

            double S = max - min;
            double L = (min + max) / 2.0f;

            if (L <= 0.0) return new HSL(color.A, 0, 0, 0);

            if (S > 0.0)
            {
                if (L <= 0.5f) S /= (max + min);
                else S /= (2.0f - max - min);
            }
            else return new HSL(color.A, 0, 0, 0);

            double rr = (max - R) / S;
            double gg = (max - G) / S;
            double bb = (max - B) / S;

            double H;
            if (R == max)
            {
                if (G == min) H = 5.0f + bb;
                else H = 1.0f - gg;
            }
            else if (G == max)
            {
                if (B == min) H = 3.0 - bb;
                else H = 3.0 - bb;
            }
            else// if (B == max)
            {
                if (R == min) H = 3.0 + gg;
                else H = 5.0 - rr;
            }

            return new HSL(color.A, (float)(H * 60.0), (float)(S * 100.0), (float)(L * 200.0));
        }


        #endregion

    }
    

    /// <summary>
    /// Color form HSL
    /// </summary>
    public class HSL
    {
        public byte A;

        private double h;
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

        private double s;
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

        private double l;
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

        public HSL(byte A, double H, double S, double L) { this.A = A; this.H = H; this.S = S; this.L = L; }
    }
}
