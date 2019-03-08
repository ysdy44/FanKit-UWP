using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

namespace FanKit.Frames.Colors
{
    public sealed partial class TouchSliderPage : Page
    {

        public TouchSliderPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.MarkdownText1.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/TouchSliderPage.xaml.txt");
            this.MarkdownText2.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/TouchSlider.xaml.txt");
            this.MarkdownText3.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/TouchSlider.cs.txt");
        }

        private void TouchSliderControl_ValueChangeStarted(object sender, double value) => this.TexBlockBorder.Visibility = Visibility.Visible;
        private void TouchSliderControl_ValueChangeDelta(object sender, double value) => this.TexBlock.Text = ((int)value).ToString();
        private void TouchSliderControl_ValueChangeCompleted(object sender, double value) => this.TexBlockBorder.Visibility = Visibility.Collapsed;

        private void TouchSliderControl_ValueChange(object sender, double value)
        {

        }
    }
}
