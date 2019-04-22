using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Colors
{
    public sealed partial class HexPickerPage : Page
    {
        public HexPickerPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.HexPicker.Color = this.SolidColorBrush.Color = Color.FromArgb(255, 0, 187, 255);

                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Colors/HexPickerPage.xaml.txt");
            };

            this.HexPicker.ColorChange += (s, value) => this.SolidColorBrush.Color = value;
        }
    }
}
