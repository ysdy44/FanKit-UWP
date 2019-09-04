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
using FanKit.Transformers;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using System.Numerics;

namespace FanKit.Frames.Transformers
{
    public sealed partial class MarqueeToolPage : Page
    {
        CanvasRenderTarget CanvasRenderTarget;

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

                this.toolType = value;
            }
        }

        public MarqueeCompositeMode CompositeMode2;
        
        Vector2 _startingPoint = new Vector2();
        TransformerRect _transformerRect;
        bool _isSingeStart;

        #region DependencyProperty


        /// <summary> Maintain a ratio when scaling. </summary>
        public bool IsSquare
        {
            get { return (bool)GetValue(IsSquareProperty); }
            set { SetValue(IsSquareProperty, value); }
        }
        /// <summary> Identifies the <see cref = "TransformerPage.IsSquare" /> dependency property. </summary>
        public static readonly DependencyProperty IsSquareProperty = DependencyProperty.Register(nameof(IsSquare), typeof(bool), typeof(TransformerPage), new PropertyMetadata(false));


        /// <summary> Scaling around the center. </summary>
        public bool IsCenter
        {
            get { return (bool)GetValue(IsCenterProperty); }
            set { SetValue(IsCenterProperty, value); }
        }
        /// <summary> Identifies the <see cref = "TransformerPage.IsCenter" /> dependency property. </summary>
        public static readonly DependencyProperty IsCenterProperty = DependencyProperty.Register(nameof(IsCenter), typeof(bool), typeof(TransformerPage), new PropertyMetadata(false));


        #endregion

        //@Construct
        public MarqueeToolPage()
        {
            this.InitializeComponent();
            this.Loaded += async (s, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Transformers/MarqueeToolPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Transformers/MarqueeToolPage.xaml.cs.txt");
            };

            //Tool
            this.ToolType = MarqueeToolType.None;
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
            this.CanvasControl.Loaded += (s2, e2) =>
            {
                this.CanvasControl.SizeChanged += (s, e) =>
                {
                    if (e.NewSize == e.PreviousSize) return;
                    this.CanvasRenderTarget = new CanvasRenderTarget(this.CanvasControl, (float)e.NewSize.Width, (float)e.NewSize.Height);
                };
            };
            this.CanvasControl.CreateResources += (sender, args) =>
            {
                CanvasRenderTarget canvasRenderTarget = new CanvasRenderTarget(sender, (float)this.CanvasControl.ActualWidth, (float)this.CanvasControl.ActualHeight);
                this.CanvasRenderTarget = canvasRenderTarget;
              };
            this.CanvasControl.Draw += (sender, args) =>
            {
                //DrawImage
                args.DrawingSession.DrawImage(this.CanvasRenderTarget);

                switch (this.ToolType)
                {
                    case MarqueeToolType.Rectangular:
                        args.DrawingSession.FillRectDodgerBlue(sender, this._transformerRect);
                        break;
                    case MarqueeToolType.Elliptical:
                        args.DrawingSession.FillEllipseDodgerBlue(this._transformerRect);
                        break;
                    case MarqueeToolType.Polygonal:
                        break;
                    case MarqueeToolType.FreeHand:
                        break;
                }
            };


            #endregion


            #region CanvasOperator


            //Single
            this.CanvasOperator.Single_Start += (point) =>
            {
                this._startingPoint = point;

                switch (this.ToolType)
                {
                    case MarqueeToolType.Rectangular:
                        this._isSingeStart = true;
                        this._transformerRect = new TransformerRect(point, point);
                         break;
                    case MarqueeToolType.Elliptical:
                        this._isSingeStart = true;
                        this._transformerRect = new TransformerRect(point, point);
                        break;
                    case MarqueeToolType.Polygonal:
                        break;
                    case MarqueeToolType.FreeHand:
                        break;
                }

                this.CanvasControl.Invalidate();
            };
            this.CanvasOperator.Single_Delta += (point) =>
            {
                switch (this.ToolType)
                {
                    case MarqueeToolType.Rectangular:
                        this._transformerRect = new TransformerRect(this._startingPoint, point);
                        break;
                    case MarqueeToolType.Elliptical:
                        this._transformerRect = new TransformerRect(this._startingPoint, point);
                        break;
                    case MarqueeToolType.Polygonal:
                        break;
                    case MarqueeToolType.FreeHand:
                        break;
                }

                this.CanvasControl.Invalidate();
            };
            this.CanvasOperator.Single_Complete += (point) =>
            {
                switch (this.ToolType)
                {
                    case MarqueeToolType.Rectangular:
                        {
                            this._isSingeStart = false;
                            this._transformerRect = new TransformerRect(this._startingPoint, point);
                            using (CanvasDrawingSession drawingSession=this.CanvasRenderTarget.CreateDrawingSession())
                            {
                                drawingSession.Clear(Windows.UI.Colors.Transparent);

                                Rect rect = this._transformerRect.ToRect();
                                drawingSession.FillRectangle(rect, Windows.UI.Colors.DodgerBlue);
                            }
                        }
                        break; 
                    case MarqueeToolType.Elliptical:
                        {
                            this._isSingeStart = false;
                            this._transformerRect = new TransformerRect(this._startingPoint, point);
                            using (CanvasDrawingSession drawingSession = this.CanvasRenderTarget.CreateDrawingSession())
                            {
                                drawingSession.Clear(Windows.UI.Colors.Transparent);

                                //TODO:换 
                                float CenterX = (this._transformerRect.Left + _transformerRect.Right) / 2;
                                float CenterY = (_transformerRect.Top + _transformerRect.Bottom) / 2;
                                // Vector2 centerPoint = new Vector2(_transformerRect.CenterX, _transformerRect.CenterY);
                                Vector2 centerPoint = new Vector2(CenterX, CenterY);
                                float width = _transformerRect.Right - _transformerRect.Left;
                                float height = _transformerRect.Bottom - _transformerRect.Top;

                                drawingSession.FillEllipse(centerPoint, width,height, Windows.UI.Colors.DodgerBlue);
                            }
                        }
                        break;
                    case MarqueeToolType.Polygonal:
                        break;
                    case MarqueeToolType.FreeHand:
                        break;
                }

                this.CanvasControl.Invalidate();
            };


            #endregion

        }
    }
}