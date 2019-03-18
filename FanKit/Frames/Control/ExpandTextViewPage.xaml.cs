using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace FanKit.Frames.Control
{
    public sealed partial class ExpandTextViewPage : Page
    {
        public ExpandTextViewPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Control/ExpandTextViewPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Control/ExpandTextView.xaml.txt");
                this.MarkdownText3.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Control/ExpandTextView.cs.txt");
            };
        }
         

        private void Button_Tapped(object sender, TappedRoutedEventArgs e) => this.ExpandTextViewControl.IsExpand = !this.ExpandTextViewControl.IsExpand;
        private void Button0_Tapped(object sender, TappedRoutedEventArgs e) => this.ExpandTextViewControl0.IsExpand = !this.ExpandTextViewControl0.IsExpand;
        private void Button1_Tapped(object sender, TappedRoutedEventArgs e) => this.ExpandTextViewControl1.IsExpand = !this.ExpandTextViewControl1.IsExpand;
        private void Button2_Tapped(object sender, TappedRoutedEventArgs e) => this.ExpandTextViewControl2.IsExpand = !this.ExpandTextViewControl2.IsExpand;
       
    }
}
