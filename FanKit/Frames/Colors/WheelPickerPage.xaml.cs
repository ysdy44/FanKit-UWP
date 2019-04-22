using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Colors
{
    public sealed partial class WheelPickerPage : Page
    {
        public WheelPickerPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.WheelPicker.Color = this.SolidColorBrush.Color = Color.FromArgb(255, 0, 187, 255);

                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Colors/WheelPickerPage.xaml.txt");
            };

            this.WheelPicker.ColorChange += (s, value) => this.SolidColorBrush.Color = value;
        }
    }
}
