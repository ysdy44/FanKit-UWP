using System;
using Windows.UI.Xaml.Media.Imaging;

namespace FanKit.Frames.Reference
{
    /// <summary>
    /// Control of reference <see cref="Microsoft.Toolkit.Uwp.UI.Controls"/>.
    /// </summary>
    public class ReferenceToolKitControls : ReferenceControl
    {
        //@Construct
        public ReferenceToolKitControls()
        {
            base.ImageSource = new BitmapImage(new Uri("ms-appx:///Icon/Reference/ToolKit.png"));
            base.Title = "ToolKit Controls";
            base.Summary = "Search 'Microsoft.Toolkit.Uwp.UI.Controls' in Nuget.";
            base.NugetName = "Microsoft.Toolkit.Uwp.UI.Controls";
            base.LinkUri = new Uri("https://www.microsoft.com/store/productId/9NBLGGH4TLCQ");
        }
    }
}