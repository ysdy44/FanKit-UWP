using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Colors
{
    public sealed partial class HSLPickerPage : Page
    {
        public HSLPickerPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.HSLPicker.Color = Color.FromArgb(255, 0, 187, 255);

                this.MarkdownText1.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/HSLPickerPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/HSLPicker.xaml.txt");
                this.MarkdownText3.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/HSLPicker.cs.txt");
                this.MarkdownText4.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/HSL.cs.txt");
            };
        }
        

        private void HSLPicker_ColorChange(object sender, Color value)=>  this.PaletteSolidBrush.Color = value;
    } 
}
