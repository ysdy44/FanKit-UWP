﻿using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Effects;
using System.Numerics;
using Windows.Foundation;


namespace FanKit.Frames.Win2Ds
{
    /// <summary>  the Dotted Line of photoshop  </summary>
    public class DottedLine
    {
        //Brush
        CanvasGradientStop[] Stops = new CanvasGradientStop[2] { new CanvasGradientStop { Color =Windows.UI.Colors.White, Position = 0 }, new CanvasGradientStop { Color = Windows.UI.Colors.Black, Position = 1 } };
        CanvasLinearGradientBrush Brush;

        //Vector
        Vector2 StartPoint;
        Vector2 EndPoint;
        Vector2 Space;

        //Image
        ICanvasImage Image;
        CanvasCommandList CommandList;
        

        public DottedLine(ICanvasResourceCreator creator, float distance = 6, float space = 1)
        {
            this.CommandList = new CanvasCommandList(creator);

            this.StartPoint = new Vector2(0, 0);
            this.EndPoint = new Vector2(distance, distance);
            this.Space = new Vector2(space, space);

            this.Brush = new CanvasLinearGradientBrush(creator, Stops, CanvasEdgeBehavior.Mirror, CanvasAlphaMode.Premultiplied);
            this.Brush.StartPoint = StartPoint;
            this.Brush.EndPoint = EndPoint;
        }


        //Render:When your image has changed, call it
        public void Render(ICanvasResourceCreator creator, float scaleX, float scaleY, ICanvasImage image)
        {
            //Scale :zoom up
            ScaleEffect effect = new ScaleEffect
            {
                Source = image,
                Scale = new Vector2(1 / scaleX, 1 / scaleY),
                InterpolationMode = CanvasImageInterpolation.NearestNeighbor,
            };
            //Crop:So that it does not exceed the canvas boundary
            Rect rect = effect.GetBounds(creator);
            CropEffect effect2 = new CropEffect
            {
                Source = effect,
                SourceRectangle = new Rect(2, 2, rect.Width - 4, rect.Height - 4),
            };


            //CanvasCommandList
            CanvasCommandList commandList = new CanvasCommandList(creator);
            using (var ds = commandList.CreateDrawingSession())
            {
                ds.Clear(Windows.UI.Colors.Transparent);
                
                ds.DrawImage(effect2);
            }
            //DottedLine
            this.Image = new LuminanceToAlphaEffect//Alpha
            {
                Source = new EdgeDetectionEffect//Edge
                {
                    Amount = 1,
                    Source = commandList
                },
            };
        }

        //Update
        public void Update()
        {
            this.StartPoint -= this.Space;
            this.EndPoint -= this.Space;
            this.Brush.StartPoint = this.StartPoint;
            this.Brush.EndPoint = this.EndPoint;
        }

        //Draw
        Rect r = new Rect();
        public void Draw(ICanvasResourceCreator creator, CanvasDrawingSession ds, double W, double H, float X = 0, float Y = 0)
        {
            if (this.Image != null)
            {
                this.CommandList = new CanvasCommandList(creator);
                this.r.Width = W;
                this.r.Height = H;

                using (var dds = this.CommandList.CreateDrawingSession())
                {
                    dds.FillRectangle(0, 0, (float)W, (float)H, Brush);
                    dds.DrawImage(this.Image, 0, 0, r, 1, CanvasImageInterpolation.NearestNeighbor, CanvasComposite.DestinationIn);
                }
                ds.DrawImage(CommandList, X, Y);
            }
        }

    }
}