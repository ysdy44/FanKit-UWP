using FanKit.Win2Ds;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Numerics;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using static FanKit.Win2Ds.HomographyController;

namespace FanKit.Frames.Win2Ds
{
    public sealed partial class HomographyControllerPage : Page
    {
        public readonly Matrix3x2 Matrix = Matrix3x2.CreateTranslation(Vector2.Zero);
        public readonly Matrix3x2 InverseMatrix = Matrix3x2.CreateTranslation(Vector2.Zero);

        HomographyController.Layer Layer;
        HomographyController.Controller Controller = new HomographyController.Controller();

        public HomographyControllerPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Win2Ds/HomographyControllerPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Win2Ds/HomographyControllerPage.xaml.cs.txt");
                this.MarkdownText3.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Win2Ds/HomographyController.cs.txt");
            };

            //Center
            this.CenterCheckBox.Checked += (sender, e) => HomographyController.IsCenter = true;
            this.CenterCheckBox.Unchecked += (sender, e) => HomographyController.IsCenter = false;
            //Ratio
            this.RatioCheckBox.Checked += (sender, e) => HomographyController.IsRatio = true;
            this.RatioCheckBox.Unchecked += (sender, e) => HomographyController.IsRatio = false;
            //StepFrequency
            this.StepFrequencyCheckBox.Checked += (sender, e) => HomographyController.IsStepFrequency = true;
            this.StepFrequencyCheckBox.Unchecked += (sender, e) => HomographyController.IsStepFrequency = false;
        }

        private HomographyController.Transformer GetTransformer(double controlWidth, double controlHeight, uint bitmapWidth, uint bitmapHeight)
        {
            float scale = Math.Min((float)controlWidth / bitmapWidth, (float)controlHeight / bitmapHeight)/2;
            Vector2 postion = new Vector2((float)controlWidth - scale * bitmapWidth, (float)controlHeight - scale * bitmapHeight) /2;
            return Transformer.CreateFromSize(bitmapWidth, bitmapHeight, postion, scale);
        }
        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Transformer.CopyWith(this.Layer, this.GetTransformer(this.CanvasControl.ActualWidth, this.CanvasControl.ActualHeight, this.Layer.Image.SizeInPixels.Width, this.Layer.Image.SizeInPixels.Height));

            this.CanvasControl.Invalidate();
        }


        #region Canvas


        private void CanvasControl_CreateResources(CanvasControl sender, CanvasCreateResourcesEventArgs args) => args.TrackAsyncAction(this.CreateResourcesAsync(sender).AsAsyncAction());
        private async Task CreateResourcesAsync(CanvasControl sender)
        {
            CanvasBitmap bitmap = await CanvasBitmap.LoadAsync(sender, "Icon/Avatar.jpg");
            this.Layer = new Layer
            {
                Transformer = this.GetTransformer(this.CanvasControl.ActualWidth, this.CanvasControl.ActualHeight, bitmap.SizeInPixels.Width, bitmap.SizeInPixels.Height),
                Image = bitmap,
            };
        }
        private void CanvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            this.RunLeftTop.Text = "(" + ((int)this.Layer.Transformer.DstLeftTop.X).ToString() + ", " + ((int)this.Layer.Transformer.DstLeftTop.Y).ToString() + ")";
            this.RunRightTop.Text = "(" + ((int)this.Layer.Transformer.DstRightTop.X).ToString() + ", " + ((int)this.Layer.Transformer.DstRightTop.Y).ToString() + ")";
            this.RunRightBottom.Text = "(" + ((int)this.Layer.Transformer.DstRightBottom.X).ToString() + ", " + ((int)this.Layer.Transformer.DstRightBottom.Y).ToString() + ")";
            this.RunLeftBottom.Text = "(" + ((int)this.Layer.Transformer.DstLeftBottom.X).ToString() + ", " + ((int)this.Layer.Transformer.DstLeftBottom.Y).ToString() + ")";

            args.DrawingSession.DrawImage(new Transform2DEffect
            {
                TransformMatrix = this.Layer.Transformer.Matrix,
                Source = this.Layer.Image
            });

            Transformer.DrawBoundNodes(args.DrawingSession, this.Layer.Transformer, this.Matrix);
        }


        #endregion


        #region Pointer


        bool IsMove;
        private void CanvasControl_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            this.IsMove = true;

            Vector2 point = e.GetCurrentPoint(this.CanvasControl).Position.ToVector2();
            this.Controller.Start(point, this.Layer, this.Matrix, this.InverseMatrix);
            this.CanvasControl.Invalidate();
        }
        private void CanvasControl_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (this.IsMove)
            {
                Vector2 point = e.GetCurrentPoint(this.CanvasControl).Position.ToVector2();
                this.Controller.Delta(point, this.Layer, this.Matrix, this.InverseMatrix);
                this.CanvasControl.Invalidate();
            }
        }
        private void CanvasControl_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (this.IsMove)
            {
                this.IsMove = false;

                Vector2 point = e.GetCurrentPoint(this.CanvasControl).Position.ToVector2();
                this.Controller.Complete(point, this.Layer, this.Matrix, this.InverseMatrix);
                this.CanvasControl.Invalidate();
            }
        }


        #endregion
    }
}
