﻿using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Control
{
    /// <summary>
    /// Page of <see cref="FanKit.Control.ThemeControl">.
    /// </summary>
    public sealed partial class ThemeControlPage : Page
    {
        //@Construct
        public ThemeControlPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Control/ThemeControlPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Control/ThemeControl.xaml.txt");
                this.MarkdownText3.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Control/ThemeControl.cs.txt");
            };
        }        
    }
}