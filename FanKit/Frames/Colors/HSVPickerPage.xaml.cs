using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Colors
{
    public sealed partial class HSVPickerPage : Page
    {
        public HSVPickerPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.HSVPicker.Color = this.SolidColorBrush.Color = Color.FromArgb(255, 0, 187, 255);

                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Colors/HSVPickerPage.xaml.txt");
            };

            this.HSVPicker.ColorChange += (s, value) => this.SolidColorBrush.Color = value;
        }
    }
}
