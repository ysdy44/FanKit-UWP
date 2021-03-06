﻿using System;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Colors
{
    /// <summary>
    /// Page of <see cref="HSVColorPickers.RGBPicker">.
    /// </summary>
    public sealed partial class RGBPickerPage : Page
    {
        //@Construct
        public RGBPickerPage()
        {
            this.InitializeComponent();
            this.Loaded += async (s2, e2) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Colors/RGBPickerPage.xaml.txt");
                this.MarkdownText1.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Colors/RGBPickerPage.xaml"));

                this.RGBPicker.Color = this.SolidColorBrush.Color = Color.FromArgb(255, 0, 187, 255);
            };

            this.RGBPicker.ColorChange += (s, value) => this.SolidColorBrush.Color = value;
        }
    }
}
