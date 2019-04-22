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
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Control/ExpandTextViewPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Control/ExpandTextView.xaml.txt");
                this.MarkdownText3.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Control/ExpandTextView.cs.txt");
            };

            this.Button.Tapped += (s, e) => this.ExpandTextView.IsExpand = !this.ExpandTextView.IsExpand;
            this.Button0.Tapped += (s, e) => this.ExpandTextView0.IsExpand = !this.ExpandTextView0.IsExpand;
            this.Button1.Tapped += (s, e) => this.ExpandTextView1.IsExpand = !this.ExpandTextView1.IsExpand;
            this.Button2.Tapped += (s, e) => this.ExpandTextView2.IsExpand = !this.ExpandTextView2.IsExpand;
        } 
    }
}
