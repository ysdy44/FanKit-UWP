using System;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Styles
{
    public sealed partial class TransitionPage : Page
    {
        public TransitionPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.Frame.Navigate(typeof(FanKit.Frames.Styles.Transition.WelcomePage));
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Styles/Transition.style.txt");
            };

            this.Button.Tapped += (sender, e) =>
            {
                this.Frame.Navigate(this.GetPage(this.ListView.SelectedIndex));
                this.Back.IsEnabled = true;
                this.Button.IsEnabled = false;
            };
            this.Back.Tapped += (sender, e) =>
            {
                if (this.Frame.CanGoBack) this.Frame.GoBack();
                this.Back.IsEnabled = false;
                this.Button.IsEnabled = true;
            };
        }

        private Type GetPage(int index)
        {
            switch (index)
            {
                case 0: return typeof(FanKit.Frames.Styles.Transition.CommonPage);
                case 1: return typeof(FanKit.Frames.Styles.Transition.ContinuumPage);
                case 2: return typeof(FanKit.Frames.Styles.Transition.DrillInPage);
                case 3: return typeof(FanKit.Frames.Styles.Transition.EntrancePage);
                case 4: return typeof(FanKit.Frames.Styles.Transition.SlidePage);
                case 5: return typeof(FanKit.Frames.Styles.Transition.SuppressPage);
                default: return null;
            }
        }
    }
}
