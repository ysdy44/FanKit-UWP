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
            this.NightRadioButton.Checked += (s, e) => this.SetTheme(ElementTheme.Dark);
            this.DefaultRadioButton.Checked += (s, e) => this.SetTheme(ElementTheme.Default);

            this.Loaded += (sender, e) =>
            {
                if (Window.Current.Content is FrameworkElement frameworkElement)
                {
                    this.LightRadioButton.IsChecked = RequestedTheme == ElementTheme.Light;
                    this.NightRadioButton.IsChecked = RequestedTheme == ElementTheme.Dark;
                    this.DefaultRadioButton.IsChecked = RequestedTheme == ElementTheme.Default;
                }
            };
        }

        private void SetTheme(ElementTheme RequestedTheme)
        {
            if (Window.Current.Content is FrameworkElement frameworkElement)
            {
                frameworkElement.RequestedTheme = RequestedTheme;
            }
        }
    }
}
