using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace FanKit.Frames.Control
{
    public sealed partial class TabButtonPage : Page
    {

        public TabButtonPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.MarkdownText1.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Control/TabButtonPage.xaml.txt");
            this.MarkdownText2.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Control/TabButton.xaml.txt");
            this.MarkdownText3.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Control/TabButton.cs.txt");

            this.sos.ShowChanged += (IsShow) =>
            {
                if (IsShow) this.SpliteFade.Begin();
                else this.SpliteShow.Begin(); 
            };
        }



        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.TabPivot.SelectedIndex == 4) this.TabPivot.SelectedIndex = 0;
            else this.TabPivot.SelectedIndex++;
        }

    }
}
