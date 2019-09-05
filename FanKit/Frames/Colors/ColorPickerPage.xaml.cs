using System;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Colors
{
    /// <summary>
    /// Page of <see cref="HSVColorPickers.ColorPicker">.
    /// </summary>
    public sealed partial class ColorPickerPage : Page
    {
        //@Construct
        public ColorPickerPage()
        {
            this.InitializeComponent();
            this.Loaded += async (s2, e2) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Colors/ColorPickerPage.xaml.txt");
                this.MarkdownText1.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Colors/ColorPickerPage.xaml"));

                this.ColorPicker.Color = this.SolidColorBrush.Color = Color.FromArgb(255, 0, 187, 255);
            };

            this.ColorPicker.ColorChange += (s, value) => this.SolidColorBrush.Color = value;
        }
    }
}
