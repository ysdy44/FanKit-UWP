using FanKit.Transformers;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using System.Numerics;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Transformers
{
    public sealed partial class DottedLinePage2 : Page
    {
        DottedLineImage DottedLineImage;
        DottedLineBrush DottedLineBrush;
        
        Vector2 canvasStartingPoint = new Vector2();
        TransformerRect _transformerRect;

        //@Construct
        public DottedLinePage2()
        {
            this.InitializeComponent();
            this.Loaded += async (s, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Transformers/DottedLinePage2.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Transformers/DottedLinePage2.xaml.cs.txt");
            };
            this.ResetButton.Tapped += (s, e) =>
            {
                using (var ds = this.DottedLineImage.CreateDrawingSession())
                {
                    ds.Clear(Windows.UI.Colors.Transparent);
                }
                this.DottedLineImage.Baking(this.CanvasAnimatedControl);
            };
            this.RadianSlider.ValueChanged += (s, e) =>
            {
                float radian = ((float)e.NewValue) * FanKit.Math.Pi / 180.0f;
                this.CanvasTransformer.Radian = radian;

                this.CanvasTransformer.ReloadMatrix();

                Matrix3x2 matrix = this.CanvasTransformer.GetMatrix();
                this.DottedLineImage.Baking(this.CanvasAnimatedControl, matrix);
                this.CanvasAnimatedControl.Invalidate();
            };


            #region Draw


            //Canvas
            this.CanvasAnimatedControl.SizeChanged += (s, e) =>
            {
                if (e.NewSize == e.PreviousSize) return;
            };
            this.CanvasAnimatedControl.CreateResources += (sender, args) =>
            {
                CanvasRenderTarget canvasRenderTarget = new CanvasRenderTarget(sender, this.CanvasTransformer.Width, this.CanvasTransformer.Height);
                this.DottedLineImage = new DottedLineImage(canvasRenderTarget);
                this.DottedLineBrush = new DottedLineBrush(sender, 6);

                Matrix3x2 matrix = this.CanvasTransformer.GetMatrix();
                this.DottedLineImage.Baking(sender, matrix);
            };
            this.CanvasAnimatedControl.Draw += (sender, args) =>
            {
                Matrix3x2 matrix = this.CanvasTransformer.GetMatrix();

                //DrawCrad
                var previousImage = new ColorSourceEffect { Color = Windows.UI.Colors.White };
                args.DrawingSession.DrawCrad(previousImage, this.CanvasTransformer);

                //DrawDottedLine
                args.DrawingSession.DrawDottedLine(sender, this.DottedLineBrush, this.DottedLineImage,  this.CanvasTransformer.Width, this.CanvasTransformer.Height);
                args.DrawingSession.FillRectDodgerBlue(this.CanvasAnimatedControl, this._transformerRect, matrix);
            };
            this.CanvasAnimatedControl.Update += (sender, args) =>
            {
                this.DottedLineBrush.Update(1);
            };


            #endregion


            #region CanvasOperator


            //Single
            this.CanvasOperator.Single_Start += (point) =>
            {
                Matrix3x2 inverseMatrix = this.CanvasTransformer.GetInverseMatrix();
                Vector2 canvasPoint = Vector2.Transform(point, inverseMatrix);
                this.canvasStartingPoint = Vector2.Transform(point, inverseMatrix);

                this._transformerRect = new TransformerRect(point, point);
            };
            this.CanvasOperator.Single_Delta += (point) =>
            {
                Matrix3x2 inverseMatrix = this.CanvasTransformer.GetInverseMatrix();
                Vector2 canvasPoint = Vector2.Transform(point, inverseMatrix);

                this._transformerRect = new TransformerRect(canvasStartingPoint, canvasPoint);
            };
            this.CanvasOperator.Single_Complete += (point) =>
            {
                using (var ds = this.DottedLineImage.CreateDrawingSession())
                {
                    ds.FillRectangle(this._transformerRect.ToRect(), Windows.UI.Colors.Gray);
                }
                Matrix3x2 matrix = this.CanvasTransformer.GetMatrix();
                this.DottedLineImage.Baking(this.CanvasAnimatedControl, matrix);

                this._transformerRect = new TransformerRect(Vector2.Zero, Vector2.Zero);
            };


            //Right
            this.CanvasOperator.Right_Start += (point) =>
            {
                this.CanvasTransformer.CacheMove(point);
            };
            this.CanvasOperator.Right_Delta += (point) =>
            {
                this.CanvasTransformer.Move(point);

                Matrix3x2 matrix = this.CanvasTransformer.GetMatrix();
                this.DottedLineImage.Baking(this.CanvasAnimatedControl, matrix);
                this.CanvasAnimatedControl.Invalidate();
            };
            this.CanvasOperator.Right_Complete += (point) =>
            {
                this.CanvasTransformer.Move(point);

                Matrix3x2 matrix = this.CanvasTransformer.GetMatrix();
                this.DottedLineImage.Baking(this.CanvasAnimatedControl, matrix);
                this.CanvasAnimatedControl.Invalidate();
            };


            //Double
            this.CanvasOperator.Double_Start += (center, space) =>
            {
                this.CanvasTransformer.CachePinch(center, space);
                this.CanvasAnimatedControl.Invalidate();
            };
            this.CanvasOperator.Double_Delta += (center, space) =>
            {
                this.CanvasTransformer.Pinch(center, space);

                Matrix3x2 matrix = this.CanvasTransformer.GetMatrix();
                this.DottedLineImage.Baking(this.CanvasAnimatedControl, matrix);
                this.CanvasAnimatedControl.Invalidate();
            };
            this.CanvasOperator.Double_Complete += (center, space) =>
            {
                Matrix3x2 matrix = this.CanvasTransformer.GetMatrix();
                this.DottedLineImage.Baking(this.CanvasAnimatedControl, matrix);
                                this.CanvasAnimatedControl.Invalidate();
            };

            //Wheel
            this.CanvasOperator.Wheel_Changed += (point, space) =>
            {
                if (space > 0)
                    this.CanvasTransformer.ZoomIn(point);
                else
                    this.CanvasTransformer.ZoomOut(point);

                Matrix3x2 matrix = this.CanvasTransformer.GetMatrix();
                this.DottedLineImage.Baking(this.CanvasAnimatedControl, matrix);
                this.CanvasAnimatedControl.Invalidate();
            };


            #endregion
        }
    }
}