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
                this.ColorPicker.Color = Color.FromArgb(255, 0, 187, 255);

                this.MarkdownText1.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/ColorPickerPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/ColorPicker.xaml.txt");
                this.MarkdownText3.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/ColorPicker.cs.txt");
            };
        }

        private void ColorPicker_ColorChange(object sender, Windows.UI.Color value)
        {
            this.PaletteSolidBrush.Color = value;
        }
    }
}
