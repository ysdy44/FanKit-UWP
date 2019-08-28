using System;
using Windows.UI.Xaml.Media.Imaging;

namespace FanKit.Frames.Reference
{
    /// <summary>
    /// Control of reference <see cref="Microsoft.Graphics.Canvas"/>.
    /// </summary>
    public class ReferenceWin2d : ReferenceControl
    {
        //@Construct
        public ReferenceWin2d()
        {
            base.ImageSource = new BitmapImage(new Uri("ms-appx:///Icon/Reference/Win2d.png"));
            base.Title = "Win2D";
            base.Summary = "Search 'Win2D' in Nuget.";
            base.PastedText = "Win2D";
            base.LinkUri = new Uri("https://github.com/Microsoft/Win2D-samples");
        }
    }
}