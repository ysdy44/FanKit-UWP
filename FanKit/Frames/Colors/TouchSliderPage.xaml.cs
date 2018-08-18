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

        private void TouchSliderControl_ValueChangeStarted(object sender, RangeBaseValueChangedEventArgs e) => this.TexBlockBorder.Visibility = Visibility.Visible;
        private void TouchSliderControl_ValueChangeDelta(object sender, RangeBaseValueChangedEventArgs e) => this.TexBlock.Text = e.NewValue.ToString();
        private void TouchSliderControl_ValueChangeCompleted(object sender, RangeBaseValueChangedEventArgs e) => this.TexBlockBorder.Visibility = Visibility.Collapsed;
        
    }
}
