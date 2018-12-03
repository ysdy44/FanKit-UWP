using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Windows.UI;

namespace FanKit.Library.Win2Ds
{
    /// <summary>The marquee is a tool that can make selections that are  rectangular and elliptical.</summary>
    public class MarqueeTool
    {

        readonly Color LightBlue = Color.FromArgb(255, 128, 198, 255);
        readonly Color Blue = Color.FromArgb(255, 54, 135, 230);
        readonly Color White = Color.FromArgb(255, 255, 255, 255);
        readonly Color Black = Color.FromArgb(255, 0, 0, 0);
        readonly Color Shadow = Color.FromArgb(70, 127, 127, 127);

        Vector2 start;
        Vector2 end;
        readonly List<Vector2> list = new List<Vector2>();

        public MarqueeToolType Tool = MarqueeToolType.Rectangular;
        public MarqueeMode MarqueeMode = MarqueeMode.None;
        public MarqueeCompositeMode CompositeMode = MarqueeCompositeMode.New;

        //Delegate
        public delegate void CompleteHandler();
        public event CompleteHandler Complete = null;


        #region Draw


        /// <summary>Draw the marquee </summary>
        /// <param name="isFill">fill or draw</param>
        public void Draw(ICanvasResourceCreator creator, CanvasDrawingSession ds, bool isFill = false)
        {
            switch (this.Tool)
            {
                case MarqueeToolType.Rectangular: this.RectangularDraw(creator, ds, isFill); break;
                case MarqueeToolType.Elliptical: this.EllipticalDraw(creator, ds, isFill); break;
                case MarqueeToolType.Polygonal: this.PolygonalDraw(creator, ds, isFill); break;
                case MarqueeToolType.FreeHand: this.FreeHandDraw(creator, ds, isFill); break;
                default: break;
            }
        }


        /// <summary>Draw the rectangular marquee</summary>
        private void RectangularDraw(ICanvasResourceCreator creator, CanvasDrawingSession ds, bool isFill)
        {
            switch (this.MarqueeMode)
            {
                case MarqueeMode.None:
                    float x0 = Math.Min(this.start.X, this.end.X);
                    float y0 = Math.Min(this.start.Y, this.end.Y);
                    float w0 = Math.Abs(this.start.X - this.end.X);
                    float h0 = Math.Abs(this.start.Y - this.end.Y);

                    if (isFill) ds.FillRectangle(x0, y0, w0, h0, this.LightBlue);
                    else
                    {
                        ds.DrawRectangle(x0, y0, w0, h0, this.Black, 2.0f);
                        ds.DrawRectangle(x0, y0, w0, h0, this.White, 1.0f);
                    }
                    break;


                case MarqueeMode.Square:
                    float w1 = Math.Abs(this.start.X - this.end.X);
                    float h1 = Math.Abs(this.start.Y - this.end.Y);
                    float square = (w1 + h1) / 2;

                    float x1 = (this.end.X > this.start.X) ? this.start.X : this.start.X - square;
                    float y1 = (this.end.Y > this.start.Y) ? this.start.Y : this.start.Y - square;

                    if (isFill) ds.FillRectangle(x1, y1, square, square, this.LightBlue);
                    else
                    {
                        ds.DrawRectangle(x1, y1, square, square, this.Black, 2.0f);
                        ds.DrawRectangle(x1, y1, square, square, this.White, 1.0f);
                    }
                    break;


                case MarqueeMode.Center:
                    float w2 = Math.Abs(this.start.X - this.end.X);
                    float h2 = Math.Abs(this.start.Y - this.end.Y);

                    float x2 = this.start.X - w2;
                    float y2 = this.start.Y - h2;

                    if (isFill) ds.FillRectangle(x2, y2, 2 * w2, 2 * h2, this.LightBlue);
                    else
                    {
                        ds.DrawRectangle(x2, y2, 2 * w2, 2 * h2, this.Black, 2.0f);
                        ds.DrawRectangle(x2, y2, 2 * w2, 2 * h2, this.White, 1.0f);
                    }
                    break;


                case MarqueeMode.SquareAndCenter:
                    float w3 = Math.Abs(this.start.X - this.end.X);
                    float h3 = Math.Abs(this.start.Y - this.end.Y);
                    float squareHalf3 = (w3 + h3) / 2;
                    float square3 = 2 * squareHalf3;

                    float x3 = this.start.X - squareHalf3;
                    float y3 = this.start.Y - squareHalf3;

                    if (isFill) ds.FillRectangle(x3, y3, square3, square3, this.LightBlue);
                    else
                    {
                        ds.DrawRectangle(x3, y3, square3, square3, this.Black, 2.0f);
                        ds.DrawRectangle(x3, y3, square3, square3, this.White, 1.0f);
                    }
                    break;

                default:
                    break;
            }
        }



        /// <summary>Draw the elliptical marquee</summary>
        private void EllipticalDraw(ICanvasResourceCreator creator, CanvasDrawingSession ds, bool isFill)
        {
            switch (this.MarqueeMode)
            {
                case MarqueeMode.None:
                    float x0 = (this.start.X + this.end.X) / 2;
                    float y0 = (this.start.Y + this.end.Y) / 2;
                    float w0 = Math.Abs(this.start.X - this.end.X) / 2;
                    float h0 = Math.Abs(this.start.Y - this.end.Y) / 2;

                    if (isFill) ds.FillEllipse(x0, y0, w0, h0, this.LightBlue);
                    else
                    {
                        ds.DrawEllipse(x0, y0, w0, h0, this.Black, 2.0f);
                        ds.DrawEllipse(x0, y0, w0, h0, this.White, 1.0f);
                    }
                    break;


                case MarqueeMode.Square:
                    float w1 = Math.Abs(this.start.X - this.end.X);
                    float h1 = Math.Abs(this.start.Y - this.end.Y);
                    float squareHalf = (w1 + h1) / 4;

                    float x1 = (this.end.X > this.start.X) ? this.start.X + squareHalf : this.start.X - squareHalf;
                    float y1 = (this.end.Y > this.start.Y) ? this.start.Y + squareHalf : this.start.Y - squareHalf;

                    if (isFill) ds.FillCircle(x1, y1, squareHalf, this.LightBlue);
                    else
                    {
                        ds.DrawCircle(x1, y1, squareHalf, this.Black, 2.0f);
                        ds.DrawCircle(x1, y1, squareHalf, this.White, 1.0f);
                    }
                    break;


                case MarqueeMode.Center:
                    float w2 = Math.Abs(this.start.X - this.end.X) / 2;
                    float h2 = Math.Abs(this.start.Y - this.end.Y) / 2;

                    if (isFill) ds.FillEllipse(this.start, w2, h2, this.LightBlue);
                    else
                    {
                        ds.DrawEllipse(this.start, w2, h2, this.Black, 2.0f);
                        ds.DrawEllipse(this.start, w2, h2, this.White, 1.0f);
                    }
                    break;


                case MarqueeMode.SquareAndCenter:
                    float w3 = Math.Abs(this.start.X - this.end.X) / 2;
                    float h3 = Math.Abs(this.start.Y - this.end.Y) / 2;
                    float square2 = (w3 + h3) / 2;

                    if (isFill) ds.FillCircle(this.start, square2, this.LightBlue);
                    else
                    {
                        ds.DrawCircle(this.start, square2, this.Black, 2.0f);
                        ds.DrawCircle(this.start, square2, this.White, 1.0f);
                    }
                    break;

                default:
                    break;
            }
        }



        /// <summary> Draw the polygonal marquee </summary>
        private void PolygonalDraw(ICanvasResourceCreator creator, CanvasDrawingSession ds, bool isFill)
        {
            if (this.list == null) return;
            if (this.list.Count == 0) return;

            CanvasGeometry geometry = CanvasGeometry.CreatePolygon(creator, this.list.ToArray());

            if (isFill) ds.FillGeometry(geometry, this.LightBlue);
            else
            {
                ds.DrawGeometry(geometry, this.Black, 2.0f);
                ds.DrawGeometry(geometry, this.White, 1.0f);

                this.DrawNodeVector(ds, this.list.First());
                this.DrawNodeVector(ds, this.list.Last());
            }
        }



        /// <summary>Draw the free hand marquee</summary>
        private void FreeHandDraw(ICanvasResourceCreator creator, CanvasDrawingSession ds, bool isFill)
        {
            if (this.list == null) return;
            if (this.list.Count == 0) return;

            CanvasGeometry geometry = CanvasGeometry.CreatePolygon(creator, this.list.ToArray());

            if (isFill) ds.FillGeometry(geometry, this.LightBlue);
            else
            {
                ds.DrawGeometry(geometry, this.Black, 2.0f);
                ds.DrawGeometry(geometry, this.White, 1.0f);
            }
        }



        /// <summary>Draw a ● </summary>
        private void DrawNodeVector(CanvasDrawingSession ds, Vector2 vector)
        {
            ds.FillCircle(vector, 10, this.Shadow);
            ds.FillCircle(vector, 8, this.White);
            ds.FillCircle(vector, 6, this.Blue);
        }



        #endregion


        #region Render


        /// <summary>  Render the marquee </summary>
        public void Render(ICanvasResourceCreator creator, CanvasRenderTarget selection) => this.ToRender(selection, this.GetRender(creator));
        public void Render(ICanvasResourceCreator creator, CanvasRenderTarget selection, Matrix3x2 matrix) => this.ToRender(selection, new Transform2DEffect
        {
            Source = this.GetRender(creator),
            TransformMatrix = matrix
        });

        private CanvasCommandList GetRender(ICanvasResourceCreator creator)
        {
            CanvasCommandList command = new CanvasCommandList(creator);
            using (CanvasDrawingSession ds = command.CreateDrawingSession())
            {
                this.Draw(creator, ds, true);
            }
            return command;
        }

        private void ToRender(CanvasRenderTarget selection, ICanvasImage image)
        {
            using (CanvasDrawingSession ds = selection.CreateDrawingSession())
            {
                if (this.CompositeMode == MarqueeCompositeMode.New) ds.Clear(Color.FromArgb(0, 0, 0, 0));
                ds.DrawImage(image, 0, 0, selection.Bounds, 1, CanvasImageInterpolation.Linear, this.GetCanvasComposite(this.CompositeMode));
            }
        }

        private CanvasComposite GetCanvasComposite(MarqueeCompositeMode mode)
        {
            switch (mode)
            {
                case MarqueeCompositeMode.New: return CanvasComposite.SourceOver;
                case MarqueeCompositeMode.Add: return CanvasComposite.SourceOver;
                case MarqueeCompositeMode.Subtract: return CanvasComposite.DestinationOut;
                case MarqueeCompositeMode.Intersect: return CanvasComposite.DestinationIn;
                case MarqueeCompositeMode.Xor: return CanvasComposite.Xor;
                default: break;
            }
            return CanvasComposite.SourceOver;
        }


        #endregion


        #region Operator


        public void Operator_Start(Vector2 v)
        {
            switch (this.Tool)
            {
                case MarqueeToolType.Rectangular: this.Rectangular_Start(v); break;
                case MarqueeToolType.Elliptical: this.Elliptical_Start(v); break;
                case MarqueeToolType.Polygonal: this.Polygonal_Start(v); break;
                case MarqueeToolType.FreeHand: this.FreeHand_Start(v); break;
                default: break;
            }
        }
        public void Operator_Delta(Vector2 v)
        {
            switch (this.Tool)
            {
                case MarqueeToolType.Rectangular: this.Rectangular_Delta(v); break;
                case MarqueeToolType.Elliptical: this.Elliptical_Delta(v); break;
                case MarqueeToolType.Polygonal: this.Polygonal_Delta(v); break;
                case MarqueeToolType.FreeHand: this.FreeHand_Delta(v); break;
                default: break;
            }
        }
        public void Operator_Complete(Vector2 v)
        {
            switch (this.Tool)
            {
                case MarqueeToolType.Rectangular: this.Rectangular_Complete(v); break;
                case MarqueeToolType.Elliptical: this.Elliptical_Complete(v); break;
                case MarqueeToolType.Polygonal: this.Polygonal_Complete(v); break;
                case MarqueeToolType.FreeHand: this.FreeHand_Complete(v); break;
                default: break;
            }
        }


        private void Rectangular_Start(Vector2 v) => this.start = this.end = v;
        private void Rectangular_Delta(Vector2 v) => this.end = v;
        private void Rectangular_Complete(Vector2 v)
        {
            this.Complete?.Invoke();//Delegate

            this.start = this.end = Vector2.Zero;
        }


        private void Elliptical_Start(Vector2 v) => this.start = this.end = v;
        private void Elliptical_Delta(Vector2 v) => this.end = v;
        private void Elliptical_Complete(Vector2 v)
        {
            this.Complete?.Invoke();//Delegate

            this.start = this.end = Vector2.Zero;
        }


        private void FreeHand_Start(Vector2 v) => this.list.Add(v);
        private void FreeHand_Delta(Vector2 v) => this.list.Add(v);
        private void FreeHand_Complete(Vector2 v)
        {
            this.Complete?.Invoke();//Delegate

            this.list.Clear();
        }



        bool isPolygonal = false;

        private void Polygonal_Start(Vector2 v)
        {
            if (this.isPolygonal) this.Polygonal_Delta(v);
            else this.FreeHand_Start(v);
        }
        private void Polygonal_Delta(Vector2 v)
        {
            if (this.list.Count > 1) this.list[this.list.Count - 1] = v;
        }
        private void Polygonal_Complete(Vector2 v)
        {
            if (this.isPolygonal)
            {
                if (this.list.Count > 2)
                {
                    if ((this.list.First() - this.list.Last()).LengthSquared() < 100.0f)
                    {
                        this.isPolygonal = false;
                        this.FreeHand_Complete(v);
                        return;
                    }
                }
            }

            this.isPolygonal = true;
            this.FreeHand_Delta(v);
        }


        #endregion

    }

    /// <summary> Tools of different shapes </summary>
    public enum MarqueeToolType
    {
        /// <summary> 口 </summary>
        Rectangular,
        /// <summary> ◯ </summary>
        Elliptical,
        /// <summary> 🗨 </summary>
        Polygonal,
        /// <summary> 🗯 </summary>
        FreeHand,
    }

    /// <summary> Constraints the marquee </summary>
    public enum MarqueeMode
    {
        /// <summary> None </summary>
        None,
        /// <summary> A square marquee </summary>
        Square,
        /// <summary> Mouse is the center of the marquee </summary>
        Center,
        /// <summary> Both of these are </summary>
        SquareAndCenter,
    }

    /// <summary>The composite mode used for the marquee. </summary>
    public enum MarqueeCompositeMode
    {
        /// <summary>New bitmaps</summary>
        New,
        /// <summary>Union of source and destination bitmaps. </summary>
        Add,
        /// <summary>Region of the source bitmap. </summary>
        Subtract,
        /// <summary> Intersection of source and destination bitmaps.</summary>
        Intersect,

        /// <summary>Union of source and destination bitmaps with xor function for pixels that overlap. </summary>
        Xor,
    }

}

