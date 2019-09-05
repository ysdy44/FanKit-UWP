using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Control
{
    /// <summary>
    /// Page of <see cref="FanKit.Control.PopupMenu">.
    /// </summary>
    public sealed partial class PopupMenuPage : Page
    {
        //@Construct
        public PopupMenuPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Control/PopupMenuPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Control/PopupMenu.xaml.txt");
                this.MarkdownText3.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Control/PopupMenu.cs.txt");
            };

            this.Button.Tapped +=  (sender, e) => this.PopupMenu.IsShow = !this.PopupMenu.IsShow;
        }
    }
}
