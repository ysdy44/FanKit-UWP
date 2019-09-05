using System;
using Windows.System;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Template
{
    public sealed partial class RadiusShadowPanelPage : Page
    {
        //@Construct
        public RadiusShadowPanelPage()
        {
            this.InitializeComponent();
            this.Loaded += async (s2, e2) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Template/RadiusShadowPanelPage.xaml.txt");
                this.MarkdownText1.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Template/RadiusShadowPanelPage.xaml"));
            };
        }
    }
}