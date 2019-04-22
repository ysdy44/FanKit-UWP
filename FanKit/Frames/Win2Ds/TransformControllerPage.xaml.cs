using FanKit.Win2Ds;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Numerics;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using static FanKit.Win2Ds.TransformController;

namespace FanKit.Frames.Win2Ds
{
    public sealed partial class TransformControllerPage : Page
    {
        TransformController.Layer Layer;
        TransformController.Controller Controller = new TransformController.Controller();

        bool IsMove;

        public TransformControllerPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Win2Ds/TransformControllerPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Win2Ds/TransformControllerPage.xaml.cs.txt");
                this.MarkdownText3.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Win2Ds/TransformController.cs.txt");
            };

            //Center
            this.CenterCheckBox.Checked += (s, e) => TransformController.IsCenter = true;
            this.CenterCheckBox.Unchecked += (s, e) => TransformController.IsCenter = false;
            //Ratio
            this.RatioCheckBox.Checked += (s, e) => TransformController.IsRatio = true;
            this.RatioCheckBox.Unchecked += (s, e) => TransformController.IsRatio = false;
            //StepFrequency
            this.StepFrequencyCheckBox.Checked += (s, e) => TransformController.IsStepFrequency = true;
            this.StepFrequencyCheckBox.Unchecked += (s, e) => TransformController.IsStepFrequency = false;

            this.Button.Tapped += (s, e) =>
            {
                this.Layer.Transformer = this.GetTransformer(this.CanvasControl.ActualWidth, this.CanvasControl.ActualHeight, this.Layer.Transformer.Width, this.Layer.Transformer.Height);

                this.CanvasControl.Invalidate();
            };


            //Canvas 
            this.CanvasControl.CreateResources += (sender, args) => args.TrackAsyncAction(this.CreateResourcesAsync(sender).AsAsyncAction());
            this.CanvasControl.Draw += (sender, args) =>
            {
                this.RunX.Text = ((int)this.Layer.Transformer.Position.X).ToString();
                this.RunY.Text = ((int)this.Layer.Transformer.Position.Y).ToString();

                this.RunW.Text = ((int)(100 * this.Layer.Transformer.XScale)).ToString();
                this.RunH.Text = ((int)(100 * this.Layer.Transformer.YScale)).ToString();

                this.RunR.Text = ((int)(this.Layer.Transformer.Radian / Transformer.PI * 180)).ToString();
                this.RunS.Text = ((int)(this.Layer.Transformer.Skew / Transformer.PI * 180)).ToString();

                args.DrawingSession.DrawImage(new Transform2DEffect
                {
                    TransformMatrix = this.Layer.Transformer.Matrix,
                    Source = this.Layer.Image
                });

                Transformer.DrawBoundNodes(args.DrawingSession, this.Layer.Transformer, this.Layer.Transformer.Matrix);
            };


            //Pointer
            this.CanvasControl.PointerPressed += (s, e) =>
            {
                this.IsMove = true;

                Vector2 point = e.GetCurrentPoint(this.CanvasControl).Position.ToVector2();
                this.Controller.Start(point, this.Layer, this.Layer.Transformer.Matrix, 1);
                this.CanvasControl.Invalidate();
            };
            this.CanvasControl.PointerMoved += (s, e) =>
            {
                if (this.IsMove)
                {
                    Vector2 point = e.GetCurrentPoint(this.CanvasControl).Position.ToVector2();
                    this.Controller.Delta(point, this.Layer, this.Layer.Transformer.Matrix, 1);
                    this.CanvasControl.Invalidate();
                }
            };
            this.CanvasControl.PointerReleased += (s, e) =>
            {
                if (this.IsMove)
                {
                    this.IsMove = false;

                    Vector2 point = e.GetCurrentPoint(this.CanvasControl).Position.ToVector2();
                    this.Controller.Complete(point, this.Layer, this.Layer.Transformer.Matrix, 1);
                    this.CanvasControl.Invalidate();
                }
            };
        }


        private TransformController.Transformer GetTransformer(double controlWidth, double controlHeight, double bitmapWidth, double bitmapHeight) => Transformer.CreateFromSize(width: (float)bitmapWidth, height: (float)bitmapHeight, postion: new Vector2((float)controlWidth / 2, (float)controlHeight / 2), scale: (float)((bitmapWidth * 2 < controlWidth && bitmapHeight * 2 < controlWidth) ? 1.0 : (controlWidth > controlHeight) ? controlHeight / 2 / bitmapHeight : controlHeight / 2 / bitmapWidth));

        private async Task CreateResourcesAsync(CanvasControl sender)
        {
            CanvasBitmap bitmap = await CanvasBitmap.LoadAsync(sender, "Icon/Avatar.jpg");
            this.Layer = new Layer
            {
                Transformer = this.GetTransformer(bitmap.SizeInPixels.Width, bitmap.SizeInPixels.Height, bitmap.SizeInPixels.Width, bitmap.SizeInPixels.Height),
                Image = bitmap,
            };
        }
    }
}


