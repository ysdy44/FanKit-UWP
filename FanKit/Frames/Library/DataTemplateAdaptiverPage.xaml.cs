using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Library
{
    public sealed partial class DataTemplateAdaptiverPage : Page
    {
        public DataTemplateAdaptiverPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Library/DataTemplateAdaptiverPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Library/DataTemplateAdaptiver.cs.txt");
            };
        }
    }
}
