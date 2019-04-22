using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Colors
{
    public sealed partial class RGBPickerPage : Page
    {
        public RGBPickerPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.RGBPicker.Color = this.SolidColorBrush.Color = Color.FromArgb(255, 0, 187, 255);

                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Colors/RGBPickerPage.xaml.txt");
            };

            this.RGBPicker.ColorChange += (s, value) => this.SolidColorBrush.Color = value;
        }
    }
}
