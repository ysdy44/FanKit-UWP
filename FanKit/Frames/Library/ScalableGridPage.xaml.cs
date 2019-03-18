using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace FanKit.Frames.Library
{
    public sealed partial class ScalableGridPage : Page
    {
        public ScalableGridPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Library/ScalableGridPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Library/ScalableGrid.cs.txt");
            };
        }
        

        private void FlipView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.FlipViewGrid.Visibility == Visibility.Visible)
                this.FlipViewGrid.Visibility = Visibility.Collapsed;
            else
                this.FlipViewGrid.Visibility = Visibility.Visible;
        }
    }
}
