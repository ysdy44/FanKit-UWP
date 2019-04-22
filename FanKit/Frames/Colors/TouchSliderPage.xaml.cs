using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Colors
{
    public sealed partial class TouchSliderPage : Page
    {
        public TouchSliderPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Colors/TouchSliderPage.xaml.txt");
            };
            
            this.TouchSlider.ValueChangeStarted += (s, value) => this.TexBlockBorder.Visibility = Visibility.Visible;
            this.TouchSlider.ValueChangeDelta += (s, value) => this.TexBlock.Text = ((int)value).ToString();
            this.TouchSlider.ValueChangeCompleted += (s, value) => this.TexBlockBorder.Visibility = Visibility.Collapsed;
        }
    }
}
