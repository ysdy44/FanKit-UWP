using FanKit.Library.Win2Ds;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Numerics;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
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

            //Center
            this.CenterCheckBox.Checked += (sender, e) => TransformController.IsCenter = true;
            this.CenterCheckBox.Unchecked += (sender, e) => TransformController.IsCenter = false;
            //Ratio
            this.RatioCheckBox.Checked += (sender, e) => TransformController.IsRatio = true;
            this.RatioCheckBox.Unchecked += (sender, e) => TransformController.IsRatio = false;
            //StepFrequency
            this.StepFrequencyCheckBox.Checked += (sender, e) => TransformController.IsStepFrequency = true;
            this.StepFrequencyCheckBox.Unchecked += (sender, e) => TransformController.IsStepFrequency = false;
        }


        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.MarkdownText1.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Win2Ds/TransformControllerPage.xaml.txt");
            this.MarkdownText2.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Win2Ds/TransformControllerPage.xaml.cs.txt");
            this.MarkdownText3.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Win2Ds/TransformController.cs.txt");
        }


        private TransformController.Transformer GetTransformer(double controlWidth, double controlHeight, double bitmapWidth, double bitmapHeight) => Transformer.CreateFromSize(width: (float)bitmapWidth, height: (float)bitmapHeight, postion: new Vector2((float)controlWidth / 2, (float)controlHeight / 2), scale: (float)((bitmapWidth * 2 < controlWidth && bitmapHeight * 2 < controlWidth) ? 1.0 : (controlWidth > controlHeight) ? controlHeight / 2 / bitmapHeight : controlHeight / 2 / bitmapWidth));
        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Layer.Transformer = this.GetTransformer(this.CanvasControl.ActualWidth, this.CanvasControl.ActualHeight, this.Layer.Transformer.Width, this.Layer.Transformer.Height);

            this.CanvasControl.Invalidate();
        }


        #region Canvas


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
            this.RunX.Text = ((int)this.Layer.Transformer.Postion.X).ToString();
            this.RunY.Text = ((int)this.Layer.Transformer.Postion.Y).ToString();

            //this.RunW.Text = ((int)(this.Layer.Transformer.Width * this.Layer.Transformer.XScale)).ToString();
            //this.RunH.Text = ((int)(this.Layer.Transformer.Height * this.Layer.Transformer.YScale)).ToString();
            this.RunW.Text = ((int)(100 * this.Layer.Transformer.XScale)).ToString();
            this.RunH.Text = ((int)(100 * this.Layer.Transformer.YScale)).ToString();

            this.RunR.Text = ((int)(this.Layer.Transformer.Radian /Transformer.PI * 180)).ToString();
            this.RunS.Text = ((int)(this.Layer.Transformer.Skew / Transformer.PI * 180)).ToString();

            args.DrawingSession.DrawImage(new Transform2DEffect
            {
                TransformMatrix = this.Layer.Transformer.Matrix,
                Source = this.Layer.Image
            });

            Transformer.DrawBoundNodes(args.DrawingSession, this.Layer.Transformer, this.Layer.Transformer.Matrix);
        }


        #endregion


        #region Pointer


        bool IsMove;
        private void CanvasControl_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            this.IsMove = true;

            Vector2 point = e.GetCurrentPoint(this.CanvasControl).Position.ToVector2();
            this.Controller.Start(point, this.Layer, this.Layer.Transformer.Matrix,1);
            this.CanvasControl.Invalidate();
        }
        private void CanvasControl_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (this.IsMove)
            {
                Vector2 point = e.GetCurrentPoint(this.CanvasControl).Position.ToVector2();
                this.Controller.Delta(point, this.Layer, this.Layer.Transformer.Matrix,1);
                this.CanvasControl.Invalidate();
            }         
        }
        private void CanvasControl_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (this.IsMove)
            {
                this.IsMove = false;

                Vector2 point = e.GetCurrentPoint(this.CanvasControl).Position.ToVector2();
                this.Controller.Complete(point, this.Layer, this.Layer.Transformer.Matrix,1);
                this.CanvasControl.Invalidate();
            }
        }


        #endregion

    }
}


