using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Windows.UI;

namespace FanKit.Library.Win2Ds
{
    class MarqueeTool
    {

        readonly Color LightBlue = Color.FromArgb(255, 128, 198, 255);
        readonly Color Blue = Color.FromArgb(255, 54, 135, 230);
        readonly Color White = Color.FromArgb(255, 255, 255, 255);
        readonly Color Black = Color.FromArgb(255, 0, 0, 0);
        readonly Color Shadow = Color.FromArgb(70, 127, 127, 127);

        public Vector2 Start;
        public Vector2 End;
        public List<Vector2> List = new List<Vector2>();
        public ToolType Tool = ToolType.Rectangular;
        public MarqueeMode MarqueeMode = MarqueeMode.None;
        public CompositeMode CompositeMode = CompositeMode.New;

        //Delegate
        public delegate void CompleteHandler();
        public event CompleteHandler Complete = null;

        public MarqueeTool()
        {
        }


        #region Draw


        /// <summary>
        /// draw the marquee
        /// </summary>
        /// <param name="creator"></param>
        /// <param name="ds"></param>
        /// <param name="isFill">fill or draw</param>
        public void Draw(ICanvasResourceCreator creator, CanvasDrawingSession ds, bool isFill = false)
        {
            switch (this.Tool)
            {
                case ToolType.Rectangular: this.RectangularDraw(creator, ds, isFill); break;
                case ToolType.Elliptical: this.EllipticalDraw(creator, ds, isFill); break;
                case ToolType.Polygonal: this.PolygonalDraw(creator, ds, isFill); break;
                case ToolType.FreeHand: this.FreeHandDraw(creator, ds, isFill); break;
                default: break;
            }
        }


        /// <summary>
        /// draw the rectangular marquee
        /// </summary>
        /// <param name="creator"></param>
        /// <param name="ds"></param>
        public void RectangularDraw(ICanvasResourceCreator creator, CanvasDrawingSession ds, bool isFill )
        {
            switch (this.MarqueeMode)
            {
                case MarqueeMode.None:
                    float x0 = Math.Min(this.Start.X, this.End.X);
                    float y0 = Math.Min(this.Start.Y, this.End.Y);
                    float w0 = Math.Abs(this.Start.X - this.End.X);
                    float h0 = Math.Abs(this.Start.Y - this.End.Y);

                    if (isFill) ds.FillRectangle(x0, y0, w0, h0, this.LightBlue);
                    else
                    {
                        ds.DrawRectangle(x0, y0, w0, h0, this.Black, 2.0f);
                        ds.DrawRectangle(x0, y0, w0, h0, this.White, 1.0f);
                    }
                    break;


                case MarqueeMode.Square:
                    float w1 = Math.Abs(this.Start.X - this.End.X);
                    float h1 = Math.Abs(this.Start.Y - this.End.Y);
                    float square = (w1 + h1) / 2;

                    float x1 = (this.End.X > this.Start.X) ? this.Start.X : this.Start.X - square;
                    float y1 = (this.End.Y > this.Start.Y) ? this.Start.Y : this.Start.Y - square;

                    if (isFill) ds.FillRectangle(x1, y1, square, square, this.LightBlue);
                    else
                    {
                        ds.DrawRectangle(x1, y1, square, square, this.Black, 2.0f);
                        ds.DrawRectangle(x1, y1, square, square, this.White, 1.0f);
                    }
                    break;


                case MarqueeMode.Center:
                    float w2 = Math.Abs(this.Start.X - this.End.X);
                    float h2 = Math.Abs(this.Start.Y - this.End.Y);

                    float x2 = this.Start.X - w2;
                    float y2 = this.Start.Y - h2;

                    if (isFill) ds.FillRectangle(x2, y2, 2 * w2, 2 * h2, this.LightBlue);
                    else
                    {
                        ds.DrawRectangle(x2, y2, 2 * w2, 2 * h2, this.Black, 2.0f);
                        ds.DrawRectangle(x2, y2, 2 * w2, 2 * h2, this.White, 1.0f);
                    }
                    break;


                case MarqueeMode.SquareAndCenter:
                    float w3 = Math.Abs(this.Start.X - this.End.X);
                    float h3 = Math.Abs(this.Start.Y - this.End.Y);
                    float squareHalf3 = (w3 + h3) / 2;
                    float square3 = 2 * squareHalf3;

                    float x3 = this.Start.X - squareHalf3;
                    float y3 = this.Start.Y - squareHalf3;

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



        /// <summary>
        /// draw the elliptical marquee
        /// </summary>
        /// <param name="creator"></param>
        /// <param name="ds"></param>
        public void EllipticalDraw(ICanvasResourceCreator creator, CanvasDrawingSession ds, bool isFill)
        {
            switch (this.MarqueeMode)
            {
                case MarqueeMode.None:
                    float x0 = (this.Start.X + this.End.X) / 2;
                    float y0 = (this.Start.Y + this.End.Y) / 2;
                    float w0 = Math.Abs(this.Start.X - this.End.X) / 2;
                    float h0 = Math.Abs(this.Start.Y - this.End.Y) / 2;

                    if (isFill) ds.FillEllipse(x0, y0, w0, h0, this.LightBlue);
                    else
                    {
                        ds.DrawEllipse(x0, y0, w0, h0, this.Black, 2.0f);
                        ds.DrawEllipse(x0, y0, w0, h0, this.White, 1.0f);
                    }
                    break;


                case MarqueeMode.Square:
                    float w1 = Math.Abs(this.Start.X - this.End.X);
                    float h1 = Math.Abs(this.Start.Y - this.End.Y);
                    float squareHalf = (w1 + h1) / 4;

                    float x1 = (this.End.X > this.Start.X) ? this.Start.X+ squareHalf : this.Start.X - squareHalf;
                    float y1 = (this.End.Y > this.Start.Y) ? this.Start.Y+ squareHalf : this.Start.Y - squareHalf;

                    if (isFill) ds.FillCircle(x1, y1, squareHalf, this.LightBlue);
                    else
                    {
                        ds.DrawCircle(x1, y1, squareHalf, this.Black, 2.0f);
                        ds.DrawCircle(x1, y1, squareHalf, this.White, 1.0f);
                    }
                    break;


                case MarqueeMode.Center:
                    float w2 = Math.Abs(this.Start.X - this.End.X) / 2;
                    float h2 = Math.Abs(this.Start.Y - this.End.Y) / 2;

                    if (isFill) ds.FillEllipse(this.Start, w2, h2, this.LightBlue);
                    else
                    {
                        ds.DrawEllipse(this.Start, w2, h2, this.Black, 2.0f);
                        ds.DrawEllipse(this.Start, w2, h2, this.White, 1.0f);
                    }
                    break;


                case MarqueeMode.SquareAndCenter:
                    float w3 = Math.Abs(this.Start.X - this.End.X) / 2;
                    float h3 = Math.Abs(this.Start.Y - this.End.Y) / 2;
                    float square2 = (w3 + h3) / 2;

                    if (isFill) ds.FillCircle(this.Start, square2, this.LightBlue);
                    else
                    {
                        ds.DrawCircle(this.Start, square2, this.Black, 2.0f);
                        ds.DrawCircle(this.Start, square2, this.White, 1.0f);
                    }
                    break;

                default:
                    break;
            }
        }



        /// <summary>
        /// draw the polygonal marquee
        /// </summary>
        /// <param name="creator"></param>
        /// <param name="ds"></param>
        public void PolygonalDraw(ICanvasResourceCreator creator, CanvasDrawingSession ds, bool isFill)
        {
            if (this.List == null) return;
            if (this.List.Count == 0) return;

            CanvasGeometry geometry = CanvasGeometry.CreatePolygon(creator, this.List.ToArray());

            if (isFill) ds.FillGeometry(geometry, this.LightBlue);
            else
            {
                ds.DrawGeometry(geometry, this.Black, 2.0f);
                ds.DrawGeometry(geometry, this.White, 1.0f);

                this.DrawNodeVector(ds, this.List.First());
                this.DrawNodeVector(ds, this.List.Last());
            }
        }



        /// <summary>
        /// draw the free hand marquee
        /// </summary>
        /// <param name="creator"></param>
        /// <param name="ds"></param>
        public void FreeHandDraw(ICanvasResourceCreator creator, CanvasDrawingSession ds, bool isFill)
        {
            if (this.List == null) return;
            if (this.List.Count == 0) return;

            CanvasGeometry geometry = CanvasGeometry.CreatePolygon(creator, this.List.ToArray());

            if (isFill) ds.FillGeometry(geometry, this.LightBlue);
            else
            {
                ds.DrawGeometry(geometry, this.Black, 2.0f);
                ds.DrawGeometry(geometry, this.White, 1.0f);
            }
        }



        /// <summary>
        /// draw a ●
        /// </summary>
        private void DrawNodeVector(CanvasDrawingSession ds, Vector2 vector)
        {
            ds.FillCircle(vector, 10, this.Shadow);
            ds.FillCircle(vector, 8, this.White);
            ds.FillCircle(vector, 6, this.Blue);
        }



        #endregion

        #region Render


        /// <summary>
        /// render the marquee
        /// </summary>
        /// <param name="creator"></param>
        /// <param name="ds"></param>
        public void Render(ICanvasResourceCreator creator, CanvasRenderTarget selection)
        {
            CanvasCommandList command = new CanvasCommandList(creator);
            using (CanvasDrawingSession ds = command.CreateDrawingSession())
            {
                this.Draw(creator,ds,true);
            }
            using (CanvasDrawingSession ds = selection.CreateDrawingSession())
            {
                if (this.CompositeMode == CompositeMode.New)ds.Clear(Color.FromArgb(0,0,0,0));
                ds.DrawImage(command, 0, 0, selection.Bounds, 1, CanvasImageInterpolation.Linear, this.getCanvasComposite(this.CompositeMode));
            }
        }



        private CanvasComposite getCanvasComposite(CompositeMode mode)
        {
            switch (mode)
            {
                case CompositeMode.New: return CanvasComposite.SourceOver; 
                case CompositeMode.Add: return CanvasComposite.SourceOver; 
                case CompositeMode.Subtract: return CanvasComposite.DestinationOut; 
                case CompositeMode.Intersect: return CanvasComposite.DestinationIn; 
                case CompositeMode.Xor: return CanvasComposite.Xor; 
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
                case ToolType.Rectangular: this.Rectangular_Start(v); break;
                case ToolType.Elliptical: this.Elliptical_Start(v); break;
                case ToolType.Polygonal: this.Polygonal_Start(v); break;
                case ToolType.FreeHand: this.FreeHand_Start(v); break;
                default: break;
            }
        }

        public void Operator_Delta(Vector2 v)
        {
            switch (this.Tool)
            {
                case ToolType.Rectangular: this.Rectangular_Delta(v); break;
                case ToolType.Elliptical: this.Elliptical_Delta(v); break;
                case ToolType.Polygonal: this.Polygonal_Delta(v); break;
                case ToolType.FreeHand: this.FreeHand_Delta(v); break;
                default: break;
            }
        }

        public void Operator_Complete(Vector2 v)
        {
            switch (this.Tool)
            {
                case ToolType.Rectangular: this.Rectangular_Complete(v); break;
                case ToolType.Elliptical: this.Elliptical_Complete(v); break;
                case ToolType.Polygonal: this.Polygonal_Complete(v); break;
                case ToolType.FreeHand: this.FreeHand_Complete(v); break;
                default: break;
            }
        }


        #endregion

        #region Operator > Rectangular


        public void Rectangular_Start(Vector2 v)
        {
            this.Start = this.End = v;
        }

        public void Rectangular_Delta(Vector2 v)
        {
            this.End = v;
        }

        public void Rectangular_Complete(Vector2 v)
        {
            this.Complete?.Invoke();//Delegate

            this.Start = this.End = Vector2.Zero;
        }


        #endregion

        #region Operator > Elliptical


        public void Elliptical_Start(Vector2 v)
        {
            this.Start = this.End = v;
        }

        public void Elliptical_Delta(Vector2 v)
        {
            this.End = v;
        }

        public void Elliptical_Complete(Vector2 v)
        {
            this.Complete?.Invoke();//Delegate

            this.Start = this.End = Vector2.Zero;
        }


        #endregion

        #region Operator > Polygonal


        bool isPolygonal = false;

        public void Polygonal_Start(Vector2 v)
        {
            if (this.isPolygonal) this.Polygonal_Delta(v);
            else this.FreeHand_Start(v);
        }

        public void Polygonal_Delta(Vector2 v)
        {
            if (this.List.Count > 1) this.List[this.List.Count - 1] = v;
        }

        public void Polygonal_Complete(Vector2 v)
        {
            if (this.isPolygonal)
            {
                if (this.List.Count > 2)
                {
                    if ((this.List.First() - this.List.Last()).LengthSquared() < 100.0f)
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

        #region Operator > FreeHand


        public void FreeHand_Start(Vector2 v)
        {
            this.List.Add(v);
        }

        public void FreeHand_Delta(Vector2 v)
        {
            this.List.Add(v);
        }

        public void FreeHand_Complete(Vector2 v)
        {
            this.Complete?.Invoke();//Delegate

            this.List.Clear();
        }


        #endregion



    }

    /// <summary> Tools of different shapes </summary>
    enum ToolType
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
    enum MarqueeMode
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


    enum CompositeMode
    {
        New,
        Add,
        Subtract,
        Intersect,

        Xor,
    }


}
