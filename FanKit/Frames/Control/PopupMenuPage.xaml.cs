using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace FanKit.Frames.Control
{
    public sealed partial class PopupMenuPage : Page
    {
        public PopupMenuPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Control/PopupMenuPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Control/PopupMenu.xaml.txt");
                this.MarkdownText3.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Control/PopupMenu.cs.txt");
            };
        }


        private void Button_Tapped(object sender, TappedRoutedEventArgs e) => this.PopupMenuControl.IsShow = !this.PopupMenuControl.IsShow;

    }
}
