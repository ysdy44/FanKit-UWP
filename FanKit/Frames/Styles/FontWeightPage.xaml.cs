using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Styles
{
    public sealed partial class FontWeightPage : Page
    {
        public FontWeightPage()
        {
            this.InitializeComponent();

            this.TopRun1.Text = "<TextBlock  FontWeight=\"";
            this.TopRun2.Text = "Normal";
            this.TopRun3.Text = "\"/>";
        }
    }
}