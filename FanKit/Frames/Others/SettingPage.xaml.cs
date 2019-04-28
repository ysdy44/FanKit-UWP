using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Others
{
    public sealed partial class SettingPage : Page
    {
        public SettingPage()
        {
            this.InitializeComponent();
            this.LightRadioButton.Checked += (s, e) => this.SetTheme(ElementTheme.Light);
            this.DarkRadioButton.Checked += (s, e) => this.SetTheme(ElementTheme.Dark);
            this.DefaultRadioButton.Checked += (s, e) => this.SetTheme(ElementTheme.Default);

            this.Loaded += (sender, e) =>
            {
                if (Window.Current.Content is FrameworkElement frameworkElement)
                {
                    ElementTheme theme = frameworkElement.RequestedTheme;
                    this.LightRadioButton.IsChecked = (theme == ElementTheme.Light);
                    this.DarkRadioButton.IsChecked = (theme == ElementTheme.Dark);
                    this.DefaultRadioButton.IsChecked = (theme == ElementTheme.Default);
                }
            };
        }

        private void SetTheme(ElementTheme RequestedTheme)
        {
            if (Window.Current.Content is FrameworkElement frameworkElement)
            {
                if (frameworkElement.RequestedTheme == RequestedTheme) return;

                frameworkElement.RequestedTheme = RequestedTheme;
            }
        }
    }
}
