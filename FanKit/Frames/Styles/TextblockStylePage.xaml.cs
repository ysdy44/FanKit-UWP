using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Styles
{
    public sealed partial class TextblockStylePage : Page
    {
        public TextblockStylePage()
        {
            this.InitializeComponent();

            this.TopRun1.Text = "<TextBlock  Style=\"{ ThemeResource ";
            this.TopRun2.Text = "BaseTextBlockStyle";
            this.TopRun3.Text = "}\" /> ";
        }
    }
}