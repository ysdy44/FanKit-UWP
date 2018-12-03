using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Numerics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Win2Ds
{
    public sealed partial class CanvasOperatorPage : Page
    {
        float Width = 100;
        float Height = 100;
        Vector2 Position;
        float Scale = 1f;

        public CanvasOperatorPage()
        {
            this.InitializeComponent();
        }
        
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.MarkdownText1.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Win2Ds/CanvasOperatorPage.xaml.txt");
            this.MarkdownText2.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Win2Ds/CanvasOperatorPage.xaml.cs.txt");
            this.MarkdownText3.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Win2Ds/CanvasOperator.cs.txt");
        }
        
        #region Operator


        private void Single_Start(Vector2 point)
        {
            this.Position = point;

            this.Invalidate("Single_Start", Visibility.Visible);
        }
        private void Single_Delta(Vector2 point)
        {
            this.Position = point;

            this.Invalidate("Single_Delta");
        }
        private void Single_Complete(Vector2 point)
        {
            this.Position = point;

            this.Invalidate("Single_Complete", Visibility.Collapsed);
        }




        Vector2 Right_Start_Point;
        Vector2 Right_Start_Position;
        private void Right_Start(Vector2 point)
        {
            this.Right_Start_Point = point;
            this.Right_Start_Position = this.Position;

            this.Invalidate("Right_Start", Visibility.Visible);
        }
        private void Right_Delta(Vector2 point)
        {
            this.Position = this.Right_Start_Position - this.Right_Start_Point + point;

            this.Invalidate("Right_Delta");
        }
        private void Right_Complete(Vector2 point)
        {
            this.Invalidate("Right_Complete", Visibility.Collapsed);
        }




        Vector2 Double_Start_Center;
        Vector2 Double_Start_Position;
        float Double_Start_Scale;
        float Double_Start_Space;
        private void Double_Start(Vector2 center, float space)
        {
            this.Double_Start_Center = center;
            this.Double_Start_Position = this.Position;

            this.Double_Start_Space = space;
            this.Double_Start_Scale = this.Scale;

            this.Invalidate("Double_Start", Visibility.Visible);
        }
        private void Double_Delta(Vector2 center, float space)
        {
            this.Position = this.Double_Start_Position - this.Double_Start_Center + center;

            this.Scale = this.Double_Start_Scale / this.Double_Start_Space * space;

            this.Invalidate("Double_Delta");
        }
        private void Double_Complete(Vector2 center, float space)
        {
            this.Invalidate("Double_Complete", Visibility.Collapsed);
        }


        //Wheel Changed
        private void Wheel_Changed(Vector2 point, float space)
        {
            if (space > 0)
            {
                if (this.Scale < 10f)
                {
                    this.Scale *= 1.1f;
                    this.Position = point + (this.Position - point) * 1.1f;
                }
            }
            else
            {
                if (this.Scale > 0.1f)
                {
                    this.Scale /= 1.1f;
                    this.Position = point + (this.Position - point) / 1.1f;
                }
            }

            this.Invalidate("Wheel_Changed", Visibility.Visible);
        }



        #endregion
        
        #region CanvasControl


        public void Invalidate(string text, Visibility? visibility = null)
        {
            this.CanvasControl.Invalidate();

            this.TipTextBlock.Text = text;
            if (visibility != null) this.TipBorder.Visibility = visibility ?? Visibility.Collapsed;
        }
        private void CanvasControl_CreateResources(CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            this.Position.X = (float)this.CanvasControl.ActualWidth / 2;
            this.Position.Y = (float)this.CanvasControl.ActualHeight / 2;
        }
        private void CanvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            this.RulerDraw(args.DrawingSession,this.Position,this.Scale);

            args.DrawingSession.Transform = Matrix3x2.CreateScale(this.Scale) * Matrix3x2.CreateTranslation(this.Position);
            args.DrawingSession.FillRectangle(-this.Width / 2, -this.Height / 2, this.Width, this.Height,Windows.UI.Colors.Gray);
        }




        CanvasTextFormat RulerTextFormat = new CanvasTextFormat() { FontSize = 12, HorizontalAlignment = CanvasHorizontalAlignment.Center, VerticalAlignment = CanvasVerticalAlignment.Center, };
        private void RulerDraw(CanvasDrawingSession ds, Vector2 position, float scale)
        {
            float ActualWidth = (float)this.CanvasControl. ActualWidth;
            float ActualHeight = (float)this.CanvasControl.ActualHeight;

            //line
            ds.DrawLine(20, 20, ActualWidth, 20, Windows.UI.Colors.Gray);//Horizontal
            ds.DrawLine(20, 20, 20, ActualHeight, Windows.UI.Colors.Gray);//Vertical

            //space
            float space = (float)(10 * scale);
            while (space < 10) space *= 5; 
            while (space > 100) space /= 5;
            float spaceFive = space * 5;

            //Horizontal
            for (float X = (float)position.X; X < ActualWidth; X += space) ds.DrawLine(X, 10, X, 20, Windows.UI.Colors.Gray);
            for (float X = (float)position.X; X > 20; X -= space) ds.DrawLine(X, 10, X, 20, Windows.UI.Colors.Gray);
            //Vertical
            for (float Y = (float)position.Y; Y < ActualHeight; Y += space) ds.DrawLine(10, Y, 20, Y, Windows.UI.Colors.Gray);
            for (float Y = (float)position.Y; Y > 20; Y -= space) ds.DrawLine(10, Y, 20, Y, Windows.UI.Colors.Gray);

            //Horizontal
            for (float X = (float)position.X; X < ActualWidth; X += spaceFive) ds.DrawLine(X, 10, X, 20, Windows.UI.Colors.Gray);
            for (float X = (float)position.X; X > 20; X -= spaceFive) ds.DrawLine(X, 10, X, 20, Windows.UI.Colors.Gray);
            //Vertical
            for (float Y = (float)position.Y; Y < ActualHeight; Y += spaceFive) ds.DrawLine(10, Y, 20, Y, Windows.UI.Colors.Gray);
            for (float Y = (float)position.Y; Y > 20; Y -= spaceFive) ds.DrawLine(10, Y, 20, Y, Windows.UI.Colors.Gray);

            //Horizontal
            for (float X = (float)position.X; X < ActualWidth; X += spaceFive) ds.DrawText(((int)(Math.Round((X - position.X) / scale))).ToString(), X, 10, Windows.UI.Colors.Gray, RulerTextFormat);
            for (float X = (float)position.X; X > 20; X -= spaceFive) ds.DrawText(((int)(Math.Round((X - position.X) / scale))).ToString(), X, 10, Windows.UI.Colors.Gray, RulerTextFormat);
            //Vertical
            for (float Y = (float)position.Y; Y < ActualHeight; Y += spaceFive) ds.DrawText(((int)(Math.Round((Y - position.Y) / scale))).ToString(), 10, Y, Windows.UI.Colors.Gray, RulerTextFormat);
            for (float Y = (float)position.Y; Y > 20; Y -= spaceFive) ds.DrawText(((int)(Math.Round((Y - position.Y) / scale))).ToString(), 10, Y, Windows.UI.Colors.Gray, RulerTextFormat);
        }


        #endregion
              
    }
}





