using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FanKit.Frames.Others
{
    public sealed partial class SettingPage : Page
    {
        public SettingPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Window.Current.Content is FrameworkElement frameworkElement)
            {
                GetTheme(frameworkElement.RequestedTheme);
            }
        }

        private void LightRadioButton_Checked(object sender, RoutedEventArgs e) => SetTheme(ElementTheme.Light);
        private void NightRadioButton_Checked(object sender, RoutedEventArgs e) => SetTheme(ElementTheme.Dark);
        private void DefaultRadioButton_Checked(object sender, RoutedEventArgs e) => SetTheme(ElementTheme.Default);


        private void GetTheme(ElementTheme RequestedTheme)
        {
            LightRadioButton.IsChecked = RequestedTheme == ElementTheme.Light;
            NightRadioButton.IsChecked = RequestedTheme == ElementTheme.Dark;
            DefaultRadioButton.IsChecked = RequestedTheme == ElementTheme.Default;
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
