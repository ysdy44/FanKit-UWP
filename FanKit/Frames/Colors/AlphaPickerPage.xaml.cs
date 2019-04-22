using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Colors
{
    public sealed partial class AlphaPickerPage : Page
    {
        public AlphaPickerPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.AlphaPicker.Alpha = 255;
                this.SolidColorBrush.Opacity = 255;
                this.SolidColorBrush.Color = Color.FromArgb(255, 0, 187, 255);

                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Colors/AlphaPickerPage.xaml.txt");
            };

            this.AlphaPicker.AlphaChange += (s, value) => this.SolidColorBrush.Opacity = value;
        }
    }
}