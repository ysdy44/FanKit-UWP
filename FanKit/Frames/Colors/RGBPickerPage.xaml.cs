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
                this.RGBPicker.Color = Color.FromArgb(255, 0, 187, 255);

                this.MarkdownText1.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/RGBPickerPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/RGBPicker.xaml.txt");
                this.MarkdownText3.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/RGBPicker.cs.txt");
            };
        }
         

        private void RGBPicker_ColorChange(object sender, Color value)=>  this.PaletteSolidBrush.Color = value;

    }
}
