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

                this.MarkdownText1.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/AlphaPickerPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/AlphaPicker.xaml.txt");
                this.MarkdownText3.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/AlphaPicker.cs.txt");
            };
        }


        private void AlphaPicker_AlphaChange(object sender, byte value)
        {
            this.PaletteSolidBrush.Color = Color.FromArgb
            (
                value,
                this.PaletteSolidBrush.Color.R,
                this.PaletteSolidBrush.Color.G,
                this.PaletteSolidBrush.Color.B
            );
        }

    }
}
