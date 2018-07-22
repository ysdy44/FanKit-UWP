using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FanKit.Frames.Library
{
    public sealed partial class DataTemplateAdaptiverPage : Page
    {
        public DataTemplateAdaptiverPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e) => SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = this.Frame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;


        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.MarkdownText1.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Library/DataTemplateAdaptiverXaml.txt");
            this.MarkdownText2.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Library/DataTemplateAdaptiverClassCs.txt");
         }
    }
}
