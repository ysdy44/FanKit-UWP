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
            base.NugetName = "HSVColorPickers";
            base.LinkUri = null;
        }
    }
}