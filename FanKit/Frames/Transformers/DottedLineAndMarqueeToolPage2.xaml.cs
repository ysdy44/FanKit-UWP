using FanKit.Transformers;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Numerics;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Transformers
{
    /// <summary>
    /// Page of <see cref="FanKit.Transformers.DottedLineImage"> and <see cref="FanKit.Transformers.MarqueeTool">.
    /// </summary>
    public sealed partial class DottedLineAndMarqueeToolPage2 : Page
    {
        //DottedLine
        Rect _sourceRect;
        public DottedLineImage DottedLineImage;
        public DottedLineBrush DottedLineBrush;

        //MarqueeTool
        private MarqueeToolType toolType;
        public MarqueeToolType ToolType
        {
            get => this.toolType;
            set
            {
                this.RectangularToolButton.IsChecked = value == MarqueeToolType.Rectangular;
                this.EllipticalToolButton.IsChecked = value == MarqueeToolType.Elliptical;
                this.PolygonalToolButton.IsChecked = value == MarqueeToolType.Polygonal;
                this.FreeHandToolButton.IsChecked = value == MarqueeToolType.FreeHand;

                if (value != MarqueeToolType.Polygonal)
                {
                    this._marqueeTool.IsStarted = false;
                    this._marqueeTool.Points.Clear();
                    this.CanvasAnimatedControl.Invalidate();
                }

                this.toolType = value;
            }
        }

        public MarqueeCompositeMode CompositeMode2;

        MarqueeTool _marqueeTool = new MarqueeTool();


        Vector2 _startingPoint = new Vector2();


        #region DependencyProperty


        /// <summary> Scaling around the center. </summary>
        public bool IsCenter
        {
            get { return (bool)GetValue(IsCenterProperty); }
            set { SetValue(IsCenterProperty, value); }
        }
        /// <summary> Identifies the <see cref = "TransformerPage.IsCenter" /> dependency property. </summary>
        public static readonly DependencyProperty IsCenterProperty = DependencyProperty.Register(nameof(IsCenter), typeof(bool), typeof(DottedLineAndMarqueeToolPage2), new PropertyMetadata(false));

        /// <summary> Maintain a ratio when scaling. </summary>
        public bool IsSquare
        {
            get { return (bool)GetValue(IsSquareProperty); }
            set { SetValue(IsSquareProperty, value); }
        }
        /// <summary> Identifies the <see cref = "TransformerPage.IsSquare" /> dependency property. </summary>
        public static readonly DependencyProperty IsSquareProperty = DependencyProperty.Register(nameof(IsSquare), typeof(bool), typeof(DottedLineAndMarqueeToolPage2), new PropertyMetadata(false));


        #endregion


        //@Construct
        public DottedLineAndMarqueeToolPage2()
        {
            this.InitializeComponent();
            this.Loaded += async (s2, e2) =>
            {
                   this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Transformers/DottedLineAndMarqueeToolPage2.xaml.txt");
                 this.MarkdownText1.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Transformers/DottedLineAndMarqueeToolPage2.xaml"));
                  this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Transformers/DottedLineAndMarqueeToolPage2.xaml.cs.txt");
                   this.MarkdownText2.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Transformers/DottedLineAndMarqueeToolPage2.xaml.cs"));
            };

            this.ResetButton.Tapped += (s, e) =>
            {
                //DottedLine
                using (var ds = this.DottedLineImage.CreateDrawingSession())
                {
                    ds.Clear(Windows.UI.Colors.Transparent);
                }
                this.DottedLineImage.Baking(this.CanvasAnimatedControl);

                this.CanvasAnimatedControl.Invalidate();
            };

            //Tool
            this.ToolType = MarqueeToolType.Rectangular;
            this.RectangularToolButton.Tapped += (s, e) => this.ToolType = MarqueeToolType.Rectangular;
            this.EllipticalToolButton.Tapped += (s, e) => this.ToolType = MarqueeToolType.Elliptical;
            this.PolygonalToolButton.Tapped += (s, e) => this.ToolType = MarqueeToolType.Polygonal;
            this.FreeHandToolButton.Tapped += (s, e) => this.ToolType = MarqueeToolType.FreeHand;

            //Composite
            this.NewRadioButton.Tapped += (s, e) => this.CompositeMode2 = MarqueeCompositeMode.New;
            this.AddRadioButton.Tapped += (s, e) => this.CompositeMode2 = MarqueeCompositeMode.Add;
            this.SubtractRadioButton.Tapped += (s, e) => this.CompositeMode2 = MarqueeCompositeMode.Subtract;
            this.IntersectRadioButton.Tapped += (s, e) => this.CompositeMode2 = MarqueeCompositeMode.Intersect;

            #region Draw


            //Canvas  
            this.CanvasAnimatedControl.SizeChanged += (s, e) =>
            {
                if (e.NewSize == e.PreviousSize) return;
                this.CanvasTransformer.Size = e.NewSize;
            };
            this.CanvasAnimatedControl.CreateResources += (sender, args) =>
            {
                int width = this.CanvasTransformer.Width;
                int height = this.CanvasTransformer.Height;

                CanvasRenderTarget canvasRenderTarget = new CanvasRenderTarget(sender, width, height);

                //DottedLine
                this._sourceRect = canvasRenderTarget.Bounds;
                this.DottedLineImage = new DottedLineImage(canvasRenderTarget);
                this.DottedLineBrush = new DottedLineBrush(sender, 6);

                this.DottedLineImage.Baking(sender);
            };
            this.CanvasAnimatedControl.Draw += (sender, args) =>
            {
                int width = this.CanvasTransformer.Width;
                int height = this.CanvasTransformer.Height;

                Matrix3x2 matrix = this.CanvasTransformer.GetMatrix();

                //DrawCrad
                var previousImage = new ColorSourceEffect { Color = Windows.UI.Colors.White };
                args.DrawingSession.DrawCrad(previousImage, this.CanvasTransformer);

                //DottedLine
                args.DrawingSession.DrawDottedLine(sender, this.DottedLineBrush, this.DottedLineImage, width, height);

                //MarqueeTool
                args.DrawingSession.DrawMarqueeTool(sender, this.ToolType, this._marqueeTool, matrix);
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

                this._startingPoint = point;

                //MarqueeTool
                this._marqueeTool.Start(canvasPoint, this.ToolType, this.IsCenter, this.IsSquare);

                this.CanvasAnimatedControl.Invalidate();
            };
            this.CanvasOperator.Single_Delta += (point) =>
            {
                Matrix3x2 inverseMatrix = this.CanvasTransformer.GetInverseMatrix();
                Vector2 canvasStartingPoint = Vector2.Transform(this._startingPoint, inverseMatrix);
                Vector2 canvasPoint = Vector2.Transform(point, inverseMatrix);
                
                //MarqueeTool
                this._marqueeTool.Delta(canvasStartingPoint, canvasPoint, this.ToolType, this.IsCenter, this.IsSquare);

                this.CanvasAnimatedControl.Invalidate();
            };
            this.CanvasOperator.Single_Complete += (point) =>
            {
                Matrix3x2 matrix = this.CanvasTransformer.GetMatrix();
                Matrix3x2 inverseMatrix = this.CanvasTransformer.GetInverseMatrix();
                Vector2 canvasStartingPoint = Vector2.Transform(this._startingPoint, inverseMatrix);
                Vector2 canvasPoint = Vector2.Transform(point, inverseMatrix);

                //MarqueeTool
                bool redraw = this._marqueeTool.Complete(canvasStartingPoint, canvasPoint, this.ToolType, matrix, this.IsCenter, this.IsSquare);

                if (redraw)
                {
                    using (CanvasDrawingSession drawingSession = this.DottedLineImage.CreateDrawingSession())
                    {
                        //MarqueeTool
                        drawingSession.FillMarqueeMaskl(this.CanvasAnimatedControl, this.ToolType, this._marqueeTool, this._sourceRect, this.CompositeMode2);
                    }
                    //DottedLine
                    this.DottedLineImage.Baking(this.CanvasAnimatedControl, matrix);
                }

                this.CanvasAnimatedControl.Invalidate();
            };
            

            //Right
            this.CanvasOperator.Right_Start += (point) =>
            {
                this.CanvasTransformer.CacheMove(point);
            };
            this.CanvasOperator.Right_Delta += (point) =>
            {
                this.CanvasTransformer.Move(point);

                //DottedLine
                Matrix3x2 matrix = this.CanvasTransformer.GetMatrix();
                this.DottedLineImage.Baking(this.CanvasAnimatedControl, matrix);
                this.CanvasAnimatedControl.Invalidate();
            };
            this.CanvasOperator.Right_Complete += (point) =>
            {
                this.CanvasTransformer.Move(point);

                //DottedLine
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

                //DottedLine
                Matrix3x2 matrix = this.CanvasTransformer.GetMatrix();
                this.DottedLineImage.Baking(this.CanvasAnimatedControl, matrix);
                this.CanvasAnimatedControl.Invalidate();
            };
            this.CanvasOperator.Double_Complete += (center, space) =>
            {
                //DottedLine
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

                //DottedLine
                Matrix3x2 matrix = this.CanvasTransformer.GetMatrix();
                this.DottedLineImage.Baking(this.CanvasAnimatedControl, matrix);
                this.CanvasAnimatedControl.Invalidate();
            };
            

            #endregion

        }

    }
}