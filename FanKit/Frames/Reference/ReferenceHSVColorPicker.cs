using System;
using Windows.UI.Xaml.Media.Imaging;

namespace FanKit.Frames.Reference
{
    /// <summary>
    /// Control of reference <see cref="HSVColorPickers"/>.
    /// </summary>
    public class ReferenceHSVColorPicker : ReferenceControl
    {
        //@Construct
        public ReferenceHSVColorPicker()
        {
            base.ImageSource = new BitmapImage(new Uri("ms-appx:///Icon/Reference/HSVColorPickers.png"));
            base.Title = "HSVColorPickers";
            base.Summary = "Search 'HSVColorPickers' in Nuget.";
            base.PastedText = "HSVColorPickers";
            base.LinkUri = new Uri("https://github.com/ysdy44/HSVColorPickers-Nuget-UWP");
            base.Version = "1.2.0";
        }
    }
}