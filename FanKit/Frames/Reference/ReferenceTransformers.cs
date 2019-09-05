using System;
using Windows.UI.Xaml.Media.Imaging;

namespace FanKit.Frames.Reference
{
    /// <summary>
    /// Control of reference <see cref="FanKit.Transformers"/>.
    /// </summary>
    public class ReferenceTransformers : ReferenceControl
    {
        //@Construct
        public ReferenceTransformers()
        {
            base.ImageSource = new BitmapImage(new Uri("ms-appx:///Icon/Reference/Transformers.png"));
            base.Title = "Transformers";
            base.Summary = "Search 'FanKit.Transformers' in Nuget.";
            base.PastedText = "FanKit.Transformers";
            base.LinkUri = new Uri("https://github.com/ysdy44/FanKit.Transformers-Nuget-UWP");
            base.Version = "1.3.2";
        }
    }
}