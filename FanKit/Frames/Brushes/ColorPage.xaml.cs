using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Brushes
{
    /// <summary>
    /// Page of Colors.
    /// </summary>
    public sealed partial class ColorPage : Page
    {
        //@Construct
        public ColorPage()
        {
            this.InitializeComponent();

            this.Paragraph1.Text = "<Border>";
            this.Paragraph2.Text = "  <Border.Background>";

            this.TopRun1.Text = "    <SolidColorBrush  Color=\"{ ThemeResource ";
            this.TopRun2.Text = "SystemAltHighColor";
            this.TopRun3.Text = "}\" /> ";

            this.Paragraph4.Text = "  </Border.Background>";
            this.Paragraph5.Text = "</Border>";
        }
    }
}