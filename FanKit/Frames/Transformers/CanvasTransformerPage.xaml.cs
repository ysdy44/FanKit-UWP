using FanKit.Transformers;
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
    /// Page of <see cref="FanKit.Transformers.CanvasTransformer"/>.
    /// </summary>
    public sealed partial class CanvasTransformerPage : Page
    {
         
        #region DependencyProperty


        /// <summary> CanvasTransformer </summary>
        public CanvasTransformer _canvasTransformer
        {
            set
            {
                this.WidthRun.Text = string.Format("{0}", (int)value.Width);
                this.HeightRun.Text = string.Format("{0}", (int)value.Height);
                this.ScaleRun.Text = string.Format("{0}%", (int)(value.Scale * 100.0f));
                this.PositionRun.Text = string.Format("({0}, {1})", (int)value.Position.X, (int)value.Position.Y);
                this.RadianRun.Text = string.Format("{0}º", (int)(value.Radian * 180.0f / FanKit.Math.Pi));
            }
        }


        /// <summary> CanvasTransformer's radian. </summary>
        public double Radian
        {
            get { return (double)GetValue(RadianProperty); }
            set { SetValue(RadianProperty, value); }
        }
        /// <summary> Identifies the <see cref = "CanvasTransformerPage.Radian" /> dependency property. </summary>
        public static readonly DependencyProperty RadianProperty = DependencyProperty.Register(nameof(Radian), typeof(double), typeof(CanvasTransformerPage), new PropertyMetadata(0.0d, (sender, e) =>
        {
            CanvasTransformerPage con = (CanvasTransformerPage)sender;

            if (e.NewValue is double value)
            {
                float radian = ((float)value) * FanKit.Math.Pi / 180.0f;
                con.CanvasTransformer.Radian = radian;

                con.CanvasTransformer.ReloadMatrix();
                con.CanvasControl.Invalidate();//Invalidate
                                               
                //DependencyProperty
                con._canvasTransformer = con.CanvasTransformer;
            }
        }));


        #endregion
        
        //@Construct
        public CanvasTransformerPage()
        {
            this.InitializeComponent();
           this.Loaded += async (s2, e2) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Transformers/CanvasTransformerPage.xaml.txt");
                this.MarkdownText1.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Transformers/CanvasTransformerPage.xaml"));
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Transformers/CanvasTransformerPage.xaml.cs.txt");
                this.MarkdownText2.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Transformers/CanvasTransformerPage.xaml.cs"));
            };

            this.ResetButton.Tapped += (s, e) =>
            {
                this.CanvasTransformer.Fit();
                this.CanvasTransformer.Radian = 0;
                this.CanvasTransformer.ReloadMatrix();
                this.CanvasControl.Invalidate();//Invalidate

                //DependencyProperty
                this._canvasTransformer = this.CanvasTransformer;
            };


            //Canvas
            this.CanvasControl.SizeChanged += (s, e) =>
            {
                if (e.NewSize == e.PreviousSize) return;

                Size size = e.NewSize;
                this.CanvasTransformer.Width = (int)size.Width / 3;
                this.CanvasTransformer.Height = (int)size.Height / 3;

                this.CanvasTransformer.Size = size;
                this.CanvasTransformer.ReloadMatrix();
            };
            this.CanvasControl.CreateResources += (sender, args) =>
            {
                Size size = new Size(sender.ActualWidth, sender.ActualHeight);

                this.CanvasTransformer.Width = (int)size.Width / 3;
                this.CanvasTransformer.Height = (int)size.Height / 3;

                this.CanvasTransformer.Position = new Vector2((float)size.Width / 2, (float)size.Height / 2);

                this.CanvasTransformer.Size = size;
                this.CanvasTransformer.ReloadMatrix();

                //DependencyProperty
                this._canvasTransformer = this.CanvasTransformer;
            };
            this.CanvasControl.Draw += (sender, args) =>
            {
                args.DrawingSession.DrawCrad(new ColorSourceEffect
                {
                    Color = Windows.UI.Colors.White
                }, this.CanvasTransformer);

                args.DrawingSession.DrawAxis(this.CanvasTransformer);
                args.DrawingSession.DrawRuler(this.CanvasTransformer);
            };


            //Single
            this.CanvasOperator.Single_Start += (point) =>
            {
            };
            this.CanvasOperator.Single_Delta += (point) =>
            {
                this.CanvasTransformer.Position = point;

                this.CanvasTransformer.ReloadMatrix();
                this.CanvasControl.Invalidate();//Invalidate

                //DependencyProperty
                this._canvasTransformer = this.CanvasTransformer;
            };
            this.CanvasOperator.Single_Complete += (point) =>
            {
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
                //DependencyProperty
                this._canvasTransformer = this.CanvasTransformer;
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
                //DependencyProperty
                this._canvasTransformer = this.CanvasTransformer;
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
                //DependencyProperty
                this._canvasTransformer = this.CanvasTransformer;
            };
        }
    }
}