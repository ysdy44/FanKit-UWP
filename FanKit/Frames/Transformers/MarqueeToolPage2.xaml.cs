﻿using FanKit.Transformers;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Numerics;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Transformers
{
    /// <summary>
    /// Page of <see cref="FanKit.Transformers.MarqueeTool">.
    /// </summary>
    public sealed partial class MarqueeToolPage2 : Page
    {
        public CanvasRenderTarget CanvasRenderTarget;

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
                    this.CanvasControl.Invalidate();
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
        public static readonly DependencyProperty IsCenterProperty = DependencyProperty.Register(nameof(IsCenter), typeof(bool), typeof(MarqueeToolPage2), new PropertyMetadata(false));

        /// <summary> Maintain a ratio when scaling. </summary>
        public bool IsSquare
        {
            get { return (bool)GetValue(IsSquareProperty); }
            set { SetValue(IsSquareProperty, value); }
        }
        /// <summary> Identifies the <see cref = "TransformerPage.IsSquare" /> dependency property. </summary>
        public static readonly DependencyProperty IsSquareProperty = DependencyProperty.Register(nameof(IsSquare), typeof(bool), typeof(MarqueeToolPage2), new PropertyMetadata(false));


        #endregion

        //@Construct
        public MarqueeToolPage2()
        {
            this.InitializeComponent();
            this.Loaded += async (s2, e2) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Transformers/MarqueeToolPage2.xaml.txt");
                this.MarkdownText1.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Transformers/MarqueeToolPage2.xaml"));
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Transformers/MarqueeToolPage2.xaml.cs.txt");
                this.MarkdownText2.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Transformers/MarqueeToolPage2.xaml.cs"));
            };

            this.ResetButton.Tapped += (s, e) =>
            {
                int width = this.CanvasTransformer.Width;
                int height = this.CanvasTransformer.Height;
                this.CanvasRenderTarget = new CanvasRenderTarget(this.CanvasControl, width, height);

                this.CanvasTransformer.Fit();
                this.CanvasControl.Invalidate();
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
            this.CanvasControl.SizeChanged += (s, e) =>
            {
                if (e.NewSize == e.PreviousSize) return;
                this.CanvasTransformer.Size = e.NewSize;
            };
            this.CanvasControl.CreateResources += (sender, args) =>
            {
                float width = this.CanvasTransformer.Width;
                float height = this.CanvasTransformer.Height;
                this.CanvasRenderTarget = new CanvasRenderTarget(sender, width, height);
            };
            this.CanvasControl.Draw += (sender, args) =>
            {
                Matrix3x2 matrix = this.CanvasTransformer.GetMatrix();

                //DrawCrad
                var previousImage = new ColorSourceEffect { Color = Windows.UI.Colors.White };
                args.DrawingSession.DrawCrad(previousImage, this.CanvasTransformer);

                var mask = new Transform2DEffect
                {
                    TransformMatrix = matrix,
                    Source = this.CanvasRenderTarget,
                };
                args.DrawingSession.DrawImage(mask);

                //MarqueeTool
                args.DrawingSession.DrawMarqueeTool(sender, this.ToolType, this._marqueeTool, matrix);
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

                this.CanvasControl.Invalidate();
            };
            this.CanvasOperator.Single_Delta += (point) =>
            {
                Matrix3x2 inverseMatrix = this.CanvasTransformer.GetInverseMatrix();
                Vector2 canvasStartingPoint = Vector2.Transform(this._startingPoint, inverseMatrix);
                Vector2 canvasPoint = Vector2.Transform(point, inverseMatrix);

                //MarqueeTool
                this._marqueeTool.Delta(canvasStartingPoint, canvasPoint, this.ToolType, this.IsCenter, this.IsSquare);

                this.CanvasControl.Invalidate();
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
                    using (CanvasDrawingSession drawingSession = this.CanvasRenderTarget.CreateDrawingSession())
                    {
                        //MarqueeTool
                        drawingSession.FillMarqueeMaskl(this.CanvasControl, this.ToolType, this._marqueeTool, this.CanvasRenderTarget.Bounds, this.CompositeMode2);
                    }
                }

                this.CanvasControl.Invalidate();
            };


            //Right
            this.CanvasOperator.Right_Start += (point) =>
            {
                this.CanvasTransformer.CacheMove(point);
            };
            this.CanvasOperator.Right_Delta += (point) =>
            {
                this.CanvasTransformer.Move(point);
                this.CanvasControl.Invalidate();
            };
            this.CanvasOperator.Right_Complete += (point) =>
            {
                this.CanvasTransformer.Move(point);
                this.CanvasControl.Invalidate();
            };


            //Double
            this.CanvasOperator.Double_Start += (center, space) =>
            {
                this.CanvasTransformer.CachePinch(center, space);
                this.CanvasControl.Invalidate();
            };
            this.CanvasOperator.Double_Delta += (center, space) =>
            {
                this.CanvasTransformer.Pinch(center, space);
                this.CanvasControl.Invalidate();
            };
            this.CanvasOperator.Double_Complete += (center, space) =>
            {
                this.CanvasControl.Invalidate();
            };

            //Wheel
            this.CanvasOperator.Wheel_Changed += (point, space) =>
            {
                if (space > 0)
                    this.CanvasTransformer.ZoomIn(point);
                else
                    this.CanvasTransformer.ZoomOut(point);

                this.CanvasControl.Invalidate();
            };

            #endregion

        }

    }
}