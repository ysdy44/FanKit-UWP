using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Control
{
    public sealed partial class SplitPanelControlPage : Page
    {
        public SplitPanelControlPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Control/SplitPanelControlPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Control/SplitPanelControl.xaml.txt");
                this.MarkdownText3.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Control/SplitPanelControl.xaml.cs.txt");
            };

            this.Button.Tapped += (sender, e) => this.SplitPanelControl.IsOpen = !this.SplitPanelControl.IsOpen;
        }
    }
}
