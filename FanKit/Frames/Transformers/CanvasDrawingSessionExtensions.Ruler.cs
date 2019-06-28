using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using System;
using System.Numerics;

namespace FanKit.Frames.Transformers
{
    /// <summary>
    /// Extensions of <see cref = "CanvasDrawingSession" />.
    /// </summary>
    public static partial class CanvasDrawingSessionExtensions
    {
        //Text
        static Windows.UI.Color TextColor = Windows.UI.Color.FromArgb(255, 127, 127, 127);
        static CanvasTextFormat TextFormat = new CanvasTextFormat()
        {
            FontSize = 14,
            HorizontalAlignment = CanvasHorizontalAlignment.Center,
            VerticalAlignment = CanvasVerticalAlignment.Center
        };



        //Axis
        const float AxisLine = 12;
        const float AxisThickLine = 20;
        static Windows.UI.Color AxisColor = Windows.UI.Color.FromArgb(255, 127, 127, 127);
        static Windows.UI.Color AxisLineColor = Windows.UI.Color.FromArgb(127, 127, 127, 127);
        static Windows.UI.Color AxisThickLineColor = Windows.UI.Color.FromArgb(127, 127, 127, 127);

        private static void _DrawAxis(CanvasDrawingSession ds, CanvasTransformer canvasTransformer, float axisLine, float axisThickLine, Windows.UI.Color axisColor, Windows.UI.Color axisLineColor, Windows.UI.Color axisThickLineColor, Windows.UI.Color textColor, CanvasTextFormat textFormat)
        {
            //Canvas
            Vector2 position = canvasTransformer.Position;
            float scale = canvasTransformer.Scale;
            float controlWidth = canvasTransformer.ControlWidth;
            float controlHeight = canvasTransformer.ControlHeight;

            //Horizontal: Axis-X
            ds.DrawLine(0, position.Y, controlWidth, position.Y, axisColor);
            //Vertical: Axis-Y
            ds.DrawLine(position.X, 0, position.X, controlHeight, axisColor);

            //Space
            float space = 10 * scale;
            while (space < 10) space *= 5;
            while (space > 100) space /= 5;
            float space5 = space * 5;

            //Horizontal: Lines-X
            for (float X = position.X; X < controlWidth; X += space) ds.DrawLine(X, position.Y - axisLine, X, position.Y, axisLineColor);//right
            for (float X = position.X; X > 0; X -= space) ds.DrawLine(X, position.Y - axisLine, X, position.Y, axisLineColor);//left
            //Vertical: Lines-Y
            for (float Y = position.Y; Y < controlHeight; Y += space) ds.DrawLine(position.X, Y, position.X + axisLine, Y, axisLineColor);//bottom
            for (float Y = position.Y; Y > 0; Y -= space) ds.DrawLine(position.X, Y, position.X + axisLine, Y, axisLineColor);//top

            //Horizontal: ThickLine-X
            for (float X = position.X; X < controlWidth; X += space5) ds.DrawLine(X, position.Y - axisThickLine, X, position.Y, axisThickLineColor);//right
            for (float X = position.X; X > 0; X -= space5) ds.DrawLine(X, position.Y - axisThickLine, X, position.Y, axisThickLineColor);//left
            //Vertical: ThickLine-Y
            for (float Y = position.Y; Y < controlHeight; Y += space5) ds.DrawLine(position.X, Y, position.X + axisThickLine, Y, axisThickLineColor);//bottom
            for (float Y = position.Y; Y > 0; Y -= space5) ds.DrawLine(position.X, Y, position.X + axisThickLine, Y, axisThickLineColor);//top

            //Horizontal: Text-X
            for (float X = position.X; X < controlWidth; X += space5) ds.DrawText(((int)(Math.Round((X - position.X) / scale))).ToString(), X, position.Y + axisLine, textColor, textFormat);
            for (float X = position.X; X > axisThickLine; X -= space5) ds.DrawText(((int)(Math.Round((X - position.X) / scale))).ToString(), X, position.Y + axisLine, textColor, textFormat);
            //Vertical: Text-Y
            for (float Y = position.Y; Y < controlHeight; Y += space5) ds.DrawText(((int)(Math.Round((Y - position.Y) / scale))).ToString(), position.X - axisLine, Y, textColor, textFormat);
            for (float Y = position.Y; Y > axisThickLine; Y -= space5) ds.DrawText(((int)(Math.Round((Y - position.Y) / scale))).ToString(), position.X - axisLine, Y, textColor, textFormat);
        }


        /// <summary>
        /// Draw a Axis-XY.
        /// </summary>
        /// <param name="canvasTransformer"> CanvasTransformer </param>
        public static void DrawAxis(this CanvasDrawingSession ds, CanvasTransformer canvasTransformer) => CanvasDrawingSessionExtensions._DrawAxis(ds, canvasTransformer, CanvasDrawingSessionExtensions.AxisLine, CanvasDrawingSessionExtensions.AxisThickLine, CanvasDrawingSessionExtensions.AxisColor, CanvasDrawingSessionExtensions.AxisLineColor, CanvasDrawingSessionExtensions.AxisThickLineColor, CanvasDrawingSessionExtensions.TextColor, CanvasDrawingSessionExtensions.TextFormat);

        /// <summary>
        /// Draw a Axis-XY.
        /// </summary>
        /// <param name="canvasTransformer"> CanvasTransformer </param>
        /// <param name="axisLine"> axis line length </param>
        /// <param name="axisThickLine"> axis thick line length </param>
        public static void DrawAxis(this CanvasDrawingSession ds, CanvasTransformer canvasTransformer, float axisLine, float axisThickLine) => CanvasDrawingSessionExtensions._DrawAxis(ds, canvasTransformer, axisLine, axisThickLine, CanvasDrawingSessionExtensions.AxisColor, CanvasDrawingSessionExtensions.AxisLineColor, CanvasDrawingSessionExtensions.AxisThickLineColor, CanvasDrawingSessionExtensions.TextColor, CanvasDrawingSessionExtensions.TextFormat);

        /// <summary>
        /// Draw a Axis-XY.
        /// </summary>
        /// <param name="canvasTransformer"> CanvasTransformer </param>
        /// <param name="axisLine"> axis line length </param>
        /// <param name="axisThickLine"> axis thick line length </param>
        /// <param name="axisColor"> axis color </param>
        /// <param name="axisLineColor"> axis line color </param>
        /// <param name="axisThickLineColor"> axis thick line color </param>
        public static void DrawAxis(this CanvasDrawingSession ds, CanvasTransformer canvasTransformer, float axisLine, float axisThickLine, Windows.UI.Color axisColor, Windows.UI.Color axisLineColor, Windows.UI.Color axisThickLineColor) => CanvasDrawingSessionExtensions._DrawAxis(ds, canvasTransformer, axisLine, axisThickLine, axisColor, axisLineColor, axisThickLineColor, CanvasDrawingSessionExtensions.TextColor, CanvasDrawingSessionExtensions.TextFormat);

        /// <summary>
        /// Draw a Axis-XY.
        /// </summary>
        /// <param name="canvasTransformer"> CanvasTransformer </param>
        /// <param name="axisLine"> axis line length </param>
        /// <param name="axisThickLine"> axis thick line length </param>
        /// <param name="axisColor"> axis color </param>
        /// <param name="axisLineColor"> axis line color </param>
        /// <param name="axisThickLineColor"> axis thick line color </param>
        /// <param name="textColor"> text color </param>
        /// <param name="textFormat"> text format </param>
        public static void DrawAxis(this CanvasDrawingSession ds, CanvasTransformer canvasTransformer, float axisLine, float axisThickLine, Windows.UI.Color axisColor, Windows.UI.Color axisLineColor, Windows.UI.Color axisThickLineColor, Windows.UI.Color textColor, CanvasTextFormat textFormat) => CanvasDrawingSessionExtensions._DrawAxis(ds, canvasTransformer, axisLine, axisThickLine, axisColor, axisLineColor, axisThickLineColor, textColor, textFormat);




        //Ruler
        const float RulerWidth = 20;
        const float RulerLine = 8;
        const float RulerThickLine = 12;

        static Windows.UI.Color RulerBackgroundColor = Windows.UI.Color.FromArgb(64, 127, 127, 127);
        static Windows.UI.Color RulerColor = Windows.UI.Color.FromArgb(255, 127, 127, 127);
        static Windows.UI.Color RulerLineColor = Windows.UI.Color.FromArgb(127, 127, 127, 127);
        static Windows.UI.Color RulerThickLineColor = Windows.UI.Color.FromArgb(127, 127, 127, 127);

        private static void _DrawRuler(CanvasDrawingSession ds, CanvasTransformer canvasTransformer, float rulerWidth, float rulerLine, float rulerThickLine, Windows.UI.Color rulerBackgroundColor, Windows.UI.Color rulerColor, Windows.UI.Color rulerLineColor, Windows.UI.Color rulerThickLineColor, Windows.UI.Color textColor, CanvasTextFormat textFormat)
        {
            //Canvas
            Vector2 position = canvasTransformer.Position;
            float scale = canvasTransformer.Scale;
            float controlWidth = canvasTransformer.ControlWidth;
            float controlHeight = canvasTransformer.ControlHeight;

            //Horizontal: Axis-X
            ds.FillRectangle(0, 0, controlWidth, rulerWidth, rulerBackgroundColor);
            ds.DrawLine(0, rulerWidth, controlWidth, rulerWidth, rulerColor);
            //Vertical: Axis-Y
            ds.FillRectangle(0, 0, rulerWidth, controlHeight, rulerBackgroundColor);
            ds.DrawLine(rulerWidth, 0, rulerWidth, controlHeight, rulerColor);

            //Space
            float space = 10 * scale;
            while (space < 10) space *= 5;
            while (space > 100) space /= 5;
            float spaceFive = space * 5;

            //End
            float lineEnd = rulerWidth - rulerLine;
            float thickLineEnd = rulerWidth - rulerThickLine;

            //Horizontal: Lines-X
            for (float X = position.X; X < controlWidth; X += space) ds.DrawLine(X, lineEnd, X, rulerWidth, rulerLineColor);//right
            for (float X = position.X; X > rulerWidth; X -= space) ds.DrawLine(X, lineEnd, X, rulerWidth, rulerLineColor);//left
            //Vertical: Lines-Y
            for (float Y = position.Y; Y < controlHeight; Y += space) ds.DrawLine(lineEnd, Y, rulerWidth, Y, rulerLineColor);//bottom
            for (float Y = position.Y; Y > rulerWidth; Y -= space) ds.DrawLine(lineEnd, Y, rulerWidth, Y, rulerLineColor);//top

            //Horizontal: ThickLine-X
            for (float X = position.X; X < controlWidth; X += spaceFive) ds.DrawLine(X, thickLineEnd, X, rulerWidth, rulerThickLineColor);//right
            for (float X = position.X; X > rulerWidth; X -= spaceFive) ds.DrawLine(X, thickLineEnd, X, rulerWidth, rulerThickLineColor);//left
            //Vertical: ThickLine-Y
            for (float Y = position.Y; Y < controlHeight; Y += spaceFive) ds.DrawLine(thickLineEnd, Y, rulerWidth, Y, rulerThickLineColor);//bottom
            for (float Y = position.Y; Y > rulerWidth; Y -= spaceFive) ds.DrawLine(thickLineEnd, Y, rulerWidth, Y, rulerThickLineColor);//top

            //Horizontal: Text-X
            for (float X = position.X; X < controlWidth; X += spaceFive) ds.DrawText(((int)(Math.Round((X - position.X) / scale))).ToString(), X, lineEnd, textColor, textFormat);
            for (float X = position.X; X > rulerWidth; X -= spaceFive) ds.DrawText(((int)(Math.Round((X - position.X) / scale))).ToString(), X, lineEnd, textColor, textFormat);
            //Vertical: Text-Y
            for (float Y = position.Y; Y < controlHeight; Y += spaceFive) ds.DrawText(((int)(Math.Round((Y - position.Y) / scale))).ToString(), lineEnd, Y, textColor, textFormat);
            for (float Y = position.Y; Y > rulerWidth; Y -= spaceFive) ds.DrawText(((int)(Math.Round((Y - position.Y) / scale))).ToString(), lineEnd, Y, textColor, textFormat);
        }

        /// <summary>
        /// Draw a Ruler.
        /// </summary>
        /// <param name="canvasTransformer"> CanvasTransformer </param>
        public static void DrawRuler(this CanvasDrawingSession ds, CanvasTransformer canvasTransformer) => CanvasDrawingSessionExtensions._DrawRuler(ds, canvasTransformer, CanvasDrawingSessionExtensions.RulerWidth, CanvasDrawingSessionExtensions.RulerLine, CanvasDrawingSessionExtensions.RulerThickLine, CanvasDrawingSessionExtensions.RulerBackgroundColor, CanvasDrawingSessionExtensions.RulerColor, CanvasDrawingSessionExtensions.RulerLineColor, CanvasDrawingSessionExtensions.RulerThickLineColor, CanvasDrawingSessionExtensions.TextColor, CanvasDrawingSessionExtensions.TextFormat);

        /// <summary>
        /// Draw a Ruler.
        /// </summary>
        /// <param name="canvasTransformer"> CanvasTransformer </param>
        /// <param name="rulerWidth"> ruler width</param>
        /// <param name="rulerLine"> ruler line length </param>
        /// <param name="rulerThickLine"> ruler thick line length </param>
        public static void DrawRuler(this CanvasDrawingSession ds, CanvasTransformer canvasTransformer, float rulerWidth, float rulerLine, float rulerThickLine) => CanvasDrawingSessionExtensions._DrawRuler(ds, canvasTransformer, rulerWidth, rulerLine, rulerThickLine, CanvasDrawingSessionExtensions.RulerBackgroundColor, CanvasDrawingSessionExtensions.RulerColor, CanvasDrawingSessionExtensions.RulerLineColor, CanvasDrawingSessionExtensions.RulerThickLineColor, CanvasDrawingSessionExtensions.TextColor, CanvasDrawingSessionExtensions.TextFormat);

        /// <summary>
        /// Draw a Ruler.
        /// </summary>
        /// <param name="canvasTransformer"> CanvasTransformer </param>
        /// <param name="rulerWidth"> ruler width</param>
        /// <param name="rulerLine"> ruler line length </param>
        /// <param name="rulerThickLine"> ruler thick line length </param>
        /// <param name="rulerBackgroundColor"> ruler backgournd color </param>
        /// <param name="rulerColor"> ruler color </param>
        /// <param name="rulerLineColor"> ruler line color </param>
        /// <param name="rulerThickLineColor"> ruler thick line color </param>
        public static void DrawRuler(this CanvasDrawingSession ds, CanvasTransformer canvasTransformer, float rulerWidth, float rulerLine, float rulerThickLine, Windows.UI.Color rulerBackgroundColor, Windows.UI.Color rulerColor, Windows.UI.Color rulerLineColor, Windows.UI.Color rulerThickLineColor) => CanvasDrawingSessionExtensions._DrawRuler(ds, canvasTransformer, rulerWidth, rulerLine, rulerThickLine, rulerBackgroundColor, rulerColor, rulerLineColor, rulerThickLineColor, CanvasDrawingSessionExtensions.TextColor, CanvasDrawingSessionExtensions.TextFormat);

        /// <summary>
        /// Draw a Ruler.
        /// </summary>
        /// <param name="canvasTransformer"> CanvasTransformer </param>
        /// <param name="rulerWidth"> ruler width</param>
        /// <param name="rulerLine"> ruler line length </param>
        /// <param name="rulerThickLine"> ruler thick line length </param>
        /// <param name="rulerBackgroundColor"> ruler backgournd color </param>
        /// <param name="rulerColor"> ruler color </param>
        /// <param name="rulerLineColor"> ruler line color </param>
        /// <param name="rulerThickLineColor"> ruler thick line color </param>
        /// <param name="textColor"> text color </param>
        /// <param name="textFormat"> text format </param>
        public static void DrawRuler(this CanvasDrawingSession ds, CanvasTransformer canvasTransformer, float rulerWidth, float rulerLine, float rulerThickLine, Windows.UI.Color rulerBackgroundColor, Windows.UI.Color rulerColor, Windows.UI.Color rulerLineColor, Windows.UI.Color rulerThickLineColor, Windows.UI.Color textColor, CanvasTextFormat textFormat) => CanvasDrawingSessionExtensions._DrawRuler(ds, canvasTransformer, rulerWidth, rulerLine, rulerThickLine, rulerBackgroundColor, rulerColor, rulerLineColor, rulerThickLineColor, textColor, textFormat);

    }
}