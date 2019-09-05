using System;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Colors
{
    /// <summary>
    /// Page of <see cref="HSVColorPickers.AlphaPicker">.
    /// </summary>
    public sealed partial class AlphaPickerPage : Page
    {
        //@Construct
        public AlphaPickerPage()
        {
            this.InitializeComponent();
            this.Loaded += async (s2, e2) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Colors/AlphaPickerPage.xaml.txt");
                this.MarkdownText1.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Colors/AlphaPickerPage.xaml"));

                this.AlphaPicker.Alpha = 255;
                this.SolidColorBrush.Opacity = 255;
                this.SolidColorBrush.Color = Color.FromArgb(255, 0, 187, 255);
            };

            this.AlphaPicker.AlphaChange += (s, value) => this.SolidColorBrush.Opacity = value;
        }
    }
}