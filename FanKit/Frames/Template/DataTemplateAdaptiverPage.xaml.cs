using System;
using Windows.System;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Template
{
    /// <summary>
    /// Page of <see cref="DataTemplateAdaptiver">.
    /// </summary>
    public sealed partial class DataTemplateAdaptiverPage : Page
    {
        //@Construct
        public DataTemplateAdaptiverPage()
        {
            this.InitializeComponent();
            this.Loaded += async (s2, e2) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Template/DataTemplateAdaptiverPage.xaml.txt");
                this.MarkdownText1.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Template/DataTemplateAdaptiverPage.xaml"));
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Template/DataTemplateAdaptiver.cs.txt");
                this.MarkdownText2.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Template/DataTemplateAdaptiver.cs"));
            };
        }
    }
}