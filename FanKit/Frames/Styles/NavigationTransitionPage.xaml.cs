using System;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Styles
{
    public sealed partial class NavigationTransitionPage : Page
    {
        public NavigationTransitionPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.ListBox.SelectedIndex = 0;
                this.ShowFrame.Navigate(typeof(FanKit.Frames.Styles.Transitions.WelcomePage));
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Styles/NavigationTransition.style.txt");
            };

            this.NavigateButton.Tapped += (sender, e) =>
            {
                int index = this.ListBox.SelectedIndex;
                Type page = this.GetPage(index);

                this.ShowFrame.Navigate(page);
                this.BackButton.IsEnabled = true;
                this.NavigateButton.IsEnabled = false;
            };
            this.BackButton.Tapped += (sender, e) =>
            {
                if (this.ShowFrame.CanGoBack) this.ShowFrame.GoBack();
                this.BackButton.IsEnabled = false;
                this.NavigateButton.IsEnabled = true;
            };
        }

        private Type GetPage(int index)
        {
            switch (index)
            {
                case 0: return typeof(FanKit.Frames.Styles.Transitions.CommonPage);
                case 1: return typeof(FanKit.Frames.Styles.Transitions.ContinuumPage);
                case 2: return typeof(FanKit.Frames.Styles.Transitions.DrillInPage);
                case 3: return typeof(FanKit.Frames.Styles.Transitions.EntrancePage);
                case 4: return typeof(FanKit.Frames.Styles.Transitions. SlidePage);
                case 5: return typeof(FanKit.Frames.Styles.Transitions.SuppressPage);
                default: return null;
            }
        }
    }
}