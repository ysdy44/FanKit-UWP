using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FanKit.Frames.Library
{
    public sealed partial class ScalableGridPage : Page
    {
        public ScalableGridPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.MarkdownText1.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Library/ScalableGrid.txt");
            this.MarkdownText2.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Library/ScalableGridPage.txt");
        }

        private void FlipView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.FlipViewGrid.Visibility == Visibility.Visible)
                this.FlipViewGrid.Visibility = Visibility.Collapsed;
            else
                this.FlipViewGrid.Visibility = Visibility.Visible;
        }
    }
}
