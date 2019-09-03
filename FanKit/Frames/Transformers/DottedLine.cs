using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Numerics;
using Windows.Foundation;
using Windows.Graphics.Effects;

namespace FanKit.Transformers
{

    public class DottedLineBrush : IDisposable
    {
        //Brush  
        public CanvasLinearGradientBrush Brush { get; private set; }
        public CanvasGradientStop[] Stops { get; private set; } = new CanvasGradientStop[2]
        {
            new CanvasGradientStop
            {
                Color = Windows.UI.Colors.White,
                Position = 0
            },
            new CanvasGradientStop
            {
                Color = Windows.UI.Colors.Black, Position = 1
            }
        };
        
        /// <summary> Initialize DottedLine</summary>
        /// <param name="distance">Distance between black and white</param>
        /// <param name="space">Refresh, change the position of the gradient</param>
        public DottedLineBrush(ICanvasResourceCreator resourceCreator, float distance = 6)
        {
            this.Brush = new CanvasLinearGradientBrush(resourceCreator, Stops, CanvasEdgeBehavior.Mirror, CanvasAlphaMode.Premultiplied)
            {
                StartPoint = new Vector2(0, 0),
                EndPoint = new Vector2(distance, distance)
            };
        }

        /// <summary>Update</summary>
        public void Update(float space = 1)
        {
            Vector2 vector = new Vector2(space, space);
            this.Brush.StartPoint -= vector;
            this.Brush.EndPoint -= vector;
        }
                
        public void Dispose()
        {
            this.Brush.Dispose();
            this.Brush = null;
            this.Stops = null;
        }
    }

    public class DottedLineImage:IDisposable
    {
        public ICanvasImage Output { get; private set; }
        public CanvasRenderTarget Input { get; private set; }

        // Initializes a new instance of the CanvasRenderTarget class.
        public DottedLineImage(CanvasRenderTarget input) => this.Input = input;
      
        // Returns a new drawing session. The drawing session draws onto the CanvasRenderTarget.
        public CanvasDrawingSession CreateDrawingSession() => this.Input.CreateDrawingSession();
        
        public void Baking(ICanvasResourceCreator creator, bool isCrop = true) => this.Output = this._createLuminance(creator, this._createCrop(creator, this.Input, isCrop));
        public void Baking(ICanvasResourceCreator creator, Matrix3x2 matrix, bool isCrop = true) => this.Output = this._createLuminance(creator, new Transform2DEffect//Transform
        {
            TransformMatrix = matrix,
            Source = this._createCrop(creator, this.Input, isCrop)
        });

        private ICanvasImage _createLuminance(ICanvasResourceCreator resourceCreator, IGraphicsEffectSource image) => new LuminanceToAlphaEffect//Alpha
        {
            Source = new EdgeDetectionEffect//Edge
            {
                Amount = 1,
                Source = image,
            }
        };
        private IGraphicsEffectSource _createCrop(ICanvasResourceCreator resourceCreator, CanvasRenderTarget canvasRenderTarget, bool isCrop)
        {
            if (isCrop == false) return canvasRenderTarget;

            Rect cropRectangle = new Rect(2, 2, canvasRenderTarget.SizeInPixels.Width - 4, canvasRenderTarget.SizeInPixels.Height - 4);

            CanvasCommandList canvasCommandList = new CanvasCommandList(resourceCreator);
            using (CanvasDrawingSession drawingSession = canvasCommandList.CreateDrawingSession())
            {
                drawingSession.Clear(Windows.UI.Colors.Transparent);
                drawingSession.DrawImage(new CropEffect
                {
                    Source = canvasRenderTarget,
                    SourceRectangle = cropRectangle,
                });
            };
            return canvasCommandList;
        }

        public void Dispose()
        {
            if (this.Output!=null)
            {
                this.Output.Dispose();
                this.Output=null;
            }

            if (this.Input != null)
            {
                this.Input.Dispose();
            }
        }
    }

    /// <summary> The Dotted Line of photoshop  </summary>
    public static class DottedLine
    {
        /// <summary>
        /// Turn to Rect.
        /// </summary>
        public static Rect ToRect(this TransformerRect transformerRect) => new Rect(transformerRect.Left, transformerRect.Top, transformerRect.Right - transformerRect.Left, transformerRect.Bottom - transformerRect.Top);

        /// <summary>Draw</summary>
        /// <param name="canvasBounds">the bounds of this CanvasCOntrol.</param>
        public static void DrawDottedLine(this CanvasDrawingSession ds, ICanvasResourceCreator creator, DottedLineBrush dottedLineBrush, DottedLineImage dottedLineImage, float width, float height, float x = 0, float y = 0)
        {
            ICanvasImage image = dottedLineImage.Output;
             Rect canvasBounds = new Rect(x, y, width, height);
             
            CanvasCommandList commandList = new CanvasCommandList(creator);
            using (var dds = commandList.CreateDrawingSession())
            {
                dds.FillRectangle(canvasBounds, dottedLineBrush.Brush);
                dds.DrawImage(image, x, y, canvasBounds, 1, CanvasImageInterpolation.NearestNeighbor, CanvasComposite.DestinationIn);
            }
            ds.DrawImage(commandList);
        }
    }

}