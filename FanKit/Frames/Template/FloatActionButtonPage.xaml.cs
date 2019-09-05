using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Template
{
    /// <summary>
    /// Page of FloatActionButton.
    /// </summary>
    public sealed partial class FloatActionButtonPage : Page
    {
        //@Construct
        public FloatActionButtonPage()
        {
            this.InitializeComponent();
            this.Loaded += async (s2, e2) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Template/FloatActionButtonPage.xaml.txt");
                this.MarkdownText1.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Template/FloatActionButtonPage.xaml"));
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Template/FloatActionButtonPage.xaml.cs.txt");
                this.MarkdownText2.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Template/FloatActionButtonPage.xaml.cs"));
            };

            this.Button.Tapped += (sender, e) =>
            {
                if (this.FloatActionButton.Visibility == Visibility.Visible)
                    this.FloatActionButton.Visibility = Visibility.Collapsed;
                else
                    this.FloatActionButton.Visibility = Visibility.Visible;
            };
        }


        //Property
        private double Span = 0;//cache

        private bool isShow; //main
        public bool IsShow
        {
            get => isShow;
            set
            {
                if (value == this.isShow) return;

                if (value) this.FloatActionButton.Visibility = Visibility.Collapsed;
                else this.FloatActionButton.Visibility = Visibility.Visible;

                this.isShow = value;
            }
        }

        private double verticalOffset;//offset
        public double VerticalOffset
        {
            get => verticalOffset;
            set
            {
                if (this.IsShow == false && value > verticalOffset)
                    Span += verticalOffset - value; //Down: cache offset
                else if (this.IsShow == true && value < verticalOffset) 
                    Span += verticalOffset - value;//Up: cache offset

                //Up: overflow
                if (Span > 20) this.IsShow = false;
                //Down: overflow
                else if (Span < -20) this.IsShow = true;

                verticalOffset = value;
            }
        }

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e) => this.VerticalOffset = ((ScrollViewer)sender).VerticalOffset;
    }
}