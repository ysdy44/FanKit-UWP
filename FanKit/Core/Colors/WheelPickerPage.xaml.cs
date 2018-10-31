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
    public sealed partial class WheelPickerPage : Page
    {
        public WheelPickerPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.WheelPicker.Color = Color.FromArgb(255, 0, 187, 255);

            this.MarkdownText1.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Colors/WheelPickerXaml.txt");
            this.MarkdownText2.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Colors/WheelPickerUserXaml.txt");
            this.MarkdownText3.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Colors/WheelPickerUserCs.txt");
        }
        private void WheelPicker_ColorChange(object sender, Color value)=>   this.PaletteSolidBrush.Color = value;
   
    }
}
