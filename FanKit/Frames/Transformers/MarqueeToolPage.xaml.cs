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
        MarqueeToolType ToolType;

        //@Construct
        public MarqueeToolPage()
        {
            this.InitializeComponent();
            this.Loaded += async (s, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Transformers/MarqueeToolPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Transformers/MarqueeToolPage.xaml.cs.txt");
            };

            #region Draw


            //Canvas
            this.CanvasControl.SizeChanged += (s, e) =>
            {
                if (e.NewSize == e.PreviousSize) return;
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
                    case MarqueeToolType.None:
                        break;
                    case MarqueeToolType.Rectangular:
                        break;
                    case MarqueeToolType.Elliptical:
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
                switch (this.ToolType)
                {
                    case MarqueeToolType.None:
                        break;
                    case MarqueeToolType.Rectangular:
                        break;
                    case MarqueeToolType.Elliptical:
                        break;
                    case MarqueeToolType.Polygonal:
                        break;
                    case MarqueeToolType.FreeHand:
                        break;
                }
            };
            this.CanvasOperator.Single_Delta += (point) =>
            {

            };
            this.CanvasOperator.Single_Complete += (point) =>
            {

            };


            #endregion

        }
    }
}
