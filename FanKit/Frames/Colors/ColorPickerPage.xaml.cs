using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Colors
{
    public sealed partial class ColorPickerPage : Page
    {
        public ColorPickerPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.ColorPicker.Color = this.SolidColorBrush.Color = Color.FromArgb(255, 0, 187, 255);

                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Colors/ColorPickerPage.xaml.txt");
            };

            this.ColorPicker.ColorChange += (s, value) => this.SolidColorBrush.Color = value;
        }
    }
}
