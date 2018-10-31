using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FanKit.Frames.Colors
{
    public sealed partial class PalettePickerPage : Page
    {
        public PalettePickerPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.PalettePicker.Color = Color.FromArgb(255, 0, 187, 255);

            this.MarkdownText1.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Colors/PalettePickerPage.xaml.txt");
            this.MarkdownText2.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Colors/PalettePicker.xaml.txt");
            this.MarkdownText3.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Colors/PalettePicker.cs.txt");

            this.MarkdownText4.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Colors/PaletteBase.cs.txt"); 
            this.MarkdownText5.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Colors/PaletteHue.cs.txt");
            this.MarkdownText6.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Colors/PaletteSaturation.cs.txt"); 
            this.MarkdownText7.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Colors/PaletteLightness.cs.txt");
        }

        private void PalettePicker_ColorChange(object sender, Color value) => this.PaletteSolidBrush.Color = value;

    }
}




