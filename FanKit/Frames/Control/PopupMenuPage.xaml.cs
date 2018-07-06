using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace FanKit.Frames.Control
{
    public sealed partial class PopupMenuPage : Page
    {
        public PopupMenuPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e) => SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = this.Frame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;


        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.MarkdownText1.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Control/PopupMenuXaml.txt");
            this.MarkdownText2.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Control/PopupMenuUserXaml.txt");
            this.MarkdownText3.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Control/PopupMenuUserCs.txt");
        }


        private void Button_Tapped(object sender, TappedRoutedEventArgs e) => this.PopupMenuControl.IsShow = !this.PopupMenuControl.IsShow;

    }
}
