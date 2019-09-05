using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Colors
{
    public sealed partial class TouchSliderPage : Page
    {
        public TouchSliderPage()
        {
            this.InitializeComponent();
            this.Loaded += async (s2, e2) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Colors/TouchSliderPage.xaml.txt");
                this.MarkdownText1.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Colors/TouchSliderPage.xaml"));
            };
            
            this.TouchSlider.ValueChangeStarted += (s, value) => this.TexBlockBorder.Visibility = Visibility.Visible;
            this.TouchSlider.ValueChangeDelta += (s, value) => this.TexBlock.Text = ((int)value).ToString();
            this.TouchSlider.ValueChangeCompleted += (s, value) => this.TexBlockBorder.Visibility = Visibility.Collapsed;
        }
    }
}
