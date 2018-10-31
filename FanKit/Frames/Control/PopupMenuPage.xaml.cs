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

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.MarkdownText1.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Control/PopupMenuPage.xaml.txt");
            this.MarkdownText2.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Control/PopupMenu.xaml.txt");
            this.MarkdownText3.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Control/PopupMenu.cs.txt");
        }


        private void Button_Tapped(object sender, TappedRoutedEventArgs e) => this.PopupMenuControl.IsShow = !this.PopupMenuControl.IsShow;

    }
}
