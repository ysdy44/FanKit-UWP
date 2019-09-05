 using System;
using Windows.UI.Xaml.Media.Imaging;

namespace FanKit.Frames.Reference
{
    /// <summary>
    /// Control of reference <see cref="Microsoft.Toolkit.Uwp.UI.Extensions"/>.
    /// </summary>
    public class ReferenceToolKitExtensions : ReferenceControl
    {
        //@Construct
        public ReferenceToolKitExtensions()
        {
            base.ImageSource = new BitmapImage(new Uri("ms-appx:///Icon/Reference/ToolKit.png"));
            base.Title = "ToolKit Extensions";
            base.Summary = "Search 'Microsoft.Toolkit.Uwp.UI.Extensions' in Nuget.";
            base.PastedText = "Microsoft.Toolkit.Uwp.UI.Extensions";
            base.LinkUri = new Uri("https://www.microsoft.com/store/productId/9NBLGGH4TLCQ");
            base.Version = "3.0.0";
        }
    }
}