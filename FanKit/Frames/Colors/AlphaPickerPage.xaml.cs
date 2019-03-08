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
    public sealed partial class AlphaPickerPage : Page
    {
        public AlphaPickerPage()
        {
            this.InitializeComponent();
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.AlphaPicker.Alpha = 255;

            this.MarkdownText1.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/AlphaPickerPage.xaml.txt");
            this.MarkdownText2.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/AlphaPicker.xaml.txt");
            this.MarkdownText3.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/AlphaPicker.cs.txt");
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
