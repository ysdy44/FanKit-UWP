using System;
using Windows.System;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Colors
{
    public sealed partial class NumberPickerPage : Page
    {
        public NumberPickerPage()
        {
            this.InitializeComponent();
            this.Loaded += async (s2, e2) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Colors/NumberPickerPage.xaml.txt");
                this.MarkdownText1.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Colors/NumberPickerPage.xaml"));
            };

            this.Slider0.ValueChanged += (s, e) => this.NumberPicker0.Value = (int)e.NewValue;
            this.Slider1.ValueChanged += (s, e) => this.NumberPicker1.Minimum = (int)e.NewValue;
            this.Slider2.ValueChanged += (s, e) => this.NumberPicker2.Maximum = (int)e.NewValue;
            this.TextBox0.TextChanged += (s, e) => this.NumberPicker3.Unit = this.TextBox0.Text;
        }
    }
}
