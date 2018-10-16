using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Numerics;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace FanKit.Frames.Win2Ds
{
    public sealed partial class DottedLinePage : Page
    {
        
        DottedLine DottedLine;
        CanvasRenderTarget InPut;

        bool IsRender = false;
        float CanvasWidth =1000;
        float CanvasHeight =1000;

        public DottedLinePage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.MarkdownText1.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Win2Ds/DottedLineXaml.txt");
            this.MarkdownText2.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Win2Ds/DottedLineCs.txt");
            this.MarkdownText3.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Win2Ds/DottedLine.txt");
        }
        private void CanvasAnimatedControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Math.Abs(e.NewSize.Width-this.CanvasWidth)>10)
            {
                this.CanvasWidth = (float)e.NewSize.Width;
                this.CanvasHeight = (float)e.NewSize.Height;

                this.IsRender = true;
            }
        }

        
      
      
       #region Canvas


        private void CanvasAnimatedControl_CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            this.DottedLine = new DottedLine(sender, 6, 1);
            this.InPut = new CanvasRenderTarget(sender, this.CanvasWidth, this.CanvasHeight);
            this.IsRender = true;
        }
        private void CanvasAnimatedControl_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            this.DottedLine.Draw(sender, args.DrawingSession, this.CanvasWidth, this.CanvasHeight);
            if (this.IsMove) args.DrawingSession.DrawRectangle(new Rect(this.MoveStart, this.MoveEnd), Windows.UI.Colors.Gray);
        }
        private void CanvasAnimatedControl_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            this.DottedLine.Update();

            if (this.IsRender)
            {
                this.IsRender = false;

                this.InPut = new CanvasRenderTarget(this.CanvasAnimatedControl, this.CanvasWidth, this.CanvasHeight);
                this.DottedLine.Render(sender, 1, 1, this.InPut);
            }
        }


        #endregion
            
            



        #region Pointer


        bool IsMove;
        Point MoveStart;
        Point MoveEnd;
        private void CanvasAnimatedControl_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            this.MoveStart = this.MoveEnd = e.GetCurrentPoint(this.CanvasAnimatedControl).Position;
            this.IsMove = true;
        }
        private void CanvasAnimatedControl_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            this.MoveEnd = e.GetCurrentPoint(this.CanvasAnimatedControl).Position;
        }
        private void CanvasAnimatedControl_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (this.IsMove)
            {
                this.IsMove = false;
                using (var ds = this.InPut.CreateDrawingSession()) ds.FillRectangle(new Rect(this.MoveStart, this.MoveEnd), Windows.UI.Colors.Gray);
            }
        }



        #endregion


        



    }
}




