using FanKit.Colors;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Colors
{
    public sealed partial class PalettePickerPage : Page
    {
        Color Color
        {
            get => this.PaletteSolidBrush.Color;
            set => this.PaletteSolidBrush.Color=value;
        }

        PalettePicker PaletteHue = new PalettePicker(new PaletteHue());
        PalettePicker PaletteSaturation = new PalettePicker(new PaletteSaturation());
        PalettePicker PaletteLightness = new PalettePicker(new PaletteLightness());

        public PalettePickerPage()
        {
            this.InitializeComponent();

            this.PaletteHue.ColorChange += (sender, value) => this.Color = value;
            this.PaletteSaturation.ColorChange += (sender, value) => this.Color = value;
            this.PaletteLightness.ColorChange += (sender, value) => this.Color = value;

            this.HueButton.Tapped += (sender, e) => this.PalettePicker(this.PaletteHue);
            this.SaturationButton.Tapped += (sender, e) => this.PalettePicker(this.PaletteSaturation);
            this.LightnessButton.Tapped += (sender, e) => this.PalettePicker(this.PaletteLightness);
        }
        private void PalettePicker(PalettePicker picker)
        {
            this.ContentControl.Content = picker;
            picker.Color = this.Color;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {  
            this.Color = Color.FromArgb(255, 0, 187, 255);

            this.MarkdownText1.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/PalettePickerPage.xaml.txt");
            this.MarkdownText2.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/PalettePickerPage.cs.txt");

            this.MarkdownText3.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/PalettePicker.xaml.txt");
            this.MarkdownText4.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/PalettePicker.cs.txt");
         }       

    }
}




