using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
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
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace FanKit.Library.Colors
{
    public sealed partial class StrawPicker : UserControl
    {
        //Delegate
        public delegate void ColorChangeHandler(object sender, Color value);
        public event ColorChangeHandler ColorChangeStarted = null;
        public event ColorChangeHandler ColorChangeDelta = null;
        public event ColorChangeHandler ColorChangeCompleted = null;

        //Popup
        Popup Popup;
        float PopupSize = 100;

        //Canvas
        CanvasDevice Device = new CanvasDevice();
        CanvasControl CanvasControl;
        CanvasBitmap Bitmap;

        //Color
        Vector2 Vector;


        #region DependencyProperty


        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(nameof(Color), typeof(Color), typeof(StrawPicker), new PropertyMetadata(Windows.UI.Colors.White, (sender, e) =>
        {
            StrawPicker con = (StrawPicker)sender;

            if (e.NewValue is Color value)
            {
                con.SolidColorBrushName.Color = value;
            }
        }));


        #endregion


        public StrawPicker()
        {
            this.InitializeComponent();

            //Canvas
            this.CanvasControl = new CanvasControl
            {
                UseSharedDevice = true,
                CustomDevice = this.Device
            };
            this.CanvasControl.Draw += this.CanvasControl_Draw;

            //Popup
            this.Popup = new Popup
            {
                IsOpen = false,
                Child = new Border
                {
                    Width = this.PopupSize,
                    Height = this.PopupSize,
                    CornerRadius = new CornerRadius(this.PopupSize / 2),
                    BorderThickness = new Thickness(1),
                    BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black),
                    Child = this.CanvasControl,
                }
            };

        }

        //Canvas
        private void CanvasControl_Draw(CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            args.DrawingSession.Transform =
               Matrix3x2.CreateTranslation(-this.Vector.X + this.PopupSize, -this.Vector.Y + this.PopupSize) *
               Matrix3x2.CreateScale(new Vector2(2)) *
               Matrix3x2.CreateTranslation(-this.PopupSize, -this.PopupSize);

            if (this.Bitmap != null)
            {
                args.DrawingSession.DrawImage(new DpiCompensationEffect
                {
                    SourceDpi = new Vector2(sender.Dpi),
                    Source = this.Bitmap
                });
            }
        }



        #region Popup & Color


        private void Border_PointerPressed(object sender, PointerRoutedEventArgs e) => Vector = e.GetCurrentPoint(Window.Current.Content).Position.ToVector2();

        private async void Border_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            //Canvas
            this.Bitmap = await this.GetRenderTargetBitmap(this.Device, Window.Current.Content);
            this.Color = this.GetColor(this.Bitmap, this.Vector);
            this.CanvasControl.Invalidate();

            //Popup
            this.Popup.HorizontalOffset = this.Vector.X - 50;
            this.Popup.VerticalOffset = this.Vector.Y - 50;
            this.Popup.IsOpen = true;

            this.ColorChangeStarted?.Invoke(this, this.Color);//Delegate
        }

        private void Border_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            //Canvas
            this.Vector += e.Delta.Translation.ToVector2();
            this.Color = this.GetColor(this.Bitmap, this.Vector);
            this.CanvasControl.Invalidate();

            //Popup
            this.Popup.HorizontalOffset = this.Vector.X - 50;
            this.Popup.VerticalOffset = this.Vector.Y - 50;

            this.ColorChangeDelta?.Invoke(this, this.Color);//Delegate
        }

        private void Border_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            this.Color = this.GetColor(this.Bitmap, this.Vector);

            if (this.Bitmap != null)
            {
                this.Bitmap.Dispose();
                this.Bitmap = null;
            }

            //Popup         
            this.Popup.IsOpen = false;

            this.ColorChangeCompleted?.Invoke(this, this.Color);//Delegate
        }


        #endregion


        #region Color


        private async Task<CanvasBitmap> GetRenderTargetBitmap(ICanvasResourceCreator creator, UIElement element)
        {
            RenderTargetBitmap render = new RenderTargetBitmap();
            await render.RenderAsync(element);
            return CanvasBitmap.CreateFromBytes(creator, await render.GetPixelsAsync(), render.PixelWidth, render.PixelHeight, DirectXPixelFormat.B8G8R8A8UIntNormalized);
        }

        private Color GetColor(CanvasBitmap bitmap, Vector2 v)
        {
            if (bitmap != null)
            {
                int left = GetLeft((int)bitmap.SizeInPixels.Width, v.X, Window.Current.Bounds.Width);
                int top = GetLeft((int)bitmap.SizeInPixels.Height, v.Y, Window.Current.Bounds.Height);

                return bitmap.GetPixelColors(left, top, 1, 1).Single();
            }
            else return Windows.UI.Colors.White;
        }

        private int GetLeft(int bitmapWidth, float x, double windowWidth)
        {
            int left = (int)(bitmapWidth * x / windowWidth);

            if (left < 0) return 0;
            else if (left >= bitmapWidth) return bitmapWidth - 1;
            return left;
        }


        #endregion


    }
}
