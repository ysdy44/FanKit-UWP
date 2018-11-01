﻿using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Numerics;
using Windows.UI.Xaml.Media;

namespace FanKit.Library.Colors
{
    /// <summary>Palette Base</summary>
    public abstract class PaletteBase
    {
        public string Name;
        public string Unit;
        public int Minimum;
        public int Maximum;

        public abstract HSL GetHSL(HSL HSL, int value);
        public abstract int GetValue(HSL HSL);

        public abstract GradientStopCollection GetSliderBrush(HSL HSL);

        public abstract void Draw(CanvasControl CanvasControl, CanvasDrawingSession ds, HSL HSL, Vector2 Center, float SquareHalfWidth, float SquareHalfHeight);
        public abstract HSL Delta(HSL HSL, Vector2 v, float SquareHalfWidth, float SquareHalfHeight);
    }
}
