using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FanKit.Frames.Template
{
    public sealed partial class FloatActionButtonPage : Page
    {
        public FloatActionButtonPage()
        {
            this.InitializeComponent();
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.MarkdownText1.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Template/FloatActionButtonXaml.txt");
            this.MarkdownText2.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Template/FloatActionButtonStyle.txt");
            this.MarkdownText3.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Template/FloatActionButtonCs.txt");
        }


        //Property
        private double verticalOffset;
        public double VerticalOffset
        {
            get => verticalOffset;
            set
            {
                if (value < verticalOffset)this.Button.Visibility= Visibility.Visible;
                else if (value > verticalOffset) this.Button.Visibility = Visibility.Collapsed;

                verticalOffset = value;
            }
        }
        
        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e) => this.VerticalOffset = ((ScrollViewer)sender).VerticalOffset;



        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.Button.Visibility == Visibility.Visible) this.Button.Visibility = Visibility.Collapsed;
            else this.Button.Visibility = Visibility.Visible;
        }


    }
}
