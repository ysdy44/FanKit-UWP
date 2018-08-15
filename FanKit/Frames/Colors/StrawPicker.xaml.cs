using Microsoft.Graphics.Canvas;
using System;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Windows.Graphics.DirectX;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace FanKit.Frames.Colors
{
    public sealed partial class StrawPicker : UserControl
    {
        //Delegate
        public delegate void ColorChangeHandler(object sender, Color Value);
        public event ColorChangeHandler ColorChange = null;

        Popup popup = new Popup();
        CanvasDevice device = new CanvasDevice();
        CanvasBitmap bitmap;


        #region DependencyProperty


        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(nameof(Color), typeof(Color), typeof(StrawPicker), new PropertyMetadata(Windows.UI.Colors.White, new PropertyChangedCallback(ColorOnChanged)));
        private static void ColorOnChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            StrawPicker con = (StrawPicker)sender;

            if (e.NewValue is Color value)
            {
                con.ColorChange?.Invoke(con, value);
            }
        }


        #endregion


        public StrawPicker()
        {
            this.InitializeComponent();

            //Popup
            UIElement element = this._popup.Child;
            this._popup.Child = null;
            popup.Child = element;
        }


        Vector2 v;
        private async void Border_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            v = e.GetCurrentPoint(Window.Current.Content).Position.ToVector2();
            this.bitmap = await this.GetRenderTargetBitmap(Window.Current.Content);

            //Popup
            popup.HorizontalOffset = v.X - 50;
            popup.VerticalOffset = v.Y - 50;
            popup.IsOpen = true;
        }
        private void Border_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            v += e.Delta.Translation.ToVector2();

            if (this.bitmap != null)
            {
                int left = GetLeft((int)this.bitmap.SizeInPixels.Width, v.X, Window.Current.Bounds.Width);
                int top = GetLeft((int)this.bitmap.SizeInPixels.Height, v.Y, Window.Current.Bounds.Height);

                this.Color = this.bitmap.GetPixelColors(left, top, 1, 1).Single();
                this.SolidColorBrushName.Color = this.Color;
            }

            //Popup
            popup.HorizontalOffset = v.X - 50;
            popup.VerticalOffset = v.Y - 50;
        }
        private void Border_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            this.bitmap.Dispose();
            this.bitmap = null;
            this.SolidColorBrushName000.Color = this.Color;

            //Popup         
            popup.IsOpen = false;
        }


        private int GetLeft(int BitmapWidth, float X, double WindowWidth)
        {
            int left = (int)(BitmapWidth * X / WindowWidth);

            if (left < 0) return 0;
            else if (left >= BitmapWidth) return BitmapWidth - 1;
            return left;
        }
        private async Task<CanvasBitmap> GetRenderTargetBitmap(UIElement element)
        {
            RenderTargetBitmap render = new RenderTargetBitmap();
            await render.RenderAsync(element);
            return CanvasBitmap.CreateFromBytes(this.device, await render.GetPixelsAsync(), render.PixelWidth, render.PixelHeight, DirectXPixelFormat.B8G8R8A8UIntNormalized);
        }

    }
}




