using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas;
using System.Numerics;
using System.Threading.Tasks;
using FanKit.Library.Win2Ds;
using static FanKit.Library.Win2Ds.TransformController;

namespace FanKit.Frames.Win2Ds
{
    public sealed partial class TransformControllerPage : Page
    {
        TransformController.Layer Layer;
        TransformController.Controller Controller = new TransformController.Controller();

        public TransformControllerPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
         //   this.MarkdownText1.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Win2Ds/CanvasOperatorPage.xaml.txt");
         //   this.MarkdownText2.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Win2Ds/CanvasOperatorPage.xaml.cs.txt");
      //      this.MarkdownText3.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Win2Ds/CanvasOperator.cs.txt");
        }

        private void CanvasControl_CreateResources(CanvasControl sender, CanvasCreateResourcesEventArgs args) => args.TrackAsyncAction(this.CreateResourcesAsync(sender).AsAsyncAction());
        private async Task CreateResourcesAsync(CanvasControl sender)
        {
            CanvasBitmap bitmap = await CanvasBitmap.LoadAsync(sender, "Icon/Avatar.jpg");
            this.Layer = new Layer
            {
                Transformer = this.GetTransformer(bitmap.SizeInPixels.Width, bitmap.SizeInPixels.Height, bitmap.SizeInPixels.Width, bitmap.SizeInPixels.Height),
                Image = bitmap,
            };
        }
        private void CanvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawImage(new Transform2DEffect
            {
                TransformMatrix = this.Layer.Transformer.Matrix,
                Source = this.Layer.Image
            });

            Transformer.DrawBoundNodes(args.DrawingSession, this.Layer.Transformer);
        }



        private void CanvasControl_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            this.Controller.Start(e.GetCurrentPoint(this.CanvasControl).Position.ToVector2(), this.Layer);

            this.CanvasControl.Invalidate();
        }
        private void CanvasControl_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            this.Controller.Delta(e.GetCurrentPoint(this.CanvasControl).Position.ToVector2(), this.Layer);

            this.CanvasControl.Invalidate();
        }
        private void CanvasControl_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            this.Controller.Complete(e.GetCurrentPoint(this.CanvasControl).Position.ToVector2(), this.Layer);

            this.CanvasControl.Invalidate();
        }



        private TransformController.Transformer GetTransformer(double controlWidth, double controlHeight, double bitmapWidth, double bitmapHeight) => Transformer.CreateFromSize((float)bitmapWidth, (float)bitmapHeight, new Vector2((float)(controlWidth) / 2, (float)(controlHeight) / 2), (float)((bitmapWidth * 2 < controlWidth && bitmapHeight * 2 < controlWidth) ? 1.0 : (controlWidth > controlHeight) ? controlHeight / 2 / bitmapHeight : controlHeight / 2 / bitmapWidth));
        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Layer.Transformer = this.GetTransformer(this.CanvasControl.ActualWidth, this.CanvasControl.ActualHeight, this.Layer.Transformer.Width, this.Layer.Transformer.Height);

            this.CanvasControl.Invalidate();
        }
    }
}
