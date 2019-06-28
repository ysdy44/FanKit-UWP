using System;
using Windows.UI.Xaml.Media.Imaging;

namespace FanKit.Frames.Reference
{
    /// <summary>
    /// Control of reference <see cref="Microsoft.Toolkit.Uwp.UI.Animations"/>.
    /// </summary>
    public class ReferenceToolKitAnimations : ReferenceControl
    {
        //@Construct
        public ReferenceToolKitAnimations()
        {
            base.ImageSource = new BitmapImage(new Uri("ms-appx:///Icon/Reference/ToolKit.png"));
            base.Title = "ToolKit Animations";
            base.Summary = "Search 'Microsoft.Toolkit.Uwp.UI.Animations' in Nuget.";
            base.NugetName = "Microsoft.Toolkit.Uwp.UI.Animations";
            base.LinkUri = new Uri("https://www.microsoft.com/store/productId/9NBLGGH4TLCQ");
        }
    }
}