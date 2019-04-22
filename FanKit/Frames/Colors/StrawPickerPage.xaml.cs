using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Colors
{
    public sealed partial class StrawPickerPage : Page
    {
        public StrawPickerPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.StrawPicker.Color = this.SolidColorBrush.Color = Color.FromArgb(255, 0, 187, 255);

                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Colors/StrawPickerPage.xaml.txt");
            };

            this.StrawPicker.ColorChange += (s, value) => this.SolidColorBrush.Color = value;
        }
    }
}
