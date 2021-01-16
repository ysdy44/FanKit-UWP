using FanKit.Transformers;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Numerics;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Transformers
{
    /// <summary>
    /// Page of <see cref="FanKit.Transformers.Transformer">.
    /// </summary>
    public sealed partial class TransformerPage : Page
    {
        TransformerMode _mode;
        Vector2 _startingPoint;
        Transformer _startingTransformer;
        Layer _layer;

        class Layer
        {
            public CanvasBitmap Image;
            public TransformerMatrix TransformerMatrix;
        }

        
        #region DependencyProperty


        /// <summary> Transformer. </summary>
        public Transformer Transformer
        {
            get { return (Transformer)GetValue(TransformerProperty); }
            set { SetValue(TransformerProperty, value); }
        }
        /// <summary> Identifies the <see cref = "TransformerPage.IsRatio" /> dependency property. </summary>
        public static readonly DependencyProperty TransformerProperty = DependencyProperty.Register(nameof(Transformer), typeof(Transformer), typeof(TransformerPage), new PropertyMetadata(new Transformer(), (sender, e) =>
        {
            TransformerPage con = (TransformerPage)sender;

            if (e.NewValue is Transformer value)
            {
                con.LeftTopTextBlock.Text = string.Format("LeftTop: ({0},{1})", (int)value.LeftTop.X, (int)value.LeftTop.Y);
                con.RightTopTextBlock.Text = string.Format("RightTop: ({0},{1})", (int)value.RightTop.X, (int)value.RightTop.Y);
                con.RightBottomTextBlock.Text = string.Format("RightBottom: ({0},{1})", (int)value.RightBottom.X, (int)value.RightBottom.Y);
                con.LeftBottomTextBlock.Text = string.Format("LeftBottom: ({0},{1})", (int)value.LeftBottom.X, (int)value.LeftBottom.Y);
            }
        }));


        /// <summary> Maintain a ratio when scaling. </summary>
        public bool IsRatio
        {
            get { return (bool)GetValue(IsRatioProperty); }
            set { SetValue(IsRatioProperty, value); }
        }
        /// <summary> Identifies the <see cref = "TransformerPage.IsRatio" /> dependency property. </summary>
        public static readonly DependencyProperty IsRatioProperty = DependencyProperty.Register(nameof(IsRatio), typeof(bool), typeof(TransformerPage), new PropertyMetadata(false));


        /// <summary> Scaling around the center. </summary>
        public bool IsCenter
        {
            get { return (bool)GetValue(IsCenterProperty); }
            set { SetValue(IsCenterProperty, value); }
        }
        /// <summary> Identifies the <see cref = "TransformerPage.IsCenter" /> dependency property. </summary>
        public static readonly DependencyProperty IsCenterProperty = DependencyProperty.Register(nameof(IsCenter), typeof(bool), typeof(TransformerPage), new PropertyMetadata(false));


        /// <summary> Step Frequency when spinning. </summary>
        public bool IsStepFrequency
        {
            get { return (bool)GetValue(IsStepFrequencyProperty); }
            set { SetValue(IsStepFrequencyProperty, value); }
        }
        /// <summary> Identifies the <see cref = "TransformerPage.IsStepFrequency" /> dependency property. </summary>
        public static readonly DependencyProperty IsStepFrequencyProperty = DependencyProperty.Register(nameof(IsStepFrequency), typeof(bool), typeof(TransformerPage), new PropertyMetadata(false));


        #endregion


        //@Construct
        public TransformerPage()
        {
            this.InitializeComponent();
            this.Loaded += async (s2, e2) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Transformers/TransformerPage.xaml.txt");
                this.MarkdownText1.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Transformers/TransformerPage.xaml"));
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Transformers/TransformerPage.xaml.cs.txt");
                this.MarkdownText2.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Transformers/TransformerPage.xaml.cs"));
            };

            this.ResetButton.Tapped += (s, e) =>
            {
                Transformer transformer = this.Reset(this._layer.Image.SizeInPixels.Width, this._layer.Image.SizeInPixels.Height, (float)this.CanvasControl.ActualWidth, (float)this.CanvasControl.ActualHeight);
                this._layer.TransformerMatrix.Destination = transformer;

                //DependencyProperty
                this.Transformer = transformer;

                this.CanvasControl.Invalidate();//Invalidate
            };


            //Canvas
            this.CanvasControl.CreateResources += (sender, args) => args.TrackAsyncAction(this.CreateResourcesAsync(sender).AsAsyncAction());
            this.CanvasControl.Draw += (sender, args) =>
            {
                //Transformer
                ICanvasImage source = this._layer.Image;
                Matrix3x2 transformMatrix = this._layer.TransformerMatrix.GetMatrix();
                Transformer transformer = this._layer.TransformerMatrix.Destination;

                //Draw
                args.DrawingSession.DrawImage(new Transform2DEffect
                {
                    Source = source,
                    TransformMatrix = transformMatrix
                });
                args.DrawingSession.DrawBoundNodes(transformer);
            };


            //Single
            this.CanvasOperator.Single_Start += (point) =>
            {
                this._startingPoint = point;

                //Controller
                this._layer.TransformerMatrix.CacheTransform();
                Transformer transformer = this._layer.TransformerMatrix.Destination;
                this._startingTransformer = transformer;
                this._mode = Transformer.ContainsNodeMode(point, transformer);

                this.CanvasControl.Invalidate();//Invalidate
            };
            this.CanvasOperator.Single_Delta += (point) =>
            {
                bool isRatio = this.IsRatio;
                bool isCenter = this.IsCenter;
                bool isStepFrequency = this.IsStepFrequency;

                //Controller
                Transformer transformer = Transformer.Controller(this._mode, _startingPoint, point, this._startingTransformer, isRatio, isCenter, isStepFrequency);
                this._layer.TransformerMatrix.Destination = transformer;

                //DependencyProperty
                this.Transformer = transformer;

                this.CanvasControl.Invalidate();//Invalidate
            };
            this.CanvasOperator.Single_Complete += (point) =>
            {
                this.CanvasControl.Invalidate();//Invalidate
            };
        }

        private async Task CreateResourcesAsync(CanvasControl sender)
        {
            //Bitmap
            CanvasBitmap bitmap = await CanvasBitmap.LoadAsync(sender, "Icon/Avatar.jpg");
            TransformerMatrix transformerMatrix = new TransformerMatrix
            {
                Source = new Transformer(bitmap.SizeInPixels.Width, bitmap.SizeInPixels.Height, Vector2.Zero),
                Destination = this.Reset(bitmap.SizeInPixels.Width, bitmap.SizeInPixels.Height, (float)sender.ActualWidth, (float)sender.ActualHeight),
            };

            //DependencyProperty
            this.Transformer = transformerMatrix.Destination;

            //Layer
            this._layer = new Layer
            {
                TransformerMatrix = transformerMatrix,
                Image = bitmap,
            };
        }

        private Transformer Reset(float bitmapWidth, float bitmapHeight, float controlWidth, float controlHeight)
        {
            Vector2 center = new Vector2(controlWidth, controlHeight) / 2.0f;
            float scale = System.Math.Min(controlWidth / bitmapWidth, controlHeight / bitmapHeight);
            float width = scale * bitmapWidth / 3.0f / 2.0f;
            float height = scale * bitmapHeight / 3.0f / 2.0f;

            Transformer destination = new Transformer
            {
                LeftTop = center + new Vector2(-width, -height),
                RightTop = center + new Vector2(+width, -height),
                RightBottom = center + new Vector2(+width, +height),
                LeftBottom = center + new Vector2(-width, +height),
            };
            return destination;
        }

    }
}