using HSVColorPickers;
using System;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Colors
{
    public sealed partial class PalettePickerPage : Page
    { 
        PalettePicker HuePicker = PalettePicker.CreateFormHue();
        PalettePicker SaturationPicker = PalettePicker.CreateFormSaturation();
        PalettePicker ValuePicker = PalettePicker.CreateFormValue();
        
        public PalettePickerPage()
        {
            this.InitializeComponent();
            this.Loaded += async (s2, e2) =>
            {
                this.SolidColorBrush.Color = Color.FromArgb(255, 0, 187, 255);

                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Colors/PalettePickerPage.xaml.txt");
                this.MarkdownText1.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Colors/PalettePickerPage.xaml"));
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Colors/PalettePickerPage.cs.txt");
                this.MarkdownText2.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Colors/PalettePickerPage.xaml.cs"));

                //Hue
                this.ContentControl.Content = this.HuePicker;
                this.HuePicker.Color = this.SolidColorBrush.Color;
            };

            //Hue
            this.HuePicker.ColorChange += (s, value) => this.SolidColorBrush.Color = value;
            this.HueButton.Tapped += (s, e) =>
            {
                this.ContentControl.Content = this.HuePicker;
                this.HuePicker.Color = this.SolidColorBrush.Color;
            };
            //Saturation
            this.SaturationPicker.ColorChange += (s, value) => this.SolidColorBrush.Color = value;
            this.SaturationButton.Tapped += (s, e) =>
            {
                this.ContentControl.Content = this.SaturationPicker;
                this.SaturationPicker.Color = this.SolidColorBrush.Color;
            };
            //Value
            this.ValuePicker.ColorChange += (s, value) => this.SolidColorBrush.Color = value;
            this.ValueButton.Tapped += (s, e) => 
            {
                this.ContentControl.Content = this.ValuePicker;
                this.ValuePicker.Color = this.SolidColorBrush.Color;
            };
        }
    }
}