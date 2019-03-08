using FanKit.Win2Ds;
using Microsoft.Graphics.Canvas;
using System;
using System.Numerics;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace FanKit.Frames.Win2Ds
{
    public sealed partial class MarqueeToolPage : Page
    {
        //MarqueeTool
        MarqueeTool MarqueeTool = new MarqueeTool();
        CanvasRenderTarget Selection;

        public MarqueeToolPage()
        {
            this.InitializeComponent();

            //MarqueeTool
            this.MarqueeTool.Complete += () => this.MarqueeTool.Render(this.CanvasControl, this.Selection);
        }


        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.MarkdownText1.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Win2Ds/MarqueeToolPage.xaml.txt");
            this.MarkdownText2.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Win2Ds/MarqueeToolPage.xaml.cs.txt");
            this.MarkdownText3.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Win2Ds/MarqueeTool.cs.txt");
        }

        #region UI


        //Tool
        private void RectangularToolButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.EllipticalToolButton.IsChecked = false;
            this.PolygonalToolButton.IsChecked = false;
            this.FreeHandToolButton.IsChecked = false;
            this.MarqueeTool.Tool = MarqueeToolType.Rectangular;
        }
        private void EllipticalToolButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.RectangularToolButton.IsChecked = false;
            this.PolygonalToolButton.IsChecked = false;
            this.FreeHandToolButton.IsChecked = false;
            this.MarqueeTool.Tool = MarqueeToolType.Elliptical;
        }
        private void PolygonalToolButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.RectangularToolButton.IsChecked = false;
            this.EllipticalToolButton.IsChecked = false;
            this.FreeHandToolButton.IsChecked = false;
            this.MarqueeTool.Tool = MarqueeToolType.Polygonal;
        }
        private void FreeHandToolButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.RectangularToolButton.IsChecked = false;
            this.EllipticalToolButton.IsChecked = false;
            this.PolygonalToolButton.IsChecked = false;
            this.MarqueeTool.Tool = MarqueeToolType.FreeHand;
        }


        //SquareCenter
        private void SquareCenter_Tapped(object sender, TappedRoutedEventArgs e)
        {
            bool square = this.Square.IsChecked ?? false;
            bool center = this.Center.IsChecked ?? false;

            if (square == false && center == false) this.MarqueeTool.MarqueeMode = MarqueeMode.None;
            else if (square && center == false) this.MarqueeTool.MarqueeMode = MarqueeMode.Square;
            else if (square == false && center) this.MarqueeTool.MarqueeMode = MarqueeMode.Center;
            else this.MarqueeTool.MarqueeMode = MarqueeMode.SquareAndCenter;
        }


        //Clear
        private void Clear_Tapped(object sender, TappedRoutedEventArgs e)
        {
            using (CanvasDrawingSession ds = this.Selection.CreateDrawingSession())
            {
                ds.Clear(Color.FromArgb(0, 0, 0, 0));
            }
            this.CanvasControl.Invalidate();
        }


        //CompositeMode
        private void New_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Add.IsChecked = false;
            this.Subtract.IsChecked = false;
            this.Intersect.IsChecked = false;
            this.MarqueeTool.CompositeMode = FanKit.Win2Ds.MarqueeCompositeMode.New;
        }
        private void Add_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.New.IsChecked = false;
            this.Subtract.IsChecked = false;
            this.Intersect.IsChecked = false;
            this.MarqueeTool.CompositeMode = FanKit.Win2Ds.MarqueeCompositeMode.Add;
        }
        private void Subtract_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.New.IsChecked = false;
            this.Add.IsChecked = false;
            this.Intersect.IsChecked = false;
            this.MarqueeTool.CompositeMode = FanKit.Win2Ds.MarqueeCompositeMode.Subtract;
        }
        private void Intersect_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.New.IsChecked = false;
            this.Add.IsChecked = false;
            this.Subtract.IsChecked = false;
            this.MarqueeTool.CompositeMode = FanKit.Win2Ds.MarqueeCompositeMode.Intersect;
        }


        #endregion


        #region Canvas


        private void CanvasControl_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            this.Selection = new CanvasRenderTarget(sender, 1000, 1000);
        }
        private void CanvasControl_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawImage(this.Selection);
            this.MarqueeTool.Draw(sender, args.DrawingSession);
        }
        private void CanvasControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.Selection != null)
            {
                if (Math.Abs(e.NewSize.Width - this.Selection.Size.Width) > 10)
                {
                    this.Selection = new CanvasRenderTarget(this.CanvasControl, (float)e.NewSize.Width, (float)e.NewSize.Height);
                }
            }
        }

        #endregion

        #region Pointer


        bool IsMove;
        private void CanvasControl_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            this.IsMove = true;

            this.MarqueeTool.Operator_Start(e.GetCurrentPoint(this.CanvasControl).Position.ToVector2());
            this.CanvasControl.Invalidate();
        }
        private void CanvasControl_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (this.IsMove)
            {
                this.MarqueeTool.Operator_Delta(e.GetCurrentPoint(this.CanvasControl).Position.ToVector2());
                this.CanvasControl.Invalidate();
            }
        }
        private void CanvasControl_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (this.IsMove)
            {
                this.IsMove = false;

                this.MarqueeTool.Operator_Complete(e.GetCurrentPoint(this.CanvasControl).Position.ToVector2());
                this.CanvasControl.Invalidate();
            }
        }








        #endregion

    }
}





















