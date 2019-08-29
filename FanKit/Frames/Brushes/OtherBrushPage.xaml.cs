using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Brushes
{
    public sealed partial class OtherBrushPage : Page
    {
        public OtherBrushPage()
        {
            this.InitializeComponent();

            this.TopRun1.Text = "<Border  Background=\"{ ThemeResource ";
            this.TopRun2.Text = "ScrollBarTrackFill";
            this.TopRun3.Text = "}\" /> ";
        }
    }
}