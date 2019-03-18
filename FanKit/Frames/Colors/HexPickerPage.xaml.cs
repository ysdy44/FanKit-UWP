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
                this.HexPicker.Color = Color.FromArgb(255, 0, 187, 255);

                this.MarkdownText1.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/HexPickerPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/HexPicker.xaml.txt");
                this.MarkdownText3.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/HexPicker.cs.txt");
            };
        }


        private void HexPicker_ColorChange(object sender, Windows.UI.Color value)
        {
            this.PaletteSolidBrush.Color = value;
        }
    }
}
