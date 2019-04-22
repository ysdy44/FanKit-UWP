using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace FanKit.Frames.Control
{
    public sealed partial class RadiusAnimaPanelPage : Page
    {
        public RadiusAnimaPanelPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Control/RadiusAnimaPanelPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Control/RadiusAnimaPanel.xaml.txt");
                this.MarkdownText3.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Control/RadiusAnimaPanel.cs.txt");
            };
        }
         

        double WidthText
        {
            set
            {
                this.Rectangle.Width = value;
                this.TextBlock.Text = value.ToString();
            }
        }

        private void Button50_Tapped(object sender, TappedRoutedEventArgs e) => this.WidthText = 50;
        private void Button100_Tapped(object sender, TappedRoutedEventArgs e) => this.WidthText = 100;
        private void Button150_Tapped(object sender, TappedRoutedEventArgs e) => this.WidthText = 150;
        private void Button200_Tapped(object sender, TappedRoutedEventArgs e) => this.WidthText = 200;
        private void Button250_Tapped(object sender, TappedRoutedEventArgs e) => this.WidthText = 250;
        private void Button300_Tapped(object sender, TappedRoutedEventArgs e) => this.WidthText = 300;

    }
}
