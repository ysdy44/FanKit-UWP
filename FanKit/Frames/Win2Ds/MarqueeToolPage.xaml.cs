using FanKit.Win2Ds;
using Microsoft.Graphics.Canvas;
using System;
using System.Numerics;
using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Win2Ds
{
    public sealed partial class MarqueeToolPage : Page
    {
        //MarqueeTool
        MarqueeTool MarqueeTool = new MarqueeTool();
        CanvasRenderTarget Selection;
               
        bool IsMove;

        public MarqueeToolPage()
        {
            this.InitializeComponent();

            //MarqueeTool
            this.MarqueeTool.Complete += () => this.MarqueeTool.Render(this.CanvasControl, this.Selection);

            this.Loaded += async (sender, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Win2Ds/MarqueeToolPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Win2Ds/MarqueeToolPage.xaml.cs.txt");
                this.MarkdownText3.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Win2Ds/MarqueeTool.cs.txt");
            };;

            
            //Tool
            this.RectangularToolButton.Tapped += (s, e) => { this.MarqueeTool.Tool = MarqueeToolType.Rectangular; this.EllipticalToolButton.IsChecked = this.PolygonalToolButton.IsChecked = this.FreeHandToolButton.IsChecked = false; };
            this.EllipticalToolButton.Tapped += (s, e) => { this.RectangularToolButton.IsChecked = this.PolygonalToolButton.IsChecked = this.FreeHandToolButton.IsChecked = false; this.MarqueeTool.Tool = MarqueeToolType.Elliptical; };
            this.PolygonalToolButton.Tapped += (s, e) => { this.MarqueeTool.Tool = MarqueeToolType.Polygonal; this.RectangularToolButton.IsChecked = this.EllipticalToolButton.IsChecked = this.FreeHandToolButton.IsChecked = false; };
            this.FreeHandToolButton.Tapped += (s, e) => { this.MarqueeTool.Tool = MarqueeToolType.FreeHand; this.RectangularToolButton.IsChecked = this.EllipticalToolButton.IsChecked = this.PolygonalToolButton.IsChecked = false; };
            //SquareCenter
            this.Square.Tapped += (s, e) => this.MarqueeTool.MarqueeMode = this.GetMode(this.Square.IsChecked ?? false, this.Center.IsChecked ?? false);
            this.Center.Tapped += (s, e) => this.MarqueeTool.MarqueeMode = this.GetMode(this.Square.IsChecked ?? false, this.Center.IsChecked ?? false);
            

            //Clear
            this.Clear.Tapped+=(s,e)=>
            {
                using (CanvasDrawingSession ds = this.Selection.CreateDrawingSession())
                {
                    ds.Clear(Color.FromArgb(0, 0, 0, 0));
                };
                this.CanvasControl.Invalidate();
            };


            //CompositeMode
            this.New.Tapped += (s, e) => { this.MarqueeTool.CompositeMode = FanKit.Win2Ds.MarqueeCompositeMode.New; this.Add.IsChecked = this.Subtract.IsChecked = this.Intersect.IsChecked = false; };
            this.Add.Tapped += (s, e) => { this.MarqueeTool.CompositeMode = FanKit.Win2Ds.MarqueeCompositeMode.Add; this.New.IsChecked = this.Subtract.IsChecked = this.Intersect.IsChecked = false; };
            this.Subtract.Tapped += (s, e) => { this.MarqueeTool.CompositeMode = FanKit.Win2Ds.MarqueeCompositeMode.Subtract; this.New.IsChecked = this.Add.IsChecked = this.Intersect.IsChecked = false; };
            this.Intersect.Tapped += (s, e) => { this.MarqueeTool.CompositeMode = FanKit.Win2Ds.MarqueeCompositeMode.Intersect; this.New.IsChecked = this.Add.IsChecked = this.Subtract.IsChecked = false; };

            //Canvas
            this.CanvasControl.CreateResources += (sender, args) =>   this.Selection = new CanvasRenderTarget(sender, 1000, 1000);
            this.CanvasControl.Draw += (sender, args) => 
             {
                args.DrawingSession.DrawImage(this.Selection);
                this.MarqueeTool.Draw(sender, args.DrawingSession);
            };
            this.CanvasControl.SizeChanged += (s, e) =>
            {
                if (e.NewSize == e.PreviousSize) return;
                if (this.Selection == null) return;
                if (System.Math.Abs(e.NewSize.Width - this.Selection.Size.Width) > 10)
                {
                    this.Selection = new CanvasRenderTarget(this.CanvasControl, (float)e.NewSize.Width, (float)e.NewSize.Height);
                }
            };


            //Pointer
            this.CanvasControl.PointerPressed+=(s,e)=>
            {
                this.IsMove = true;
                 this.MarqueeTool.Operator_Start(e.GetCurrentPoint(this.CanvasControl).Position.ToVector2());
                this.CanvasControl.Invalidate();
            };
            this.CanvasControl.PointerMoved+=(s,e)=>
            {
                if (this.IsMove)
                {
                    this.MarqueeTool.Operator_Delta(e.GetCurrentPoint(this.CanvasControl).Position.ToVector2());
                    this.CanvasControl.Invalidate();
                };
            };
            this.CanvasControl.PointerReleased += (s, e) =>
            {
                if (this.IsMove)
                {
                    this.IsMove = false;

                    this.MarqueeTool.Operator_Complete(e.GetCurrentPoint(this.CanvasControl).Position.ToVector2());
                    this.CanvasControl.Invalidate();
                };
            };
        }

        private MarqueeMode GetMode(bool square, bool center)
        {
            if (square == false && center == false) return MarqueeMode.None;
            else if (square && center == false) return MarqueeMode.Square;
            else if (square == false && center) return MarqueeMode.Center;
            else return MarqueeMode.SquareAndCenter;
        }         
    }
}