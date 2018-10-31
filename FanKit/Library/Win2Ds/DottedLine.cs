using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Effects;
using System.Numerics;
using Windows.Foundation;


namespace FanKit.Library.Win2Ds
{
    /// <summary>  the Dotted Line of photoshop  </summary>
    public class DottedLine
    {
        //Brush  
        Vector2 Space;
        CanvasLinearGradientBrush Brush;
        CanvasGradientStop[] Stops = new CanvasGradientStop[2] { new CanvasGradientStop { Color =Windows.UI.Colors.White, Position = 0 }, new CanvasGradientStop { Color = Windows.UI.Colors.Black, Position = 1 } };

        //Image
        ICanvasImage OutPut;

        public DottedLine(ICanvasResourceCreator creator, float distance = 6, float space = 1)
        {            
            this.Space = new Vector2(space, space);

            this.Brush = new CanvasLinearGradientBrush(creator, Stops, CanvasEdgeBehavior.Mirror, CanvasAlphaMode.Premultiplied)
            {
                StartPoint = new Vector2(0, 0),
                EndPoint = new Vector2(distance, distance)
            };
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

            
            //DottedLine
            this.OutPut = new LuminanceToAlphaEffect//Alpha
            {
                Source = new EdgeDetectionEffect//Edge
                {
                    Amount = 1,
                    Source = effect2
                },
            };
        }

        //Update
        public void Update()
        {
            this.Brush.StartPoint -= this.Space;
            this.Brush.EndPoint -= this.Space;
        }

        //Draw
        Rect r = new Rect();
        public void Draw(ICanvasResourceCreator creator, CanvasDrawingSession ds, double W, double H, float X = 0, float Y = 0)
        {
            if (this.OutPut != null)
            {
                CanvasCommandList CommandList = new CanvasCommandList(creator);
                using (var dds = CommandList.CreateDrawingSession())
                {
                    dds.FillRectangle(0, 0, (float)W, (float)H, this.Brush);
                    dds.DrawImage(this.OutPut, 0, 0, new Rect(0,0,W,H), 1, CanvasImageInterpolation.NearestNeighbor, CanvasComposite.DestinationIn);
                }
                ds.DrawImage(CommandList, X, Y);
            }
        }

    }
}