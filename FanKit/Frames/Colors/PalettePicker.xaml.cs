﻿using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Numerics;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

namespace FanKit.Frames.Colors
{
    public partial class PalettePicker : UserControl
    {
        //Delegate
        public delegate void ColorChangeHandler(object sender, Color Value);
        public event ColorChangeHandler ColorChange = null;

        //Value
        public Vector2 Center = new Vector2(50, 50);
        public float SquareWidth = 100;
        public float SquareHeight = 100;
        public float SquareHalfWidth => this.SquareWidth / 2;
        public float SquareHalfHeight => this.SquareHeight / 2;
        public float StrokePadding = 12;

        #region DependencyProperty


        protected Color color = Color.FromArgb(255, 255, 255, 255);
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
        public static readonly DependencyProperty HSLProperty = DependencyProperty.Register(nameof(HSL), typeof(HSL), typeof(PalettePicker), new PropertyMetadata(new HSL(255, 360, 100, 100), new PropertyChangedCallback(HSLOnChanged)));
        private static void HSLOnChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            PalettePicker con = (PalettePicker)sender;

            if (e.NewValue is HSL NewValue) con.HSLChanged(NewValue);
        }
        public virtual void HSLChanged(HSL value)
        {
            byte A = HSL.A;
            double H = HSL.H;
            double S = HSL.S;
            double L = HSL.L;

            this.Slider.Value = this.Picker.Value = this.PaletteBase.GetValue(value);
            this.Slider.SliderBackground = this.PaletteBase.GetSliderBrush(value);

            this.CanvasControl.Invalidate();

            this.color = HSLtoRGB(A,H,S,L);
            this.ColorChange?.Invoke(this, this.Color);
        }


        #endregion


        private PaletteBase paletteBase;
        public PaletteBase PaletteBase
        {
            get => paletteBase;
            set
            {
                if (value != null)
                {
                    this.Picker.Unit = value.Unit;
                    this.Slider.Minimum = this.Picker.Minimum = value.Minimum;
                    this.Slider.Maximum = this.Picker.Maximum = value.Maximum;

                    this.Slider.Value = this.Picker.Value = value.GetValue(this.HSL);
                    this.Slider.SliderBackground = value.GetSliderBrush(this.HSL);

                    this.CanvasControl.Invalidate();

                    this.paletteBase = value;
                }
            }
        }
        public List<PaletteBase> PaletteBases = new List<PaletteBase>
        {
            new PaletteHue(),
            new PaletteSaturation(),
            new PaletteLightness(),
        };

        public PalettePicker()
        {
            this.InitializeComponent();

            this.ComboBox.ItemsSource = this.PaletteBases;
            this.ComboBox.SelectionChanged += (s, e) => this.PaletteBase = this.PaletteBases[this.ComboBox.SelectedIndex];
            this.ComboBox.Loaded += (s, e) => this.ComboBox.SelectedIndex = 0;
        }


        private void Picker_ValueChange(object sender, int Value)
        {
            if (this.PaletteBase!=null) this.HSL = this.PaletteBase.GetHSL(this.HSL, Value);
        }
        private void Slider_ValueChangeDelta(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (this.PaletteBase != null) this.HSL = this.PaletteBase.GetHSL(this.HSL, (int)e.NewValue);
        }


        private void CanvasControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Center = e.NewSize.ToVector2() / 2;

            this.SquareWidth = (float)e.NewSize.Width - this.StrokePadding * 2;
            this.SquareHeight = (float)e.NewSize.Height - this.StrokePadding * 2;
        }
        private void CanvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args) =>this.PaletteBase.Draw(this.CanvasControl, args.DrawingSession,this.HSL,this.Center,this.SquareHalfWidth,this.SquareHalfHeight);


        protected bool IsPalette = false;
        protected Vector2 v;
        private void CanvasControl_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            v = e.Position.ToVector2() - this.Center;

            this.IsPalette = Math.Abs(v.X) < this.SquareWidth && Math.Abs(v.Y) < this.SquareHeight;
        }
        private void CanvasControl_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (this.IsPalette)
            {
                v += e.Delta.Translation.ToVector2();

                this.HSL = this.PaletteBase.Delta(this.HSL, v, this.SquareHalfWidth, this.SquareHalfHeight);
            }
        }
        private void CanvasControl_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e) => this.IsPalette = false;


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


        #endregion   }
    }
}














