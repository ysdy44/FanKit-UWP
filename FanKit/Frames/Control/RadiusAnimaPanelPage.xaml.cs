using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Control
{
    public sealed partial class RadiusAnimaPanelPage : Page
    {
        double WidthText
        {
            set
            {
                this.Rectangle.Width = value;
                this.TextBlock.Text = value.ToString();
            }
        }

        public RadiusAnimaPanelPage()
        {
            this.InitializeComponent();

            this.Button50.Tapped += (s, e) => this.WidthText = 50;
            this.Button100.Tapped += (s, e) => this.WidthText = 100;
            this.Button150.Tapped += (s, e) => this.WidthText = 150;
            this.Button200.Tapped += (s, e) => this.WidthText = 200;
            this.Button250.Tapped += (s, e) => this.WidthText = 250;
            this.Button300.Tapped += (s, e) => this.WidthText = 300;

            this.Loaded += async (sender, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Control/RadiusAnimaPanelPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Control/RadiusAnimaPanel.xaml.txt");
                this.MarkdownText3.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Control/RadiusAnimaPanel.cs.txt");
            };
        }      
    }
}