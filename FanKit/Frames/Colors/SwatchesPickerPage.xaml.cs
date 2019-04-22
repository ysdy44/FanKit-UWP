using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Colors
{
    public sealed partial class SwatchesPickerPage : Page
    {
        public SwatchesPickerPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.SwatchesPicker.Color = this.SolidColorBrush.Color = Color.FromArgb(255, 0, 187, 255);

                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Colors/SwatchesPickerPage.xaml.txt");
            };

            this.SwatchesPicker.ColorChange += (s, value) => this.SolidColorBrush.Color = value;
        }
    }
}
