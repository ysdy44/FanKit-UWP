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
            this.MarkdownText1.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Colors/TouchSliderXaml.txt");
            this.MarkdownText2.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Colors/TouchSliderUserXaml.txt");
            this.MarkdownText3.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Colors/TouchSliderUserCs.txt");
        }

        private void TouchSliderControl0_ValueChangeStarted(object sender, RangeBaseValueChangedEventArgs e) => this.TexBlockBorder0.Visibility = Visibility.Visible;
        private void TouchSliderControl0_ValueChangeDelta(object sender, RangeBaseValueChangedEventArgs e) => this.TexBlock0.Text = e.NewValue.ToString();
        private void TouchSliderControl0_ValueChangeCompleted(object sender, RangeBaseValueChangedEventArgs e) => this.TexBlockBorder0.Visibility = Visibility.Collapsed;

        private void TouchSliderControl1_ValueChangeStarted(object sender, RangeBaseValueChangedEventArgs e) => this.TexBlockBorder1.Visibility = Visibility.Visible;
        private void TouchSliderControl1_ValueChangeDelta(object sender, RangeBaseValueChangedEventArgs e) => this.TexBlock1.Text = e.NewValue.ToString();
        private void TouchSliderControl1_ValueChangeCompleted(object sender, RangeBaseValueChangedEventArgs e) => this.TexBlockBorder1.Visibility = Visibility.Collapsed;

    }
}
