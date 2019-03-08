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
 
namespace FanKit.Frames.Control
{ 
    public sealed partial class RadiusAnimaPanelPage : Page
    {
        public RadiusAnimaPanelPage()
        {
            this.InitializeComponent();
        }


        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.MarkdownText1.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Control/RadiusAnimaPanelPage.xaml.txt");
            this.MarkdownText2.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Control/RadiusAnimaPanel.xaml.txt");
            this.MarkdownText3.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Control/RadiusAnimaPanel.cs.txt");
        }


        double WidthText
        {
            set
            {
                this.Rectangle.Width = value;
                this.TextBlock.Text = value.ToString();
            }
        }

        private void Button50_Tapped(object sender, TappedRoutedEventArgs e) => this.WidthText = 50;
        private void Button100_Tapped(object sender, TappedRoutedEventArgs e) => this.WidthText = 100;
        private void Button150_Tapped(object sender, TappedRoutedEventArgs e) => this.WidthText = 150;
        private void Button200_Tapped(object sender, TappedRoutedEventArgs e) => this.WidthText = 200;
        private void Button250_Tapped(object sender, TappedRoutedEventArgs e) => this.WidthText = 250;
        private void Button300_Tapped(object sender, TappedRoutedEventArgs e) => this.WidthText = 300;

    }
}
