using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Numerics;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;


namespace FanKit.Frames.Colors
{
    public sealed partial class WheelPicker : UserControl
    {

        //Delegate
        public delegate void ColorChangeHandler(object sender, Color Value);
        public event ColorChangeHandler ColorChange = null;


        #region DependencyProperty


        private Color color = Color.FromArgb(255, 255, 255, 255);
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
        public static readonly DependencyProperty HSLProperty = DependencyProperty.Register(nameof(HSL), typeof(HSL), typeof(WheelPicker), new PropertyMetadata(new HSL(255, 360, 100, 100), new PropertyChangedCallback(HSLOnChanged)));
        private static void HSLOnChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            WheelPicker con = (WheelPicker)sender;

            if (e.NewValue is HSL NewValue) con.HSLChanged(NewValue);
        }
        private void HSLChanged(HSL value)
        {
            double H = value.H;
            double S = value.S;
            double L = value.L;



            Color c = HSLtoRGB(255, H, S, L);
            this.color = c;
            this.ColorChange?.Invoke(this, c);
        }


        #endregion


        Vector2 Center = new Vector2(50, 50);
        float Radio = 100;

        float StrokeWidth = 8;
        float SquareRadio => (this.Radio - this.StrokeWidth) / 1.414213562373095f;


        public WheelPicker()
        {
            this.InitializeComponent();
        }


        private void CanvasControl_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            this.Center = e.NewSize.ToVector2() / 2;

            this.Radio = (float)Math.Min(e.NewSize.Width, e.NewSize.Height) / 2 - this.StrokeWidth;
        }
        private void CanvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            //Wheel
            for (float angle = 0; angle < (float)Math.PI * 2; angle += (float)(2 * Math.PI) / (int)(Math.PI * Radio * 2 / this.StrokeWidth)) args.DrawingSession.FillCircle((float)Math.Cos(angle) * this.Radio + this.Center.X, (float)Math.Sin(angle) * this.Radio + this.Center.Y, this.StrokeWidth, HSLtoRGB(((angle * 180.0 / Math.PI) + 360.0) % 360.0));
            args.DrawingSession.DrawCircle(this.Center, this.Radio - this.StrokeWidth, Windows.UI.Colors.Gray);
            args.DrawingSession.DrawCircle(this.Center, this.Radio + this.StrokeWidth, Windows.UI.Colors.Gray);

            //Thumb
            double ang = (float)(((this.HSL.H + 360.0) % 360.0) * Math.PI / 180.0);
            float wx = (float)Math.Cos(ang) * this.Radio + this.Center.X;
            float wy = (float)Math.Sin(ang) * this.Radio + this.Center.Y;
            args.DrawingSession.DrawCircle(wx, wy, 8, Windows.UI.Colors.Black, 4);
            args.DrawingSession.DrawCircle(wx, wy, 8, Windows.UI.Colors.White, 2);


            //Palette
            Rect rect = new Rect(this.Center.X - this.SquareRadio, this.Center.Y - this.SquareRadio, this.SquareRadio * 2, this.SquareRadio * 2);
            args.DrawingSession.FillRoundedRectangle(rect, 4, 4, new CanvasLinearGradientBrush(this.CanvasControl, Windows.UI.Colors.White, HSLtoRGB(this.HSL.H)) { StartPoint = new Vector2(this.Center.X - this.SquareRadio, this.Center.Y), EndPoint = new Vector2(this.Center.X + this.SquareRadio, this.Center.Y) });
            args.DrawingSession.FillRoundedRectangle(rect, 4, 4, new CanvasLinearGradientBrush(this.CanvasControl, Windows.UI.Colors.Transparent, Windows.UI.Colors.Black) { StartPoint = new Vector2(this.Center.X, this.Center.Y - this.SquareRadio), EndPoint = new Vector2(this.Center.X, this.Center.Y + this.SquareRadio) });
            args.DrawingSession.DrawRoundedRectangle(rect, 4, 4, Windows.UI.Colors.Gray);

            //Thumb 
            float px = ((float)this.HSL.S - 50) * this.SquareRadio / 50 + this.Center.X;
            float py = (50 - (float)this.HSL.L) * this.SquareRadio / 50 + this.Center.Y;
            args.DrawingSession.DrawCircle(px, py, 8, Windows.UI.Colors.Black, 4);
            args.DrawingSession.DrawCircle(px, py, 8, Windows.UI.Colors.White, 2);
        }



        bool IsWheel = false;
        bool IsPalette = false;
        Vector2 sssssv;
        private void CanvasControl_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            sssssv = e.Position.ToVector2();
            Vector2 v = e.Position.ToVector2() - this.Center;

            this.IsWheel = v.Length() + this.StrokeWidth > this.Radio && v.Length() - this.StrokeWidth < this.Radio;
            this.IsPalette = Math.Abs(v.X) < this.SquareRadio && Math.Abs(v.Y) < this.SquareRadio;

            this.CanvasControl.Invalidate();
        }
        private void CanvasControl_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (this.IsWheel|| this.IsPalette)
            {
                double  H = (((Math.Atan2(sssssv.Y + e.Cumulative.Translation.Y- this.Center.Y, sssssv.X + e.Cumulative.Translation.X  - this.Center.X)) * 180.0 / Math.PI) + 360.0) % 360.0;
                double S = (sssssv.X+e.Cumulative.Translation.X - this.Center.X) * 50 / this.SquareRadio + 50;
                double L = 50 - (sssssv.Y+ e.Cumulative.Translation.Y  - this.Center.Y) * 50 / this.SquareRadio;

                this.HSL = new HSL(this.HSL.A,this.IsWheel ? H : this.HSL.H,this.IsPalette ? S : this.HSL.S,this.IsPalette ? L : this.HSL.L);
                this.CanvasControl.Invalidate();
            }
        }
        private void CanvasControl_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e) => this.IsWheel = this.IsPalette = false;


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
}
