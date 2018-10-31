using FanKit.Library;
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


namespace FanKit.Frames.Helpers
{
    public sealed partial class TransitionPage : Page
    {

        public TransitionPage()
        {
            this.InitializeComponent();
        }
        

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FanKit.Frames.Helpers.Transition.WelcomePage));

            this.MarkdownText1.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Helpers/Transition.style.txt");
        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            switch (this.ListView.SelectedIndex)
            {
                case 0:
                    this.Frame.Navigate(typeof(FanKit.Frames.Helpers.Transition.CommonPage));
                    break;
                case 1:
                    this.Frame.Navigate(typeof(FanKit.Frames.Helpers.Transition.ContinuumPage));
                    break;
                case 2:
                    this.Frame.Navigate(typeof(FanKit.Frames.Helpers.Transition.DrillInPage));
                    break;
                case 3:
                    this.Frame.Navigate(typeof(FanKit.Frames.Helpers.Transition.EntrancePage));
                    break;
                case 4:
                    this.Frame.Navigate(typeof(FanKit.Frames.Helpers.Transition.SlidePage));
                    break;
                case 5:
                    this.Frame.Navigate(typeof(FanKit.Frames.Helpers.Transition.SuppressPage));
                    break;
                default:
                    break;
            }

            this.Back.IsEnabled = true;
            this.Button.IsEnabled = false;
        }

        private void Back_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)  this.Frame.GoBack(); 
            this.Back.IsEnabled =false;
            this.Button.IsEnabled = true;
        }

    }
}
