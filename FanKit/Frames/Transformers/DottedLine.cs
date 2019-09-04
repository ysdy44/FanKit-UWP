using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.Geometry;
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
        /// Fill a Elliptical (color is <see cref="Windows.UI.Colors.DodgerBlue"/>).
        /// </summary>
        /// <param name="drawingSession"> The drawing-session. </param>
        /// <param name="transformerRect"> The transformer-rect. </param>
        public static void FillEllipseDodgerBlue(this CanvasDrawingSession drawingSession, TransformerRect transformerRect)
        {
            //TODO:换 
            float CenterX = (transformerRect.Left + transformerRect.Right) / 2;
            float CenterY = (transformerRect.Top + transformerRect.Bottom) / 2;
            // Vector2 centerPoint = new Vector2(transformerRect.CenterX, transformerRect.CenterY);
            Vector2 centerPoint = new Vector2(CenterX, CenterY);

            float radiusX = (transformerRect.Right - transformerRect.Left) / 2;
            float radiusY = (transformerRect.Bottom - transformerRect.Top) / 2;

            drawingSession.FillEllipse(centerPoint, radiusX, radiusY, Windows.UI.Color.FromArgb(90, 54, 135, 230));
            drawingSession.DrawEllipse(centerPoint, radiusX, radiusY, Windows.UI.Colors.DodgerBlue);
        }

        /// <summary>
        /// Fill a Elliptical (color is <see cref="Windows.UI.Colors.DodgerBlue"/>).
        /// </summary>
        /// <param name="drawingSession"> The drawing-session. </param>
        /// <param name="resourceCreator"> The resource-creator. </param>
        /// <param name="transformerRect"> The transformer-rect. </param>
        /// <param name="matrix"> The matrix. </param>
        public static void FillEllipseDodgerBlue(this CanvasDrawingSession drawingSession, ICanvasResourceCreator resourceCreator, TransformerRect transformerRect, Matrix3x2 matrix)
        {
            /// <summary>
            /// A Ellipse has left, top, right, bottom four nodes.
            /// 
            /// Control points on the left and right sides of the node.
            /// 
            /// The distance of the control point 
            /// is 0.552f times
            /// the length of the square edge.
            /// <summary>

            //LTRB
            /*
            Vector2 left = Vector2.Transform(new Vector2(transformerRect.Left, transformerRect.CenterY), matrix);
            Vector2 top = Vector2.Transform(new Vector2(transformerRect.CenterX, transformerRect.Top), matrix);
            Vector2 right = Vector2.Transform(new Vector2(transformerRect.Right, transformerRect.CenterY), matrix);
            Vector2 bottom = Vector2.Transform(new Vector2(transformerRect.CenterX, transformerRect.Bottom), matrix);
             */
            //TODO:换 
            float CenterX = (transformerRect.Left + transformerRect.Right) / 2;
            float CenterY = (transformerRect.Top + transformerRect.Bottom) / 2;
            Vector2 left = Vector2.Transform(new Vector2(transformerRect.Left, CenterY), matrix);
            Vector2 top = Vector2.Transform(new Vector2(CenterX, transformerRect.Top), matrix);
            Vector2 right = Vector2.Transform(new Vector2(transformerRect.Right, CenterY), matrix);
            Vector2 bottom = Vector2.Transform(new Vector2(CenterX, transformerRect.Bottom), matrix);


            //HV
            Vector2 horizontal = (right - left) * 0.276f;// vector / 2 * 0.552f
            Vector2 vertical = (bottom - top) * 0.276f;// vector / 2 * 0.552f

            //Control
            Vector2 left1 = left - vertical;
            Vector2 left2 = left + vertical;
            Vector2 top1 = top + horizontal;
            Vector2 top2 = top - horizontal;
            Vector2 right1 = right + vertical;
            Vector2 right2 = right - vertical;
            Vector2 bottom1 = bottom - horizontal;
            Vector2 bottom2 = bottom + horizontal;

            //Path
            CanvasPathBuilder pathBuilder = new CanvasPathBuilder(resourceCreator);
            pathBuilder.BeginFigure(bottom);
            pathBuilder.AddCubicBezier(bottom1, left2, left);
            pathBuilder.AddCubicBezier(left1, top2, top);
            pathBuilder.AddCubicBezier(top1, right2, right);
            pathBuilder.AddCubicBezier(right1, bottom2, bottom);
            pathBuilder.EndFigure(CanvasFigureLoop.Closed);
    
            //Geometry
            CanvasGeometry canvasGeometry = CanvasGeometry.CreatePath(pathBuilder);
            drawingSession.FillGeometry(canvasGeometry, Windows.UI.Color.FromArgb(90, 54, 135, 230));
            drawingSession.DrawGeometry(canvasGeometry, Windows.UI.Colors.DodgerBlue);
        }


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