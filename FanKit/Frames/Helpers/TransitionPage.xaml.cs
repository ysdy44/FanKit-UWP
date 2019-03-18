using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace FanKit.Frames.Helpers
{
    public sealed partial class TransitionPage : Page
    {
        public TransitionPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.Frame.Navigate(typeof(FanKit.Frames.Helpers.Transition.WelcomePage));

                this.MarkdownText1.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Helpers/Transition.style.txt");

            };
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
